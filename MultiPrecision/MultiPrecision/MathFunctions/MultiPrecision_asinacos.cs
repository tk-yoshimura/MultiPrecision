using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Atan(MultiPrecision<N> x) {
            if (!Consts.Atan.Initialized) {
                Consts.Atan.Initialize();
            }

            MultiPrecision<N> x2 = Ldexp(x, 1);
            MultiPrecision<N> s = x2 / (x * x + 1);
            MultiPrecision<N> z = x2 * s;

            MultiPrecision<N> v = Zero, w = One;

            foreach (MultiPrecision<N> f in Consts.Atan.FracTable) {
                MultiPrecision<N> dv = f * w;

                if (dv.IsZero || v.Exponent > dv.Exponent + Bits) {
                    break;
                }

                v += dv;
                w *= z;
            }

            MultiPrecision<N> y = Ldexp(v * s, -1);

            return y;
        }

        private static partial class Consts {
            public static class Atan {
                public static bool Initialized { private set; get; } = false;
                public static ReadOnlyCollection<MultiPrecision<N>> FracTable { private set; get; } = null;
        
                public static void Initialize() {
                    MultiPrecision<N> numer = 1, denom = 1, k = 1, s = 2;
                    List<MultiPrecision<N>> fracs = new List<MultiPrecision<N>>();

                    while (denom.Exponent - numer.Exponent < Bits * 2 && denom.IsFinite) {
                        fracs.Add((numer * numer) / denom); 
 
                        numer *= k;
                        denom *= s * (s + 1);
                        
                        k += 1;
                        s += 2;
                    }

                    FracTable = Array.AsReadOnly(fracs.ToArray());

                    Initialized = true;
                }
            }
        }
    }
}
