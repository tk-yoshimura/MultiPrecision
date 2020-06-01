using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void LeadingZeroCountTest() {
            BigUInt<Pow2.N4, Pow2.N1> n1 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n2 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n3 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x3FFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n4 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0u, 0xFFFFFFFFu, 0x3FFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n5 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu });
            BigUInt<Pow2.N4, Pow2.N1> n6 = new BigUInt<Pow2.N4, Pow2.N1>();
            BigUInt<Pow2.N4, Pow2.N1> n7 = new BigUInt<Pow2.N4, Pow2.N1>(1u);
            BigUInt<Pow2.N4, Pow2.N1> n8 = new BigUInt<Pow2.N4, Pow2.N1>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0u });

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
