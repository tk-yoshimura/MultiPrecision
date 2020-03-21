using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CosHalfPITest() {
            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.CosHalfPI(x);
                double y_expect = Math.Cos((double)x * Math.PI / 2);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }
        }
    }
}
