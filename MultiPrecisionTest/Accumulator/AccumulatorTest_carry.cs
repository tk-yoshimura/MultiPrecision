using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    public partial class AccumulatorTest {

        [TestMethod]
        public void CarryAddTest() {
            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(2);
            BigInteger bi = (BigInteger)v;

            v.CarryAdd(3, 0xFFFFFFFFu);
            bi += new BigInteger(0xFFFFFFFFu) << (32 * 3);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);

            v.CarryAdd(5, 1u);
            bi += new BigInteger(0x1u) << (32 * 5);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarryAdd(i, 0xFFFFFFFCu);
                    bi += new BigInteger(0xFFFFFFFCu) << (32 * (int)i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, (BigInteger)v);
                }
            }

            Random random = new Random(1234);

            for (int k = 0; k < 100000; k++) {
                int dig = (int)random.Next(10);
                UInt32 n = (UInt32)random.Next();

                v.CarryAdd(dig, n);
                bi += new BigInteger(n) << (32 * (int)dig);
                Assert.AreEqual(bi, (BigInteger)v);
            }
        }

        [TestMethod]
        public void CarrySubTest() {
            UInt32[] mantissa_full = Enumerable.Repeat(0xFFFFFFFFu, Accumulator<Pow2.N16>.Length).ToArray();

            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa_full);
            BigInteger bi = (BigInteger)v;

            v.CarrySub(3, 0xFFFFFFFFu);
            bi -= new BigInteger(0xFFFFFFFFu) << (32 * 3);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);

            v.CarrySub(5, 1u);
            bi -= new BigInteger(0x1u) << (32 * 5);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarrySub(i, 0xFFFFFFFCu);
                    bi -= new BigInteger(0xFFFFFFFCu) << (32 * (int)i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, (BigInteger)v);
                }
            }

            Random random = new Random(1234);

            for (int k = 0; k < 100000; k++) {
                int dig = random.Next(10);
                UInt32 n = (UInt32)random.Next();

                v.CarrySub(dig, n);
                bi -= new BigInteger(n) << (32 * (int)dig);
                Assert.AreEqual(bi, (BigInteger)v);
            }
        }

        [TestMethod]
        public void CarryAddSubTest() {
            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(2);
            BigInteger bi = (BigInteger)v;

            v.CarryAdd(3, 0xFFFFFFFFu);
            bi += new BigInteger(0xFFFFFFFFu) << (32 * 3);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);

            v.CarryAdd(5, 1u);
            bi += new BigInteger(0x1u) << (32 * 5);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarryAdd(i, 0xFFFFFFFCu);
                    bi += new BigInteger(0xFFFFFFFCu) << (32 * (int)i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, (BigInteger)v);
                }
            }

            v.CarrySub(3, 0xFFFFFFFFu);
            bi -= new BigInteger(0xFFFFFFFFu) << (32 * 3);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);

            v.CarrySub(5, 1u);
            bi -= new BigInteger(0x1u) << (32 * 5);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarrySub(i, 0xFFFFFFFCu);
                    bi -= new BigInteger(0xFFFFFFFCu) << (32 * (int)i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, (BigInteger)v);
                }
            }
        }
    }
}
