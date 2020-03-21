using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ETest() {
            MultiPrecision<Pow2.N8> e = MultiPrecision<Pow2.N8>.E;

            Console.WriteLine((double)e);
            Console.WriteLine(e);
            Assert.AreEqual(Math.E, (double)e, 1e-5);
        }
    }
}
