using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Atan(MultiPrecision<N> x) {
            if(x <= One && x >= MinusOne) { 
                MultiPrecision<N> z = Abs(x) / Sqrt(x * x + 1);
                MultiPrecision<N> w = Sqrt(SquareAsin(z));

                return new MultiPrecision<N>(x.Sign, w.exponent, w.mantissa);
            }
            else {
                MultiPrecision<N> invx = One / x;
                MultiPrecision<N> z = Abs(invx) / Sqrt(invx * invx + 1);
                MultiPrecision<N> w = Sqrt(SquareAsin(z));

                if(x.Sign == Sign.Plus) { 
                    return Ldexp(PI, -1) - w;
                }
                else { 
                    return w - Ldexp(PI, -1);
                }
            }
        }

        internal static MultiPrecision<N> SquareAsin(MultiPrecision<N> x) { 
            if (!Consts.SquareAsin.Initialized) {
                Consts.SquareAsin.Initialize();
            }

            #if DEBUG
            Debug<ArithmeticException>.Assert(x >= Zero && x < One);
            #endif

            MultiPrecision<N> z = Zero, dz = Zero, s = Ldexp(x * x, 2), t = s;

            bool convergence = false;
            
            foreach(MultiPrecision<N> f in Consts.SquareAsin.FracTable) { 
                dz = t * f;
                z += dz;
                t *= s;

                if (dz.IsZero || z.Exponent - dz.Exponent > Bits) {
                    convergence = true;
                    break;
                }
            }

            #if DEBUG
            Debug<ArithmeticException>.Assert(convergence);
            #endif

            return Ldexp(z, -1);
        }

        private static partial class Consts {
            public static class SquareAsin {
                public static bool Initialized { private set; get; } = false;
                public static ReadOnlyCollection<MultiPrecision<N>> FracTable { private set; get; } = null;
        
                public static void Initialize() {
                    MultiPrecision<N> n = 1, n_frac = 1, n2_frac = 2;
                    List<MultiPrecision<N>> fracs = new List<MultiPrecision<N>>();

                    while(fracs.Count < 1 || fracs.Last().Exponent >= -Bits * 2) {
                        fracs.Add((n_frac * n_frac) / (n * n * n2_frac)); 
 
                        n += 1;
                        n_frac *= n;
                        n2_frac *= (2 * n - 1) * (2 * n);

                        if (!n2_frac.IsFinite) { 
                            break;
                        }
                    }

                    FracTable = Array.AsReadOnly(fracs.ToArray());

                    Initialized = true;
                }
            }
        }
    }
}
