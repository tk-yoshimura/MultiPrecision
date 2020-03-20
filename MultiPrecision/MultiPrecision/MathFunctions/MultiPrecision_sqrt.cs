using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static class Sqrt {
                public static bool initialized = false;
                public static MultiPrecision<N> c4 = null;
                public static MultiPrecision<N> approx_a = null, approx_b = null, approx_c = null;
            }
        }

        public static MultiPrecision<N> Sqrt(MultiPrecision<N> x) {
            if (!Consts.Sqrt.initialized) {
                InitializeSqrtConsts();
            }

            if (x.Sign == Sign.Minus || x.IsNaN) {
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

            MultiPrecision<N> a = Consts.Sqrt.approx_a + v * (Consts.Sqrt.approx_b + v * Consts.Sqrt.approx_c);
            MultiPrecision<N> h = One - v * a * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= One + h * Ldexp(Consts.Sqrt.c4 + h + Ldexp(h, 1), -3);
                h = One - v * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<N> y = Ldexp(v * a, (int)((exponent - exponent % 2) >> 1));

            return y;
        }

        private static void InitializeSqrtConsts() {
            Consts.Sqrt.c4 = 4;

            Consts.Sqrt.approx_a = (17 - 6 * Sqrt2) / 6;
            Consts.Sqrt.approx_b = (5 * Sqrt2 - 9) / 4;
            Consts.Sqrt.approx_c = (5 - 3 * Sqrt2) / 12;

            Consts.Sqrt.initialized = true;
        }
    }
}
