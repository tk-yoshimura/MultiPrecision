using System;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        public static BigUInt<N, K> operator +(BigUInt<N, K> a, BigUInt<N, K> b) {
            return Add(a, b);
        }

        public static BigUInt<N, K> operator -(BigUInt<N, K> a, BigUInt<N, K> b) {
            return Sub(a, b);
        }

        public static BigUInt<N, K> operator +(BigUInt<N, K> a, UInt32 b) {
            return Add(a, b);
        }

        public static BigUInt<N, K> operator -(BigUInt<N, K> a, UInt32 b) {
            return Sub(a, b);
        }

        public static BigUInt<N, K> operator *(BigUInt<N, K> a, BigUInt<N, K> b) {
            return Mul(a, b);
        }

        public static BigUInt<N, K> operator /(BigUInt<N, K> a, BigUInt<N, K> b) {
            return Div(a, b).div;
        }

        public static BigUInt<N, K> operator %(BigUInt<N, K> a, BigUInt<N, K> b) {
            return Div(a, b).rem;
        }

        private void Add(BigUInt<N, K> v) {
            for (int dig = 0; dig < Length; dig++) {
                CarryAdd(dig, v[dig]);
            }
        }

        private void Sub(BigUInt<N, K> v) {
            for (int dig = 0; dig < Length; dig++) {
                CarrySub(dig, v[dig]);
            }
        }

        public static BigUInt<N, K> Add(BigUInt<N, K> v1, BigUInt<N, K> v2) {
            BigUInt<N, K> ret = v1.Copy();

            ret.Add(v2);

            return ret;
        }

        public static BigUInt<N, K> Sub(BigUInt<N, K> v1, BigUInt<N, K> v2) {
            BigUInt<N, K> ret = v1.Copy();

            ret.Sub(v2);

            return ret;
        }

        public static BigUInt<N, K> Add(BigUInt<N, K> v1, UInt32 v2) {
            BigUInt<N, K> ret = v1.Copy();

            ret.CarryAdd(0, v2);

            return ret;
        }

        public static BigUInt<N, K> Sub(BigUInt<N, K> v1, UInt32 v2) {
            BigUInt<N, K> ret = v1.Copy();

            ret.CarrySub(0, v2);

            return ret;
        }

        public static BigUInt<N, K> Mul(BigUInt<N, K> v1, BigUInt<N, K> v2) {
            BigUInt<N, K> ret = Zero.Copy();

            int v1_digits = v1.Digits, v2_digits = v2.Digits;

            for (int dig1 = 0; dig1 < v1_digits; dig1++) {
                if (v1[dig1] == 0) {
                    continue;
                }

                for (int dig2 = 0; dig2 < v2_digits; dig2++) {
                    if (v2[dig2] == 0) {
                        continue;
                    }

                    (UInt32 h, UInt32 l) = UIntUtil.Unpack((UInt64)v1[dig1] * (UInt64)v2[dig2]);

                    ret.CarryAdd(dig1 + dig2, l);
                    ret.CarryAdd(dig1 + dig2 + 1, h);
                }
            }

            return ret;
        }

        public static BigUInt<N, K> Mul(UInt64 n, BigUInt<N, K> v) {
            BigUInt<N, K> ret = Zero.Copy();

            (UInt32 v11, UInt32 v10) = UIntUtil.Unpack(n);

            int v_digits = v.Digits;

            for (int dig = 0; dig < v_digits; dig++) {
                if (v[dig] == 0) {
                    continue;
                }

                if(v10 > 0u) { 
                    (UInt32 h, UInt32 l) = UIntUtil.Unpack((UInt64)v10 * (UInt64)v[dig]);

                    ret.CarryAdd(dig, l);
                    ret.CarryAdd(dig + 1, h);
                }

                if(v11 > 0u) { 
                    (UInt32 h, UInt32 l) = UIntUtil.Unpack((UInt64)v11 * (UInt64)v[dig]);

                    ret.CarryAdd(dig + 1, l);
                    ret.CarryAdd(dig + 2, h);
                }
            }

            return ret;
        }

        public static BigUInt<N, K> Mul(BigUInt<N, K> v, UInt64 n) { 
            return Mul(n, v);
        }

        public static BigUInt<N, Pow2.N2> ExpandMul(BigUInt<N, Pow2.N1> v1, BigUInt<N, Pow2.N1> v2) {
            BigUInt<N, Pow2.N2> ret = BigUInt<N, Pow2.N2>.Zero.Copy();

            int v1_digits = v1.Digits, v2_digits = v2.Digits;

            for (int dig1 = 0; dig1 < v1_digits; dig1++) {
                if (v1[dig1] == 0) {
                    continue;
                }

                for (int dig2 = 0; dig2 < v2_digits; dig2++) {
                    if (v2[dig2] == 0) {
                        continue;
                    }

                    (UInt32 h, UInt32 l) = UIntUtil.Unpack((UInt64)v1[dig1] * (UInt64)v2[dig2]);

                    ret.CarryAdd(dig1 + dig2, l);
                    ret.CarryAdd(dig1 + dig2 + 1, h);
                }
            }

            return ret;
        }

        public static (BigUInt<N, K> div, BigUInt<N, K> rem) Div(BigUInt<N, K> v1, BigUInt<N, K> v2) {
            if (v2.IsZero) {
                throw new DivideByZeroException();
            }

            int lzc_v1 = v1.LeadingZeroCount, lzc_v2 = v2.LeadingZeroCount;
            int sft = Math.Min(lzc_v1, lzc_v2 - lzc_v2 / UIntUtil.UInt32Bits * UIntUtil.UInt32Bits);

            BigUInt<N, K> v1_sft = LeftShift(v1, sft), v2_sft = LeftShift(v2, sft);

            BigUInt<N, K> div = Zero.Copy(), rem = v1_sft;

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

                BigUInt<N, K> sub = Mul(n, v2_sft);
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

                BigUInt<N, K> sub = Mul(n, v2_sft);
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

        public static BigUInt<N, K> RoundDiv(BigUInt<N, K> v1, BigUInt<N, K> v2) {
            (BigUInt<N, K> div, BigUInt<N, K> rem) = Div(v1, v2);

            rem.LeftShift(1);
            if (rem >= v2) {
                div.CarryAdd(0, 1u);
            }

            return div;
        }
    }
}
