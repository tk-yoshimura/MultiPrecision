using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    public partial class AccumulatorTest {

        [TestMethod]
        public void AddTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 50000; i++) {
                int digits2 = random.Next(Accumulator<Pow2.N16>.Length / 2);
                int digits1 = random.Next(Accumulator<Pow2.N16>.Length - digits2);

                UInt32[] mantissa1 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Accumulator<Pow2.N16> v1 = new Accumulator<Pow2.N16>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Accumulator<Pow2.N16> v2 = new Accumulator<Pow2.N16>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.Add(v1, v2);
                BigInteger bi = bi1 + bi2;

                Console.WriteLine(bi);
                Console.WriteLine(v);
                Assert.AreEqual(bi, (BigInteger)v);
            }
        }

        [TestMethod]
        public void SubTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 50000; i++) { 
                int digits2 = random.Next(Accumulator<Pow2.N16>.Length / 2 - 2);
                int digits1 = random.Next(digits2 + 1, digits2 + Accumulator<Pow2.N16>.Length / 2);

                UInt32[] mantissa1 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Accumulator<Pow2.N16> v1 = new Accumulator<Pow2.N16>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Accumulator<Pow2.N16> v2 = new Accumulator<Pow2.N16>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                try {
                    Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.Sub(v1, v2);
                    BigInteger bi = bi1 - bi2;

                    Console.WriteLine(bi);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, (BigInteger)v);
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

            for(int i = 0; i <= 50000; i++) { 
                int digits2 = random.Next(Accumulator<Pow2.N16>.Length / 2);
                int digits1 = random.Next(Accumulator<Pow2.N16>.Length - digits2);

                UInt32[] mantissa1 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Accumulator<Pow2.N16> v1 = new Accumulator<Pow2.N16>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Accumulator<Pow2.N16> v2 = new Accumulator<Pow2.N16>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.Mul(v1, v2);
                BigInteger bi = bi1 * bi2;

                Console.WriteLine(bi);
                Console.WriteLine(v);
                Assert.AreEqual(bi, (BigInteger)v);
            }
        }

        [TestMethod]
        public void MulFullTest() {
            UInt32[] mantissa1 = (new UInt32[Accumulator<Pow2.N16>.Length])
                .Select((_, idx) => idx < Accumulator<Pow2.N16>.Length / 2 ? 0xFFFFFFFFu : 0
            ).ToArray();
            UInt32[] mantissa2 = (new UInt32[Accumulator<Pow2.N16>.Length])
                .Select((_, idx) => idx < Accumulator<Pow2.N16>.Length / 2 ? 0xFFFFFFFFu : 0
            ).ToArray();
                
            Accumulator<Pow2.N16> v1 = new Accumulator<Pow2.N16>(mantissa1);
            Accumulator<Pow2.N16> v2 = new Accumulator<Pow2.N16>(mantissa2);
            BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

            Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.Mul(v1, v2);
            BigInteger bi = bi1 * bi2;

            Console.WriteLine(bi);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);
        }

        [TestMethod]
        public void DivTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 50000; i++) { 
                int digits2 = random.Next(1, Accumulator<Pow2.N16>.Length);
                int digits1 = random.Next(digits2, Accumulator<Pow2.N16>.Length);

                UInt32[] mantissa1 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Accumulator<Pow2.N16> v1 = new Accumulator<Pow2.N16>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Accumulator<Pow2.N16> v2 = new Accumulator<Pow2.N16>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                if (v2.IsZero) {
                    continue;
                }

                (Accumulator<Pow2.N16> vdiv, Accumulator<Pow2.N16> vrem)
                    = Accumulator<Pow2.N16>.Div(v1, v2);
                BigInteger bidiv = bi1 / bi2, birem = bi1 - bi2 * bidiv;
                
                Assert.IsTrue(vrem < v2);

                Assert.AreEqual(birem, (BigInteger)vrem, "rem");
                Assert.AreEqual(bidiv, (BigInteger)vdiv, "div");

                Assert.AreEqual(v1, Accumulator<Pow2.N16>.Add(Accumulator<Pow2.N16>.Mul(vdiv, v2), vrem));
            }
        }

        [TestMethod]
        public void DivFullTest() {

            for (int sft1 = 0; sft1 < Accumulator<Pow2.N8>.Bits; sft1++) {

                for (int sft2 = 0; sft2 < Accumulator<Pow2.N8>.Bits; sft2++) {

                    Accumulator<Pow2.N8> v1 = Accumulator<Pow2.N8>.Full >> sft1;
                    Accumulator<Pow2.N8> v2 = Accumulator<Pow2.N8>.Full >> sft2;
                    BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                    (Accumulator<Pow2.N8> vdiv, Accumulator<Pow2.N8> vrem)
                        = Accumulator<Pow2.N8>.Div(v1, v2);
                    BigInteger bidiv = bi1 / bi2, birem = bi1 - bi2 * bidiv;

                    Assert.IsTrue(vrem < v2);

                    Assert.AreEqual(birem, (BigInteger)vrem, "rem");
                    Assert.AreEqual(bidiv, (BigInteger)vdiv, "div");

                    Assert.AreEqual(v1, Accumulator<Pow2.N8>.Add(Accumulator<Pow2.N8>.Mul(vdiv, v2), vrem));

                }
            }
        }

        [TestMethod]
        public void MulDivTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 50000; i++) { 
                int digits2 = random.Next(2, Accumulator<Pow2.N16>.Length / 2);
                int digits1 = random.Next(1, Accumulator<Pow2.N16>.Length - digits2 - 1);

                UInt32[] mantissa1 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits2 ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Accumulator<Pow2.N16> v1 = new Accumulator<Pow2.N16>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Accumulator<Pow2.N16> v2 = new Accumulator<Pow2.N16>(mantissa2) >> random.Next(UIntUtil.UInt32Bits);
                Accumulator<Pow2.N16> v3 = new Accumulator<Pow2.N16>((UInt32)random.Next(4));

                if (random.Next(3) < 2 || v1.IsZero || v2.IsZero || v3.IsZero) {
                    (Accumulator<Pow2.N16> vdiv, Accumulator<Pow2.N16> vrem) =
                        Accumulator<Pow2.N16>.Div(Accumulator<Pow2.N16>.Add(Accumulator<Pow2.N16>.Mul(v1, v2), v3), v2);

                    Assert.AreEqual(v1, vdiv);
                    Assert.AreEqual(v3, vrem);
                }
                else{
                    try {
                        (Accumulator<Pow2.N16> vdiv, Accumulator<Pow2.N16> vrem) =
                            Accumulator<Pow2.N16>.Div(Accumulator<Pow2.N16>.Sub(Accumulator<Pow2.N16>.Mul(v1, v2), v3), v2);

                        Assert.AreEqual(v1 - new Accumulator<Pow2.N16>(1), vdiv);
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
        public void MulShiftTest() {
            Accumulator<Pow2.N4> v1 = new Accumulator<Pow2.N4>(Mantissa<Pow2.N4>.Full);
            Accumulator<Pow2.N4> v2 = new Accumulator<Pow2.N4>(Mantissa<Pow2.N4>.One);
            Accumulator<Pow2.N4> v3 = v1 * 5 / 9;

            Accumulator<Pow2.N4> v11 = Accumulator<Pow2.N4>.MulShift(v1, v1);
            Console.WriteLine(v11.ToHexcode());

            Accumulator<Pow2.N4> v12 = Accumulator<Pow2.N4>.MulShift(v1, v2);
            Console.WriteLine(v12.ToHexcode());

            Accumulator<Pow2.N4> v13 = Accumulator<Pow2.N4>.MulShift(v1, v3);
            Console.WriteLine(v13.ToHexcode());

            Accumulator<Pow2.N4> v21 = Accumulator<Pow2.N4>.MulShift(v2, v1);
            Console.WriteLine(v21.ToHexcode());

            Accumulator<Pow2.N4> v22 = Accumulator<Pow2.N4>.MulShift(v2, v2);
            Console.WriteLine(v22.ToHexcode());

            Accumulator<Pow2.N4> v23 = Accumulator<Pow2.N4>.MulShift(v2, v3);
            Console.WriteLine(v23.ToHexcode());

            Accumulator<Pow2.N4> v31 = Accumulator<Pow2.N4>.MulShift(v3, v1);
            Console.WriteLine(v31.ToHexcode());

            Accumulator<Pow2.N4> v32 = Accumulator<Pow2.N4>.MulShift(v3, v2);
            Console.WriteLine(v32.ToHexcode());

            Accumulator<Pow2.N4> v33 = Accumulator<Pow2.N4>.MulShift(v3, v3);
            Console.WriteLine(v33.ToHexcode());
        }
    }
}
