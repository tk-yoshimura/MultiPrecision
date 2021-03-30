using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void SqrtTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sqrt(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Sqrt((double)x), y);
            }
        }

        [TestMethod]
        public void SqrtBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0, 1, 4 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    if (x.Sign == Sign.Minus) {
                        continue;
                    }

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sqrt(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Sqrt((double)x), y);

                    ys.Add(y);
                }

                if (b != 0) {
                    TestTool.NearlyNeighbors(ys, 1);
                }
                TestTool.SmoothnessSatisfied(ys, 2);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }
    }
}
