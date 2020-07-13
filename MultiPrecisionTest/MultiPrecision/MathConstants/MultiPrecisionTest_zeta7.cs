using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Zeta7Test() {
            MultiPrecision<Pow2.N8> zeta7 = MultiPrecision<Pow2.N8>.Zeta7;

            Console.WriteLine(zeta7);
            Console.WriteLine(zeta7.ToHexcode());

            TestTool.Tolerance(1.00834927738192283, zeta7);
        }

        [TestMethod]
        public void Zeta7DigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.Zeta7);
            Console.WriteLine(MultiPrecision<Pow2.N8>.Zeta7.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.Zeta7);
            Console.WriteLine(MultiPrecision<Pow2.N16>.Zeta7.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.Zeta7);
            Console.WriteLine(MultiPrecision<Pow2.N32>.Zeta7.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.Zeta7);
            Console.WriteLine(MultiPrecision<Pow2.N64>.Zeta7.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N128>.Zeta7.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N256>.Zeta7.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N512>.Zeta7.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N1024>.Zeta7.ToHexcode());
        }
    }
}
