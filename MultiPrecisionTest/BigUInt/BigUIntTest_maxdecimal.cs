using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void MaxDecimalTest() {
            Console.WriteLine(BigUInt<Pow2.N4>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N4>.Decimal(BigUInt<Pow2.N4>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N4>.Decimal(BigUInt<Pow2.N4>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N4>.MaxDecimalDigits),
                BigUInt<Pow2.N4>.Decimal(BigUInt<Pow2.N4>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N8>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N8>.Decimal(BigUInt<Pow2.N8>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N8>.Decimal(BigUInt<Pow2.N8>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N8>.MaxDecimalDigits),
                BigUInt<Pow2.N8>.Decimal(BigUInt<Pow2.N8>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N16>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N16>.Decimal(BigUInt<Pow2.N16>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N16>.Decimal(BigUInt<Pow2.N16>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N16>.MaxDecimalDigits),
                BigUInt<Pow2.N16>.Decimal(BigUInt<Pow2.N16>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N32>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N32>.Decimal(BigUInt<Pow2.N32>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N32>.Decimal(BigUInt<Pow2.N32>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N32>.MaxDecimalDigits),
                BigUInt<Pow2.N32>.Decimal(BigUInt<Pow2.N32>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N64>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N64>.Decimal(BigUInt<Pow2.N64>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N64>.Decimal(BigUInt<Pow2.N64>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N64>.MaxDecimalDigits),
                BigUInt<Pow2.N64>.Decimal(BigUInt<Pow2.N64>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N128>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N128>.Decimal(BigUInt<Pow2.N128>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N128>.Decimal(BigUInt<Pow2.N128>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N128>.MaxDecimalDigits),
                BigUInt<Pow2.N128>.Decimal(BigUInt<Pow2.N128>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N256>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N256>.Decimal(BigUInt<Pow2.N256>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N256>.Decimal(BigUInt<Pow2.N256>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N256>.MaxDecimalDigits),
                BigUInt<Pow2.N256>.Decimal(BigUInt<Pow2.N256>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N512>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N512>.Decimal(BigUInt<Pow2.N512>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N512>.Decimal(BigUInt<Pow2.N512>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N512>.MaxDecimalDigits),
                BigUInt<Pow2.N512>.Decimal(BigUInt<Pow2.N512>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N1024>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N1024>.Decimal(BigUInt<Pow2.N1024>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N1024>.Decimal(BigUInt<Pow2.N1024>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N1024>.MaxDecimalDigits),
                BigUInt<Pow2.N1024>.Decimal(BigUInt<Pow2.N1024>.MaxDecimalDigits).ToString()
            );
        }
    }
}
