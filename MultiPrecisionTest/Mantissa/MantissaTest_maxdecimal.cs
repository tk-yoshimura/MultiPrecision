using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest {
    public partial class MantissaTest {
        [TestMethod]
        public void MaxDecimalTest() {
            Console.WriteLine(Mantissa<Pow2.N4>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N4>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N4>.MaxDecimal.ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N8>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N8>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N8>.MaxDecimal.ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N16>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N16>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N16>.MaxDecimal.ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N32>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N32>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N32>.MaxDecimal.ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N64>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N64>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N64>.MaxDecimal.ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N128>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N128>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N128>.MaxDecimal.ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N256>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N256>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N256>.MaxDecimal.ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N512>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N512>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N512>.MaxDecimal.ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N1024>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N1024>.MaxDecimal);
            Console.WriteLine(Mantissa<Pow2.N1024>.MaxDecimal.ToHexcode());
        }
    }
}
