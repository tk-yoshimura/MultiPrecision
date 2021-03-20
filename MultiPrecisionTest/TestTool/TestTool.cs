using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionTest {
    public static class TestTool {

        public static IEnumerable<MultiPrecision<N>> EnumerateNeighbor<N>(MultiPrecision<N> v, int n = 10) where N : struct, IConstant {
            List<MultiPrecision<N>> vs = new List<MultiPrecision<N>>();

            MultiPrecision<N> u = v;
            for (int i = 0; i < n; i++) {
                u = MultiPrecision<N>.BitDecrement(u);
                vs.Add(u);
            }

            vs.Reverse();

            vs.Add(v);

            u = v;
            for (int i = 0; i < n; i++) {
                u = MultiPrecision<N>.BitIncrement(u);
                vs.Add(u);
            }

            return vs;
        }

        public static IEnumerable<MultiPrecision<N>> AllRangeSet<N>() where N : struct, IConstant {
            yield return MultiPrecision<N>.Zero;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                yield return i;
                yield return -i;
            }

            MultiPrecision<N> p = 1;
            for (int i = 0; i < 32; i++) {
                yield return p;
                yield return -p;

                p *= 2;
            }

            MultiPrecision<N> n = 1;
            for (int i = 0; i < 32; i++) {
                yield return n;
                yield return -n;

                n /= 2;
            }

            MultiPrecision<N> p2 = 255;
            for (int i = 0; i < 32; i++) {
                yield return p2;
                yield return -p2;

                p2 *= 2;
            }

            MultiPrecision<N> n2 = 255;
            for (int i = 0; i < 32; i++) {
                yield return n2;
                yield return -n2;

                n2 /= 2;
            }

            MultiPrecision<N> p3 = 3;
            for (int i = 0; i < 32; i++) {
                yield return p3 / 7;
                yield return -p3 / 7;

                p3 *= 4;
            }

            MultiPrecision<N> n3 = 3;
            for (int i = 0; i < 32; i++) {
                yield return n3 / 7;
                yield return -n3 / 7;

                n3 /= 4;
            }

            MultiPrecision<N> p4 = 4;
            for (int i = 0; i < 32; i++) {
                yield return p4 / 5;
                yield return -p4 / 5;

                p4 *= 4;
            }

            MultiPrecision<N> n4 = 4;
            for (int i = 0; i < 32; i++) {
                yield return n4 / 5;
                yield return -n4 / 5;

                n4 /= 4;
            }
        }

        public static IEnumerable<MultiPrecision<N>> PositiveRangeSet<N>() where N : struct, IConstant {
            yield return MultiPrecision<N>.Zero;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                yield return i;
            }

            MultiPrecision<N> p = 1;
            for (int i = 0; i < 32; i++) {
                yield return p;

                p *= 2;
            }

            MultiPrecision<N> n = 1;
            for (int i = 0; i < 32; i++) {
                yield return n;

                n /= 2;
            }

            MultiPrecision<N> p2 = 255;
            for (int i = 0; i < 32; i++) {
                yield return p2;

                p2 *= 2;
            }

            MultiPrecision<N> n2 = 255;
            for (int i = 0; i < 32; i++) {
                yield return n2;

                n2 /= 2;
            }
        }

        public static IEnumerable<MultiPrecision<N>> ShortRangeSet<N>() where N : struct, IConstant {
            yield return MultiPrecision<N>.Zero;

            for (Int64 i = 1; i <= 100000; i *= 10) {
                yield return i;
                yield return -i;
            }

            MultiPrecision<N> p = 1;
            for (int i = 0; i < 8; i++) {
                yield return p;
                yield return -p;

                p *= 2;
            }

            MultiPrecision<N> n = 1;
            for (int i = 0; i < 8; i++) {
                yield return n;
                yield return -n;

                n /= 2;
            }

            MultiPrecision<N> p2 = 255;
            for (int i = 0; i < 8; i++) {
                yield return p2;
                yield return -p2;

                p2 *= 2;
            }

            MultiPrecision<N> n2 = 255;
            for (int i = 0; i < 8; i++) {
                yield return n2;
                yield return -n2;

                n2 /= 2;
            }

            MultiPrecision<N> p3 = 255;
            for (int i = 0; i < 4; i++) {
                yield return p3 / 7;
                yield return -p3 / 7;

                p3 *= 4;
            }

            MultiPrecision<N> n3 = 255;
            for (int i = 0; i < 4; i++) {
                yield return n3 / 7;
                yield return -n3 / 7;

                n3 /= 4;
            }
        }

        public static IEnumerable<MultiPrecision<N>> TruncateSet<N>() where N : struct, IConstant {
            MultiPrecision<N>[] ps = { 0, "0.1", "0.5", "0.9" };

            foreach (var p in ps) {
                for (Int64 i = 0; i <= 32; i++) {
                    yield return p + i;
                    yield return p - i;
                }

                for (Int64 i = 1; i <= 100000000000; i *= 10) {
                    yield return p + i;
                    yield return p - i;
                }

                for (Int64 i = 1; i <= 1000; i *= 10) {
                    yield return p + 1 / (MultiPrecision<N>)i;
                    yield return p - 1 / (MultiPrecision<N>)i;
                }
            }
        }

        public static IEnumerable<MultiPrecision<N>> LimitSet<N>() where N : struct, IConstant {
            for (int i = 1000; i <= 10000; i += 1000) {
                yield return MultiPrecision<N>.Pow10(i);
                yield return -MultiPrecision<N>.Pow10(i);
                yield return MultiPrecision<N>.Pow10(-i);
                yield return -MultiPrecision<N>.Pow10(-i);
            }
        }

        public static void Tolerance<N>(double expected, MultiPrecision<N> actual, double minerr = 1e-10, double rateerr = 1e-8, bool ignore_expected_nan = false, bool ignore_sign = false) where N : struct, IConstant {
            if (double.IsNaN(expected)) {
                if (!ignore_expected_nan) {
                    Assert.IsTrue(actual.IsNaN, "unmatch nan");
                }

                return;
            }

            if (!ignore_sign) {
                Assert.AreEqual(Math.Sign(expected), Math.Sign((double)actual), "unmatch sign");
            }

            if (double.IsInfinity(expected)) {
                Assert.IsTrue(double.IsInfinity(actual.ToDouble()), "not infinity");

                return;
            }

            double delta = minerr + Math.Abs(expected) * rateerr;

            Assert.AreEqual(expected, (double)actual, delta);
        }
    }
}
