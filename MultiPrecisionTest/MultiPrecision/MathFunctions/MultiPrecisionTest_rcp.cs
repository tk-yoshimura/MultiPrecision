using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void RcpTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Rcp(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(1 / (double)x, y);
            }

            Assert.AreEqual(1, MultiPrecision<Pow2.N8>.Rcp(1));

            Assert.AreEqual(0.5, MultiPrecision<Pow2.N8>.Rcp(2));

            Assert.AreEqual(2, MultiPrecision<Pow2.N8>.Rcp(0.5));

            Assert.AreEqual(1, MultiPrecision<Pow2.N16>.Rcp(1));

            Assert.AreEqual(0.5, MultiPrecision<Pow2.N16>.Rcp(2));

            Assert.AreEqual(2, MultiPrecision<Pow2.N16>.Rcp(0.5));
        }

        [TestMethod]
        public void RcpRawTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = 1 / x;

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(1 / (double)x, y);
            }
        }

        [TestMethod]
        public void RcpBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = [-2, -1, -0.5, 0.5, 1, 2];

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = [];

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Rcp(x);
                    MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Rcp(y);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.WriteLine(z);
                    Console.WriteLine(z.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(1 / (double)x, y);

                    Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(x, z, 1));

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 2);
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }
    }
}
