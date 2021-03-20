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

            Int64 exponent = x.Exponent;
            UInt32[] mantissa = new UInt32[Accumulator<N>.Length];

            for (int i = mantissa.Length - 1, p = 0, first_setbit = Mantissa<N>.Bits; i >= 0 && p <= first_setbit + Mantissa<N>.Bits; i--) {

                UInt32 m = 0;

                for (int j = 0; j < UIntUtil.UInt32Bits && p <= first_setbit + Mantissa<N>.Bits; j++, p++) {
                    v *= v;
                    m <<= 1;

                    if (v.Value[Accumulator<N>.Length - 1] > UIntUtil.UInt32Round) {
                        m |= 1u;

                        v = Accumulator<N>.RightRoundBlockShift(v, Mantissa<N>.Length);

                        if (first_setbit >= Mantissa<N>.Bits) {
                            first_setbit = p;
                        }
                    }
                    else {
                        v = Accumulator<N>.RightRoundShift(v, Mantissa<N>.Bits - 1);
                    }
                }

                mantissa[i] = m;
            }

            Accumulator<N> mantissa_acc = new(mantissa, enable_clone: false);

            if (mantissa_acc.IsZero) {
                return exponent;
            }

            (Mantissa<N> n, int sft) = mantissa_acc.Mantissa;

            MultiPrecision<N> intpart = exponent;
            MultiPrecision<N> decpart = new(Sign.Plus, -(Int64)sft - 1, n, round: false);

            MultiPrecision<N> y = intpart + decpart;

            return y;
        }
    }
}
