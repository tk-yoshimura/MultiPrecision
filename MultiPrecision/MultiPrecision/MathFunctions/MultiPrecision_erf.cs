using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Erf(MultiPrecision<N> x) {
            if (x.IsZero) {
                return Zero;
            }
            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? One : MinusOne;
            }

            MultiPrecision<Next<N>> y = ErfErfcCore(x, is_erf: true);

            return MultiPrecisionUtil.Convert<N, Next<N>>(y);
        }

        public static MultiPrecision<N> Erfc(MultiPrecision<N> x) {
            if (x.IsZero) {
                return One;
            }
            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? Zero : Integer(2);
            }

            MultiPrecision<Next<N>> y = ErfErfcCore(x, is_erf: false);

            return MultiPrecisionUtil.Convert<N, Next<N>>(y);
        }


        private static MultiPrecision<Next<N>> ErfErfcCore(MultiPrecision<N> x, bool is_erf) {
            if (!Consts.Erf.Initialized) {
                Consts.Erf.Initialize();
            }

            MultiPrecision<Next<N>> x_next = MultiPrecisionUtil.Convert<Next<N>, N>(x);
            MultiPrecision<Next<N>> y;

            if (x.Exponent <= 0) {
                MultiPrecision<Next<N>> z = MultiPrecision<Next<N>>.One;
                MultiPrecision<Next<N>> squa_x = x_next * x_next;
                y = MultiPrecision<Next<N>>.Zero;

                foreach (MultiPrecision<Next<N>> t in Consts.Erf.Table) {
                    MultiPrecision<Next<N>> dy = t * z;
                    y += dy;

                    if (dy.IsZero || y.Exponent - dy.Exponent > Bits) {
                        break;
                    }

                    z *= squa_x;
                }

                y *= x_next * Consts.Erf.G;

                if (!is_erf) {
                    y = MultiPrecision<Next<N>>.One - y;
                }
            }
            else if (x.Sign == Sign.Plus) {
                MultiPrecision<Next<N>> z = x_next * MultiPrecision<Next<N>>.Sqrt2;
                MultiPrecision<Next<N>> a = 0;

                // Number of convergences in length = 8, less than this number for length = 16.
                const double s = 57.387608, p = -1.809676;
                long n = (long)((double)Length * Length * s * Math.Pow((double)x, p)) + 1;

                while (n > 0) {
                    a = n / (z + a);
                    n--;
                }

                y = MultiPrecision<Next<N>>.Exp(-x_next * x_next) / (z + a) * Consts.Erf.C;

                if (is_erf) {
                    y = MultiPrecision<Next<N>>.One - y;
                }
            }
            else {
                if (is_erf) {
                    y = -ErfErfcCore(-x, is_erf: true);
                }
                else { 
                    y = 2 - ErfErfcCore(-x, is_erf: false);
                }
            }

            return y;
        }

        private static partial class Consts {
            public static class Erf {
                public static bool Initialized { private set; get; } = false;

                public static MultiPrecision<Next<N>> G { private set; get; } = null;
                public static MultiPrecision<Next<N>> C { private set; get; } = null;
                public static ReadOnlyCollection<MultiPrecision<Next<N>>> Table { private set; get; } = null;

                public static void Initialize() {
                    List<MultiPrecision<Next<N>>> table = new List<MultiPrecision<Next<N>>>();

                    MultiPrecision<Next<N>> v = MultiPrecision<Next<N>>.One;
                    MultiPrecision<Next<N>> d = MultiPrecision<Next<N>>.Zero;
                    MultiPrecision<Next<N>> t = MultiPrecision<Next<N>>.One;

                    long i = 0;

                    while (t.Exponent >= -Bits * 2) {
                        t = ((i & 1) == 0 ? MultiPrecision<Next<N>>.One : MultiPrecision<Next<N>>.MinusOne) / (v * (2 * i + 1));

                        if (t.IsZero) {
                            break;
                        }

                        table.Add(t);
                        d += 1;
                        v *= d;
                        i++;
                    }

                    Table = table.AsReadOnly();

                    G = 2 / MultiPrecision<Next<N>>.Sqrt(MultiPrecision<Next<N>>.PI);
                    C = MultiPrecision<Next<N>>.Sqrt(2 / MultiPrecision<Next<N>>.PI);

                    Initialized = true;
                }
            }
        }
    }
}
