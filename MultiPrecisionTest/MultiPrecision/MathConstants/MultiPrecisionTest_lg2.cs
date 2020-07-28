using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Lg2Test() {
            MultiPrecision<Pow2.N8> lg2 = MultiPrecision<Pow2.N8>.Lg2;

            Console.WriteLine(lg2);
            Console.WriteLine(lg2.ToHexcode());

            TestTool.Tolerance(Math.Log10(2), lg2);
        }

        [TestMethod]
        public void Lg2DigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.Lg2);
            Console.WriteLine(MultiPrecision<Pow2.N8>.Lg2.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.Lg2);
            Console.WriteLine(MultiPrecision<Pow2.N16>.Lg2.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.Lg2);
            Console.WriteLine(MultiPrecision<Pow2.N32>.Lg2.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.Lg2);
            Console.WriteLine(MultiPrecision<Pow2.N64>.Lg2.ToHexcode());
        }
    }
}
