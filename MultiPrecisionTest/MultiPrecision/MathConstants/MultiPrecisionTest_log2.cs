using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Ln2Test() {
            MultiPrecision<Pow2.N8> ln2 = MultiPrecision<Pow2.N8>.Ln2;

            Console.WriteLine(ln2);
            Console.WriteLine(ln2.ToHexcode());

            TestTool.Tolerance(Math.Log(2), ln2);
        }

        [TestMethod]
        public void Ln2DigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.Ln2);
            Console.WriteLine(MultiPrecision<Pow2.N8>.Ln2.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.Ln2);
            Console.WriteLine(MultiPrecision<Pow2.N16>.Ln2.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.Ln2);
            Console.WriteLine(MultiPrecision<Pow2.N32>.Ln2.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.Ln2);
            Console.WriteLine(MultiPrecision<Pow2.N64>.Ln2.ToHexcode());
        }

        [TestMethod]
        public void LbETest() {
            MultiPrecision<Pow2.N8> LbE = MultiPrecision<Pow2.N8>.LbE;

            Console.WriteLine(LbE);
            Console.WriteLine(LbE.ToHexcode());

            TestTool.Tolerance(Math.Log2(Math.E), LbE);
        }

        [TestMethod]
        public void LbEDigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.LbE);
            Console.WriteLine(MultiPrecision<Pow2.N8>.LbE.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.LbE);
            Console.WriteLine(MultiPrecision<Pow2.N16>.LbE.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.LbE);
            Console.WriteLine(MultiPrecision<Pow2.N32>.LbE.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.LbE);
            Console.WriteLine(MultiPrecision<Pow2.N64>.LbE.ToHexcode());
        }
    }
}
