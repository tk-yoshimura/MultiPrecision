using System;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {
        public static Mantissa<N> operator +(Mantissa<N> a, UInt32 b) {
            return Add(a, b);
        }

        public static Mantissa<N> operator -(Mantissa<N> a, UInt32 b) {
            return Sub(a, b);
        }

        public static (Mantissa<N> v, int sft) Add(Mantissa<N> v1, Mantissa<N> v2, int v2_sft) {
            BigUInt<Plus1<N>> ret = BigUInt<N>.Add<Plus1<N>>(
                v1.value, 
                BigUInt<N>.RightRoundShift(v2.value, v2_sft)
            );

            uint lzc = ret.LeadingZeroCount;
            int sft = UIntUtil.UInt32Bits - (int)lzc, exponent = sft;

            if (sft > 0) {
                ret = BigUInt<Plus1<N>>.RightRoundShift(ret, sft, enable_clone: false);
                if (ret.Digits > Length) {
                    return (One, exponent + 1);
                }
            }
            else {
                ret = BigUInt<Plus1<N>>.LeftShift(ret, -sft, enable_clone: false);
            }
            
            return (new Mantissa<N>(ret.Convert<N>(check_overflow: false)), exponent);
        }

        public static (Mantissa<N> v, int sft) Sub(Mantissa<N> v1, Mantissa<N> v2, int v2_sft) {
            BigUInt<Plus1<N>> ret = BigUInt<Plus1<N>>.Sub(
                v1.value.Convert<Plus1<N>>(offset: 1), 
                BigUInt<Plus1<N>>.RightRoundShift(v2.value.Convert<Plus1<N>>(offset: 1), v2_sft)
            );

            uint lzc = ret.LeadingZeroCount;
            int sft = UIntUtil.UInt32Bits - (int)lzc, exponent = -(int)lzc;

            if (sft > 0) {
                ret = BigUInt<Plus1<N>>.RightRoundShift(ret, sft, enable_clone: false);
                if (ret.Digits > Length) {
                    return (One, exponent + 1);
                }
            }
            else {
                ret = BigUInt<Plus1<N>>.LeftShift(ret, -sft, enable_clone: false);
            }
            
            return (new Mantissa<N>(ret.Convert<N>(check_overflow: false)), exponent);
        }

        public static Mantissa<N> Add(Mantissa<N> v1, UInt32 v2) {
            return new Mantissa<N>(BigUInt<N>.Add(v1.value, v2));
        }

        public static Mantissa<N> Sub(Mantissa<N> v1, UInt32 v2) {
            return new Mantissa<N>(BigUInt<N>.Sub(v1.value, v2));
        }

        public static (Mantissa<N> v, int sft) Mul(Mantissa<N> v1, Mantissa<N> v2) {
            BigUInt<Double<N>> ret = BigUInt<N>.Mul<Double<N>>(v1.value, v2.value);

            uint lzc = ret.LeadingZeroCount;
            int sft = Bits - (int)lzc, exponent = 1 - (int)lzc;

            if (sft > 0) {
                ret = BigUInt<Double<N>>.RightRoundShift(ret, sft, enable_clone: false);
                if (ret.Digits > Length) {
                    return (One, exponent + 1);
                }
            }
            else {
                ret = BigUInt<Double<N>>.LeftShift(ret, -sft, enable_clone: false);
            }

            return (new Mantissa<N>(ret.Convert<N>(check_overflow: false)), exponent);
        }

        public static (Mantissa<N> v, int sft) Div(Mantissa<N> v1, Mantissa<N> v2) {
            BigUInt<Double<N>> ret = BigUInt<N>.RoundDiv(
                v1.value.Convert<Double<N>>(offset: Length), 
                v2.value
            );

            uint lzc = ret.LeadingZeroCount;
            int sft = Bits - (int)lzc, exponent = sft - 1;

            if (sft > 0) {
                ret = BigUInt<Double<N>>.RightRoundShift(ret, sft, enable_clone: false);
                if (ret.Digits > Length) {
                    return (One, exponent + 1);
                }
            }
            else {
                ret = BigUInt<Double<N>>.LeftShift(ret, -sft, enable_clone: false);
            }

            return (new Mantissa<N>(ret.Convert<N>(check_overflow: false)), exponent);
        }
    }
}
