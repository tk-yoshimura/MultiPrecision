using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ConstTest() {
            MultiPrecision<Pow2.N8> zero = MultiPrecision<Pow2.N8>.Zero;
            Console.WriteLine((double)zero);

            MultiPrecision<Pow2.N8> one = MultiPrecision<Pow2.N8>.One;
            Console.WriteLine((double)one);

            MultiPrecision<Pow2.N8> minus_one = MultiPrecision<Pow2.N8>.MinusOne;
            Console.WriteLine((double)minus_one);

            MultiPrecision<Pow2.N8> nan = MultiPrecision<Pow2.N8>.NaN;
            Console.WriteLine((double)nan);
            Assert.IsTrue(nan.IsNaN);
        }
    }
}
