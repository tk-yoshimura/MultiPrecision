using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {
        [TestMethod]
        public void CmpTest() {
            const int length = 4;
            Random random = new Random(1234);

            for (int i = 0; i <= 2500; i++) {
                UInt32[] mantissa1 = UIntUtil.Random(random, length, random.Next(length * UIntUtil.UInt32Bits + 1));
                UInt32[] mantissa2 = UIntUtil.Random(random, length, random.Next(length * UIntUtil.UInt32Bits + 1));

                BigUInt<Pow2.N4> n1 = new BigUInt<Pow2.N4>(mantissa1);
                BigUInt<Pow2.N4> n2 = random.Next(8) < 7 ? new BigUInt<Pow2.N4>(mantissa2) : n1.Copy();
                BigInteger bi1 = n1, bi2 = n2;

                Console.WriteLine(n1);
                Console.WriteLine(n2);

                Assert.AreEqual(n1.Equals(n2), bi1.Equals(bi2));
                Assert.AreEqual(n1 == n2, bi1 == bi2);
                Assert.AreEqual(n1 != n2, bi1 != bi2);
                Assert.AreEqual(n1 < n2, bi1 < bi2);
                Assert.AreEqual(n1 > n2, bi1 > bi2);
                Assert.AreEqual(n1 <= n2, bi1 <= bi2);
                Assert.AreEqual(n1 >= n2, bi1 >= bi2);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void IsZeroFullTest() {
            BigUInt<Pow2.N4> n1 = new BigUInt<Pow2.N4>(new UInt32[] { ~0u, ~0u, ~0u, ~0u });
            BigUInt<Pow2.N4> n2 = new BigUInt<Pow2.N4>(new UInt32[] { 0u, ~0u, ~0u, ~0u });
            BigUInt<Pow2.N4> n3 = new BigUInt<Pow2.N4>(new UInt32[] { 0u, 0u, 0u, 0u });

            Assert.IsFalse(n1.IsZero);
            Assert.IsTrue(n1.IsFull);

            Assert.IsFalse(n2.IsZero);
            Assert.IsFalse(n2.IsFull);

            Assert.IsTrue(n3.IsZero);
            Assert.IsFalse(n3.IsFull);
        }
    }
}
