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

            MultiPrecision<Plus1<N>> x_expand = MultiPrecisionUtil.Convert<Plus1<N>, N>(x);

            Int64 exponent = x_expand.Exponent;
            MultiPrecision<Plus1<N>> v = new MultiPrecision<Plus1<N>>(Sign.Plus, exponent % 2, x_expand.mantissa, round: false);

            MultiPrecision<Plus1<N>> a = Consts.Sqrt.ApproxA + v * (Consts.Sqrt.ApproxB + v * Consts.Sqrt.ApproxC);
            MultiPrecision<Plus1<N>> h = MultiPrecision<Plus1<N>>.One - v * a * a;
            MultiPrecision<Plus1<N>> c4 = 4;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= MultiPrecision<Plus1<N>>.One + h * MultiPrecision<Plus1<N>>.Ldexp(c4 + h + MultiPrecision<Plus1<N>>.Ldexp(h, 1), -3);
                h = MultiPrecision<Plus1<N>>.One - v * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<Plus1<N>> y_expand = MultiPrecision<Plus1<N>>.Ldexp(v * a, (int)((exponent - exponent % 2) >> 1));

            MultiPrecision<N> y = MultiPrecisionUtil.Convert<N, Plus1<N>>(y_expand);

            return y;
        }


        private static partial class Consts {
            public static class Sqrt {
                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<Plus1<N>> ApproxA { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> ApproxB { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> ApproxC { private set; get; } = null;

                public static void Initialize() {
                    ApproxA = (17 - 6 * MultiPrecision<Plus1<N>>.Sqrt2) / 6;
                    ApproxB = (5 * MultiPrecision<Plus1<N>>.Sqrt2 - 9) / 4;
                    ApproxC = (5 - 3 * MultiPrecision<Plus1<N>>.Sqrt2) / 12;

                    Initialized = true;
                }
            }
        }
    }
}
