using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Zeta3Test() {
            MultiPrecision<Pow2.N8> zeta3 = MultiPrecision<Pow2.N8>.Zeta3;

            Console.WriteLine(zeta3);
            Console.WriteLine(zeta3.ToHexcode());

            TestTool.Tolerance(1.20205690315959429, zeta3);
        }

        [TestMethod]
        public void Zeta3DigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.Zeta3);
            Console.WriteLine(MultiPrecision<Pow2.N8>.Zeta3.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.Zeta3);
            Console.WriteLine(MultiPrecision<Pow2.N16>.Zeta3.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.Zeta3);
            Console.WriteLine(MultiPrecision<Pow2.N32>.Zeta3.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.Zeta3);
            Console.WriteLine(MultiPrecision<Pow2.N64>.Zeta3.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N128>.Zeta3.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N256>.Zeta3.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N512>.Zeta3.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N1024>.Zeta3.ToHexcode());
        }
    }
}
