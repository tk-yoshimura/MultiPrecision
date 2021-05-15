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
        }
    }
}
