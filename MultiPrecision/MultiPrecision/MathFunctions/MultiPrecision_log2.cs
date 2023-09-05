namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log2(MultiPrecision<N> x) {
            if (!(x >= Zero)) {
                return NaN;
            }
            if (IsZero(x)) {
                return NegativeInfinity;
            }
            if (x == PositiveInfinity) {
                return PositiveInfinity;
            }
            if (x.mantissa == Mantissa<N>.One) {
                return x.Exponent;
            }

            BigUInt<Double<N>> v = new(x.mantissa.Value, offset: 0);

            int sft;
            for (sft = 0; sft < BigUInt<Double<N>>.Bits; sft++) {
                v *= v;

                if (v.Value[BigUInt<Double<N>>.Length - 1] > UIntUtil.UInt32Round) {
                    v = BigUInt<Double<N>>.RightRoundBlockShift(v, Mantissa<N>.Length, enable_clone: false);
                    break;
                }
                else {
                    v = BigUInt<Double<N>>.RightRoundShift(v, Mantissa<N>.Bits - 1, enable_clone: false);
                }
            }
            if (sft == BigUInt<Double<N>>.Bits) {
                return x.Exponent;
            }

            UInt32[] mantissa = new UInt32[Mantissa<N>.Length];
            UInt32 m = 1;

            for (int i = mantissa.Length - 1; i >= 0; i--) {
                for (int j = (i < mantissa.Length - 1) ? 0 : 1; j < UIntUtil.UInt32Bits; j++) {
                    v *= v;
                    m <<= 1;

                    if (v.Value[BigUInt<Double<N>>.Length - 1] > UIntUtil.UInt32Round) {
                        v = BigUInt<Double<N>>.RightRoundBlockShift(v, Mantissa<N>.Length, enable_clone: false);
                        m |= 1u;
                    }
                    else {
                        v = BigUInt<Double<N>>.RightRoundShift(v, Mantissa<N>.Bits - 1, enable_clone: false);
                    }
                }

                mantissa[i] = m;
                m = 0;
            }

            v *= v;
            bool round = v.Value[BigUInt<Double<N>>.Length - 1] > UIntUtil.UInt32Round;

            long intpart = x.Exponent;
            MultiPrecision<N> decpart = new(Sign.Plus, -(Int64)sft - 1, new Mantissa<N>(mantissa, enable_clone: false), round);

            MultiPrecision<N> y = intpart + decpart;

            return y;
        }
    }
}
