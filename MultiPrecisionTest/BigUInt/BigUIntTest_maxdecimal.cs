using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void MaxDecimalTest() {
            Console.WriteLine(BigUInt<Pow2.N4, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N4, Pow2.N1>.Decimal(BigUInt<Pow2.N4, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N4, Pow2.N1>.Decimal(BigUInt<Pow2.N4, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N4, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N4, Pow2.N1>.Decimal(BigUInt<Pow2.N4, Pow2.N1>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N8, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N8, Pow2.N1>.Decimal(BigUInt<Pow2.N8, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N8, Pow2.N1>.Decimal(BigUInt<Pow2.N8, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N8, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N8, Pow2.N1>.Decimal(BigUInt<Pow2.N8, Pow2.N1>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N16, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N16, Pow2.N1>.Decimal(BigUInt<Pow2.N16, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N16, Pow2.N1>.Decimal(BigUInt<Pow2.N16, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N16, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N16, Pow2.N1>.Decimal(BigUInt<Pow2.N16, Pow2.N1>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N32, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N32, Pow2.N1>.Decimal(BigUInt<Pow2.N32, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N32, Pow2.N1>.Decimal(BigUInt<Pow2.N32, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N32, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N32, Pow2.N1>.Decimal(BigUInt<Pow2.N32, Pow2.N1>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N64, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N64, Pow2.N1>.Decimal(BigUInt<Pow2.N64, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N64, Pow2.N1>.Decimal(BigUInt<Pow2.N64, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N64, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N64, Pow2.N1>.Decimal(BigUInt<Pow2.N64, Pow2.N1>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N128, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N128, Pow2.N1>.Decimal(BigUInt<Pow2.N128, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N128, Pow2.N1>.Decimal(BigUInt<Pow2.N128, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N128, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N128, Pow2.N1>.Decimal(BigUInt<Pow2.N128, Pow2.N1>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N256, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N256, Pow2.N1>.Decimal(BigUInt<Pow2.N256, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N256, Pow2.N1>.Decimal(BigUInt<Pow2.N256, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N256, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N256, Pow2.N1>.Decimal(BigUInt<Pow2.N256, Pow2.N1>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N512, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N512, Pow2.N1>.Decimal(BigUInt<Pow2.N512, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N512, Pow2.N1>.Decimal(BigUInt<Pow2.N512, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N512, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N512, Pow2.N1>.Decimal(BigUInt<Pow2.N512, Pow2.N1>.MaxDecimalDigits).ToString()
            );

            Console.WriteLine(BigUInt<Pow2.N1024, Pow2.N1>.MaxDecimalDigits);
            Console.WriteLine(BigUInt<Pow2.N1024, Pow2.N1>.Decimal(BigUInt<Pow2.N1024, Pow2.N1>.MaxDecimalDigits));
            Console.WriteLine(BigUInt<Pow2.N1024, Pow2.N1>.Decimal(BigUInt<Pow2.N1024, Pow2.N1>.MaxDecimalDigits).ToHexcode());

            Assert.AreEqual(
                "1" + new string('0', BigUInt<Pow2.N1024, Pow2.N1>.MaxDecimalDigits),
                BigUInt<Pow2.N1024, Pow2.N1>.Decimal(BigUInt<Pow2.N1024, Pow2.N1>.MaxDecimalDigits).ToString()
            );
        }
    }
}
