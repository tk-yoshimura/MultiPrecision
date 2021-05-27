using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> BesselJ(MultiPrecision<N> nu, MultiPrecision<N> x) {
            if (Abs(nu) > 64) {
                throw new ArgumentOutOfRangeException(
                    nameof(nu),
                    "In the calculation of the Bessel function, nu with an absolute value greater than 64 is not supported."
                );
            }
            if (nu.IsNaN || x.IsNaN) {
                return NaN;
            }

            if (x.Sign == Sign.Minus) {
                if (nu != Truncate(nu)) {
                    return NaN;
                }

                long n = (long)nu;
                return ((n & 1L) == 0) ? BesselJ(nu, Abs(x)) : -BesselJ(nu, Abs(x));
            }

            if (!x.IsFinite) {
                return 0;
            }
            if (x.Exponent >= Bits) {
                return NaN;
            }

            if (nu.Sign == Sign.Minus && nu == Truncate(nu)) {
                long n = (long)nu;
                return ((n & 1L) == 0) ? BesselJ(Abs(nu), x) : -BesselJ(Abs(nu), x);
            }

            if (nu - Point5 == Floor(nu)) {
                long n = (long)Floor(nu);

                if (n >= -2 && n < 2) {
                    MultiPrecision<Plus1<N>> x_ex = x.Convert<Plus1<N>>();
                    MultiPrecision<Plus1<N>> envelope = MultiPrecision<Plus1<N>>.Sqrt(2 / (MultiPrecision<Plus1<N>>.PI * x_ex));

                    if (n == -2) {
                        return -(envelope * (MultiPrecision<Plus1<N>>.Cos(x_ex) / x_ex + MultiPrecision<Plus1<N>>.Sin(x_ex))).Convert<N>();
                    }
                    if (n == -1) {
                        return (envelope * MultiPrecision<Plus1<N>>.Cos(x_ex)).Convert<N>();
                    }
                    if (n == 0) {
                        MultiPrecision<N> y = (envelope * MultiPrecision<Plus1<N>>.Sin(x_ex)).Convert<N>();

                        return y.IsNaN ? 0 : y;
                    }
                    if (n == 1) {
                        MultiPrecision<N> y = (envelope * (MultiPrecision<Plus1<N>>.Sin(x_ex) / x_ex - MultiPrecision<Plus1<N>>.Cos(x_ex))).Convert<N>();

                        return y.IsNaN ? 0 : y;
                    }
                }
            }

            if (x.Exponent <= -0x1000000) {
                return nu.IsZero ? 1 : ((nu.Sign == Sign.Plus || nu == Truncate(nu)) ? 0 : NaN);
            }

            if (x < Consts.BesselJY.ApproxThreshold) {
                return MultiPrecision<Plus1<N>>.BesselJNearZero(nu.Convert<Plus1<N>>(), x.Convert<Plus1<N>>()).Convert<N>();
            }
            else {
                return BesselJLimit(nu, x);
            }
        }

        public static MultiPrecision<N> BesselY(MultiPrecision<N> nu, MultiPrecision<N> x) {
            if (Abs(nu) > 64) {
                throw new ArgumentOutOfRangeException(
                    nameof(nu),
                    "In the calculation of the Bessel function, nu with an absolute value greater than 64 is not supported."
                );
            }
            if (nu.IsNaN || x.IsNaN) {
                return NaN;
            }

            if (x.Sign == Sign.Minus) {
                if (nu != Truncate(nu)) {
                    return NaN;
                }

                long n = (long)nu;
                return ((n & 1L) == 0) ? BesselY(nu, Abs(x)) : -BesselY(nu, Abs(x));
            }

            if (!x.IsFinite) {
                return 0;
            }
            if (x.Exponent >= Bits) {
                return NaN;
            }

            if (nu.Sign == Sign.Minus && nu == Truncate(nu)) {
                long n = (long)nu;
                return ((n & 1L) == 0) ? BesselY(Abs(nu), x) : -BesselY(Abs(nu), x);
            }

            if (nu - Point5 == Floor(nu)) {
                long n = (long)Floor(nu);

                if (n >= -2 && n < 2) {
                    MultiPrecision<Plus1<N>> x_ex = x.Convert<Plus1<N>>();
                    MultiPrecision<Plus1<N>> envelope = MultiPrecision<Plus1<N>>.Sqrt(2 / (MultiPrecision<Plus1<N>>.PI * x_ex));

                    if (n == -2) {
                        MultiPrecision<N> y = -(envelope * (MultiPrecision<Plus1<N>>.Sin(x_ex) / x_ex - MultiPrecision<Plus1<N>>.Cos(x_ex))).Convert<N>();

                        return y.IsNaN ? 0 : y;
                    }
                    if (n == -1) {
                        MultiPrecision<N> y = (envelope * MultiPrecision<Plus1<N>>.Sin(x_ex)).Convert<N>();

                        return y.IsNaN ? 0 : y;
                    }
                    if (n == 0) {
                        return -(envelope * MultiPrecision<Plus1<N>>.Cos(x_ex)).Convert<N>();
                    }
                    if (n == 1) {
                        return -(envelope * (MultiPrecision<Plus1<N>>.Cos(x_ex) / x_ex + MultiPrecision<Plus1<N>>.Sin(x_ex))).Convert<N>();
                    }
                }
            }

            if (x.Exponent <= -0x1000000) {
                return nu.IsZero ? NegativeInfinity : NaN;
            }

            if (x < Consts.BesselJY.ApproxThreshold) {
                return BesselYNearZero(nu, x);
            }
            else {
                return BesselYLimit(nu, x);
            }
        }

        public static MultiPrecision<N> BesselI(MultiPrecision<N> nu, MultiPrecision<N> x) {
            if (Abs(nu) > 64) {
                throw new ArgumentOutOfRangeException(
                    nameof(nu),
                    "In the calculation of the Bessel function, nu with an absolute value greater than 64 is not supported."
                );
            }
            if (nu.IsNaN || x.IsNaN || x.Sign == Sign.Minus) {
                return NaN;
            }

            if (nu.Sign == Sign.Minus && nu == Truncate(nu)) {
                return BesselI(Abs(nu), x);
            }

            if (nu - Point5 == Floor(nu)) {
                long n = (long)Floor(nu);

                if (n >= -2 && n < 2) {
                    MultiPrecision<Plus1<N>> x_ex = x.Convert<Plus1<N>>();
                    MultiPrecision<Plus1<N>> r = MultiPrecision<Plus1<N>>.Sqrt2 / MultiPrecision<Plus1<N>>.Sqrt(MultiPrecision<Plus1<N>>.PI * x_ex);

                    if (n == -2) {
                        return -(r * (MultiPrecision<Plus1<N>>.Cosh(x_ex) / x_ex - MultiPrecision<Plus1<N>>.Sinh(x_ex))).Convert<N>();
                    }
                    if (n == -1) {
                        return (r * MultiPrecision<Plus1<N>>.Cosh(x_ex)).Convert<N>();
                    }
                    if (n == 0) {
                        MultiPrecision<N> y = (r * MultiPrecision<Plus1<N>>.Sinh(x_ex)).Convert<N>();

                        return y.IsNormal ? y : 0;
                    }
                    if (n == 1) {
                        MultiPrecision<N> y = -(r * (MultiPrecision<Plus1<N>>.Sinh(x_ex) / x_ex - MultiPrecision<Plus1<N>>.Cosh(x_ex))).Convert<N>();

                        return y.IsNormal ? y : 0;
                    }
                }
            }

            if (x.Exponent <= -0x1000000) {
                return nu.IsZero ? 1 : ((nu.Sign == Sign.Plus || nu == Truncate(nu)) ? 0 : NaN);
            }

            if (x < Consts.BesselIK.ApproxThreshold) {
                return BesselINearZero(nu, x);
            }
            else {
                return BesselILimit(nu, x);
            }
        }

        public static MultiPrecision<N> BesselK(MultiPrecision<N> nu, MultiPrecision<N> x) {
            if (Abs(nu) > 64) {
                throw new ArgumentOutOfRangeException(
                    nameof(nu),
                    "In the calculation of the Bessel function, nu with an absolute value greater than 64 is not supported."
                );
            }
            if (nu.IsNaN || x.IsNaN || x.Sign == Sign.Minus) {
                return NaN;
            }

            if (nu.Sign == Sign.Minus) {
                return BesselK(Abs(nu), x);
            }

            if (nu - Point5 == Floor(nu)) {
                long n = (long)Floor(nu);

                if (n >= 0 && n < 2) {
                    MultiPrecision<Plus1<N>> x_ex = x.Convert<Plus1<N>>();
                    MultiPrecision<Plus1<N>> r = MultiPrecision<Plus1<N>>.Exp(-x_ex) * MultiPrecision<Plus1<N>>.Sqrt(MultiPrecision<Plus1<N>>.PI / (2 * x_ex));

                    if (n == 0) {
                        return r.Convert<N>();
                    }
                    if (n == 1) {
                        return (r * (1 + 1 / x_ex)).Convert<N>();
                    }
                }
            }

            if (x.Exponent <= -0x1000000) {
                return nu.IsZero ? PositiveInfinity : NaN;
            }

            if (x < Consts.BesselIK.ApproxThreshold) {
                return BesselKNearZero(nu, x);
            }
            else {
                return BesselKLimit(nu, x);
            }
        }

        private static MultiPrecision<N> BesselJNearZero(MultiPrecision<N> nu, MultiPrecision<N> z) {
            Consts.BesselNearZeroCoef table = Consts.Bessel.NearZeroCoef(nu);

            MultiPrecision<Double<N>> z_ex = z.Convert<Double<N>>();
            MultiPrecision<Double<N>> u = 1;
            MultiPrecision<Double<N>> w = z_ex * z_ex, ww = w * w;

            MultiPrecision<Double<N>> x = 0;
            bool probably_convergenced = false;

            for (int k = 0; k < int.MaxValue - 1; k += 2, u *= ww) {
                MultiPrecision<Double<N>> c = u * (table.Value(k) - w * table.Value(k + 1));

                x += c;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    if (probably_convergenced) {
                        break;
                    }
                    else {
                        probably_convergenced = true;
                        continue;
                    }
                }
                probably_convergenced = false;

                if (k >= Bits && Math.Max(x.Exponent, c.Exponent) < -Bits * 2) {
                    return 0;
                }
            }

            MultiPrecision<Plus1<N>> p;
            if (nu == Truncate(nu)) {
                int n = (int)nu;

                p = MultiPrecision<Plus1<N>>.Pow(z.Convert<Plus1<N>>() / 2, n);
            }
            else {
                p = MultiPrecision<Plus1<N>>.Pow(z.Convert<Plus1<N>>() / 2, nu.Convert<Plus1<N>>());
            }

            MultiPrecision<Plus1<N>> y = x.Convert<Plus1<N>>() * p;

            return y.Convert<N>();
        }

        private static MultiPrecision<N> BesselJLimit(MultiPrecision<N> nu, MultiPrecision<N> z) {
            Consts.BesselLimitCoef table = Consts.Bessel.LimitCoef(nu);

            MultiPrecision<Plus4<N>> z_ex = z.Convert<Plus4<N>>();
            MultiPrecision<Plus4<N>> v = 1 / z_ex;
            MultiPrecision<Plus4<N>> w = v * v;

            MultiPrecision<Plus4<N>> x = 0, y = 0, p = 1, q = v;

            Sign sign = Sign.Plus;

            for (int k = 0; k <= Consts.BesselJY.LimitApproxTerms; k++, p *= w, q *= w) {
                MultiPrecision<Plus4<N>> c = p * table.Value(k * 2);
                MultiPrecision<Plus4<N>> s = q * table.Value(k * 2 + 1);

                if (sign == Sign.Plus) {
                    x += c;
                    y += s;
                    sign = Sign.Minus;
                }
                else {
                    x -= c;
                    y -= s;
                    sign = Sign.Plus;
                }

                if (!c.IsZero && x.Exponent - c.Exponent <= MultiPrecision<Plus1<N>>.Bits) {
                    continue;
                }
                if (!s.IsZero && y.Exponent - s.Exponent <= MultiPrecision<Plus1<N>>.Bits) {
                    continue;
                }

                break;
            }

            MultiPrecision<Plus1<N>> z_ex1 = z.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> omega = z_ex1 - (2 * nu.Convert<Plus1<N>>() + 1) * MultiPrecision<Plus1<N>>.PI / 4;
            MultiPrecision<Plus1<N>> m = 
                x.Convert<Plus1<N>>() * MultiPrecision<Plus1<N>>.Cos(omega)
                - y.Convert<Plus1<N>>() * MultiPrecision<Plus1<N>>.Sin(omega);
            MultiPrecision<Plus1<N>> t = m * MultiPrecision<Plus1<N>>.Sqrt(2 / (MultiPrecision<Plus1<N>>.PI * z_ex1));

            return t.Convert<N>();
        }

        private static MultiPrecision<N> BesselYNearZero(MultiPrecision<N> nu, MultiPrecision<N> z) {
            int n = (int)Round(nu);
            int needs_bits = Bits + 16;

            if (nu != n) {
                MultiPrecision<N> dnu = nu - n;

                if (dnu.Exponent >= -16) {
                    return MultiPrecision<Plus2<N>>.BesselYNonIntegerNu(nu.Convert<Plus2<N>>(), z.Convert<Plus2<N>>(), needs_bits).Convert<N>();
                }
                if (dnu.Exponent >= -32) {
                    return MultiPrecision<Plus4<N>>.BesselYNonIntegerNu(nu.Convert<Plus4<N>>(), z.Convert<Plus4<N>>(), needs_bits).Convert<N>();
                }
                if (dnu.Exponent >= -48) {
                    return MultiPrecision<Plus8<N>>.BesselYNonIntegerNu(nu.Convert<Plus8<N>>(), z.Convert<Plus8<N>>(), needs_bits).Convert<N>();
                }
                if (dnu.Exponent >= -80) {
                    return MultiPrecision<Plus16<N>>.BesselYNonIntegerNu(nu.Convert<Plus16<N>>(), z.Convert<Plus16<N>>(), needs_bits).Convert<N>();
                }
                if (dnu.Exponent >= -144) {
                    return MultiPrecision<Plus32<N>>.BesselYNonIntegerNu(nu.Convert<Plus32<N>>(), z.Convert<Plus32<N>>(), needs_bits).Convert<N>();
                }
                if (dnu.Exponent >= -272) {
                    return MultiPrecision<Plus64<N>>.BesselYNonIntegerNu(nu.Convert<Plus64<N>>(), z.Convert<Plus64<N>>(), needs_bits).Convert<N>();
                }

                throw new ArgumentException(
                    "The calculation of the BesselY function value is invalid because it loses digits" +
                    " when nu is extremely close to an integer. (|nu - round(nu)| < 1.32 x 10^-82 and nu != round(nu))",
                    nameof(nu));
            }

            return MultiPrecision<Plus1<N>>.BesselYIntegerNuNearZero(n, z.Convert<Plus1<N>>()).Convert<N>();
        }

        private static MultiPrecision<N> BesselYNonIntegerNu(MultiPrecision<N> nu, MultiPrecision<N> z, int needs_bits) {
            Consts.BesselNearZeroCoef table_pos = Consts.Bessel.NearZeroCoef(nu);
            Consts.BesselNearZeroCoef table_neg = Consts.Bessel.NearZeroCoef(-nu);

            MultiPrecision<Double<N>> z_ex = z.Convert<Double<N>>(), nu_ex = nu.Convert<Double<N>>();
            MultiPrecision<Double<N>> p = MultiPrecision<Double<N>>.Pow(z_ex / 2, nu_ex);
            MultiPrecision<Double<N>> cos = MultiPrecision<Double<N>>.Consts.Bessel.SinCos(nu_ex).cos;
            MultiPrecision<Double<N>> u = p * cos, v = 1 / p;
            MultiPrecision<Double<N>> w = z_ex * z_ex, ww = w * w;

            MultiPrecision<Double<N>> x = 0;
            bool probably_convergenced = false;

            for (int k = 0; k < int.MaxValue - 1; k += 2, u *= ww, v *= ww) {
                MultiPrecision<Double<N>> c_pos = u * (table_pos.Value(k) - w * table_pos.Value(k + 1));
                MultiPrecision<Double<N>> c_neg = v * (table_neg.Value(k) - w * table_neg.Value(k + 1));

                MultiPrecision<Double<N>> c = c_pos - c_neg;

                x += c;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    if (probably_convergenced) {
                        break;
                    }
                    else {
                        probably_convergenced = true;
                        continue;
                    }
                }
                probably_convergenced = false;

                if (k >= Bits && Math.Max(x.Exponent, c.Exponent) < -Bits * 2) {
                    return 0;
                }
            }

            MultiPrecision<Plus1<N>> sin = MultiPrecision<Plus1<N>>.Consts.Bessel.SinCos(nu.Convert<Plus1<N>>()).sin;

            MultiPrecision<Plus1<N>> y = x.Convert<Plus1<N>>() / sin;

            return y.Convert<N>();
        }

        private static MultiPrecision<N> BesselYIntegerNuNearZero(int n, MultiPrecision<N> z) {
            Consts.BesselNearZeroCoef nearzero_table = Consts.Bessel.NearZeroCoef(n);
            Consts.BesselIntegerFiniteTermCoef finite_table = Consts.Bessel.IntegerFiniteTermCoef(n);
            Consts.BesselIntegerConvergenceTermCoef convergence_table = Consts.Bessel.IntegerConvergenceTermCoef(n);

            MultiPrecision<Double<N>> z_ex = z.Convert<Double<N>>();
            MultiPrecision<Double<N>> m = MultiPrecision<Double<N>>.Pow(z_ex / 2, n), inv_mm = 1 / (m * m);
            MultiPrecision<Double<N>> u = m, v = 2 * m * MultiPrecision<Double<N>>.Log(z_ex / 2);
            MultiPrecision<Double<N>> w = z_ex * z_ex;

            MultiPrecision<Double<N>> x = 0;
            bool probably_convergenced = false;

            Sign sign = Sign.Plus;

            for (int k = 0; k < int.MaxValue; k++, u *= w, v *= w) {
                MultiPrecision<Double<N>> c_pos = v * nearzero_table.Value(k);
                MultiPrecision<Double<N>> c_neg = u * convergence_table.Value(k);
                MultiPrecision<Double<N>> c = c_pos - c_neg;

                if (sign == Sign.Plus) {
                    x += c;
                    sign = Sign.Minus;
                }
                else {
                    x -= c;
                    sign = Sign.Plus;
                }

                if (k < n) {
                    x -= u * inv_mm * finite_table.Value(k);
                }
                else {
                    if(c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                        if (probably_convergenced) {
                            break;
                        }
                        else {
                            probably_convergenced = true;
                            continue;
                        }
                    }
                    probably_convergenced = false;

                    if (k >= Bits && Math.Max(x.Exponent, c.Exponent) < -Bits * 2) {
                        return 0;
                    }
                }
            }

            MultiPrecision<Plus1<N>> d = x.Convert<Plus1<N>>() / MultiPrecision<Plus1<N>>.PI;

            return d.Convert<N>();
        }

        private static MultiPrecision<N> BesselYLimit(MultiPrecision<N> nu, MultiPrecision<N> z) {
            Consts.BesselLimitCoef table = Consts.Bessel.LimitCoef(nu);

            MultiPrecision<Plus4<N>> z_ex = z.Convert<Plus4<N>>();
            MultiPrecision<Plus4<N>> v = 1 / z_ex;
            MultiPrecision<Plus4<N>> w = v * v;

            MultiPrecision<Plus4<N>> x = 0, y = 0, p = 1, q = v;

            Sign sign = Sign.Plus;

            for (int k = 0; k <= Consts.BesselJY.LimitApproxTerms; k++, p *= w, q *= w) {
                MultiPrecision<Plus4<N>> c = p * table.Value(k * 2);
                MultiPrecision<Plus4<N>> s = q * table.Value(k * 2 + 1);

                if (sign == Sign.Plus) {
                    x += c;
                    y += s;
                    sign = Sign.Minus;
                }
                else {
                    x -= c;
                    y -= s;
                    sign = Sign.Plus;
                }

                if (!c.IsZero && x.Exponent - c.Exponent <= MultiPrecision<Plus1<N>>.Bits) {
                    continue;
                }
                if (!s.IsZero && y.Exponent - s.Exponent <= MultiPrecision<Plus1<N>>.Bits) {
                    continue;
                }

                break;
            }

            MultiPrecision<Plus1<N>> z_ex1 = z.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> omega = z_ex1 - (2 * nu.Convert<Plus1<N>>() + 1) * MultiPrecision<Plus1<N>>.PI / 4;
            MultiPrecision<Plus1<N>> m = 
                x.Convert<Plus1<N>>() * MultiPrecision<Plus1<N>>.Sin(omega)
                + y.Convert<Plus1<N>>() * MultiPrecision<Plus1<N>>.Cos(omega);
            MultiPrecision<Plus1<N>> t = m * MultiPrecision<Plus1<N>>.Sqrt(2 / (MultiPrecision<Plus1<N>>.PI * z_ex1));

            return t.Convert<N>();
        }

        private static MultiPrecision<N> BesselINearZero(MultiPrecision<N> nu, MultiPrecision<N> z) {
            Consts.BesselNearZeroCoef table = Consts.Bessel.NearZeroCoef(nu);

            MultiPrecision<Double<N>> z_ex = z.Convert<Double<N>>();
            MultiPrecision<Double<N>> u = 1;
            MultiPrecision<Double<N>> w = z_ex * z_ex;

            MultiPrecision<Double<N>> x = 0;

            for (int k = 0; k < int.MaxValue; k++, u *= w) {
                MultiPrecision<Double<N>> c = u * table.Value(k);

                x += c;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    break;
                }
            }

            MultiPrecision<Plus1<N>> p;
            if (nu == Truncate(nu)) {
                int n = (int)nu;

                p = MultiPrecision<Plus1<N>>.Pow(z.Convert<Plus1<N>>() / 2, n);
            }
            else {
                p = MultiPrecision<Plus1<N>>.Pow(z.Convert<Plus1<N>>() / 2, nu.Convert<Plus1<N>>());
            }

            MultiPrecision<Plus1<N>> y = x.Convert<Plus1<N>>() * p;

            return y.Convert<N>();
        }

        private static MultiPrecision<N> BesselILimit(MultiPrecision<N> nu, MultiPrecision<N> z) {
            Consts.BesselLimitCoef table = Consts.Bessel.LimitCoef(nu);

            MultiPrecision<Plus4<N>> z_ex = z.Convert<Plus4<N>>();
            MultiPrecision<Plus4<N>> v = 1 / z_ex;

            MultiPrecision<Plus4<N>> x = 0, p = 1;

            Sign sign = Sign.Plus;

            for (int k = 0; k <= Consts.BesselIK.LimitApproxTerms; k++, p *= v) {
                MultiPrecision<Plus4<N>> c = p * table.Value(k);

                if (sign == Sign.Plus) {
                    x += c;
                    sign = Sign.Minus;
                }
                else {
                    x -= c;
                    sign = Sign.Plus;
                }

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    break;
                }
            }

            MultiPrecision<Plus1<N>> z_ex1 = z.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> r =
                MultiPrecision<Plus1<N>>.Exp(z_ex1) / MultiPrecision<Plus1<N>>.Sqrt(2 * MultiPrecision<Plus1<N>>.PI * z_ex1);

            MultiPrecision<Plus1<N>> y = r * x.Convert<Plus1<N>>();

            return y.Convert<N>();
        }

        private static MultiPrecision<N> BesselKNearZero(MultiPrecision<N> nu, MultiPrecision<N> z) {
            int n = (int)Round(nu);

            if (nu != n) {
                MultiPrecision<N> dnu = nu - n;

                if (dnu.Exponent >= -96) {
                    return MultiPrecision<Plus4<N>>.BesselKNonIntegerNu(nu.Convert<Plus4<N>>(), z.Convert<Plus4<N>>()).Convert<N>();
                }
                if (dnu.Exponent >= -272) {
                    return MultiPrecision<Plus8<N>>.BesselKNonIntegerNu(nu.Convert<Plus8<N>>(), z.Convert<Plus8<N>>()).Convert<N>();
                }

                throw new ArgumentException(
                    "The calculation of the BesselK function value is invalid because it loses digits" +
                    " when nu is extremely close to an integer. (|nu - round(nu)| < 1.32 x 10^-82 and nu != round(nu))",
                    nameof(nu));
            }

            return MultiPrecision<Plus4<N>>.BesselKIntegerNuNearZero(n, z.Convert<Plus4<N>>()).Convert<N>();
        }

        private static MultiPrecision<N> BesselKNonIntegerNu(MultiPrecision<N> nu, MultiPrecision<N> z) {
            Consts.BesselNearZeroCoef table_pos = Consts.Bessel.NearZeroCoef(nu);
            Consts.BesselNearZeroCoef table_neg = Consts.Bessel.NearZeroCoef(-nu);

            MultiPrecision<Double<N>> z_ex = z.Convert<Double<N>>();
            MultiPrecision<Double<N>> p = MultiPrecision<Double<N>>.Pow(z_ex / 2, nu.Convert<Double<N>>());
            MultiPrecision<Double<N>> u = p, v = 1 / p;
            MultiPrecision<Double<N>> w = z_ex * z_ex;

            MultiPrecision<Double<N>> x = 0;
            bool probably_convergenced = false;

            for (int k = 0; k < int.MaxValue; k++, u *= w, v *= w) {
                MultiPrecision<Double<N>> c_pos = u * table_pos.Value(k), c_neg = v * table_neg.Value(k);
                MultiPrecision<Double<N>> c = c_neg - c_pos;

                x += c;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    if (probably_convergenced) {
                        break;
                    }
                    else {
                        probably_convergenced = true;
                        continue;
                    }
                }
                probably_convergenced = false;
            }

            MultiPrecision<Plus1<N>> sin = MultiPrecision<Plus1<N>>.Consts.Bessel.SinCos(nu.Convert<Plus1<N>>()).sin;

            MultiPrecision<Plus1<N>> y = (x.Convert<Plus1<N>>() * MultiPrecision<Plus1<N>>.PI) / (2 * sin);

            return y.Convert<N>();
        }

        private static MultiPrecision<N> BesselKIntegerNuNearZero(int n, MultiPrecision<N> z) {
            Consts.BesselNearZeroCoef nearzero_table = Consts.Bessel.NearZeroCoef(n);
            Consts.BesselIntegerFiniteTermCoef finite_table = Consts.Bessel.IntegerFiniteTermCoef(n);
            Consts.BesselIntegerConvergenceTermCoef convergence_table = Consts.Bessel.IntegerConvergenceTermCoef(n);

            MultiPrecision<Double<N>> z_ex = z.Convert<Double<N>>();
            MultiPrecision<Double<N>> m = MultiPrecision<Double<N>>.Pow(z_ex / 2, n), inv_mm = 1 / (m * m);
            MultiPrecision<Double<N>> u = m, v = 2 * m * MultiPrecision<Double<N>>.Log(z_ex / 2);
            MultiPrecision<Double<N>> w = z_ex * z_ex;

            MultiPrecision<Double<N>> x = 0;
            bool probably_convergenced = false;

            Sign sign = Sign.Plus;

            for (int k = 0; k < int.MaxValue; k++, u *= w, v *= w) {
                MultiPrecision<Double<N>> c_pos = u * convergence_table.Value(k);
                MultiPrecision<Double<N>> c_neg = v * nearzero_table.Value(k);
                MultiPrecision<Double<N>> c = c_pos - c_neg;

                if ((n & 1) == 0) {
                    x += c;
                }
                else {
                    x -= c;
                }

                if (k < n) {
                    MultiPrecision<Double<N>> d = u * inv_mm * finite_table.Value(k);
                    
                    if (sign == Sign.Plus) {
                        x += d;
                        sign = Sign.Minus;
                    }
                    else {
                        x -= d;
                        sign = Sign.Plus;
                    }
                }
                else {
                    if(c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                        if (probably_convergenced) {
                            break;
                        }
                        else {
                            probably_convergenced = true;
                            continue;
                        }
                    }
                    probably_convergenced = false;
                }
            }

            MultiPrecision<N> y = x.Convert<N>() / 2;

            return y;
        }

        private static MultiPrecision<N> BesselKLimit(MultiPrecision<N> nu, MultiPrecision<N> z) {
            Consts.BesselLimitCoef table = Consts.Bessel.LimitCoef(nu);

            MultiPrecision<Plus4<N>> z_ex = z.Convert<Plus4<N>>();
            MultiPrecision<Plus4<N>> v = 1 / z_ex;

            MultiPrecision<Plus4<N>> x = 0, p = 1;

            for (int k = 0; k <= Consts.BesselIK.LimitApproxTerms; k++, p *= v) {
                MultiPrecision<Plus4<N>> c = p * table.Value(k);

                x += c;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    break;
                }
            }

            MultiPrecision<Plus1<N>> z_ex1 = z.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> r =
                MultiPrecision<Plus1<N>>.Exp(-z_ex1) * MultiPrecision<Plus1<N>>.Sqrt(MultiPrecision<Plus1<N>>.PI / (2 * z_ex1));

            MultiPrecision<Plus1<N>> y = r * x.Convert<Plus1<N>>();

            return y.Convert<N>();
        }

        internal static MultiPrecision<N> BesselJYApproxThreshold => Consts.BesselJY.ApproxThreshold;

        internal static MultiPrecision<N> BesselIKApproxThreshold => Consts.BesselIK.ApproxThreshold;

        private static partial class Consts {
            public static class BesselJY {
                public static MultiPrecision<N> ApproxThreshold { private set; get; }

                public static int LimitApproxTerms { private set; get; }

                static BesselJY() {
                    if (Length > 65) {
                        throw new ArgumentOutOfRangeException(
                            "In the bessel function, the calculation is invalid for precision greater than 65 in length.",
                            nameof(Length)
                        );
                    }

                    ApproxThreshold = Math.Ceiling(50 + 11.0965 * Length);

                    LimitApproxTerms = (int)Math.Ceiling(41 + 11.0190 * Length);

#if DEBUG
                    Trace.WriteLine($"BesselJY<{Length}> initialized.");
#endif
                }
            }

            public static class BesselIK {
                public static MultiPrecision<N> ApproxThreshold { private set; get; }

                public static int LimitApproxTerms { private set; get; }

                static BesselIK() {
                    if (Length > 65) {
                        throw new ArgumentOutOfRangeException(
                            "In the bessel function, the calculation is invalid for precision greater than 65 in length.",
                            nameof(Length)
                        );
                    }

                    ApproxThreshold = Math.Ceiling(45 + 11.0965 * Length);

                    LimitApproxTerms = (int)Math.Ceiling(80 + 21.2180 * Length);

#if DEBUG
                    Trace.WriteLine($"BesselIK<{Length}> initialized.");
#endif
                }
            }

            public static class Bessel {
                private readonly static Dictionary<MultiPrecision<N>, BesselNearZeroCoef> nearzero_table = new();
                private readonly static Dictionary<MultiPrecision<N>, BesselLimitCoef> limit_table = new();

                private readonly static Dictionary<int, BesselIntegerFiniteTermCoef> integer_finite_table = new();
                private readonly static Dictionary<int, BesselIntegerConvergenceTermCoef> interger_convergence_table = new();

                private readonly static Dictionary<MultiPrecision<N>, (MultiPrecision<N> sin, MultiPrecision<N> cos)> sincos_table = new();

                public static BesselNearZeroCoef NearZeroCoef(MultiPrecision<N> nu) {
                    BesselNearZeroCoef table;
                    if (nearzero_table.ContainsKey(nu)) {
                        table = nearzero_table[nu];
                    }
                    else {
                        table = new BesselNearZeroCoef(nu);
                        nearzero_table.Add(nu, table);
                    }

                    return table;
                }

                public static BesselLimitCoef LimitCoef(MultiPrecision<N> nu) {
                    BesselLimitCoef table;
                    if (limit_table.ContainsKey(nu)) {
                        table = limit_table[nu];
                    }
                    else {
                        table = new BesselLimitCoef(nu);
                        limit_table.Add(nu, table);
                    }

                    return table;
                }

                public static BesselIntegerFiniteTermCoef IntegerFiniteTermCoef(int n) {
                    BesselIntegerFiniteTermCoef table;
                    if (integer_finite_table.ContainsKey(n)) {
                        table = integer_finite_table[n];
                    }
                    else {
                        table = new BesselIntegerFiniteTermCoef(n);
                        integer_finite_table.Add(n, table);
                    }

                    return table;
                }

                public static BesselIntegerConvergenceTermCoef IntegerConvergenceTermCoef(int n) {
                    BesselIntegerConvergenceTermCoef table;
                    if (interger_convergence_table.ContainsKey(n)) {
                        table = interger_convergence_table[n];
                    }
                    else {
                        table = new BesselIntegerConvergenceTermCoef(n);
                        interger_convergence_table.Add(n, table);
                    }

                    return table;
                }

                public static (MultiPrecision<N> sin, MultiPrecision<N> cos) SinCos(MultiPrecision<N> nu) {
                    if (!sincos_table.ContainsKey(nu)) {
                        sincos_table.Add(nu, (SinPI(nu), CosPI(nu)));
                    }

                    return sincos_table[nu];
                }
            }

            public class BesselNearZeroCoef {
                private readonly MultiPrecision<Double<N>> nu;
                private readonly List<MultiPrecision<Double<N>>> a_table = new();
                private readonly List<MultiPrecision<Double<N>>> c_table = new();

                public BesselNearZeroCoef(MultiPrecision<N> nu) {
                    this.nu = nu.Convert<Double<N>>();

                    MultiPrecision<Double<N>> a0;

                    if (nu >= 0 && nu == Truncate(nu)) {
                        long n = (long)nu;

                        a0 = 1;
                        for (int k = 2; k <= n; k++) {
                            a0 *= k;
                        }
                    }
                    else {
                        a0 = MultiPrecision<Double<N>>.Length < 256 ?
                             MultiPrecision<Double<N>>.Gamma(nu.Convert<Double<N>>() + 1) :
                             MultiPrecision<Pow2.N256>.Gamma(nu.Convert<Pow2.N256>() + 1).Convert<Double<N>>();
                    }

                    this.a_table.Add(a0);
                    this.c_table.Add(1 / a0);
                }

                public MultiPrecision<Double<N>> Value(int n) {
                    if (n < 0) {
                        throw new ArgumentOutOfRangeException(nameof(n));
                    }

                    if (n < c_table.Count) {
                        return c_table[n];
                    }

                    for (long k = c_table.Count; k <= n; k++) {
                        MultiPrecision<Double<N>> a = a_table.Last() * (checked(4 * k) * (nu + k));

                        a_table.Add(a);
                        c_table.Add(1 / a);
                    }

                    return c_table[n];
                }
            }

            public class BesselLimitCoef {
                private readonly MultiPrecision<Plus4<N>> squa_nu4;
                private readonly List<MultiPrecision<Plus4<N>>> a_table = new();

                public BesselLimitCoef(MultiPrecision<N> nu) {
                    this.squa_nu4 = 4 * MultiPrecision<Plus4<N>>.Square(nu.Convert<Plus4<N>>());

                    MultiPrecision<Plus4<N>> a1 = (squa_nu4 - 1) / 8;

                    this.a_table.Add(1);
                    this.a_table.Add(a1);
                }

                public MultiPrecision<Plus4<N>> Value(int n) {
                    if (n < 0) {
                        throw new ArgumentOutOfRangeException(nameof(n));
                    }

                    if (n < a_table.Count) {
                        return a_table[n];
                    }

                    for (long k = a_table.Count; k <= n; k++) {
                        MultiPrecision<Plus4<N>> a =
                            a_table.Last() * MultiPrecision<Plus4<N>>.Div(squa_nu4 - checked((2 * k - 1) * (2 * k - 1)), checked(k * 8));

                        a_table.Add(a);
                    }

                    return a_table[n];
                }
            }

            public class BesselIntegerFiniteTermCoef {
                private readonly List<MultiPrecision<Double<N>>> a_table = new();

                public BesselIntegerFiniteTermCoef(int n) {
                    if (n < 0) {
                        throw new ArgumentOutOfRangeException(nameof(n));
                    }

                    MultiPrecision<Double<N>> a = 1;
                    for (int i = 2; i < n; i++) {
                        a *= i;
                    }

                    this.a_table.Add(a);

                    for (long k = 1; k < n; k++) {
                        a /= checked(4 * k * (n - k));
                        this.a_table.Add(a);
                    }
                }

                public MultiPrecision<Double<N>> Value(int k) {
                    if (k < 0 || k >= a_table.Count) {
                        throw new ArgumentOutOfRangeException(nameof(k));
                    }

                    return a_table[k];
                }
            }

            public class BesselIntegerConvergenceTermCoef {
                private static readonly MultiPrecision<Double<N>> b;

                private readonly int n;
                private readonly List<MultiPrecision<Double<N>>> a_table = new();
                private MultiPrecision<Double<N>> r;

                static BesselIntegerConvergenceTermCoef() {
                    b = 2 * MultiPrecision<Double<N>>.EulerGamma;
                }

                public BesselIntegerConvergenceTermCoef(int n) {
                    if (n < 0) {
                        throw new ArgumentOutOfRangeException(nameof(n));
                    }

                    MultiPrecision<Double<N>> r0 = 1;
                    for (int i = 2; i <= n; i++) {
                        r0 *= i;
                    }

                    MultiPrecision<Double<N>> a0 = (MultiPrecision<Double<N>>.HarmonicNumber(n) - b) / r0;

                    this.n = n;
                    this.r = r0;
                    this.a_table.Add(a0);
                }

                public MultiPrecision<Double<N>> Value(int k) {
                    if (k < 0) {
                        throw new ArgumentOutOfRangeException(nameof(k));
                    }

                    if (k < a_table.Count) {
                        return a_table[k];
                    }

                    for (long i = a_table.Count; i <= k; i++) {
                        r *= checked(4 * i * (n + i));

                        MultiPrecision<Double<N>> a =
                            (MultiPrecision<Double<N>>.HarmonicNumber(checked((int)i))
                            + MultiPrecision<Double<N>>.HarmonicNumber(checked((int)(n + i))) - b) / r;

                        a_table.Add(a);
                    }

                    return a_table[k];
                }
            }
        }
    }
}
