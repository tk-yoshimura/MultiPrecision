using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    public partial class MantissaTest {

        [TestMethod]
        public void CarryAddTest() {
            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(2);
            BigInteger bi = v;

            v.CarryAdd(3, 0xFFFFFFFFu);
            bi += new BigInteger(0xFFFFFFFFu) << (32 * 3);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);

            v.CarryAdd(5, 1u);
            bi += new BigInteger(0x1u) << (32 * 5);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarryAdd(i, 0xFFFFFFFCu);
                    bi += new BigInteger(0xFFFFFFFCu) << (32 * i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, v);
                }
            }

            Random random = new Random(1234);

            for (int k = 0; k < 100000; k++) {
                int dig = random.Next(10);
                UInt32 n = (UInt32)random.Next();

                v.CarryAdd(dig, n);
                bi += new BigInteger(n) << (32 * dig);
                Assert.AreEqual(bi, v);
            }
        }

        [TestMethod]
        public void CarrySubTest() {
            UInt32[] mantissa_full = Enumerable.Repeat(0xFFFFFFFFu, Mantissa<Pow2.N32>.Length).ToArray();

            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa_full);
            BigInteger bi = v;

            v.CarrySub(3, 0xFFFFFFFFu);
            bi -= new BigInteger(0xFFFFFFFFu) << (32 * 3);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);

            v.CarrySub(5, 1u);
            bi -= new BigInteger(0x1u) << (32 * 5);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarrySub(i, 0xFFFFFFFCu);
                    bi -= new BigInteger(0xFFFFFFFCu) << (32 * i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, v);
                }
            }

            Random random = new Random(1234);

            for (int k = 0; k < 100000; k++) {
                int dig = random.Next(10);
                UInt32 n = (UInt32)random.Next();

                v.CarrySub(dig, n);
                bi -= new BigInteger(n) << (32 * dig);
                Assert.AreEqual(bi, v);
            }
        }

        [TestMethod]
        public void CarryAddSubTest() {
            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(2);
            BigInteger bi = v;

            v.CarryAdd(3, 0xFFFFFFFFu);
            bi += new BigInteger(0xFFFFFFFFu) << (32 * 3);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);

            v.CarryAdd(5, 1u);
            bi += new BigInteger(0x1u) << (32 * 5);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarryAdd(i, 0xFFFFFFFCu);
                    bi += new BigInteger(0xFFFFFFFCu) << (32 * i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, v);
                }
            }

            v.CarrySub(3, 0xFFFFFFFFu);
            bi -= new BigInteger(0xFFFFFFFFu) << (32 * 3);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);

            v.CarrySub(5, 1u);
            bi -= new BigInteger(0x1u) << (32 * 5);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarrySub(i, 0xFFFFFFFCu);
                    bi -= new BigInteger(0xFFFFFFFCu) << (32 * i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, v);
                }
            }
        }
    }
}
