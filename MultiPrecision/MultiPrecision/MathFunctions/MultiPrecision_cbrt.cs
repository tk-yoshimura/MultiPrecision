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

            Int64 exponent = x.Exponent;
            MultiPrecision<N> v = new MultiPrecision<N>(Sign.Plus, exponent % 3, x.mantissa, round: false);

            MultiPrecision<N> a = Consts.Cbrt.ApproxA + v * (Consts.Cbrt.ApproxB + v * Consts.Cbrt.ApproxC);
            MultiPrecision<N> h = One - v * a * a * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= One + h * (Consts.Cbrt.DifferenceA + h * (Consts.Cbrt.DifferenceB + h * Consts.Cbrt.DifferenceC));
                h = One - v * a * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<N> y = Ldexp(v * a * a, (int)((exponent - exponent % 3) / 3));

            return (x.Sign == Sign.Plus) ? y : -y;
        }


        private static partial class Consts {
            public static class Cbrt {
                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<N> ApproxA { private set; get; } = null;
                public static MultiPrecision<N> ApproxB { private set; get; } = null;
                public static MultiPrecision<N> ApproxC { private set; get; } = null;
                public static MultiPrecision<N> DifferenceA { private set; get; } = null;
                public static MultiPrecision<N> DifferenceB { private set; get; } = null;
                public static MultiPrecision<N> DifferenceC { private set; get; } = null;

                public static void Initialize() {
                    ApproxA = (67 - 7 * Pow2((MultiPrecision<N>)4 / 3)) / 42;
                    ApproxB = (21 * Pow2(One / 3) - 37) / 56;
                    ApproxC = (11 - 7 * Pow2(One / 3)) / 168;

                    DifferenceA = (MultiPrecision<N>)1 / 3;
                    DifferenceB = (MultiPrecision<N>)2 / 9;
                    DifferenceC = (MultiPrecision<N>)14 / 81;

                    Initialized = true;
                }
            }
        }
    }
}
