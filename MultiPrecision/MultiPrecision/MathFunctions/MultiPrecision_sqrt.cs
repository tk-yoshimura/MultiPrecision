using System;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Sqrt(MultiPrecision<N> x) {
            if(x.sign == Sign.Minus || x.IsNaN) { 
                return NaN;
            }
            if (!x.IsFinite) { 
                return PositiveInfinity;
            }

            Int64 exponent = x.exponent;
            MultiPrecision<N> v = new MultiPrecision<N>(Sign.Plus, exponent % 2, x.mantissa, denormal_flush: false);



            return y;
        }
    }
}
