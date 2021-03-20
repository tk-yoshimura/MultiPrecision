using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Sequences {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ChebyshevTest() {
            for (int n = 4; n <= 64; n += 4) {
                for (int m = 4; m <= n; m += 4) {
                    Console.WriteLine($"C({n},{m}) = {MultiPrecision<Pow2.N8>.ChebyshevCoef(n, m)}");
                }
            }

            for (int n = 1; n <= 64; n++) {
                for (int m = 1; m <= n; m++) {
                    Console.WriteLine($"C({n},{m}) = {MultiPrecision<Pow2.N8>.ChebyshevCoef(n, m)}");
                }
            }
        }
    }
}
