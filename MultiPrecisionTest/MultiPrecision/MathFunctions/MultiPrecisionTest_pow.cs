using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void PowTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(x, 1.5);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Pow((double)x, 1.5), y);
            }

            foreach(MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(x, 0);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Pow((double)x, 0), y);
            }

            foreach(MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(x, -1.5);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Pow((double)x, -1.5), y);
            }
        }

        [TestMethod]
        public void PowUnnormalValueTest() {
            MultiPrecision<Pow2.N8> nz = MultiPrecision<Pow2.N8>.Pow(MultiPrecision<Pow2.N8>.NaN, 0);
            Assert.IsTrue(nz.IsNaN);

            MultiPrecision<Pow2.N8> zn = MultiPrecision<Pow2.N8>.Pow(0, MultiPrecision<Pow2.N8>.NaN);
            Assert.IsTrue(zn.IsNaN);

            MultiPrecision<Pow2.N8> zz = MultiPrecision<Pow2.N8>.Pow(0, 0);
            Assert.AreEqual(1, zz);

            MultiPrecision<Pow2.N8> nn = MultiPrecision<Pow2.N8>.Pow(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.NaN);
            Assert.IsTrue(nn.IsNaN);
        }
    }
}
