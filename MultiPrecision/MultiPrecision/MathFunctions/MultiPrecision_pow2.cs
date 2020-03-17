using System;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        private static partial class Consts {
            public static MultiPrecision<N> rcp_log2 = null;
        }

        public static MultiPrecision<N> Pow2(MultiPrecision<N> x) {
            if (Consts.rcp_log2 is null) {
                Consts.rcp_log2 = Log(2);
            }

            if (x.IsNaN) {
                return NaN;
            }

            MultiPrecision<N> x_int = Floor(x);

            if(x_int.Exponent >= UIntUtil.UInt32Bits) {
                if(x_int.sign == Sign.Plus) { 
                    return PositiveInfinity;
                }
                else {
                    return Zero;
                }
            }

            Int64 exp = x_int.mantissa.Value.Last() >> (UIntUtil.UInt32Bits - (int)x_int.Exponent);

            MultiPrecision<N> x_frac = x - x_int;

            MultiPrecision<N> v = Consts.rcp_log2 * x_frac;
            MultiPrecision<N> w = v;
            MultiPrecision<N> m = 1;
            MultiPrecision<N> i = 1;

            while(w.Exponent >= -Length) {
                m += w;
                i += 1;
                w *= v / i;
            }

            MultiPrecision<N> y = new MultiPrecision<N>(Sign.Plus, m.Exponent + exp, m.mantissa, denormal_flush: false);

            return y;
        }
    }
}
