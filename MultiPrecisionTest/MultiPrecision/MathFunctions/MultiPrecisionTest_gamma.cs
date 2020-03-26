using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void LogGammaTest() {
            for (int i = 0; i < 200; i++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -1) * i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                Console.Write("\n");
            }
        }

        [TestMethod]
        public void GammaTest() {
            for (int i = -200; i < 200; i++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -1) * i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                Console.Write("\n");
            }
        }
    }
}
