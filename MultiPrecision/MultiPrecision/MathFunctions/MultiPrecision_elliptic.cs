using System.Collections.Generic;
using System.Diagnostics;
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

            MultiPrecision<N> y = MultiPrecision<Plus1<N>>.EllipticKCore(k.Convert<Plus1<N>>()).Convert<N>();

            return y;
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

            MultiPrecision<N> y = MultiPrecision<Plus1<N>>.EllipticECore(k.Convert<Plus1<N>>()).Convert<N>();

            return y;
        }

        private static MultiPrecision<N> EllipticKCore(MultiPrecision<N> k) {
            MultiPrecision<N> squa_k = k * k;

            if (squa_k.Exponent > -32) {
                MultiPrecision<N> c = Sqrt(1 - squa_k), cp1 = 1 + c, cm1 = 1 - c;

                return 2 / cp1 * EllipticKCore(cm1 / cp1);
            }

            MultiPrecision<N> x = 1, w = squa_k;

            for (int i = 1; i < int.MaxValue; i++) {
                MultiPrecision<N> c = Consts.Elliptic.KTable(i) * w;

                x += c;

                if (c.IsZero || x.Exponent - c.Exponent > Bits) {
                    break;
                }

                w *= squa_k;
            }

            MultiPrecision<N> y = x * PI / 2;

            return y;
        }

        private static MultiPrecision<N> EllipticECore(MultiPrecision<N> k) {
            MultiPrecision<N> squa_k = k * k;

            if (squa_k.Exponent > -32) {
                MultiPrecision<N> c = Sqrt(1 - squa_k), cp1 = 1 + c, cm1 = 1 - c, r = cm1 / cp1;

                return cp1 * EllipticECore(r) - 2 * c / cp1 * EllipticKCore(r);
            }

            MultiPrecision<N> x = 1, w = squa_k;

            for (int i = 1; i < int.MaxValue; i++) {
                MultiPrecision<N> c = Consts.Elliptic.ETable(i) * w;

                x += c;

                if (c.IsZero || x.Exponent - c.Exponent > Bits) {
                    break;
                }

                w *= squa_k;
            }

            MultiPrecision<N> y = x * PI / 2;

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
