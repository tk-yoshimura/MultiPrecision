using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void SqrtTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) { 

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
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sqrt(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Sqrt((double)x), y);
                }

                Console.Write("\n");
            }
        }
    }
}
