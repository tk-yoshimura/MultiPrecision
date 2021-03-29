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
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { -2, -1, 0, 1, 2 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
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
                TestTool.SmoothSatisfied(ys, 2);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void Pow2UnnormalValueTest() {
            MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow2(MultiPrecision<Pow2.N8>.NaN);

            Assert.IsTrue(y.IsNaN);

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Pow2(MultiPrecision<Pow2.N8>.PositiveInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);

            MultiPrecision<Pow2.N8> minf = MultiPrecision<Pow2.N8>.Pow2(MultiPrecision<Pow2.N8>.NegativeInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.Zero, minf);
        }
    }
}
