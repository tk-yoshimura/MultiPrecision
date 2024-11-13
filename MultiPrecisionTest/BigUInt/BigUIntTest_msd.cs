using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void MostSignificantDigitsTest() {
            BigUInt<Pow2.N4> n1 = new([0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu], enable_clone: false);
            BigUInt<Pow2.N4> n2 = new([0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu], enable_clone: false);
            BigUInt<Pow2.N4> n3 = new([0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x3FFFFFFFu], enable_clone: false);
            BigUInt<Pow2.N4> n4 = new([0xFFFFFFFFu, 0u, 0xFFFFFFFFu, 0x3FFFFFFFu], enable_clone: false);
            BigUInt<Pow2.N4> n5 = new([0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu], enable_clone: false);
            BigUInt<Pow2.N4> n6 = new();
            BigUInt<Pow2.N4> n7 = new(1u);
            BigUInt<Pow2.N4> n8 = new([0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0u], enable_clone: false);

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
