using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Atan(MultiPrecision<N> x) {
            if (x.IsNaN) {
                return NaN;
            }

            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? Ldexp(PI, -1) : -Ldexp(PI, -1);
            }

            if (x <= One && x >= MinusOne) {
                MultiPrecision<N> z = Abs(x) / Sqrt(x * x + One);
                MultiPrecision<N> w = Sqrt(SquareAsin(z));

                return new MultiPrecision<N>(x.Sign, w.exponent, w.mantissa);
            }
            else {
                MultiPrecision<N> invx = One / x;
                MultiPrecision<N> z = Abs(invx) / Sqrt(invx * invx + One);
                MultiPrecision<N> w = Sqrt(SquareAsin(z));

                if (x.Sign == Sign.Plus) {
                    return Ldexp(PI, -1) - w;
                }
                else {
                    return w - Ldexp(PI, -1);
                }
            }
        }

        public static MultiPrecision<N> Asin(MultiPrecision<N> x) {
            if (!(x >= MinusOne && x <= One)) {
                return NaN;
            }

            if (x == MinusOne) {
                return -Ldexp(PI, -1);
            }
            if (x == One) {
                return Ldexp(PI, -1);
            }

            if (Abs(x) <= Ldexp(Sqrt2, -1)) {
                MultiPrecision<N> w = Sqrt(SquareAsin(Abs(x)));
                return new MultiPrecision<N>(x.Sign, w.exponent, w.mantissa);
            }
            else {
                MultiPrecision<N> z = x / (Sqrt(One - x * x) + One);
                return Ldexp(Atan(z), 1);
            }
        }

        public static MultiPrecision<N> Acos(MultiPrecision<N> x) {
            return Ldexp(PI, -1) - Asin(x);
        }

        public static MultiPrecision<N> Atan2(MultiPrecision<N> y, MultiPrecision<N> x) {
            if (x.IsZero && y.IsZero) {
                return Zero;
            }
            if (!x.IsFinite || !y.IsFinite) {
                return NaN;
            }
            if (Abs(x) >= Abs(y)) {
                MultiPrecision<N> yx = y / x;
                return x.Sign == Sign.Plus ? Atan(yx) : ((y.Sign == Sign.Plus) ? (Atan(yx) + PI) : (Atan(yx) - PI));
            }
            if (Abs(x) <= Abs(y)) {
                MultiPrecision<N> xy = x / y;
                return y.Sign == Sign.Plus ? (Ldexp(PI, -1) - Atan(xy)) : (-Ldexp(PI, -1) - Atan(xy));
            }
            return NaN;
        }

        internal static MultiPrecision<N> SquareAsin(MultiPrecision<N> x) {
            if (!Consts.SquareAsin.Initialized) {
                Consts.SquareAsin.Initialize();
            }

#if DEBUG
            Debug<ArithmeticException>.Assert(x >= Zero && x < One);
#endif

            MultiPrecision<Plus1<N>> x_expand = MultiPrecisionUtil.Convert<Plus1<N>, N>(x);
            MultiPrecision<Plus1<N>> z = MultiPrecision<Plus1<N>>.Zero, dz = MultiPrecision<Plus1<N>>.Zero;
            MultiPrecision<Plus1<N>> s = MultiPrecision<Plus1<N>>.Ldexp(x_expand * x_expand, 2), t = s;

#if DEBUG
            bool convergenced = false;
#endif

            foreach (MultiPrecision<Plus1<N>> f in Consts.SquareAsin.FracTable) {
                dz = t * f;
                z += dz;
                t *= s;

                if (dz.IsZero || z.Exponent - dz.Exponent > Bits) {
#if DEBUG
                    convergenced = true;
#endif
                    break;
                }
            }

#if DEBUG
            Debug<ArithmeticException>.Assert(convergenced);
#endif

            return Ldexp(MultiPrecisionUtil.Convert<N, Plus1<N>>(z), -1);
        }

        private static partial class Consts {
            public static class SquareAsin {
                public static bool Initialized { private set; get; } = false;
                public static ReadOnlyCollection<MultiPrecision<Plus1<N>>> FracTable { private set; get; } = null;

                public static void Initialize() {
                    MultiPrecision<Plus1<N>> n = 1, n_frac = 1, n2_frac = 2;
                    List<MultiPrecision<Plus1<N>>> fracs = new List<MultiPrecision<Plus1<N>>>();

                    while (fracs.Count < 1 || fracs.Last().Exponent >= -Bits * 2) {
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
