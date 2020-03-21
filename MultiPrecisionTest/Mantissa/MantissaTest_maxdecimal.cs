using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest {
    public partial class MantissaTest {
        [TestMethod]
        public void MaxDecimalTest() {
            Console.WriteLine(Mantissa<Pow2.N4>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N4>.Decimal(Mantissa<Pow2.N4>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N4>.Decimal(Mantissa<Pow2.N4>.MaxDecimalDigits).ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N8>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N8>.Decimal(Mantissa<Pow2.N8>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N8>.Decimal(Mantissa<Pow2.N8>.MaxDecimalDigits).ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N16>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N16>.Decimal(Mantissa<Pow2.N16>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N16>.Decimal(Mantissa<Pow2.N16>.MaxDecimalDigits).ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N32>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N32>.Decimal(Mantissa<Pow2.N32>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N32>.Decimal(Mantissa<Pow2.N32>.MaxDecimalDigits).ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N64>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N64>.Decimal(Mantissa<Pow2.N64>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N64>.Decimal(Mantissa<Pow2.N64>.MaxDecimalDigits).ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N128>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N128>.Decimal(Mantissa<Pow2.N128>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N128>.Decimal(Mantissa<Pow2.N128>.MaxDecimalDigits).ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N256>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N256>.Decimal(Mantissa<Pow2.N256>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N256>.Decimal(Mantissa<Pow2.N256>.MaxDecimalDigits).ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N512>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N512>.Decimal(Mantissa<Pow2.N512>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N512>.Decimal(Mantissa<Pow2.N512>.MaxDecimalDigits).ToHexcode());

            Console.WriteLine(Mantissa<Pow2.N1024>.MaxDecimalDigits);
            Console.WriteLine(Mantissa<Pow2.N1024>.Decimal(Mantissa<Pow2.N1024>.MaxDecimalDigits));
            Console.WriteLine(Mantissa<Pow2.N1024>.Decimal(Mantissa<Pow2.N1024>.MaxDecimalDigits).ToHexcode());
        }
    }
}
