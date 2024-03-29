using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Numerics;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {

        [TestMethod]
        public void ToStringTest() {
            Random random = new(1234);

            for (int i = 0; i <= 2500; i++) {
                UInt32[] mantissa =
                    UIntUtil.Random(
                        random,
                        BigUInt<Pow2.N32>.Length,
                        random.Next(BigUInt<Pow2.N32>.Bits + 1)
                    );

                BigUInt<Pow2.N32> v = new(mantissa, enable_clone: false);
                BigInteger bi = v;

                Assert.AreEqual(bi.ToString(), v.ToString());

                Console.WriteLine(v);
            }
        }

        [TestMethod]
        public void ToStringFullTest() {
            BigUInt<Pow2.N32> v = BigUInt<Pow2.N32>.Full;
            BigInteger bi = v;

            Assert.AreEqual(bi.ToString(), v.ToString());

            Console.WriteLine(v);
        }
    }
}
