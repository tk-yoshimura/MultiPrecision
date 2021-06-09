using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> EllipticK(MultiPrecision<N> k) {
            if (!k.IsFinite || k.Sign == Sign.Minus || k > One) {
                return NaN;
            }

            if (k.IsZero) {
                return PI / 2;
            }

            if (k == One) {
                return PositiveInfinity;
            }

            if ((1 - k).Exponent >= -32) {
                MultiPrecision<N> y = MultiPrecision<Plus1<N>>.EllipticKCore(k.Convert<Plus1<N>>()).Convert<N>();
                return y;
            }
            else {
                MultiPrecision<N> y = MultiPrecision<Double<N>>.EllipticKCore(k.Convert<Double<N>>()).Convert<N>();
                return y;
            }
        }

        public static MultiPrecision<N> EllipticE(MultiPrecision<N> k) {
            if (!k.IsFinite || k.Sign == Sign.Minus || k > One) {
                return NaN;
            }

            if (k.IsZero) {
                return PI / 2;
            }

            if (k == One) {
                return One;
            }

            if ((1 - k).Exponent >= -32) {
                MultiPrecision<N> y = MultiPrecision<Plus1<N>>.EllipticECore(
                    k.Convert<Plus1<N>>(),
                    new Dictionary<MultiPrecision<Plus1<N>>, MultiPrecision<Plus1<N>>>()).Convert<N>();

                return Max(One, y);
            }
            else {
                MultiPrecision<N> y = MultiPrecision<Double<N>>.EllipticECore(
                    k.Convert<Double<N>>(),
                    new Dictionary<MultiPrecision<Double<N>>, MultiPrecision<Double<N>>>()).Convert<N>();

                return Max(One, y);
            }
        }

        public static MultiPrecision<N> EllipticPi(MultiPrecision<N> n, MultiPrecision<N> k) {
            if (!k.IsFinite || k.Sign == Sign.Minus || k > One || n > One) {
                return NaN;
            }

            if (n.IsZero) {
                return EllipticK(k);
            }

            if (n == One) {
                return PositiveInfinity;
            }

            if (k.IsZero) {
                return PI / (2 * Sqrt(1 - n));
            }

            if (k == One) {
                return PositiveInfinity;
            }

            MultiPrecision<N> y = MultiPrecision<Plus1<N>>.EllipticPiCore(n.Convert<Plus1<N>>(), k.Convert<Plus1<N>>()).Convert<N>();

            return y;
        }

        private static MultiPrecision<N> EllipticKCore(MultiPrecision<N> k,
            [AllowNull] Dictionary<MultiPrecision<N>, MultiPrecision<N>> kvalue_cache = null) {

            if (kvalue_cache is not null && kvalue_cache.ContainsKey(k)) {
                return kvalue_cache[k];
            }

            MultiPrecision<N> squa_k = k * k;
            MultiPrecision<N> y;

            if (squa_k.Exponent > -32) {
                MultiPrecision<N> c = Sqrt(1 - squa_k), cp1 = 1 + c, cm1 = 1 - c;

                y = 2 / cp1 * EllipticKCore(cm1 / cp1, kvalue_cache);
            }
            else {
                MultiPrecision<N> x = 1, w = squa_k;

                for (int i = 1; i < int.MaxValue; i++) {
                    MultiPrecision<N> c = Consts.Elliptic.KTable(i) * w;

                    x += c;

                    if (c.IsZero || x.Exponent - c.Exponent > Bits) {
                        break;
                    }

                    w *= squa_k;
                }

                y = x * PI / 2;
            }

            if (kvalue_cache is not null) {
                kvalue_cache.Add(k, y);
            }

            return y;
        }

        private static MultiPrecision<N> EllipticECore(MultiPrecision<N> k,
            [AllowNull] Dictionary<MultiPrecision<N>, MultiPrecision<N>> kvalue_cache = null) {

            MultiPrecision<N> squa_k = k * k;
            MultiPrecision<N> y;

            if (squa_k.Exponent > -32) {
                MultiPrecision<N> c = Sqrt(1 - squa_k), cp1 = 1 + c, cm1 = 1 - c, r = cm1 / cp1;

                MultiPrecision<N> e = 2 * c / cp1 * EllipticKCore(r, kvalue_cache);

                y = cp1 * EllipticECore(r, kvalue_cache) - e;
            }
            else {
                MultiPrecision<N> x = 1, w = squa_k;

                for (int i = 1; i < int.MaxValue; i++) {
                    MultiPrecision<N> c = Consts.Elliptic.ETable(i) * w;

                    x += c;

                    if (c.IsZero || x.Exponent - c.Exponent > Bits) {
                        break;
                    }

                    w *= squa_k;
                }

                y = x * PI / 2;
            }

            return y;
        }

        private static MultiPrecision<N> EllipticPiCore(MultiPrecision<N> n, MultiPrecision<N> k) {
            MultiPrecision<N> a = 1;
            MultiPrecision<N> b = Sqrt(1 - k * k);
            MultiPrecision<N> p = Sqrt(1 - n);
            MultiPrecision<N> q = 1;
            MultiPrecision<N> sum_q = 1;

            while (!q.IsZero && sum_q.Exponent - q.Exponent < Bits) {
                MultiPrecision<N> ab = a * b, p_squa = p * p;
                MultiPrecision<N> p_squa_pab = p_squa + ab, p_squa_mab = p_squa - ab;

                MultiPrecision<N> a_next = (a + b) / 2;
                MultiPrecision<N> b_next = Sqrt(ab);
                MultiPrecision<N> p_next = p_squa_pab / (2 * p);
                MultiPrecision<N> q_next = q / 2 * p_squa_mab / p_squa_pab;

                a = a_next;
                b = b_next;
                p = p_next;
                q = q_next;

                sum_q += q;
            }

            MultiPrecision<N> y = (2 + sum_q * n / (1 - n)) * EllipticK(k) / 2;

            return y;
        }

        private static partial class Consts {
            public static class Elliptic {
                private static readonly List<MultiPrecision<N>> k_table, e_table;

                static Elliptic() {
                    k_table = new() { 1 };
                    e_table = new() { 1 };

#if DEBUG
                    Trace.WriteLine($"Elliptic<{Length}> initialized.");
#endif
                }

                public static MultiPrecision<N> KTable(int n) {
                    for (int i = k_table.Count; i <= n; i++) {
                        MultiPrecision<N> k = k_table.Last() * checked(4 * i * (i - 1) + 1) / checked(4 * i * i);

                        k_table.Add(k);
                    }

                    return k_table[n];
                }

                public static MultiPrecision<N> ETable(int n) {
                    for (int i = e_table.Count; i <= n; i++) {
                        MultiPrecision<N> e = KTable(i) * checked(1 - 2 * i);

                        e_table.Add(e);
                    }

                    return e_table[n];
                }
            }
        }
    }
}
