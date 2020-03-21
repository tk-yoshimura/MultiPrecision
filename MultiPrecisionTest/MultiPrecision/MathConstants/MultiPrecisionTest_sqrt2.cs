using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Sqrt2Test() {
            MultiPrecision<Pow2.N8> sqrt2 = MultiPrecision<Pow2.N8>.Sqrt2;

            Console.WriteLine((double)sqrt2);
            Console.WriteLine(sqrt2);
            Assert.AreEqual(Math.Sqrt(2), (double)sqrt2, 1e-5);
        }
    }
}
