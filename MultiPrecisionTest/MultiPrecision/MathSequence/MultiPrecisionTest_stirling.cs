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
        }
    }
}
