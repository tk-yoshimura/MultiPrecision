using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Atan(MultiPrecision<N> x) {
            if (x.IsNaN) {
                return NaN;
            }

            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? PI / 2 : -PI / 2;
            }

            MultiPrecision<Plus1<N>> x_ex = x.Convert<Plus1<N>>();

            if (x <= One && x >= MinusOne) {
                MultiPrecision<Plus1<N>> z = MultiPrecision<Plus1<N>>.Abs(x_ex) / MultiPrecision<Plus1<N>>.Sqrt(x_ex * x_ex + 1);
                MultiPrecision<N> w = MultiPrecision<Plus1<N>>.Sqrt(MultiPrecision<Plus1<N>>.SquareAsin(z)).Convert<N>();

                if (w.IsZero) {
                    return x;
                }

                return new MultiPrecision<N>(x.Sign, w.exponent, w.mantissa);
            }
            else {
                MultiPrecision<Plus1<N>> invx = 1 / x_ex;
                MultiPrecision<Plus1<N>> z = MultiPrecision<Plus1<N>>.Abs(invx) / MultiPrecision<Plus1<N>>.Sqrt(invx * invx + 1);
                MultiPrecision<N> w = MultiPrecision<Plus1<N>>.Sqrt(MultiPrecision<Plus1<N>>.SquareAsin(z)).Convert<N>();

                if (x.Sign == Sign.Plus) {
                    return PI / 2 - w;
                }
                else {
                    return w - PI / 2;
                }
            }
        }

        public static MultiPrecision<N> Asin(MultiPrecision<N> x) {
            if (!(x >= MinusOne && x <= One)) {
                return NaN;
            }

            if (x == MinusOne) {
                return -PI / 2;
            }
            if (x == One) {
                return PI / 2;
            }

            if (Abs(x) <= Sqrt2 / 2) {
                MultiPrecision<Plus1<N>> x_ex = x.Convert<Plus1<N>>();

                MultiPrecision<N> w =
                    MultiPrecision<Plus1<N>>.Sqrt(
                        MultiPrecision<Plus1<N>>.SquareAsin(
                            MultiPrecision<Plus1<N>>.Abs(x_ex))).Convert<N>();

                if (w.IsZero) {
                    return x;
                }

                return new MultiPrecision<N>(x.Sign, w.exponent, w.mantissa);
            }
            else {
                MultiPrecision<N> z = x / (Sqrt(1 - x * x) + 1);
                return Atan(z) * 2;
            }
        }

        public static MultiPrecision<N> Acos(MultiPrecision<N> x) {
            return PI / 2 - Asin(x);
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
            else {
                MultiPrecision<N> xy = x / y;
                return y.Sign == Sign.Plus ? (PI / 2 - Atan(xy)) : (-PI / 2 - Atan(xy));
            }
        }

        internal static MultiPrecision<N> SquareAsin(MultiPrecision<N> x) {
#if DEBUG
            Debug<ArithmeticException>.Assert(x >= Zero && x < One);
#endif

            MultiPrecision<Plus1<N>> x_expand = x.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> z = MultiPrecision<Plus1<N>>.Zero, dz = MultiPrecision<Plus1<N>>.Zero;
            MultiPrecision<Plus1<N>> s = 4 * x_expand * x_expand, t = s;

#if DEBUG
            bool convergenced = false;
#endif

            foreach (MultiPrecision<Plus1<N>> f in Consts.SquareAsin.FracTable) {
                dz = t * f;
                z += dz;
                t *= s;

                if (dz.IsZero || z.Exponent - dz.Exponent > MultiPrecision<Plus1<N>>.Bits) {
#if DEBUG
                    convergenced = true;
#endif
                    break;
                }
            }

#if DEBUG
            Debug<ArithmeticException>.Assert(convergenced);
#endif

            return z.Convert<N>() / 2;
        }

        private static partial class Consts {
            public static class SquareAsin {
                public static IReadOnlyList<MultiPrecision<Plus1<N>>> FracTable { private set; get; } = null;

                static SquareAsin() {
                    MultiPrecision<Plus1<N>> n = 1, n_frac = 1, n2_frac = 2;
                    List<MultiPrecision<Plus1<N>>> fracs = new();

                    while (fracs.Count < 1 || fracs.Last().Exponent >= -MultiPrecision<Plus1<N>>.Bits * 2) {
                        fracs.Add((n_frac * n_frac) / (n * n * n2_frac));

                        n += 1;
                        n_frac *= n;
                        n2_frac *= (2 * n - 1) * (2 * n);

                        if (!n2_frac.IsFinite) {
                            break;
                        }
                    }

                    FracTable = Array.AsReadOnly(fracs.ToArray());

#if DEBUG
                    Trace.WriteLine($"SquareAsin<{Length}> initialized.");
#endif
                }
            }
        }
    }
}
