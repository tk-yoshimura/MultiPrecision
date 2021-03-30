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
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { -2, -1, -0.5, 0.5, 1, 2 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Rcp(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(1 / (double)x, y);

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
