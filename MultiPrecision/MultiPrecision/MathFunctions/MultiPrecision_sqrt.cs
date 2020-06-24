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

            MultiPrecision<Next<N>> x_next = MultiPrecisionUtil.Convert<Next<N>, N>(x);

            Int64 exponent = x_next.Exponent;
            MultiPrecision<Next<N>> v = new MultiPrecision<Next<N>>(Sign.Plus, exponent % 2, x_next.mantissa, round: false);

            MultiPrecision<Next<N>> a = Consts.Sqrt.ApproxA + v * (Consts.Sqrt.ApproxB + v * Consts.Sqrt.ApproxC);
            MultiPrecision<Next<N>> h = MultiPrecision<Next<N>>.One - v * a * a;
            MultiPrecision<Next<N>> c4 = 4;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= MultiPrecision<Next<N>>.One + h * MultiPrecision<Next<N>>.Ldexp(c4 + h + MultiPrecision<Next<N>>.Ldexp(h, 1), -3);
                h = MultiPrecision<Next<N>>.One - v * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<Next<N>> y_next = MultiPrecision<Next<N>>.Ldexp(v * a, (int)((exponent - exponent % 2) >> 1));

            MultiPrecision<N> y = MultiPrecisionUtil.Convert<N, Next<N>>(y_next);

            return y;
        }


        private static partial class Consts {
            public static class Sqrt {
                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<Next<N>> ApproxA { private set; get; } = null;
                public static MultiPrecision<Next<N>> ApproxB { private set; get; } = null;
                public static MultiPrecision<Next<N>> ApproxC { private set; get; } = null;

                public static void Initialize() {
                    ApproxA = (17 - 6 * MultiPrecision<Next<N>>.Sqrt2) / 6;
                    ApproxB = (5 * MultiPrecision<Next<N>>.Sqrt2 - 9) / 4;
                    ApproxC = (5 - 3 * MultiPrecision<Next<N>>.Sqrt2) / 12;

                    Initialized = true;
                }
            }
        }
    }
}
