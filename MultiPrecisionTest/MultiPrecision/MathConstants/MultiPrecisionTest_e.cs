using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ETest() {
            MultiPrecision<Pow2.N8> e = MultiPrecision<Pow2.N8>.E;

            Console.WriteLine(e);
            Console.WriteLine(e.ToHexcode());

            TestTool.Tolerance(Math.E, e);
        }

        [TestMethod]
        public void EDigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.E);
            Console.WriteLine(MultiPrecision<Pow2.N8>.E.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.E);
            Console.WriteLine(MultiPrecision<Pow2.N16>.E.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.E);
            Console.WriteLine(MultiPrecision<Pow2.N32>.E.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.E);
            Console.WriteLine(MultiPrecision<Pow2.N64>.E.ToHexcode());
        }
    }
}
