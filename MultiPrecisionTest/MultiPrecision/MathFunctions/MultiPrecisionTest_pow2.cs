using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Pow2Test() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow2(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Pow(2, (double)x), y);
            }
        }

        [TestMethod]
        public void Pow2BorderTest() {
            MultiPrecision<Pow2.N8>[] borders_n8 = new MultiPrecision<Pow2.N8>[] { -2, -1, -0.5, 0, 0.5, 1, 2 };

            foreach (MultiPrecision<Pow2.N8> b in borders_n8) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow2(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Pow(2, (double)x), y);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 4);
                TestTool.SmoothnessSatisfied(ys, 1.25);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            MultiPrecision<Pow2.N16>[] borders_n16 = new MultiPrecision<Pow2.N16>[] { -2, -1, -0.5, 0, 0.5, 1, 2 };

            foreach (MultiPrecision<Pow2.N16> b in borders_n16) {
                List<MultiPrecision<Pow2.N16>> ys = new();

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Pow2(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Pow(2, (double)x), y);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 4);
                TestTool.SmoothnessSatisfied(ys, 1.25);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void Pow2UnnormalValueTest() {
            MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow2(MultiPrecision<Pow2.N8>.NaN);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(y));

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Pow2(MultiPrecision<Pow2.N8>.PositiveInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);

            MultiPrecision<Pow2.N8> minf = MultiPrecision<Pow2.N8>.Pow2(MultiPrecision<Pow2.N8>.NegativeInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.Zero, minf);
        }
    }
}
