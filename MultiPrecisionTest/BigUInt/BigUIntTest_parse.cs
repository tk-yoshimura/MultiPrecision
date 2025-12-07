using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {

        [TestMethod]
        public void ParseTest() {
            Random random = new(1234);

            for (int i = 0; i <= 2500; i++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N16>.Length, random.Next(BigUInt<Pow2.N16>.Bits + 1));

                BigUInt<Pow2.N16> v = new(mantissa, enable_clone: false);
                BigUInt<Pow2.N16> v2 = new(v.ToString());

                Assert.AreEqual(v, v2);
            }

            for (int i = 0; i <= 2500; i++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, random.Next(BigUInt<Pow2.N32>.Bits + 1));

                BigUInt<Pow2.N32> v = new(mantissa, enable_clone: false);
                BigUInt<Pow2.N32> v2 = new(v.ToString());

                Assert.AreEqual(v, v2);
            }

            for (int i = 0; i <= 2500; i++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N64>.Length, random.Next(BigUInt<Pow2.N64>.Bits + 1));

                BigUInt<Pow2.N64> v = new(mantissa, enable_clone: false);
                BigUInt<Pow2.N64> v2 = new(v.ToString());

                Assert.AreEqual(v, v2);
            }
        }

        [TestMethod]
        public void ParseFullTest() {
            {
                BigUInt<Pow2.N16> v = BigUInt<Pow2.N16>.Full;
                BigUInt<Pow2.N16> v2 = new(v.ToString());

                Assert.AreEqual(v, v2);
            }
            {
                BigUInt<Pow2.N32> v = BigUInt<Pow2.N32>.Full;
                BigUInt<Pow2.N32> v2 = new(v.ToString());

                Assert.AreEqual(v, v2);
            }
            {
                BigUInt<Pow2.N64> v = BigUInt<Pow2.N64>.Full;
                BigUInt<Pow2.N64> v2 = new(v.ToString());

                Assert.AreEqual(v, v2);
            }
        }

        [TestMethod]
        public void ParseOverflowTest() {
            Assert.ThrowsExactly<OverflowException>(() => {
                BigUInt<Pow2.N16> v = BigUInt<Pow2.N16>.Full;
                BigUInt<Pow2.N16> v2 = new(v.ToString()[..^1] + '9');
            });
            Assert.ThrowsExactly<OverflowException>(() => {
                BigUInt<Pow2.N32> v = BigUInt<Pow2.N32>.Full;
                BigUInt<Pow2.N32> v2 = new(v.ToString()[..^1] + '9');
            });
            Assert.ThrowsExactly<OverflowException>(() => {
                BigUInt<Pow2.N64> v = BigUInt<Pow2.N64>.Full;
                BigUInt<Pow2.N64> v2 = new(v.ToString()[..^1] + '9');
            });
        }
    }
}
