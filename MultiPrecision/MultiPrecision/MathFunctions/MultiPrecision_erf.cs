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

            MultiPrecision<N> y = ErfErfcCore(x, is_erf: true);

            return RoundMantissa(y, Bits - Consts.Erf.RoundBits);
        }

        public static MultiPrecision<N> Erfc(MultiPrecision<N> x) {
            if (x.IsZero) {
                return One;
            }
            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? Zero : Integer(2);
            }

            MultiPrecision<N> y = ErfErfcCore(x, is_erf: false);

            return RoundMantissa(y, Bits - Consts.Erf.RoundBits);
        }


        private static MultiPrecision<N> ErfErfcCore(MultiPrecision<N> x, bool is_erf) {
            if (!Consts.Erf.Initialized) {
                Consts.Erf.Initialize();
            }

            if (x.Exponent <= 0) {
                MultiPrecision<N> z = One;
                MultiPrecision<N> squa_x = x * x;
                MultiPrecision<N> y = Zero;

                foreach (MultiPrecision<N> t in Consts.Erf.Table) {
                    MultiPrecision<N> dy = t * z;
                    y += dy;

                    if (dy.IsZero || y.Exponent - dy.Exponent > Bits) {
                        break;
                    }

                    z *= squa_x;
                }

                y *= x * Consts.Erf.G;

                if (!is_erf) {
                    y = One - y;
                }

                return y;
            }
            else if (x.Sign == Sign.Plus) {
                MultiPrecision<N> z = x * Sqrt2;
                MultiPrecision<N> a = 0;

                // Number of convergences in length = 8, less than this number for length = 16.
                const double s = 57.387608, p = -1.809676;
                long n = (long)((double)Length * Length * s * Math.Pow((double)x, p)) + 1;

                while (n > 0) {
                    a = n / (z + a);
                    n--;
                }

                MultiPrecision<N> y = Exp(-x * x) / (z + a) * Consts.Erf.C;

                if (is_erf) {
                    y = One - y;
                }

                return y;
            }
            else {
                if (is_erf) {
                    return -ErfErfcCore(-x, is_erf: true);
                }
                else { 
                    return Integer(2) - ErfErfcCore(-x, is_erf: false);
                }
            }
        }

        private static partial class Consts {
            public static class Erf {
                public static bool Initialized { private set; get; } = false;
                public static int RoundBits { private set; get; } = 0;

                public static MultiPrecision<N> G { private set; get; } = null;
                public static MultiPrecision<N> C { private set; get; } = null;
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
                    C = Sqrt(2 / PI);

                    Initialized = true;

                    FixRoundBits();
                }

                public static void FixRoundBits(int matches = 5) {
                    MultiPrecision<N> x = 2;
                    MultiPrecision<N>[] ys = new MultiPrecision<N>[matches];

                    for(int i = 0; i < matches; i++) { 
                        ys[i] = ErfErfcCore(x, is_erf: false);
                        x = BitDecrement(x);
                    }

                    while (RoundBits < Bits / 2) {
                        int i;
                        for(i = 1; i < matches; i++) {
                            if (!NearlyEqualBits(ys[0], ys[i], RoundBits)){ 
                                RoundBits++;
                                break;
                            }
                        }
                        if(i == matches){
                            break;
                        }
                    }

                    RoundBits++;

#if DEBUG
                    Trace.WriteLine($"Erfc round bits : {RoundBits}bits @{Length}length");
#endif

                }
            }
        }
    }
}
