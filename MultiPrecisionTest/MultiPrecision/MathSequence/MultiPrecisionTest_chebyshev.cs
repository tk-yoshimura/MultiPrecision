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

            //T0(x) = 1
            Assert.AreEqual(1, MultiPrecision<Pow2.N8>.ChebyshevCoef(1, 1));
            
            //T1(x) = x
            Assert.AreEqual(0, MultiPrecision<Pow2.N8>.ChebyshevCoef(2, 1));
            Assert.AreEqual(1, MultiPrecision<Pow2.N8>.ChebyshevCoef(2, 2));
            
            //T2(x) = -1 + 2x^2
            Assert.AreEqual(-1, MultiPrecision<Pow2.N8>.ChebyshevCoef(3, 1));
            Assert.AreEqual(0, MultiPrecision<Pow2.N8>.ChebyshevCoef(3, 2));
            Assert.AreEqual(2, MultiPrecision<Pow2.N8>.ChebyshevCoef(3, 3));

            //T3(x) = -3x + 4x^3
            Assert.AreEqual(0, MultiPrecision<Pow2.N8>.ChebyshevCoef(4, 1));
            Assert.AreEqual(-3, MultiPrecision<Pow2.N8>.ChebyshevCoef(4, 2));
            Assert.AreEqual(0, MultiPrecision<Pow2.N8>.ChebyshevCoef(4, 3));
            Assert.AreEqual(4, MultiPrecision<Pow2.N8>.ChebyshevCoef(4, 4));
        }
    }
}
