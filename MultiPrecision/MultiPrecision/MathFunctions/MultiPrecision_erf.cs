using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Erf(MultiPrecision<N> x) {
            if (x.IsZero) {
                return Zero;
            }
            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? One : MinusOne;
            }

            MultiPrecision<Plus1<N>> y = ErfErfcCore(x, is_erf: true);

            return MultiPrecisionUtil.Convert<N, Plus1<N>>(y);
        }

        public static MultiPrecision<N> Erfc(MultiPrecision<N> x) {
            if (x.IsZero) {
                return One;
            }
            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? Zero : Integer(2);
            }

            MultiPrecision<Plus1<N>> y = ErfErfcCore(x, is_erf: false);

            return MultiPrecisionUtil.Convert<N, Plus1<N>>(y);
        }


        private static MultiPrecision<Plus1<N>> ErfErfcCore(MultiPrecision<N> x, bool is_erf) {
            if (!Consts.Erf.Initialized) {
                Consts.Erf.Initialize();
            }

            MultiPrecision<Plus1<N>> x_expand = MultiPrecisionUtil.Convert<Plus1<N>, N>(x);
            MultiPrecision<Plus1<N>> y;

            if (x.Exponent <= 0) {
                MultiPrecision<Plus1<N>> z = MultiPrecision<Plus1<N>>.One;
                MultiPrecision<Plus1<N>> squa_x = x_expand * x_expand;
                y = MultiPrecision<Plus1<N>>.Zero;

                foreach (MultiPrecision<Plus1<N>> t in Consts.Erf.Table) {
                    MultiPrecision<Plus1<N>> dy = t * z;
                    y += dy;

                    if (dy.IsZero || y.Exponent - dy.Exponent > Bits) {
                        break;
                    }

                    z *= squa_x;
                }

                y *= x_expand * Consts.Erf.G;

                if (!is_erf) {
                    y = MultiPrecision<Plus1<N>>.One - y;
                }
            }
            else if (x.Sign == Sign.Plus) {
                MultiPrecision<Plus1<N>> z = x_expand * MultiPrecision<Plus1<N>>.Sqrt2;
                MultiPrecision<Plus1<N>> a = 0;

                const double s = 54, pa = 1.35, pb = 0.035, limit = 32;
                double m = (double)Length * (double)Length;

                long n = (long)(m * s * Math.Pow((double)x, -pa * Math.Pow(m, pb)) + limit);

                while (n > 0) {
                    a = n / (z + a);
                    n--;
                }

                y = MultiPrecision<Plus1<N>>.Exp(-x_expand * x_expand) / (z + a) * Consts.Erf.C;

                if (is_erf) {
                    y = MultiPrecision<Plus1<N>>.One - y;
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

                public static MultiPrecision<Plus1<N>> G { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> C { private set; get; } = null;
                public static ReadOnlyCollection<MultiPrecision<Plus1<N>>> Table { private set; get; } = null;

                public static void Initialize() {
                    List<MultiPrecision<Plus1<N>>> table = new List<MultiPrecision<Plus1<N>>>();

                    MultiPrecision<Plus1<N>> v = MultiPrecision<Plus1<N>>.One;
                    MultiPrecision<Plus1<N>> d = MultiPrecision<Plus1<N>>.Zero;
                    MultiPrecision<Plus1<N>> t = MultiPrecision<Plus1<N>>.One;

                    long i = 0;

                    while (t.Exponent >= -Bits * 2) {
                        t = ((i & 1) == 0 ? MultiPrecision<Plus1<N>>.One : MultiPrecision<Plus1<N>>.MinusOne) / (v * (2 * i + 1));

                        if (t.IsZero) {
                            break;
                        }

                        table.Add(t);
                        d += 1;
                        v *= d;
                        i++;
                    }

                    Table = table.AsReadOnly();

                    G = 2 / MultiPrecision<Plus1<N>>.Sqrt(MultiPrecision<Plus1<N>>.PI);
                    C = MultiPrecision<Plus1<N>>.Sqrt(2 / MultiPrecision<Plus1<N>>.PI);

                    Initialized = true;
                }
            }
        }
    }
}
