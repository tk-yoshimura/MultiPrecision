using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void LeadingZeroCountTest() {
            BigUInt<Pow2.N4> n1 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n2 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n3 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x3FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n4 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0u, 0xFFFFFFFFu, 0x3FFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n5 = new BigUInt<Pow2.N4>(new UInt32[] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu }, enable_clone: false);
            BigUInt<Pow2.N4> n6 = new BigUInt<Pow2.N4>();
            BigUInt<Pow2.N4> n7 = new BigUInt<Pow2.N4>(1u);
            BigUInt<Pow2.N4> n8 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0u }, enable_clone: false);

            Assert.AreEqual(0u, n1.LeadingZeroCount);
            Assert.AreEqual(1u, n2.LeadingZeroCount);
            Assert.AreEqual(2u, n3.LeadingZeroCount);
            Assert.AreEqual(2u, n4.LeadingZeroCount);
            Assert.AreEqual(0u, n5.LeadingZeroCount);
            Assert.AreEqual(128u, n6.LeadingZeroCount);
            Assert.AreEqual(127u, n7.LeadingZeroCount);
            Assert.AreEqual(33u, n8.LeadingZeroCount);
        }
    }
}
