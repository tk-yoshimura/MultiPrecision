using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Zeta5Test() {
            MultiPrecision<Pow2.N8> zeta5 = MultiPrecision<Pow2.N8>.Zeta5;

            Console.WriteLine(zeta5);
            Console.WriteLine(zeta5.ToHexcode());

            TestTool.Tolerance(1.03692775514336993, zeta5);
        }

        [TestMethod]
        public void Zeta5DigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.Zeta5);
            Console.WriteLine(MultiPrecision<Pow2.N8>.Zeta5.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.Zeta5);
            Console.WriteLine(MultiPrecision<Pow2.N16>.Zeta5.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.Zeta5);
            Console.WriteLine(MultiPrecision<Pow2.N32>.Zeta5.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.Zeta5);
            Console.WriteLine(MultiPrecision<Pow2.N64>.Zeta5.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N128>.Zeta5.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N256>.Zeta5.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N512>.Zeta5.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N1024>.Zeta5.ToHexcode());
        }
    }
}
