using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Sequences {
    [TestClass]
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void StirlingTest() {
            for (int n = 4; n <= 64; n += 4) {
                Console.WriteLine($"S({n}) = {MultiPrecision<Pow2.N8>.StirlingSequence(n)}");
            }

            for (int n = 1; n <= 64; n++) {
                Console.WriteLine($"S({n}) = {MultiPrecision<Pow2.N8>.StirlingSequence(n)}");
            }

            Assert.AreEqual(MultiPrecision<Pow2.N8>.Rcp(12), MultiPrecision<Pow2.N8>.StirlingSequence(1));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Rcp(12), MultiPrecision<Pow2.N8>.StirlingSequence(2));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(59, 360), MultiPrecision<Pow2.N8>.StirlingSequence(3));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(29, 60), MultiPrecision<Pow2.N8>.StirlingSequence(4));
        }
    }
}
