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

            MultiPrecision<Plus1<N>> x_expand = x.Convert<Plus1<N>>();

            Int64 exponent = x_expand.Exponent;
            MultiPrecision<Plus1<N>> v = new(Sign.Plus, exponent % 3, x_expand.mantissa, round: false);

            MultiPrecision<Plus1<N>> a = Consts.Cbrt.ApproxA + v * (Consts.Cbrt.ApproxB + v * Consts.Cbrt.ApproxC);
            MultiPrecision<Plus1<N>> h = 1 - v * a * a * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= 1 + h * (Consts.Cbrt.DifferenceA + h * (Consts.Cbrt.DifferenceB + h * Consts.Cbrt.DifferenceC));
                h = 1 - v * a * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<Plus1<N>> y_expand = MultiPrecision<Plus1<N>>.Ldexp(v * a * a, (int)((exponent - exponent % 3) / 3));

            MultiPrecision<N> y = y_expand.Convert<N>();

            return (x.Sign == Sign.Plus) ? y : -y;
        }


        private static partial class Consts {
            public static class Cbrt {
                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<Plus1<N>> ApproxA { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> ApproxB { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> ApproxC { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> DifferenceA { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> DifferenceB { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> DifferenceC { private set; get; } = null;

                public static void Initialize() {
                    ApproxA = (67 - 7 * MultiPrecision<Plus1<N>>.Pow2(MultiPrecision<Plus1<N>>.Div(4, 3))) / 42;
                    ApproxB = (21 * MultiPrecision<Plus1<N>>.Pow2(MultiPrecision<Plus1<N>>.One / 3) - 37) / 56;
                    ApproxC = (11 - 7 * MultiPrecision<Plus1<N>>.Pow2(MultiPrecision<Plus1<N>>.One / 3)) / 168;

                    DifferenceA = MultiPrecision<Plus1<N>>.Div(1, 3);
                    DifferenceB = MultiPrecision<Plus1<N>>.Div(2, 9);
                    DifferenceC = MultiPrecision<Plus1<N>>.Div(14, 81);

                    Initialized = true;
                }
            }
        }
    }
}
