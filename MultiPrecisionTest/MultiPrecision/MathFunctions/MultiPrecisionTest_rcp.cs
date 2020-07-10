using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void RcpTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Rcp(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(1 / (double)x, y);
            }
        }

        [TestMethod]
        public void RcpRawTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = 1 / x;

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(1 / (double)x, y);
            }
        }

        [TestMethod]
        public void RcpBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { -2, -1, 1, 2 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    if(x.Sign == Sign.Minus) { 
                        continue;
                    }

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Rcp(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(1 / (double)x, y);
                }

                Console.Write("\n");
            }
        }
    }
}
