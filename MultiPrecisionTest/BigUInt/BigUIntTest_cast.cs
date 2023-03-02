using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Numerics;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void ToBigIntegerTest() {
            BigUInt<Pow2.N4> n1 = new();
            BigUInt<Pow2.N4> n2 = new(2u);
            BigUInt<Pow2.N4> n3 = new(0x12345678ABCDEFul);
            BigUInt<Pow2.N4> n4 = new(new UInt32[] { 0x1234u, 0x5678u, 0x9ABCu, 0xDEF0u }, enable_clone: false);

            Assert.AreEqual(new BigInteger(0), (BigInteger)n1);
            Assert.AreEqual(new BigInteger(2u), (BigInteger)n2);
            Assert.AreEqual(new BigInteger(0x12345678ABCDEFul), (BigInteger)n3);
            Assert.AreEqual(
                new BigInteger(0x1234u)
                + (new BigInteger(0x5678u) << UIntUtil.UInt32Bits)
                + (new BigInteger(0x9ABCu) << (UIntUtil.UInt32Bits * 2))
                + (new BigInteger(0xDEF0u) << (UIntUtil.UInt32Bits * 3)),
                (BigInteger)n4
            );
        }
    }
}
