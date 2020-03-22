using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.Mantissa {
    public partial class MantissaTest {

        [TestMethod]
        public void AddTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int digits2 = random.Next(Mantissa<Pow2.N32>.Length / 2);
                int digits1 = random.Next(Mantissa<Pow2.N32>.Length - digits2);

                UInt32[] mantissa1 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();

                Mantissa<Pow2.N32> v1 = new Mantissa<Pow2.N32>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Mantissa<Pow2.N32> v2 = new Mantissa<Pow2.N32>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                BigInteger bi1 = v1, bi2 = v2;

                Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Add(v1, v2);
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
                int digits2 = random.Next(Mantissa<Pow2.N32>.Length / 2 - 2);
                int digits1 = random.Next(digits2 + 1, digits2 + Mantissa<Pow2.N32>.Length / 2);

                UInt32[] mantissa1 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();

                Mantissa<Pow2.N32> v1 = new Mantissa<Pow2.N32>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Mantissa<Pow2.N32> v2 = new Mantissa<Pow2.N32>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                BigInteger bi1 = v1, bi2 = v2;

                try {
                    Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Sub(v1, v2);
                    BigInteger bi = bi1 - bi2;

                    Console.WriteLine(bi);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, v);
                }
                catch (OverflowException) {
                    Console.WriteLine(v1);
                    Console.WriteLine(v2);
                    throw;
                }
            }
        }

        [TestMethod]
        public void MulTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int digits2 = random.Next(Mantissa<Pow2.N32>.Length / 2);
                int digits1 = random.Next(Mantissa<Pow2.N32>.Length - digits2);

                UInt32[] mantissa1 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();

                Mantissa<Pow2.N32> v1 = new Mantissa<Pow2.N32>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Mantissa<Pow2.N32> v2 = new Mantissa<Pow2.N32>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                BigInteger bi1 = v1, bi2 = v2;

                Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Mul(v1, v2);
                BigInteger bi = bi1 * bi2;

                Console.WriteLine(bi);
                Console.WriteLine(v);
                Assert.AreEqual(bi, v);
            }
        }

        [TestMethod]
        public void MulFullTest() {
            UInt32[] mantissa1 = (new UInt32[Mantissa<Pow2.N32>.Length])
                .Select((_, idx) => idx < Mantissa<Pow2.N32>.Length / 2 ? 0xFFFFFFFFu : 0
            ).ToArray();
            UInt32[] mantissa2 = (new UInt32[Mantissa<Pow2.N32>.Length])
                .Select((_, idx) => idx < Mantissa<Pow2.N32>.Length / 2 ? 0xFFFFFFFFu : 0
            ).ToArray();

            Mantissa<Pow2.N32> v1 = new Mantissa<Pow2.N32>(mantissa1);
            Mantissa<Pow2.N32> v2 = new Mantissa<Pow2.N32>(mantissa2);
            BigInteger bi1 = v1, bi2 = v2;

            Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Mul(v1, v2);
            BigInteger bi = bi1 * bi2;

            Console.WriteLine(bi);
            Console.WriteLine(v);
            Assert.AreEqual(bi, v);
        }

        [TestMethod]
        public void DivTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int digits2 = random.Next(1, Mantissa<Pow2.N32>.Length);
                int digits1 = random.Next(digits2, Mantissa<Pow2.N32>.Length);

                UInt32[] mantissa1 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();

                Mantissa<Pow2.N32> v1 = new Mantissa<Pow2.N32>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Mantissa<Pow2.N32> v2 = new Mantissa<Pow2.N32>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                BigInteger bi1 = v1, bi2 = v2;

                if (v2.IsZero) {
                    continue;
                }

                (Mantissa<Pow2.N32> vdiv, Mantissa<Pow2.N32> vrem)
                    = Mantissa<Pow2.N32>.Div(v1, v2);
                BigInteger bidiv = bi1 / bi2, birem = bi1 - bi2 * bidiv;

                Assert.IsTrue(vrem < v2);

                Assert.AreEqual(birem, vrem, "rem");
                Assert.AreEqual(bidiv, vdiv, "div");

                Assert.AreEqual(v1, Mantissa<Pow2.N32>.Add(Mantissa<Pow2.N32>.Mul(vdiv, v2), vrem));
            }
        }

        [TestMethod]
        public void DivFullTest() {

            for (int sft1 = 0; sft1 < Mantissa<Pow2.N8>.Bits; sft1++) {

                for (int sft2 = 0; sft2 < Mantissa<Pow2.N8>.Bits; sft2++) {

                    Mantissa<Pow2.N8> v1 = Mantissa<Pow2.N8>.Full >> sft1;
                    Mantissa<Pow2.N8> v2 = Mantissa<Pow2.N8>.Full >> sft2;
                    BigInteger bi1 = v1, bi2 = v2;

                    (Mantissa<Pow2.N8> vdiv, Mantissa<Pow2.N8> vrem)
                        = Mantissa<Pow2.N8>.Div(v1, v2);
                    BigInteger bidiv = bi1 / bi2, birem = bi1 - bi2 * bidiv;

                    Assert.IsTrue(vrem < v2);

                    Assert.AreEqual(birem, vrem, "rem");
                    Assert.AreEqual(bidiv, vdiv, "div");

                    Assert.AreEqual(v1, Mantissa<Pow2.N8>.Add(Mantissa<Pow2.N8>.Mul(vdiv, v2), vrem));

                }
            }
        }

        [TestMethod]
        public void MulDivTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 50000; i++) {
                int digits2 = random.Next(2, Mantissa<Pow2.N32>.Length / 2);
                int digits1 = random.Next(1, Mantissa<Pow2.N32>.Length - digits2 - 1);

                UInt32[] mantissa1 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();

                Mantissa<Pow2.N32> v1 = new Mantissa<Pow2.N32>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Mantissa<Pow2.N32> v2 = new Mantissa<Pow2.N32>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                Mantissa<Pow2.N32> v3 = new Mantissa<Pow2.N32>((UInt32)random.Next(4));

                if (random.Next(3) < 2 || v1.IsZero || v2.IsZero || v3.IsZero) {
                    (Mantissa<Pow2.N32> vdiv, Mantissa<Pow2.N32> vrem) =
                        Mantissa<Pow2.N32>.Div(Mantissa<Pow2.N32>.Add(Mantissa<Pow2.N32>.Mul(v1, v2), v3), v2);

                    Assert.AreEqual(v1, vdiv);
                    Assert.AreEqual(v3, vrem);
                }
                else {
                    try {
                        (Mantissa<Pow2.N32> vdiv, Mantissa<Pow2.N32> vrem) =
                            Mantissa<Pow2.N32>.Div(Mantissa<Pow2.N32>.Sub(Mantissa<Pow2.N32>.Mul(v1, v2), v3), v2);

                        Assert.AreEqual(v1 - new Mantissa<Pow2.N32>(1), vdiv);
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
    }
}
