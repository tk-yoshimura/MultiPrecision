using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void DigitsTest() {
            BigUInt<Pow2.N4> n1 = new(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n2 = new(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n3 = new(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x3FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n4 = new(new UInt32[] { 0xFFFFFFFFu, 0u, 0xFFFFFFFFu, 0x3FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n5 = new(new UInt32[] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n6 = new();
            BigUInt<Pow2.N4> n7 = new(1u);
            BigUInt<Pow2.N4> n8 = new(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0u }, enable_clone: false);

            Assert.AreEqual(4u, n1.Digits);
            Assert.AreEqual(4u, n2.Digits);
            Assert.AreEqual(4u, n3.Digits);
            Assert.AreEqual(4u, n4.Digits);
            Assert.AreEqual(4u, n5.Digits);
            Assert.AreEqual(0u, n6.Digits);
            Assert.AreEqual(1u, n7.Digits);
            Assert.AreEqual(3u, n8.Digits);
        }
    }
}
