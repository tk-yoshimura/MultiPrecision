using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void EulerGammaTest() {
            MultiPrecision<Pow2.N8> eulergamma = MultiPrecision<Pow2.N8>.EulerGamma;

            Console.WriteLine(eulergamma);
            Console.WriteLine(eulergamma.ToHexcode());

            TestTool.Tolerance(0.57721566490153286, eulergamma);
        }

        [TestMethod]
        public void EulerGammaDigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.EulerGamma);
            Console.WriteLine(MultiPrecision<Pow2.N8>.EulerGamma.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.EulerGamma);
            Console.WriteLine(MultiPrecision<Pow2.N16>.EulerGamma.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.EulerGamma);
            Console.WriteLine(MultiPrecision<Pow2.N32>.EulerGamma.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.EulerGamma);
            Console.WriteLine(MultiPrecision<Pow2.N64>.EulerGamma.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N128>.EulerGamma.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N256>.EulerGamma.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N512>.EulerGamma.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N1024>.EulerGamma.ToHexcode());
        }
    }
}
