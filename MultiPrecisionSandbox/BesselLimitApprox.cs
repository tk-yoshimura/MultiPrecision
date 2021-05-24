using MultiPrecision;
using System.Collections.Generic;

namespace MultiPrecisionSandbox {
    public static class BesselLimitApprox<N> where N : struct, IConstant {
        readonly static Dictionary<MultiPrecision<N>, BesselLimitCoef<Plus1<N>>> coef_table = new();

        public static int BesselTermConvergence(MultiPrecision<N> nu, MultiPrecision<N> z) {
            BesselLimitCoef<Plus1<N>> table;
            if (coef_table.ContainsKey(nu)) {
                table = coef_table[nu];
            }
            else {
                table = new BesselLimitCoef<Plus1<N>>(nu.Convert<Plus1<N>>());
                coef_table.Add(nu, table);
            }

            MultiPrecision<Plus1<N>> v = 1 / z.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> w = v * v;

            MultiPrecision<Plus1<N>> x = 0, y = 0, p = 1, q = v;
            MultiPrecision<Plus1<N>> prev_c = null, prev_s = null;

            Sign sign = Sign.Plus;

            for (int k = 0; k <= int.MaxValue; k++) {
                MultiPrecision<Plus1<N>> c = p * table.Value(k * 2);
                MultiPrecision<Plus1<N>> s = q * table.Value(k * 2 + 1);

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

                p *= w;
                q *= w;

                if (prev_c is not null && prev_c.Exponent < c.Exponent) {
                    return int.MaxValue;
                }
                if (prev_s is not null && prev_s.Exponent < s.Exponent) {
                    return int.MaxValue;
                }

                prev_c = c;
                prev_s = s;

                if (!c.IsZero && x.Exponent - c.Exponent <= MultiPrecision<Plus1<N>>.Bits) {
                    continue;
                }
                if (!s.IsZero && y.Exponent - s.Exponent <= MultiPrecision<Plus1<N>>.Bits) {
                    continue;
                }

                return k;
            }

            return int.MaxValue;
        }

        public static MultiPrecision<N> Value(MultiPrecision<N> nu, MultiPrecision<N> z, int terms) {
            BesselLimitCoef<Plus1<N>> table;
            if (coef_table.ContainsKey(nu)) {
                table = coef_table[nu];
            }
            else {
                table = new BesselLimitCoef<Plus1<N>>(nu.Convert<Plus1<N>>());
                coef_table.Add(nu, table);
            }

            MultiPrecision<Plus1<N>> v = 1 / z.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> w = v * v;

            MultiPrecision<Plus1<N>> x = 0, y = 0, p = 1, q = v;

            Sign sign = Sign.Plus;

            for (int k = 0; k <= terms; k++) {
                MultiPrecision<Plus1<N>> c = p * table.Value(k * 2);
                MultiPrecision<Plus1<N>> s = q * table.Value(k * 2 + 1);

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

                p *= w;
                q *= w;

                if (!c.IsZero && x.Exponent - c.Exponent <= MultiPrecision<Plus1<N>>.Bits) {
                    continue;
                }
                if (!s.IsZero && y.Exponent - s.Exponent <= MultiPrecision<Plus1<N>>.Bits) {
                    continue;
                }

                if (k == terms) {
                    return MultiPrecision<N>.NaN;
                }

                break;
            }

            MultiPrecision<Plus1<N>> omega = z.Convert<Plus1<N>>() - (2 * nu.Convert<Plus1<N>>() + 1) * MultiPrecision<Plus1<N>>.PI / 4;
            MultiPrecision<Plus1<N>> m = x * MultiPrecision<Plus1<N>>.Cos(omega) - y * MultiPrecision<Plus1<N>>.Sin(omega);
            MultiPrecision<Plus1<N>> t = m * MultiPrecision<Plus1<N>>.Sqrt(2 / (MultiPrecision<Plus1<N>>.PI * z.Convert<Plus1<N>>()));

            return t.Convert<N>();
        }
    }
}
