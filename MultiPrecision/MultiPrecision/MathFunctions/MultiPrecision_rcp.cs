using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Rcp(MultiPrecision<N> x) {
            if (!Consts.Rcp.Initialized) {
                Consts.Rcp.Initialize();
            }

            if (x.IsNaN) {
                return NaN;
            }
            if (!x.IsFinite) {
                return Zero;
            }
            if (x.IsZero) {
                return x.Sign == Sign.Plus ? PositiveInfinity : NegativeInfinity;
            }

            MultiPrecision<Plus1<N>> x_expand = x.Convert<Plus1<N>>();

            MultiPrecision<Plus1<N>> v = new MultiPrecision<Plus1<N>>(Sign.Plus, 0, x_expand.mantissa, round: false);

            MultiPrecision<Plus1<N>> a = Consts.Rcp.ApproxA + v * Consts.Rcp.ApproxB;
            MultiPrecision<Plus1<N>> h = MultiPrecision<Plus1<N>>.One - v * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                MultiPrecision<Plus1<N>> squa_h = h * h;

                a *= MultiPrecision<Plus1<N>>.One + (MultiPrecision<Plus1<N>>.One + squa_h) * (h + squa_h);
                h = MultiPrecision<Plus1<N>>.One - v * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<Plus1<N>> y_expand = new MultiPrecision<Plus1<N>>(x.Sign, -x.Exponent + a.Exponent, a.mantissa, round: false);

            MultiPrecision<N> y = y_expand.Convert<N>();

            return y;
        }


        private static partial class Consts {
            public static class Rcp {
                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<Plus1<N>> ApproxA { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> ApproxB { private set; get; } = null;

                public static void Initialize() {
                    ApproxA = MultiPrecision<Plus1<N>>.Ldexp(3, -1);
                    ApproxB = MultiPrecision<Plus1<N>>.Ldexp(-1, -1);

                    Initialized = true;
                }
            }
        }
    }
}
