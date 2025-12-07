using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Atan(MultiPrecision<N> x) {
            if (IsNaN(x)) {
                return NaN;
            }

            if (!IsFinite(x)) {
                return x.Sign == Sign.Plus ? Pi / 2 : -Pi / 2;
            }

            MultiPrecision<Plus1<N>> x_ex = x.Convert<Plus1<N>>();

            if (x.Exponent < 0) {
                MultiPrecision<Plus1<N>> z = MultiPrecision<Plus1<N>>.Abs(x_ex) / MultiPrecision<Plus1<N>>.Sqrt(x_ex * x_ex + 1);
                MultiPrecision<N> w = MultiPrecision<Plus1<N>>.Sqrt(MultiPrecision<Plus1<N>>.SquareAsin(z)).Convert<N>();

                if (IsZero(w)) {
                    return x;
                }

                return new MultiPrecision<N>(x.Sign, w.exponent, w.mantissa);
            }
            else {
                MultiPrecision<Plus1<N>> invx = 1 / x_ex;
                MultiPrecision<Plus1<N>> z = MultiPrecision<Plus1<N>>.Abs(invx) / MultiPrecision<Plus1<N>>.Sqrt(invx * invx + 1);
                MultiPrecision<N> w = MultiPrecision<Plus1<N>>.Sqrt(MultiPrecision<Plus1<N>>.SquareAsin(z)).Convert<N>();

                if (x.Sign == Sign.Plus) {
                    return Pi / 2 - w;
                    }
                else {
                    return w - Pi / 2;
                }
            }
        }

        public static MultiPrecision<N> Asin(MultiPrecision<N> x) {
            if (!(x >= MinusOne && x <= One)) {
                return NaN;
            }

            if (x == MinusOne) {
                return -Pi / 2;
            }
            if (x == One) {
                return Pi / 2;
            }

            if (Abs(x) <= Sqrt2 / 2) {
                MultiPrecision<Plus1<N>> x_ex = x.Convert<Plus1<N>>();

                MultiPrecision<N> w =
                    MultiPrecision<Plus1<N>>.Sqrt(
                        MultiPrecision<Plus1<N>>.SquareAsin(
                            MultiPrecision<Plus1<N>>.Abs(x_ex))).Convert<N>();

                if (IsZero(w)) {
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
            return Pi / 2 - Asin(x);
        }

        public static MultiPrecision<N> Atan2(MultiPrecision<N> y, MultiPrecision<N> x) {
            if (IsZero(x) && IsZero(y)) {
                return Zero;
            }
            if (!IsFinite(x) || !IsFinite(y)) {
                return NaN;
            }
            if (Abs(x) >= Abs(y)) {
                MultiPrecision<N> yx = y / x;
                return x.Sign == Sign.Plus ? Atan(yx) : ((y.Sign == Sign.Plus) ? (Atan(yx) + Pi) : (Atan(yx) - Pi));
            }
            else {
                MultiPrecision<N> xy = x / y;
                return y.Sign == Sign.Plus ? (Pi / 2 - Atan(xy)) : (-Pi / 2 - Atan(xy));
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

                if (MultiPrecision<Plus1<N>>.IsZero(dz) || z.Exponent - dz.Exponent > MultiPrecision<Plus1<N>>.Bits) {
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
                public static ReadOnlyCollection<MultiPrecision<Plus1<N>>> FracTable { private set; get; } = null;

                static SquareAsin() {
                    MultiPrecision<Plus1<N>> n = 1, n_frac = 1, n2_frac = 2;
                    List<MultiPrecision<Plus1<N>>> fracs = [];

                    while (fracs.Count < 1 || fracs.Last().Exponent >= -MultiPrecision<Plus1<N>>.Bits * 2) {
                        fracs.Add((n_frac * n_frac) / (n * n * n2_frac));

                        n += 1;
                        n_frac *= n;
                        n2_frac *= (2 * n - 1) * (2 * n);

                        if (!MultiPrecision<Plus1<N>>.IsFinite(n2_frac)) {
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
