using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Gamma(MultiPrecision<N> x) {
            MultiPrecision<N> y = Exp(LogGamma(x));

            return y;
        }

        public static MultiPrecision<N> LogGamma(MultiPrecision<N> x) {
            if (!Consts.LogGamma.Initialized) {
                Consts.LogGamma.Initialize();
            }

            if (x <= Zero) {
                return NaN;
            }

            MultiPrecision<N> w = (x + Ldexp(One, -1)) * Log(x) - x + Consts.LogGamma.C0;
            MultiPrecision<N> z = 1, i = 1;

#if DEBUG
            bool convergenced = false;
#endif

            foreach(MultiPrecision<N> s in Consts.LogGamma.SterlingTable) {
                z *= x + i;

                MultiPrecision<N> dw = s / z;
                w += dw;

                if (dw.IsZero || w.Exponent - dw.Exponent > Bits) {
#if DEBUG
                    convergenced = true;
#endif
                    break;
                }

                i += One;
            }

#if DEBUG
            //Debug<ArithmeticException>.Assert(convergenced);
#endif

            return w;
        }

        private static partial class Consts {
            public static class LogGamma {
                public static bool Initialized { private set; get; } = false;

                public static MultiPrecision<N> C0 { private set; get; } = null;
                public static ReadOnlyCollection<MultiPrecision<N>> SterlingTable { private set; get; } = null;

                public static void Initialize() {
                    C0 = Ldexp(Log(Ldexp(PI, 1)), -1);

                    MultiPrecision<N>[] c_poly = new MultiPrecision<N>[] { Ldexp(MinusOne, -1), One };
                    List<MultiPrecision<N>> sterling_table = new List<MultiPrecision<N>>() { One / 12 };

                    for (int i = 1; i < Bits * 20; i++) {
                        MultiPrecision<N>[] c_poly_next = new MultiPrecision<N>[i + 2];
                        MultiPrecision<N> n = i;

                        c_poly_next[0] = c_poly[0] * n;
                        for (int j = 1; j < i + 1; j++) {
                            c_poly_next[j] = c_poly[j - 1] + c_poly[j] * n;
                        }

                        c_poly_next[i + 1] = One;

                        c_poly = c_poly_next;

                        MultiPrecision<N> poly_integral = Zero;
                        for (int j = 0; j < c_poly.Length; j++) {
                            poly_integral += c_poly[j] / (j + 2);
                        }

                        MultiPrecision<N> c = poly_integral / (i + 1);

                        if (!c.IsFinite) {
                            break;
                        }

                        sterling_table.Add(c);
                    }

                    SterlingTable = Array.AsReadOnly(sterling_table.ToArray());

                    Initialized = true;
                }
            }
        }
    }
}
