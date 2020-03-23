using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void MostSignificantDigitsTest() {
            BigUInt<Pow2.N4, Pow2.N1> n1 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n2 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n3 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x3FFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n4 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0u, 0xFFFFFFFFu, 0x3FFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n5 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n6 = new BigUInt<Pow2.N4, Pow2.N1>();
            BigUInt<Pow2.N4, Pow2.N1> n7 = new BigUInt<Pow2.N4, Pow2.N1>(1u);
            BigUInt<Pow2.N4, Pow2.N1> n8 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0u });

            Assert.AreEqual(UIntUtil.Pack(0xFFFFFFFFu, 0xFFFFFFFFu), n1.MostSignificantDigits);
            Assert.AreEqual(UIntUtil.Pack(0x7FFFFFFFu, 0xFFFFFFFFu), n2.MostSignificantDigits);
            Assert.AreEqual(UIntUtil.Pack(0x3FFFFFFFu, 0xFFFFFFFFu), n3.MostSignificantDigits);
            Assert.AreEqual(UIntUtil.Pack(0x3FFFFFFFu, 0xFFFFFFFFu), n4.MostSignificantDigits);
            Assert.AreEqual(UIntUtil.Pack(0xFFFFFFFFu, 0xFFFFFFFFu), n5.MostSignificantDigits);
            Assert.AreEqual(0ul, n6.MostSignificantDigits);
            Assert.AreEqual(0ul, n7.MostSignificantDigits);
            Assert.AreEqual(0x7FFFFFFFul, n8.MostSignificantDigits);
        }
    }
}
