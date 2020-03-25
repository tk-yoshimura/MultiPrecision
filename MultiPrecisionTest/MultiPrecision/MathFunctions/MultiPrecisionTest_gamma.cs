using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void LogGammaTest() {
            for (int i = 1; i < 100; i++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -1) * i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
            }
        }

        [TestMethod]
        public void GammaTest() {
            for (int i = 2; i < 100; i++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -1) * i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
            }
        }

        [TestMethod]
        public void LogGammaBorderTest() {
        }

        [TestMethod]
        public void LogGammaUnnormalValueTest() {
        }
    }
}
