using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        
        public static MultiPrecision<N> Sqrt(MultiPrecision<N> x) {
            if (!Consts.Sqrt.Initialized) {
                Consts.Sqrt.Initialize();
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

            MultiPrecision<N> a = Consts.Sqrt.ApproxA + v * (Consts.Sqrt.ApproxB + v * Consts.Sqrt.ApproxC);
            MultiPrecision<N> h = One - v * a * a;
            MultiPrecision<N> c4 = Integer(4);
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= One + h * Ldexp(c4 + h + Ldexp(h, 1), -3);
                h = One - v * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<N> y = Ldexp(v * a, (int)((exponent - exponent % 2) >> 1));

            return y;
        }

        
        private static partial class Consts {
            public static class Sqrt {
                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<N> ApproxA { private set; get; } = null;
                public static MultiPrecision<N> ApproxB { private set; get; } = null;
                public static MultiPrecision<N> ApproxC { private set; get; } = null;

                public static void Initialize() {
                    ApproxA = (17 - 6 * Sqrt2) / 6;
                    ApproxB = (5 * Sqrt2 - 9) / 4;
                    ApproxC = (5 - 3 * Sqrt2) / 12;

                    Initialized = true;
                }
            }
        }
    }
}
