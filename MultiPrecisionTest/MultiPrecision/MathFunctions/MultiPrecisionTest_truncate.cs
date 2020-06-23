using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void TruncateTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.TruncateSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Truncate((double)x), y);
            }
        }

        [TestMethod]
        public void FloorTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.TruncateSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Floor((double)x), y);
            }
        }

        [TestMethod]
        public void CeilingTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.TruncateSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Ceiling((double)x), y);
            }
        }

        [TestMethod]
        public void TruncateUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(v);

                Assert.IsTrue(y.IsNaN);
            }
        }
    }
}
