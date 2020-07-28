using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Lb10Test() {
            MultiPrecision<Pow2.N8> lb10 = MultiPrecision<Pow2.N8>.Lb10;

            Console.WriteLine(lb10);
            Console.WriteLine(lb10.ToHexcode());

            TestTool.Tolerance(Math.Log2(10), lb10);
        }

        [TestMethod]
        public void Lb10DigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.Lb10);
            Console.WriteLine(MultiPrecision<Pow2.N8>.Lb10.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.Lb10);
            Console.WriteLine(MultiPrecision<Pow2.N16>.Lb10.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.Lb10);
            Console.WriteLine(MultiPrecision<Pow2.N32>.Lb10.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.Lb10);
            Console.WriteLine(MultiPrecision<Pow2.N64>.Lb10.ToHexcode());
        }
    }
}
