using System;

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

        public static BigUInt<N> operator *(BigUInt<N> a, UInt64 b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator *(UInt64 a, BigUInt<N> b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator /(BigUInt<N> a, BigUInt<N> b) {
            return Div(a, b).div;
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt64 b) {
            return Div(a, b).div;
        }

        public static BigUInt<N> operator %(BigUInt<N> a, BigUInt<N> b) {
            return Div(a, b).rem;
        }

        public static UInt64 operator %(BigUInt<N> a, UInt64 b) {
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
            BigUInt<Double<N>> v = ExpandMul(v1, v2);

            if (v.Digits > Length) {
                throw new OverflowException();
            }

            return new BigUInt<N>(v.value[..Length], enable_clone: false);
        }

        public static BigUInt<N> Mul(UInt64 n, BigUInt<N> v) {
            if (n == 0) {
                return Zero.Copy();
            }
            if (n == 1) {
                return v.Copy();
            }

            UInt32[] u = new UInt32[2];

            (u[1], u[0]) = UIntUtil.Unpack(n);

            UInt32[] arr = UIntUtil.Vector.Mul(v.value, u);

            if (UIntUtil.Digits(arr) > Length) {
                throw new OverflowException();
            }

            return new BigUInt<N>(arr[..Length], enable_clone: false);
        }

        public static BigUInt<N> Mul(BigUInt<N> v, UInt64 n) {
            return Mul(n, v);
        }

        public static BigUInt<Double<N>> ExpandMul(BigUInt<N> v1, BigUInt<N> v2) {
            UInt32[] arr = UIntUtil.Vector.Mul(v1.value, v2.value);

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

        public static (BigUInt<N> div, UInt64 rem) Div(BigUInt<N> v1, UInt64 v2) {
            if (v2 == 0) {
                throw new DivideByZeroException();
            }

            int lzc_v1 = v1.LeadingZeroCount, lzc_v2 = UIntUtil.LeadingZeroCount(v2);
            int sft = Math.Min(lzc_v1, lzc_v2 - lzc_v2 / UIntUtil.UInt32Bits * UIntUtil.UInt32Bits);

            BigUInt<N> v1_sft = LeftShift(v1, sft);
            UInt64 v2_sft = v2 << sft;
            (UInt32 v2_sft_hi, UInt32 v2_sft_lo) = UIntUtil.Unpack(v2_sft);

            BigUInt<N> div = Zero.Copy(), rem = v1_sft;

            int denom_digits = v2_sft > UInt32.MaxValue ? 2 : 1;
            UInt64 denom = v2_sft_hi > 0 ? ((UInt64)v2_sft_hi + (v2_sft_lo > 0 ? 1ul : 0ul)) : (UInt64)v2_sft_lo;

            for (int i = Length - 1; i >= denom_digits;) {
                UInt64 numer = UIntUtil.Pack(rem[i], rem[i - 1]);

                UInt64 n = numer / denom;
                (UInt32 nh, UInt32 nl) = UIntUtil.Unpack(n);
                div.CarryAdd(i - denom_digits + 1, nh);
                div.CarryAdd(i - denom_digits, nl);

                BigUInt<N> sub = new(UIntUtil.Mul(n, v2_sft), 0);
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

                BigUInt<N> sub = new(UIntUtil.Mul(n, v2_sft), 0);
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

            return (div, UIntUtil.Pack(rem[1], rem[0]));
        }
    }
}
