namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {
        public static Mantissa<N> operator +(Mantissa<N> a, UInt32 b) {
            return Add(a, b);
        }

        public static Mantissa<N> operator -(Mantissa<N> a, UInt32 b) {
            return Sub(a, b);
        }

        public static (Mantissa<N> v, int exponent) Add(Mantissa<N> v1, Mantissa<N> v2, int v2_sft) {
            BigUInt<Plus1<N>> ret = BigUInt<N>.Add<Plus1<N>>(
                v1.value,
                BigUInt<N>.RightRoundShift(v2.value, v2_sft)
            );

            uint lzc = ret.LeadingZeroCount;
            int sft = UIntUtil.UInt32Bits - (int)lzc, exponent = sft;

            return Adjust(ret, sft, exponent);
        }

        public static (Mantissa<N> v, int exponent) Add(Mantissa<N> v1, UInt64 v2, int v2_sft) {
            if (v2_sft >= 0) {
                if (v2_sft > BigUInt<N>.Bits - 1) {
                    int truncation_bits = v2_sft - (BigUInt<N>.Bits - 1);

                    if (truncation_bits > UIntUtil.UInt64Bits) {
                        return (v1, 0);
                    }

                    v2 >>= truncation_bits - 1;
                    v2 = ((v2 & 1uL) == 0uL) ? (v2 >> 1) : ((v2 >> 1) + 1uL);
                    v2_sft = BigUInt<N>.Bits - 1;
                }

                BigUInt<Plus2<N>> ret = BigUInt<Plus2<N>>.Add(
                    v1.value.Convert<Plus2<N>>(),
                    v2,
                    BigUInt<N>.Bits - 1 - v2_sft
                );

                uint lzc = ret.LeadingZeroCount;
                int sft = UIntUtil.UInt64Bits - (int)lzc, exponent = sft;

                return Adjust(ret, sft, exponent);
            }
            else { 
                BigUInt<Plus2<N>> ret = BigUInt<Plus2<N>>.Add(
                    BigUInt<Plus2<N>>.RightRoundShift(v1.value.Convert<Plus2<N>>(), -v2_sft, enable_clone: false),
                    v2,
                    BigUInt<N>.Bits - 1
                );

                uint lzc = ret.LeadingZeroCount;
                int sft = UIntUtil.UInt64Bits - (int)lzc, exponent = sft - v2_sft;

                return Adjust(ret, sft, exponent);
            }
        }

        public static (Mantissa<N> v, int exponent) Sub(Mantissa<N> v1, Mantissa<N> v2, int v2_sft) {
            BigUInt<Plus1<N>> ret = BigUInt<Plus1<N>>.Sub(
                v1.value.Convert<Plus1<N>>(offset: 1),
                BigUInt<Plus1<N>>.RightRoundShift(v2.value.Convert<Plus1<N>>(offset: 1), v2_sft)
            );

            uint lzc = ret.LeadingZeroCount;
            int sft = UIntUtil.UInt32Bits - (int)lzc, exponent = -(int)lzc;

            return Adjust(ret, sft, exponent);
        }

        public static Mantissa<N> Add(Mantissa<N> v1, UInt32 v2) {
            return new Mantissa<N>(BigUInt<N>.Add(v1.value, v2));
        }

        public static Mantissa<N> Sub(Mantissa<N> v1, UInt32 v2) {
            return new Mantissa<N>(BigUInt<N>.Sub(v1.value, v2));
        }

        public static (Mantissa<N> v, int exponent) Mul(Mantissa<N> v1, Mantissa<N> v2) {
            BigUInt<Double<N>> ret = BigUInt<N>.Mul<Double<N>>(v1.value, v2.value);

            uint lzc = ret.LeadingZeroCount;
            int sft = Bits - (int)lzc, exponent = 1 - (int)lzc;

            return Adjust(ret, sft, exponent);
        }

        public static (Mantissa<N> v, int exponent) Mul(Mantissa<N> v1, UInt64 v2) {
            BigUInt<Plus2<N>> ret = BigUInt<N>.Mul<Plus2<N>>(v1.value, v2);

            uint lzc = ret.LeadingZeroCount;
            int sft = UIntUtil.UInt64Bits - (int)lzc, exponent = sft;

            return Adjust(ret, sft, exponent);
        }

        public static (Mantissa<N> v, int exponent) Div(Mantissa<N> v1, Mantissa<N> v2) {
            BigUInt<Double<N>> ret = BigUInt<N>.RoundDiv(
                v1.value.Convert<Double<N>>(offset: Length),
                v2.value
            );

            uint lzc = ret.LeadingZeroCount;
            int sft = Bits - (int)lzc, exponent = sft - 1;

            return Adjust(ret, sft, exponent);
        }

        public static (Mantissa<N> v, int exponent) Div(Mantissa<N> v1, UInt64 v2) {
            BigUInt<Plus2<N>> ret = BigUInt<Plus2<N>>.RoundDiv(
                v1.value.Convert<Plus2<N>>(offset: 2),
                v2
            );

            uint lzc = ret.LeadingZeroCount;
            int sft = UIntUtil.UInt64Bits - (int)lzc, exponent = -(int)lzc;

            return Adjust(ret, sft, exponent);
        }

        private static (Mantissa<N> v, int exponent) Adjust<M>(BigUInt<M> ret, int sft, int exponent) where M: struct, IConstant {
            if (sft > 0) {
                ret = BigUInt<M>.RightRoundShift(ret, sft, enable_clone: false);
                if (ret.Digits > Length) {
                    return (One, exponent + 1);
                }
            }
            else {
                ret = BigUInt<M>.LeftShift(ret, -sft, enable_clone: false);
            }

            const bool check_overflow =
#if DEBUG
                true;
#else 
                false;
#endif

            return (new Mantissa<N>(ret.Convert<N>(check_overflow)), exponent);
        }
    }
}
