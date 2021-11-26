using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Sequences {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void HarmonicTest() {
            for (int n = 0; n <= 64; n += 4) {
                Console.WriteLine($"H({n}) = {MultiPrecision<Pow2.N8>.HarmonicNumber(n)}");
                Console.WriteLine(MultiPrecision<Pow2.N8>.HarmonicNumber(n).ToHexcode());
            }

            for (int n = 0; n <= 64; n++) {
                Console.WriteLine($"H({n}) = {MultiPrecision<Pow2.N8>.HarmonicNumber(n)}");
                //Console.WriteLine(MultiPrecision<Pow2.N8>.HarmonicNumber(n).ToHexcode());
            }

            Assert.AreEqual(0, MultiPrecision<Pow2.N8>.HarmonicNumber(0));
            Assert.AreEqual(1, MultiPrecision<Pow2.N8>.HarmonicNumber(1));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(3, 2), MultiPrecision<Pow2.N8>.HarmonicNumber(2));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(11, 6), MultiPrecision<Pow2.N8>.HarmonicNumber(3));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(25, 12), MultiPrecision<Pow2.N8>.HarmonicNumber(4));
        }
    }
}
