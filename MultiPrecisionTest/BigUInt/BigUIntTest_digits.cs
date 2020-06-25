using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void DigitsTest() {
            BigUInt<Pow2.N4> n1 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu });
            BigUInt<Pow2.N4> n2 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu });
            BigUInt<Pow2.N4> n3 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x3FFFFFFFu });
            BigUInt<Pow2.N4> n4 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0u, 0xFFFFFFFFu, 0x3FFFFFFFu });
            BigUInt<Pow2.N4> n5 = new BigUInt<Pow2.N4>(new UInt32[] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu });
            BigUInt<Pow2.N4> n6 = new BigUInt<Pow2.N4>();
            BigUInt<Pow2.N4> n7 = new BigUInt<Pow2.N4>(1u);
            BigUInt<Pow2.N4> n8 = new BigUInt<Pow2.N4>(new UInt32[] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0u });

            Assert.AreEqual(4, n1.Digits);
            Assert.AreEqual(4, n2.Digits);
            Assert.AreEqual(4, n3.Digits);
            Assert.AreEqual(4, n4.Digits);
            Assert.AreEqual(4, n5.Digits);
            Assert.AreEqual(1, n6.Digits);
            Assert.AreEqual(1, n7.Digits);
            Assert.AreEqual(3, n8.Digits);
        }
    }
}
