using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CbrtTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Cbrt((double)x), y);
            }
        }

        [TestMethod]
        public void CbrtBorderTest() {
            MultiPrecision<Pow2.N8>[] borders_n8 = new MultiPrecision<Pow2.N8>[] { -8, -1, 0, 1, 8 };

            foreach (MultiPrecision<Pow2.N8> b in borders_n8) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(x);
                    MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Cube(y);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.WriteLine(z);
                    Console.WriteLine(z.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Cbrt((double)x), y);

                    if (b != 0) {
                        Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(x, z, 1));
                    }

                    ys.Add(y);
                }

                if (b != 0) {
                    TestTool.NearlyNeighbors(ys, 1);
                }
                TestTool.SmoothnessSatisfied(ys, (b != 0) ? 1.25 : 10);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            MultiPrecision<Pow2.N16>[] borders_n16 = new MultiPrecision<Pow2.N16>[] { -8, -1, 0, 1, 8 };

            foreach (MultiPrecision<Pow2.N16> b in borders_n16) {
                List<MultiPrecision<Pow2.N16>> ys = new();

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Cbrt(x);
                    MultiPrecision<Pow2.N16> z = MultiPrecision<Pow2.N16>.Cube(y);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.WriteLine(z);
                    Console.WriteLine(z.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Cbrt((double)x), y);

                    if (b != 0) {
                        Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(x, z, 1));
                    }

                    ys.Add(y);
                }

                if (b != 0) {
                    TestTool.NearlyNeighbors(ys, 1);
                }
                TestTool.SmoothnessSatisfied(ys, (b != 0) ? 1.25 : 10);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void CbrtUnnormalValueTest() {
            MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(MultiPrecision<Pow2.N8>.NaN);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(y));

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Cbrt(MultiPrecision<Pow2.N8>.PositiveInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);

            MultiPrecision<Pow2.N8> minf = MultiPrecision<Pow2.N8>.Cbrt(MultiPrecision<Pow2.N8>.NegativeInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.NegativeInfinity, minf);
        }
    }
}
