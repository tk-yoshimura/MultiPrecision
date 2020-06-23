using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Log2Test() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Log2((double)x), y);
            }
        }

        [TestMethod]
        public void Log2BorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0, 1, 2, 4 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Log2((double)x), y);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void Log2UnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.BitDecrement(0),
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(v);

                Assert.IsTrue(y.IsNaN);
            }

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Log2(MultiPrecision<Pow2.N8>.PositiveInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);
        }
    }
}
