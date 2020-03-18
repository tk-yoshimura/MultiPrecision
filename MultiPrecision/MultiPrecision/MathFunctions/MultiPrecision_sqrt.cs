using System;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> c4 = null;
        }

        public static MultiPrecision<N> Sqrt(MultiPrecision<N> x) {
            if (Consts.c4 is null) {
                Consts.c4 = 4;
            }

            if (x.sign == Sign.Minus || x.IsNaN) {
                return NaN;
            }
            if (!x.IsFinite) {
                return PositiveInfinity;
            }
            if (x.IsZero) { 
                return Zero;
            }

            Int64 exponent = x.Exponent;
            MultiPrecision<N> v = new MultiPrecision<N>(Sign.Plus, exponent % 2, x.mantissa, round: false);

            MultiPrecision<N> a = One - Ldexp(v - One, -2);
            MultiPrecision<N> h = One - v * a * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;
 
            while (h_exponent_prev > h_exponent_post) {
                a *= One + h * Ldexp(Consts.c4 + h + Ldexp(h, 1), -3);
                h = One - v * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<N> y = Ldexp(v * a, (int)((exponent - exponent % 2) >> 1));

            return y;
        }
    }
}
