using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ExpTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Exp((double)x), y);
            }
        }

        [TestMethod]
        public void Expm1Test() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Expm1(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Exp((double)x) - 1, y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void Expm1BorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { -1, 0, 1 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 5)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Expm1(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Exp((double)x) - 1, y);

                    ys.Add(y);
                }

                if (b != 0) {
                    TestTool.NearlyNeighbors(ys, 3);
                }
                TestTool.SmoothSatisfied(ys, 3);

                Console.Write("\n");
            }
        }
    }
}
