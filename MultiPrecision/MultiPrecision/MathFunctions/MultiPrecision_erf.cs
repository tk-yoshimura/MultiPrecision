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
            if(x.Exponent >= 1) { 
                return One - Erfc(x);
            }

            if (!Consts.Erf.Initialized) { 
                Consts.Erf.Initialize();
            }

            MultiPrecision<N> z = One;
            MultiPrecision<N> y = Zero;
            MultiPrecision<N> squa_x = x * x;

            foreach(MultiPrecision<N> t in Consts.Erf.Table) { 
                MultiPrecision<N> dy = t * z;
                y += dy;

                if(dy.IsZero || dy.Exponent + Bits < y.Exponent) {
                    break;
                }

                z *= squa_x;
            }

            y *= x * Consts.Erf.G;

            return y;
        }

        public static MultiPrecision<N> Erfc(MultiPrecision<N> x) {
            if (x.IsZero) { 
                return One;
            }
            if (!x.IsFinite) { 
                return x.Sign == Sign.Plus ? Zero : Integer(2);
            }
            if(x.Exponent < 1) { 
                return One - Erf(x);
            }
            if(x.Sign == Sign.Minus) { 
                return Integer(2) - Erfc(-x);
            }

            MultiPrecision<N> z = x * Consts.sqrt2;
            MultiPrecision<N> a = 0;

            for (int n = Bits; n > 0; n--){
                a = n / (z + a);
            }

            MultiPrecision<N> y = Exp(-x * x) / (z + a) * Sqrt(2 / Consts.pi);

            return y;
        }

        private static partial class Consts {
            public static class Erf {
                public static bool Initialized { private set; get; } = false;

                public static MultiPrecision<N> G { private set; get; } = null;
                public static ReadOnlyCollection<MultiPrecision<N>> Table { private set; get; } = null;

                public static void Initialize() {
                    List<MultiPrecision<N>> table = new List<MultiPrecision<N>>();

                    MultiPrecision<N> v = One;
                    MultiPrecision<N> d = Zero;
                    MultiPrecision<N> t = One;

                    long i = 0;

                    while (t.Exponent >= -Bits * 2) {
                        t = ((i & 1) == 0 ? One : MinusOne) / (v * (2 * i + 1));

                        if (t.IsZero) { 
                            break;
                        }

                        table.Add(t);
                        d += 1;
                        v *= d;
                        i++;
                    }

                    Table = table.AsReadOnly();

                    G = 2 / Sqrt(PI);

                    Initialized = true;
                }
            }
        }
    }
}
