using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log2(MultiPrecision<N> x) {
            if(!(x >= Zero)) {
                return NaN;
            }

            MultiPrecision<N> v = new MultiPrecision<N>(Sign.Plus, ExponentZero, x.mantissa);

            Int64 exponent = x.Exponent;
            UInt32[] mantissa = new UInt32[Accumulator<N>.Length];

            for(int i = 0, init = Mantissa<N>.Bits; i < Accumulator<N>.Bits && i <= init + Mantissa<N>.Bits; i++) { 
                v *= v;
                if(v.exponent > ExponentZero) { 
                    UIntUtil.SetBit(mantissa, i);
                    v = new MultiPrecision<N>(Sign.Plus, ExponentZero, v.mantissa);

                    if (init >= Mantissa<N>.Bits) {
                        init = i;
                    }
                }
            }

            Accumulator<N> mantissa_acc = new Accumulator<N>(mantissa);

            if (mantissa_acc.IsZero) {
                return exponent;
            }

            (Mantissa<N> n, int sft) = mantissa_acc.Mantissa;

            MultiPrecision<N> intpart = exponent;
            MultiPrecision<N> decpart = new MultiPrecision<N>(Sign.Plus, - sft - 1, n, round: false);

            MultiPrecision<N> y = intpart + decpart;

            return y;
        }
    }
}
