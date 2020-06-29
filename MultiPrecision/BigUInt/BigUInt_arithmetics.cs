using System;
using System.Runtime.Intrinsics;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {

        public static BigUInt<N> operator +(BigUInt<N> a, BigUInt<N> b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator -(BigUInt<N> a, BigUInt<N> b) {
            return Sub(a, b);
        }

        public static BigUInt<N> operator +(BigUInt<N> a, UInt32 b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator -(BigUInt<N> a, UInt32 b) {
            return Sub(a, b);
        }

        public static BigUInt<N> operator *(BigUInt<N> a, BigUInt<N> b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator /(BigUInt<N> a, BigUInt<N> b) {
            return Div(a, b).div;
        }

        public static BigUInt<N> operator %(BigUInt<N> a, BigUInt<N> b) {
            return Div(a, b).rem;
        }

        private void Add(BigUInt<N> v) {
            for (int dig = 0; dig < Length; dig++) {
                CarryAdd(dig, v[dig]);
            }
        }

        private void Sub(BigUInt<N> v) {
            for (int dig = 0; dig < Length; dig++) {
                CarrySub(dig, v[dig]);
            }
        }

        public static BigUInt<N> Add(BigUInt<N> v1, BigUInt<N> v2) {
            BigUInt<N> ret = v1.Copy();

            ret.Add(v2);

            return ret;
        }

        public static BigUInt<N> Sub(BigUInt<N> v1, BigUInt<N> v2) {
            BigUInt<N> ret = v1.Copy();

            ret.Sub(v2);

            return ret;
        }

        public static BigUInt<N> Add(BigUInt<N> v1, UInt32 v2) {
            BigUInt<N> ret = v1.Copy();

            ret.CarryAdd(0, v2);

            return ret;
        }

        public static BigUInt<N> Sub(BigUInt<N> v1, UInt32 v2) {
            BigUInt<N> ret = v1.Copy();

            ret.CarrySub(0, v2);

            return ret;
        }

        public static BigUInt<N> Mul(BigUInt<N> v1, BigUInt<N> v2) {
            Vector256<UInt32>[] vs = UIntUtil.ToVector(v1.value);
            Vector256<UInt64>[] ws = new Vector256<UInt64>[vs.Length * 2 + 1];

            uint v2_digits = (uint)v2.Digits;

            for (uint dig2 = 0; dig2 < v2_digits; dig2++) {
                Vector256<UInt32>[] us = UIntUtil.ToVector(v2.value[dig2], Length);

                (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) = UIntUtil.Mul(vs, us);

                UIntUtil.Add(ws, lo, dig2);
                UIntUtil.Add(ws, hi, dig2 + 1);
            }

            UInt32[] arr = UIntUtil.FinalizeAdd(ws, Length);

            return new BigUInt<N>(arr, enable_clone: false);
        }

        public static BigUInt<N> Mul(UInt64 n, BigUInt<N> v) {
            (UInt32 v11, UInt32 v10) = UIntUtil.Unpack(n);

            Vector256<UInt32>[] vs = UIntUtil.ToVector(v.value);
            Vector256<UInt64>[] ws = new Vector256<UInt64>[vs.Length + 1];

            if(v10 != 0) { 
                Vector256<UInt32>[] us = UIntUtil.ToVector(v10, Length);

                (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) = UIntUtil.Mul(vs, us);

                UIntUtil.Add(ws, lo, 0);
                UIntUtil.Add(ws, hi, 1);
            }

            if(v11 != 0) { 
                Vector256<UInt32>[] us = UIntUtil.ToVector(v11, Length);

                (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) = UIntUtil.Mul(vs, us);

                UIntUtil.Add(ws, lo, 1);
                UIntUtil.Add(ws, hi, 2);
            }

            UInt32[] arr = UIntUtil.FinalizeAdd(ws, Length);

            return new BigUInt<N>(arr, enable_clone: false);
        }

        public static BigUInt<N> Mul(BigUInt<N> v, UInt64 n) { 
            return Mul(n, v);
        }

        public static BigUInt<Double<N>> ExpandMul(BigUInt<N> v1, BigUInt<N> v2) {
            Vector256<UInt32>[] vs = UIntUtil.ToVector(v1.value);
            Vector256<UInt64>[] ws = new Vector256<UInt64>[vs.Length * 2 + 1];

            uint v2_digits = (uint)v2.Digits;

            for (uint dig2 = 0; dig2 < v2_digits; dig2++) {
                Vector256<UInt32>[] us = UIntUtil.ToVector(v2.value[dig2], Length);

                (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) = UIntUtil.Mul(vs, us);

                UIntUtil.Add(ws, lo, dig2);
                UIntUtil.Add(ws, hi, dig2 + 1);
            }

            UInt32[] arr = UIntUtil.FinalizeAdd(ws, Length * 2);

            return new BigUInt<Double<N>>(arr, enable_clone: false);
        }

        public static (BigUInt<N> div, BigUInt<N> rem) Div(BigUInt<N> v1, BigUInt<N> v2) {
            if (v2.IsZero) {
                throw new DivideByZeroException();
            }

            int lzc_v1 = v1.LeadingZeroCount, lzc_v2 = v2.LeadingZeroCount;
            int sft = Math.Min(lzc_v1, lzc_v2 - lzc_v2 / UIntUtil.UInt32Bits * UIntUtil.UInt32Bits);

            BigUInt<N> v1_sft = LeftShift(v1, sft), v2_sft = LeftShift(v2, sft);

            BigUInt<N> div = Zero.Copy(), rem = v1_sft;

            UInt64 denom = 0;
            int denom_digits = v2_sft.Digits;

            for (int i = Length - 1; i >= 0; i--) {
                if (denom == 0 && v2_sft[i] != 0) {
                    denom = v2_sft[i];
                    continue;
                }
                if (denom > 0 && v2_sft[i] != 0) {
                    denom += 1;
                    break;
                }
            }

            for (int i = Length - 1; i >= denom_digits;) {
                UInt64 numer = UIntUtil.Pack(rem[i], rem[i - 1]);

                UInt64 n = numer / denom;
                (UInt32 nh, UInt32 nl) = UIntUtil.Unpack(n);
                div.CarryAdd(i - denom_digits + 1, nh);
                div.CarryAdd(i - denom_digits, nl);

                BigUInt<N> sub = Mul(n, v2_sft);
                sub.LeftBlockShift(i - denom_digits);

                rem.Sub(sub);

                if (rem[i] == 0) {
                    i--;
                }
            }

            while (true) {
                UInt64 numer = rem[denom_digits - 1];

                UInt64 n = numer / denom;
                (UInt32 nh, UInt32 nl) = UIntUtil.Unpack(n);
                div.CarryAdd(1, nh);
                div.CarryAdd(0, nl);

                BigUInt<N> sub = Mul(n, v2_sft);
                rem.Sub(sub);

                if (n == 0) {
                    break;
                }
            }

            if (v2_sft <= rem) {
                div.CarryAdd(0, 1);
                rem.Sub(v2_sft);
            }

            rem.RightShift(sft);

#if DEBUG
            Debug<ArithmeticException>.Assert(rem < v2);
#endif

            return (div, rem);
        }

        public static BigUInt<N> RoundDiv(BigUInt<N> v1, BigUInt<N> v2) {
            (BigUInt<N> div, BigUInt<N> rem) = Div(v1, v2);

            rem.LeftShift(1);
            if (rem >= v2) {
                div.CarryAdd(0, 1u);
            }

            return div;
        }
    }
}
