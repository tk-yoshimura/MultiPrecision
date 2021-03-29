using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log2(MultiPrecision<N> x) {
            if (!(x >= Zero)) {
                return NaN;
            }
            if (x.IsZero) {
                return NegativeInfinity;
            }
            if (x == PositiveInfinity) {
                return PositiveInfinity;
            }
            if (x.mantissa == Mantissa<N>.One) {
                return x.Exponent;
            }

            Accumulator<N> v = new(x.mantissa);

            int sft;
            for (sft = 0; sft < Accumulator<N>.Bits; sft++) {
                v *= v;

                if (v.Value[Accumulator<N>.Length - 1] > UIntUtil.UInt32Round) {
                    v = Accumulator<N>.RightRoundBlockShift(v, Mantissa<N>.Length);
                    break;
                }
                else {
                    v = Accumulator<N>.RightRoundShift(v, Mantissa<N>.Bits - 1);
                }
            }
            if (sft == Accumulator<N>.Bits) {
                return x.Exponent;
            }

            UInt32[] mantissa = new UInt32[Mantissa<N>.Length];
            UInt32 m = 1;

            for (int i = mantissa.Length - 1; i >= 0; i--) {
                for (int j = (i < mantissa.Length - 1) ? 0 : 1; j < UIntUtil.UInt32Bits; j++) {
                    v *= v;
                    m <<= 1;

                    if (v.Value[Accumulator<N>.Length - 1] > UIntUtil.UInt32Round) {
                        v = Accumulator<N>.RightRoundBlockShift(v, Mantissa<N>.Length);
                        m |= 1u;
                    }
                    else {
                        v = Accumulator<N>.RightRoundShift(v, Mantissa<N>.Bits - 1);
                    }
                }

                mantissa[i] = m;
                m = 0;
            }

            v *= v;
            bool round = v.Value[Accumulator<N>.Length - 1] > UIntUtil.UInt32Round;

            long intpart = x.Exponent;
            MultiPrecision<N> decpart = new(Sign.Plus, -(Int64)sft - 1, new Mantissa<N>(mantissa, enable_clone: false), round);

            MultiPrecision<N> y = intpart + decpart;

            return y;
        }
    }
}
