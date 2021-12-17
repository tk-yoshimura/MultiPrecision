using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MultiPrecisionSandbox {
    public static class BesselYoshidaCoef<N> where N : struct, IConstant {
        public static (MultiPrecision<N>[] cs, MultiPrecision<N>[] ds) Table(MultiPrecision<N> nu, int m) {
            BesselLimitCoef<Plus16<N>> limitcoef = new(nu.Convert<Plus16<N>>());

            MultiPrecision<N>[] cs = new MultiPrecision<N>[m + 1], ds = new MultiPrecision<N>[m + 1];

            for (int j = 0; j <= m; j++) {
                ds[j] = ShiftedLegendre.Table(m, m - j) / ((m - j + 1) * limitcoef.Value(m - j + 1)).Convert<N>();

                MultiPrecision<Plus16<N>> sum = 0;

                for (int p = m - j; p <= m; p++) {
                    sum += ShiftedLegendre.Table(m, p) * limitcoef.Value(p - m + j) / ((p + 1) * limitcoef.Value(p + 1));
                }

                cs[j] = sum.Convert<N>();
            }

            return (cs, ds);
        }

        public static MultiPrecision<N>[][] Table(int m) {
            MultiPrecision<N>[][] dss = new MultiPrecision<N>[m + 1][];

            BigInteger[] fs = new BigInteger[m + 2];
            BigInteger[][] ps = new BigInteger[m + 1][], qs = new BigInteger[m + 1][];

            (fs[0], fs[1]) = (1, 1);
            (ps[0], ps[1]) = (new BigInteger[1] { 1 }, new BigInteger[2] { -1, 1 });
            (qs[0], qs[1]) = (new BigInteger[1] { 1 }, new BigInteger[2] { -(2 * m + 1) * (2 * m + 1), 1 });

            for (int k = 2; k <= m; k++) {
                (ps[k], qs[k]) = (new BigInteger[k + 1], new BigInteger[k + 1]);

                int sq2km1 = (2 * k - 1) * (2 * k - 1), sq2mkp3 = (2 * (m - k) + 3) * (2 * (m - k) + 3); 

                ps[k][0] = -sq2km1 * ps[k - 1][0];
                ps[k][k] = 1;

                qs[k][0] = -sq2mkp3 * qs[k - 1][0];
                qs[k][k] = 1;

                for (int l = 1; l < k; l++) {
                    ps[k][l] = ps[k - 1][l - 1] - sq2km1 * ps[k - 1][l];
                    qs[k][l] = qs[k - 1][l - 1] - sq2mkp3 * qs[k - 1][l];
                }

                fs[k] = k * fs[k - 1];
            }

            fs[m + 1] = (m + 1) * fs[m];

            for (int i = 0; i <= m; i++) {
                dss[i] = new MultiPrecision<N>[i + 1];

                for (int j = 0; j <= i; j++) {
                    MultiPrecision<Plus16<N>> b = 0;

                    for (int l = 0; l <= j; l++) { 
                        for (int k = j - l; k <= i - l; k++) {
                            b += (MultiPrecision<Plus16<N>>)(ShiftedLegendre.Table(m, m - k) * ps[i - k][l] * qs[k][j - l] * fs[m - k]) / (fs[i - k] * fs[m + 1]);
                        }
                    }

                    dss[i][j] = MultiPrecision<Plus16<N>>.Ldexp(b, 2 * j - 3 * i).Convert<N>();
                }
            }

            return dss;
        }

        public static (MultiPrecision<N>[] cs, MultiPrecision<N>[] ds) Table(MultiPrecision<N> nu, MultiPrecision<N>[][] dss) {
            int m = dss.Length - 1;

            MultiPrecision<N> squa_nu = nu * nu;
            MultiPrecision<N>[] cs = new MultiPrecision<N>[m + 1], ds = new MultiPrecision<N>[m + 1];

            for (int i = 0; i <= m; i++) {
                MultiPrecision<N> d = dss[i][i], c = 0;

                for (int l = 0; l < i; l++) {
                    d *= MultiPrecision<N>.Square(m - l + MultiPrecision<N>.Point5) - squa_nu;
                }

                MultiPrecision<N> u = 1;
                for (int j = 0; j <= i; j++) {
                    c += dss[i][j] * u;
                    u *= squa_nu;
                }

                cs[i] = c;
                ds[i] = d;
            }

            return (cs, ds);
        }

        public static MultiPrecision<N> Value(MultiPrecision<N> z, MultiPrecision<N>[] cs, MultiPrecision<N>[] ds) { 
            MultiPrecision<N> t = 1 / z, tn = 1;
            MultiPrecision<N> c = 0, d = 0;

            for (int j = 0; j < cs.Length; j++) {
                c += cs[j] * tn;
                d += ds[j] * tn;
                tn *= t;
            }

            MultiPrecision<N> y = MultiPrecision<N>.Sqrt(t * MultiPrecision<N>.PI / 2) * c / d;
            y *= MultiPrecision<N>.Exp(-z);

            return y;
        }
    }
}
