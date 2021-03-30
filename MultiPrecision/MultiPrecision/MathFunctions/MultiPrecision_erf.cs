using MultiPrecision.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Erf(MultiPrecision<N> x) {
            if (x.IsZero) {
                return 0;
            }
            if (x.IsNaN) {
                return NaN;
            }
            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? 1 : -1;
            }
            if (x.Sign == Sign.Minus) {
                return -Erf(Abs(x));
            }

            if (x.Exponent < Consts.Erf.ExponentThreshold) {
                MultiPrecision<Plus4<N>> y = ErfTaylorSeriesApprox(x);

                return y.Convert<N>();
            }
            else {
                double xd = (double)x;
                double erfc_exponent_estimate = 1.4 * xd * xd;

                int needs_bits = Bits - (int)Math.Min(Bits, erfc_exponent_estimate);
                if (needs_bits < 1) {
                    return 1;
                }

                long n = ErfcConvergenceTable.N(needs_bits, xd);
                MultiPrecision<Plus4<N>> y = ErfcContinueFractionalApprox(x, n);

                return (1 - y).Convert<N>();
            }
        }

        public static MultiPrecision<N> Erfc(MultiPrecision<N> x) {
            if (x.IsZero) {
                return 1;
            }
            if (x.IsNaN) {
                return NaN;
            }
            if (!x.IsFinite) {
                return x.Sign == Sign.Plus ? 0 : 2;
            }
            if (x.Sign == Sign.Minus) {
                return 1 + Erf(Abs(x));
            }

            if (x.Exponent < Consts.Erf.ExponentThreshold) {
                MultiPrecision<Plus4<Plus4<N>>> y =
                    MultiPrecision<Plus4<N>>.ErfTaylorSeriesApprox(x.Convert<Plus4<N>>());

                return (1 - y).Convert<N>();
            }
            else {
                long n = ErfcConvergenceTable.N(Bits, (double)x);
                MultiPrecision<Plus4<N>> y = ErfcContinueFractionalApprox(x, n);

                return y.Convert<N>();
            }
        }

        private static MultiPrecision<Plus4<N>> ErfTaylorSeriesApprox(MultiPrecision<N> z) {
            MultiPrecision<Plus4<N>> z_ex = z.Convert<Plus4<N>>();
            MultiPrecision<Plus4<N>> w = z_ex * z_ex, v = w;
            MultiPrecision<Plus4<N>> y = 1;

            foreach (MultiPrecision<Plus4<N>> t in Consts.Erf.ErfDenomTable) {
                MultiPrecision<Plus4<N>> dy = t * v;
                y += dy;

                if (dy.IsZero || y.Exponent - dy.Exponent > MultiPrecision<Plus4<N>>.Bits) {
                    break;
                }

                v *= w;
            }

            y *= 2 * z_ex * Consts.Erf.C;

            return y;
        }

        private static MultiPrecision<Plus4<N>> ErfcContinueFractionalApprox(MultiPrecision<N> z, long n) {
            MultiPrecision<Plus4<N>> z_ex = z.Convert<Plus4<N>>();
            MultiPrecision<Plus4<N>> w = z_ex * z_ex;

            MultiPrecision<Plus4<N>> f =
                (MultiPrecision<Plus4<N>>.Sqrt(25 + w * (440 + w * (488 + w * 16 * (10 + w))))
                 - 5 + w * 4 * (1 + w))
                / (20 + w * 8);

            for (long k = checked(4 * n - 3); k >= 1; k -= 4) {
                MultiPrecision<Plus4<N>> c0 = (k + 2) * f;
                MultiPrecision<Plus4<N>> c1 = w * ((k + 3) + 2 * f);
                MultiPrecision<Plus4<N>> d0 = checked((k + 1) * (k + 3)) + (4 * k + 6) * f;
                MultiPrecision<Plus4<N>> d1 = 2 * c1;

                f = w + k * (c0 + c1) / (d0 + d1);
            }

            MultiPrecision<Plus4<N>> y = z_ex * MultiPrecision<Plus4<N>>.Exp(-w) * Consts.Erf.C / f;

            return y;
        }

        private static partial class Consts {
            public static class Erf {
                public static MultiPrecision<Plus4<N>> C { private set; get; } = null;

                public static int ExponentThreshold { private set; get; } = 3;
                public static ReadOnlyCollection<MultiPrecision<Plus4<N>>> ErfDenomTable { private set; get; } = null;

                static Erf() {
                    if (Length > 260) {
                        throw new ArgumentOutOfRangeException(nameof(Length));
                    }

                    C = 1 / MultiPrecision<Plus4<N>>.Sqrt(MultiPrecision<Plus4<N>>.PI);

                    List<MultiPrecision<Plus4<N>>> table = new();
                    MultiPrecision<Plus4<N>> coef = 1;

                    int n = 1;
                    while ((n < int.MaxValue) && (2 * n * ExponentThreshold + coef.Exponent >= -MultiPrecision<Plus4<N>>.Bits)) {
                        coef = 1 / ErfTaylorSeries.Coef<Plus4<N>>(n);
                        table.Add(coef);

                        n++;
                    }

                    ErfDenomTable = table.AsReadOnly();

#if DEBUG
                    Trace.WriteLine($"Erf<{Length}> initialized.");
#endif
                }
            }
        }
    }

    static class ErfTaylorSeries {
        private static readonly List<BigInteger> factorials = new() {
            1
        };

        private static readonly List<BigInteger> coefs = new() {
            1
        };

        public static MultiPrecision<N> Coef<N>(int n) where N : struct, IConstant {
            if (n < 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            if (n < factorials.Count) {
                return coefs[n];
            }

            for (int k = factorials.Count; k <= n; k++) {
                BigInteger fact = factorials[k - 1] * k;
                BigInteger coef = fact * checked(2 * k + 1);

                if (k % 2 == 1) {
                    coef = -coef;
                }

                factorials.Add(fact);
                coefs.Add(coef);
            }

            return coefs[n];
        }
    }

    static class ErfcConvergenceTable {
        private static readonly int[] bits;
        private static readonly double[] zs;
        private static readonly int[,] ns;
        private static readonly (int min, int max) bit_range;
        private static readonly (double min, double max) z_range;

        static ErfcConvergenceTable() {
            (int[] bits, List<double> zs, List<int[]> ns) = ReadTable();

            ErfcConvergenceTable.bits = bits;
            ErfcConvergenceTable.zs = zs.ToArray();
            ErfcConvergenceTable.ns = new int[zs.Count, bits.Length];
            ErfcConvergenceTable.z_range = (zs[0], zs[^1]);
            ErfcConvergenceTable.bit_range = (bits[0], bits[^1]);

            for (int j = 0; j < zs.Count; j++) {
                int[] n = ns[j];

                for (int i = 0; i < bits.Length; i++) {
                    ErfcConvergenceTable.ns[j, i] = n[i];
                }
            }

#if DEBUG
            Trace.WriteLine($"Erfc initialized.");
#endif
        }

        private static (int[] bits, List<double> zs, List<int[]> ns) ReadTable() {
            int[] bits;
            List<double> zs = new();
            List<int[]> ns = new();

            string[] lines = Resources.erfc_convergence_table.Split("\r\n");

            string[] header = lines[0].Split(',');

            if (header[0] != "convergence_n(z-bits)") {
                throw new FormatException();
            }

            bits = header.Skip(1).Select((str) => int.Parse(str)).ToArray();

            for (int i = 1; i < bits.Length; i++) {
                if (bits[i - 1] >= bits[i]) {
                    throw new FormatException();
                }
            }

            foreach (string line in lines[1..]) {
                if (string.IsNullOrWhiteSpace(line)) {
                    break;
                }

                string[] row = line.Split(',');

                if (row.Length != bits.Length + 1) {
                    throw new FormatException();
                }

                double z = double.Parse(row[0]);
                int[] n = row.Skip(1).Select((str) => int.Parse(str)).ToArray();

                if (zs.Count > 0 && zs.Last() >= z) {
                    throw new FormatException();
                }

                zs.Add(z);
                ns.Add(n);
            }

            return (bits, zs, ns);
        }

        public static long N(int bit, double z) {
            int bit_index = bits.Length - 2, z_index = zs.Length - 2;

            if (bit < bit_range.min) {
                bit_index = 0;
                bit = bit_range.min;
            }
            else {
                for (int i = 1; i < bits.Length; i++) {
                    if (bit < bits[i]) {
                        bit_index = i - 1;
                        break;
                    }
                }
            }

            if (z < z_range.min) {
                throw new ArgumentOutOfRangeException(nameof(z));
            }
            else if (z > z_range.max) {
                z = z_range.max;
            }
            else {
                for (int i = 1; i < zs.Length; i++) {
                    if (z < zs[i]) {
                        z_index = i - 1;
                        break;
                    }
                }
            }

            double b = Math.Log(bit);
            double b0 = Math.Log(bits[bit_index]), b1 = Math.Log(bits[bit_index + 1]);
            double z0 = zs[z_index], z1 = zs[z_index + 1];
            double n00 = Math.Log(ns[z_index, bit_index]), n01 = Math.Log(ns[z_index, bit_index + 1]);
            double n10 = Math.Log(ns[z_index + 1, bit_index]), n11 = Math.Log(ns[z_index + 1, bit_index + 1]);

            double u = (b - b0) / (b1 - b0), v = (z - z0) / (z1 - z0);
            double n_log = n00 + (n01 - n00) * u + (n10 - n00) * v + (n11 - n10 - n01 + n00) * u * v;

            long n = Math.Max(4, checked((long)Math.Ceiling(Math.Exp(n_log))));

            return n;
        }
    }
}
