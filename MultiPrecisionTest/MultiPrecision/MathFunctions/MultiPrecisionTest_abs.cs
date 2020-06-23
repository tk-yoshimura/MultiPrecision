using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    [TestClass]
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void AbsTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Abs((double)x), y);
            }
        }

        [TestMethod]
        public void AbsBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Abs((double)x), y);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void AbsUnnormalValueTest() {
            MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.NaN);

            Assert.IsTrue(y.IsNaN);

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.PositiveInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);

            MultiPrecision<Pow2.N8> minf = MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.NegativeInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, minf);
        }
    }
}
