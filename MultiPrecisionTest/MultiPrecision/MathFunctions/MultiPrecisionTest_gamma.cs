using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        //[TestMethod]
        //public void LogGammaTest() {
        //    for (int i = 1; i < 100; i++) {
        //        MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -1) * i;
        //        MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(x);

        //        Console.WriteLine(x);
        //        Console.WriteLine(y);
        //    }
        //}

        [TestMethod]
        public void GammaTest() {
            for (int i = 1; i < 100; i++) {
                MultiPrecision<Pow2.N16> x = MultiPrecision<Pow2.N16>.Ldexp(MultiPrecision<Pow2.N16>.One, -1) * i;
                MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                Console.Write("\n");
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
