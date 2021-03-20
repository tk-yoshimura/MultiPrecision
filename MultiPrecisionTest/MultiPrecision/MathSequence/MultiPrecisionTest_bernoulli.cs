using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Sequences {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void BernoulliTest() {
            for (int n = 0; n <= 64; n += 4) {
                Console.WriteLine($"B({2 * n}) = {MultiPrecision<Pow2.N8>.BernoulliSequence(n)}");
            }

            for (int n = 0; n <= 64; n++) {
                Console.WriteLine($"B({2 * n}) = {MultiPrecision<Pow2.N8>.BernoulliSequence(n)}");
            }
        }
    }
}
