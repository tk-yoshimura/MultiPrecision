using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {

        [TestMethod]
        public void ParseTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 2500; i++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N16, Pow2.N1>.Length, random.Next(BigUInt<Pow2.N16, Pow2.N1>.Bits + 1));

                BigUInt<Pow2.N16, Pow2.N1> v = new BigUInt<Pow2.N16, Pow2.N1>(mantissa);
                BigUInt<Pow2.N16, Pow2.N1> v2 = new BigUInt<Pow2.N16, Pow2.N1>(v.ToString());

                Assert.AreEqual(v, v2);
            }

            for (int i = 0; i <= 2500; i++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32, Pow2.N1>.Length, random.Next(BigUInt<Pow2.N32, Pow2.N1>.Bits + 1));

                BigUInt<Pow2.N32, Pow2.N1> v = new BigUInt<Pow2.N32, Pow2.N1>(mantissa);
                BigUInt<Pow2.N32, Pow2.N1> v2 = new BigUInt<Pow2.N32, Pow2.N1>(v.ToString());

                Assert.AreEqual(v, v2);
            }

            for (int i = 0; i <= 2500; i++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N64, Pow2.N1>.Length, random.Next(BigUInt<Pow2.N64, Pow2.N1>.Bits + 1));

                BigUInt<Pow2.N64, Pow2.N1> v = new BigUInt<Pow2.N64, Pow2.N1>(mantissa);
                BigUInt<Pow2.N64, Pow2.N1> v2 = new BigUInt<Pow2.N64, Pow2.N1>(v.ToString());

                Assert.AreEqual(v, v2);
            }
        }

        [TestMethod]
        public void ParseFullTest() {
            {
                BigUInt<Pow2.N16, Pow2.N1> v = BigUInt<Pow2.N16, Pow2.N1>.Full;
                BigUInt<Pow2.N16, Pow2.N1> v2 = new BigUInt<Pow2.N16, Pow2.N1>(v.ToString());

                Assert.AreEqual(v, v2);
            }
            {
                BigUInt<Pow2.N32, Pow2.N1> v = BigUInt<Pow2.N32, Pow2.N1>.Full;
                BigUInt<Pow2.N32, Pow2.N1> v2 = new BigUInt<Pow2.N32, Pow2.N1>(v.ToString());

                Assert.AreEqual(v, v2);
            }
            {
                BigUInt<Pow2.N64, Pow2.N1> v = BigUInt<Pow2.N64, Pow2.N1>.Full;
                BigUInt<Pow2.N64, Pow2.N1> v2 = new BigUInt<Pow2.N64, Pow2.N1>(v.ToString());

                Assert.AreEqual(v, v2);
            }
        }

        [TestMethod]
        public void ParseOverflowTest() {
            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<Pow2.N16, Pow2.N1> v = BigUInt<Pow2.N16, Pow2.N1>.Full;
                BigUInt<Pow2.N16, Pow2.N1> v2 = new BigUInt<Pow2.N16, Pow2.N1>(v.ToString()[..^1] + '9');
            });
            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<Pow2.N32, Pow2.N1> v = BigUInt<Pow2.N32, Pow2.N1>.Full;
                BigUInt<Pow2.N32, Pow2.N1> v2 = new BigUInt<Pow2.N32, Pow2.N1>(v.ToString()[..^1] + '9');
            });
            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<Pow2.N64, Pow2.N1> v = BigUInt<Pow2.N64, Pow2.N1>.Full;
                BigUInt<Pow2.N64, Pow2.N1> v2 = new BigUInt<Pow2.N64, Pow2.N1>(v.ToString()[..^1] + '9');
            });
        }
    }
}
