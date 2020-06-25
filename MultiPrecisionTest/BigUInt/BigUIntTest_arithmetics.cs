using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {

        [TestMethod]
        public void AddTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int bits1 = random.Next(BigUInt<Pow2.N32>.Bits + 1);
                int bits2 = random.Next(BigUInt<Pow2.N32>.Bits - bits1);

                UInt32[] value1 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits1);
                UInt32[] value2 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits2);

                BigUInt<Pow2.N32> v1 = new BigUInt<Pow2.N32>(value1);
                BigUInt<Pow2.N32> v2 = new BigUInt<Pow2.N32>(value2);
                BigInteger bi1 = v1, bi2 = v2;

                BigUInt<Pow2.N32> v = BigUInt<Pow2.N32>.Add(v1, v2);
                BigInteger bi = bi1 + bi2;

                Console.WriteLine(bi);
                Console.WriteLine(v);
                Assert.AreEqual(bi, v);
            }
        }

        [TestMethod]
        public void SubTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int bits1 = random.Next(BigUInt<Pow2.N32>.Bits / 2 + 1, BigUInt<Pow2.N32>.Bits + 1);
                int bits2 = random.Next(BigUInt<Pow2.N32>.Bits - bits1);

                UInt32[] value1 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits1);
                UInt32[] value2 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits2);

                BigUInt<Pow2.N32> v1 = new BigUInt<Pow2.N32>(value1);
                BigUInt<Pow2.N32> v2 = new BigUInt<Pow2.N32>(value2);
                BigInteger bi1 = v1, bi2 = v2;

                BigUInt<Pow2.N32> v = BigUInt<Pow2.N32>.Sub(v1, v2);
                BigInteger bi = bi1 - bi2;

                Console.WriteLine(bi);
                Console.WriteLine(v);
                Assert.AreEqual(bi, v);
            }
        }

        [TestMethod]
        public void MulTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int bits1 = random.Next(BigUInt<Pow2.N32>.Bits + 1);
                int bits2 = random.Next(BigUInt<Pow2.N32>.Bits - bits1);

                UInt32[] value1 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits1);
                UInt32[] value2 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits2);

                BigUInt<Pow2.N32> v1 = new BigUInt<Pow2.N32>(value1);
                BigUInt<Pow2.N32> v2 = new BigUInt<Pow2.N32>(value2);
                BigInteger bi1 = v1, bi2 = v2;

                BigUInt<Pow2.N32> v = BigUInt<Pow2.N32>.Mul(v1, v2);
                BigInteger bi = bi1 * bi2;

                Console.WriteLine(bi);
                Console.WriteLine(v);
                Assert.AreEqual(bi, v);
            }
        }

        [TestMethod]
        public void MulFullTest() {
            UInt32[] value = (new UInt32[BigUInt<Pow2.N32>.Length])
                .Select((_, idx) => idx < BigUInt<Pow2.N32>.Length / 2 ? 0xFFFFFFFFu : 0
            ).ToArray();

            BigUInt<Pow2.N32> v1 = new BigUInt<Pow2.N32>(value);
            BigUInt<Pow2.N32> v2 = new BigUInt<Pow2.N32>(value);
            BigInteger bi1 = v1, bi2 = v2;

            BigUInt<Pow2.N32> v = BigUInt<Pow2.N32>.Mul(v1, v2);
            BigInteger bi = bi1 * bi2;

            Console.WriteLine(bi);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);
        }

        [TestMethod]
        public void ExpandMulTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int bits1 = random.Next(BigUInt<Pow2.N32>.Bits + 1);
                int bits2 = random.Next(BigUInt<Pow2.N32>.Bits + 1);

                UInt32[] value1 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits1);
                UInt32[] value2 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits2);

                BigUInt<Pow2.N32> v1 = new BigUInt<Pow2.N32>(value1);
                BigUInt<Pow2.N32> v2 = new BigUInt<Pow2.N32>(value2);
                BigInteger bi1 = v1, bi2 = v2;

                BigUInt<Double<Pow2.N32>> v = BigUInt<Pow2.N32>.ExpandMul(v1, v2);
                BigInteger bi = bi1 * bi2;

                Console.WriteLine(bi);
                Console.WriteLine(v);
                Assert.AreEqual(bi, v);
            }
        }

        [TestMethod]
        public void ExpandMulFullTest() {
            BigUInt<Pow2.N32> v1 = BigUInt<Pow2.N32>.Full;
            BigUInt<Pow2.N32> v2 = BigUInt<Pow2.N32>.Full;
            BigInteger bi1 = v1, bi2 = v2;

            BigUInt<Double<Pow2.N32>> v = BigUInt<Pow2.N32>.ExpandMul(v1, v2);
            BigInteger bi = bi1 * bi2;

            Console.WriteLine(bi);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);
        }

        [TestMethod]
        public void DivTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int bits1 = random.Next(BigUInt<Pow2.N32>.Bits / 2 + 1, BigUInt<Pow2.N32>.Bits + 1);
                int bits2 = random.Next(BigUInt<Pow2.N32>.Bits - bits1);

                UInt32[] value1 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits1);
                UInt32[] value2 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits2);

                BigUInt<Pow2.N32> v1 = new BigUInt<Pow2.N32>(value1);
                BigUInt<Pow2.N32> v2 = new BigUInt<Pow2.N32>(value2);
                BigInteger bi1 = v1, bi2 = v2;

                if (v2.IsZero) {
                    continue;
                }

                (BigUInt<Pow2.N32> vdiv, BigUInt<Pow2.N32> vrem)
                    = BigUInt<Pow2.N32>.Div(v1, v2);
                BigInteger bidiv = bi1 / bi2, birem = bi1 - bi2 * bidiv;

                Assert.IsTrue(vrem < v2);

                Assert.AreEqual(birem, vrem, "rem");
                Assert.AreEqual(bidiv, vdiv, "div");

                Assert.AreEqual(v1, BigUInt<Pow2.N32>.Add(BigUInt<Pow2.N32>.Mul(vdiv, v2), vrem));
            }
        }

        [TestMethod]
        public void DivFullTest() {

            for (int sft1 = 0; sft1 < BigUInt<Pow2.N8>.Bits; sft1++) {

                for (int sft2 = 0; sft2 < BigUInt<Pow2.N8>.Bits; sft2++) {

                    BigUInt<Pow2.N8> v1 = BigUInt<Pow2.N8>.Full >> sft1;
                    BigUInt<Pow2.N8> v2 = BigUInt<Pow2.N8>.Full >> sft2;
                    BigInteger bi1 = v1, bi2 = v2;

                    (BigUInt<Pow2.N8> vdiv, BigUInt<Pow2.N8> vrem)
                        = BigUInt<Pow2.N8>.Div(v1, v2);
                    BigInteger bidiv = bi1 / bi2, birem = bi1 - bi2 * bidiv;

                    Assert.IsTrue(vrem < v2);

                    Assert.AreEqual(birem, vrem, "rem");
                    Assert.AreEqual(bidiv, vdiv, "div");

                    Assert.AreEqual(v1, BigUInt<Pow2.N8>.Add(BigUInt<Pow2.N8>.Mul(vdiv, v2), vrem));

                }
            }
        }

        [TestMethod]
        public void MulDivTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int bits1 = random.Next(BigUInt<Pow2.N32>.Bits + 1);
                int bits2 = random.Next(BigUInt<Pow2.N32>.Bits - bits1);

                UInt32[] value1 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits1);
                UInt32[] value2 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits2);

                BigUInt<Pow2.N32> v1 = new BigUInt<Pow2.N32>(value1);
                BigUInt<Pow2.N32> v2 = new BigUInt<Pow2.N32>(value2);
                BigUInt<Pow2.N32> v3 = new BigUInt<Pow2.N32>((UInt32)random.Next(4));

                if (v2.IsZero || v2 <= v3) {
                    continue;
                }

                if (random.Next(3) < 2 || v1.IsZero || v2.IsZero || v3.IsZero) {
                    (BigUInt<Pow2.N32> vdiv, BigUInt<Pow2.N32> vrem) =
                        BigUInt<Pow2.N32>.Div(BigUInt<Pow2.N32>.Add(BigUInt<Pow2.N32>.Mul(v1, v2), v3), v2);

                    Assert.AreEqual(v1, vdiv);
                    Assert.AreEqual(v3, vrem);
                }
                else {
                    try {
                        (BigUInt<Pow2.N32> vdiv, BigUInt<Pow2.N32> vrem) =
                            BigUInt<Pow2.N32>.Div(BigUInt<Pow2.N32>.Sub(BigUInt<Pow2.N32>.Mul(v1, v2), v3), v2);

                        Assert.AreEqual(v1 - new BigUInt<Pow2.N32>(1), vdiv);
                        Assert.AreEqual(v2 - v3, vrem);
                    }
                    catch (OverflowException) {
                        Console.WriteLine(v1);
                        Console.WriteLine(v2);
                        Console.WriteLine(v3);
                        throw;
                    }
                }
            }
        }

        [TestMethod]
        public void RoundDivTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int bits1 = random.Next(BigUInt<Pow2.N32>.Bits / 2 + 1, BigUInt<Pow2.N32>.Bits + 1);
                int bits2 = random.Next(BigUInt<Pow2.N32>.Bits - bits1);

                UInt32[] value1 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits1);
                UInt32[] value2 = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, bits2);

                BigUInt<Pow2.N32> v1 = new BigUInt<Pow2.N32>(value1);
                BigUInt<Pow2.N32> v2 = new BigUInt<Pow2.N32>(value2);
                BigInteger bi1 = v1, bi2 = v2;

                if (v2.IsZero) {
                    continue;
                }

                BigUInt<Pow2.N32> vdiv = BigUInt<Pow2.N32>.RoundDiv(v1, v2);
                BigInteger bidiv = bi1 / bi2, birem = bi1 % bi2;

                if (birem * 2 >= bi2) {
                    bidiv++;
                }

                Assert.AreEqual(bidiv, vdiv);
            }
        }
    }
}
