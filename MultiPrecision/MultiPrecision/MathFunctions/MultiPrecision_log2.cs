using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log2(MultiPrecision<N> x) {
            if(!(x >= Zero)) {
                return NaN;
            }

            MultiPrecision<N> v = new MultiPrecision<N>(Sign.Plus, ExponentZero, x.mantissa);

            Int64 exponent = x.Exponent;
            UInt32[] mantissa = new UInt32[Mantissa<N>.Length];
            
            for(int i = 0; i < Mantissa<N>.Bits; i++) { 
                v *= v;
                if(v.exponent > ExponentZero) { 
                    UIntUtil.SetBit(mantissa, i);
                    v = new MultiPrecision<N>(Sign.Plus, ExponentZero, v.mantissa);
                }
            }

            MultiPrecision<N> intpart = exponent;
            MultiPrecision<N> decpart = new MultiPrecision<N>(
                sign: Sign.Plus, 
                exponent: -1, 
                mantissa: new Mantissa<N>(mantissa), 
                denormal_flush: false
            );

            MultiPrecision<N> y = intpart + decpart;

            return y;
        }
    }
}
