using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Cbrt(MultiPrecision<N> x) {
            if (!Consts.Cbrt.Initialized) {
                Consts.Cbrt.Initialize();
            }

            if (x.IsNaN) {
                return NaN;
            }
            if (!x.IsFinite) {
                return (x.Sign == Sign.Plus) ? PositiveInfinity : NegativeInfinity;
            }
            if (x.IsZero) {
                return Zero;
            }

            MultiPrecision<Next<N>> x_next = MultiPrecisionUtil.Convert<Next<N>, N>(x);

            Int64 exponent = x_next.Exponent;
            MultiPrecision<Next<N>> v = new MultiPrecision<Next<N>>(Sign.Plus, exponent % 3, x_next.mantissa, round: false);

            MultiPrecision<Next<N>> a = Consts.Cbrt.ApproxA + v * (Consts.Cbrt.ApproxB + v * Consts.Cbrt.ApproxC);
            MultiPrecision<Next<N>> h = MultiPrecision<Next<N>>.One - v * a * a * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= MultiPrecision<Next<N>>.One + h * (Consts.Cbrt.DifferenceA + h * (Consts.Cbrt.DifferenceB + h * Consts.Cbrt.DifferenceC));
                h = MultiPrecision<Next<N>>.One - v * a * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<Next<N>> y_next = MultiPrecision<Next<N>>.Ldexp(v * a * a, (int)((exponent - exponent % 3) / 3));

            MultiPrecision<N> y = MultiPrecisionUtil.Convert<N, Next<N>>(y_next);

            return (x.Sign == Sign.Plus) ? y : -y;
        }


        private static partial class Consts {
            public static class Cbrt {
                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<Next<N>> ApproxA { private set; get; } = null;
                public static MultiPrecision<Next<N>> ApproxB { private set; get; } = null;
                public static MultiPrecision<Next<N>> ApproxC { private set; get; } = null;
                public static MultiPrecision<Next<N>> DifferenceA { private set; get; } = null;
                public static MultiPrecision<Next<N>> DifferenceB { private set; get; } = null;
                public static MultiPrecision<Next<N>> DifferenceC { private set; get; } = null;

                public static void Initialize() {
                    ApproxA = (67 - 7 * MultiPrecision<Next<N>>.Pow2(MultiPrecision<Next<N>>.Div(4, 3))) / 42;
                    ApproxB = (21 * MultiPrecision<Next<N>>.Pow2(MultiPrecision<Next<N>>.One / 3) - 37) / 56;
                    ApproxC = (11 - 7 * MultiPrecision<Next<N>>.Pow2(MultiPrecision<Next<N>>.One / 3)) / 168;

                    DifferenceA = MultiPrecision<Next<N>>.Div(1, 3);
                    DifferenceB = MultiPrecision<Next<N>>.Div(2, 9);
                    DifferenceC = MultiPrecision<Next<N>>.Div(14, 81);

                    Initialized = true;
                }
            }
        }
    }
}
