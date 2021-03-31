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

            Assert.AreEqual(0, n1.LeadingZeroCount);
            Assert.AreEqual(1, n2.LeadingZeroCount);
            Assert.AreEqual(2, n3.LeadingZeroCount);
            Assert.AreEqual(2, n4.LeadingZeroCount);
            Assert.AreEqual(0, n5.LeadingZeroCount);
            Assert.AreEqual(128, n6.LeadingZeroCount);
            Assert.AreEqual(127, n7.LeadingZeroCount);
            Assert.AreEqual(33, n8.LeadingZeroCount);
        }
    }
}
