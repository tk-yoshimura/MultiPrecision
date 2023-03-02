using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void RangeTest() {
            Console.WriteLine(MultiPrecision<Pow2.N4>.DecimalDigits);
            Console.WriteLine(MultiPrecision<Pow2.N4>.MinValue);
            Console.WriteLine(MultiPrecision<Pow2.N4>.MaxValue);
            Console.Write("\n");

            Console.WriteLine(MultiPrecision<Pow2.N8>.DecimalDigits);
            Console.WriteLine(MultiPrecision<Pow2.N8>.MinValue);
            Console.WriteLine(MultiPrecision<Pow2.N8>.MaxValue);
            Console.Write("\n");

            Console.WriteLine(MultiPrecision<Pow2.N16>.DecimalDigits);
            Console.WriteLine(MultiPrecision<Pow2.N16>.MinValue);
            Console.WriteLine(MultiPrecision<Pow2.N16>.MaxValue);
            Console.Write("\n");

            Console.WriteLine(MultiPrecision<Pow2.N32>.DecimalDigits);
            Console.WriteLine(MultiPrecision<Pow2.N32>.MinValue);
            Console.WriteLine(MultiPrecision<Pow2.N32>.MaxValue);
            Console.Write("\n");

            Console.WriteLine(MultiPrecision<Pow2.N64>.DecimalDigits);
            Console.WriteLine(MultiPrecision<Pow2.N64>.MinValue);
            Console.WriteLine(MultiPrecision<Pow2.N64>.MaxValue);
            Console.Write("\n");

            Console.WriteLine(MultiPrecision<Pow2.N128>.DecimalDigits);

            Console.WriteLine(MultiPrecision<Pow2.N256>.DecimalDigits);

            Console.WriteLine(MultiPrecision<Pow2.N512>.DecimalDigits);

            Console.WriteLine(MultiPrecision<Pow2.N1024>.DecimalDigits);
        }
    }
}
