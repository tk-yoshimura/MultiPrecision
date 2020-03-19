using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void PITest() {
            MultiPrecision<Pow2.N8> pi = MultiPrecision<Pow2.N8>.PI;

            Console.WriteLine((double)pi);
            Console.WriteLine(pi);
            Assert.AreEqual(Math.PI, (double)pi, 1e-5);
        }
    }
}
