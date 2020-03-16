using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    [TestClass]
    public class AccumulatorTest {
        [TestMethod]
        public void CreateTest() {
            UInt32[] mantissa_one = (new UInt32[] { 0x80000000u }).Concat(new UInt32[Accumulator<Pow2.N16>.Length - 1]).ToArray();

            Console.WriteLine(new Accumulator<Pow2.N16>());
            Console.WriteLine(new Accumulator<Pow2.N16>(mantissa_one));
            Console.WriteLine(new Accumulator<Pow2.N16>(2));

            Assert.AreEqual(Mantissa<Pow2.N16>.Length * 2, Accumulator<Pow2.N16>.Length);
        }

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

            for (uint i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarryAdd(i, 0xFFFFFFFCu);
                    bi += new BigInteger(0xFFFFFFFCu) << (32 * (int)i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, (BigInteger)v);
                }
            }

            Random random = new Random(1234);

            for (int k = 0; k < 100000; k++) {
                uint dig = (uint)random.Next(10);
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

            for (uint i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarrySub(i, 0xFFFFFFFCu);
                    bi -= new BigInteger(0xFFFFFFFCu) << (32 * (int)i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, (BigInteger)v);
                }
            }

            Random random = new Random(1234);

            for (int k = 0; k < 100000; k++) {
                uint dig = (uint)random.Next(10);
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

            for (uint i = 0; i < 10; i++) {
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

            for (uint i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    v.CarrySub(i, 0xFFFFFFFCu);
                    bi -= new BigInteger(0xFFFFFFFCu) << (32 * (int)i);
                    Console.WriteLine(v);
                    Assert.AreEqual(bi, (BigInteger)v);
                }
            }
        }

        [TestMethod]
        public void LeftShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length]).Select((_) => (UInt32)random.Next()).ToArray();
            
            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa);
            BigInteger bi = (BigInteger)v;

            Accumulator<Pow2.N16> v_full = Accumulator<Pow2.N16>.Full;
            BigInteger bi_full = (BigInteger)v_full;

            for(int sft = 0; sft <= 2500; sft++) { 
                Accumulator<Pow2.N16> v_sft = v << sft;
                BigInteger bi_sft = (bi << sft) & bi_full;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }

        [TestMethod]
        public void RightShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa);
            BigInteger bi = (BigInteger)v;

            for(int sft = 0; sft <= 2500; sft++) { 
                Accumulator<Pow2.N16> v_sft = v >> sft;
                BigInteger bi_sft = bi >> sft;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }

        [TestMethod]
        public void LeftShiftArrayTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length]).Select((_) => (UInt32)random.Next()).ToArray();
            
            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa);
            BigInteger bi = (BigInteger)v;

            Accumulator<Pow2.N16> v_full = Accumulator<Pow2.N16>.Full;
            BigInteger bi_full = (BigInteger)v_full;

            for(uint sft = 0; sft <= 100; sft++) { 
                Accumulator<Pow2.N16> v_sft = v.Copy();
                v_sft.LeftShiftArray(sft);
                BigInteger bi_sft = (bi << ((int)sft * 32)) & bi_full;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }

        [TestMethod]
        public void RightShiftArrayTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa);
            BigInteger bi = (BigInteger)v;

            for(uint sft = 0; sft <= 100; sft++) {
                Accumulator<Pow2.N16> v_sft = v.Copy();
                v_sft.RightShiftArray(sft);
                BigInteger bi_sft = bi >> ((int)sft * 32);

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }

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
        public void CmpTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 2500; i++) { 
                int digits1 = random.Next(Accumulator<Pow2.N16>.Length);
                int digits2 = random.Next(Accumulator<Pow2.N16>.Length);

                UInt32[] mantissa1 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits1 && random.Next(2) < 1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits2 && random.Next(2) < 1 ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Accumulator<Pow2.N16> v1 = new Accumulator<Pow2.N16>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Accumulator<Pow2.N16> v2 = random.Next(8) < 7
                                        ? new Accumulator<Pow2.N16>(mantissa2) >> random.Next(UIntUtil.UInt32Bits)
                                        : v1.Copy();
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                Console.WriteLine(v1);
                Console.WriteLine(v2);

                Assert.AreEqual(v1.Equals(v2), bi1.Equals(bi2));
                Assert.AreEqual(v1 == v2, bi1 == bi2);
                Assert.AreEqual(v1 != v2, bi1 != bi2);
                Assert.AreEqual(v1 < v2, bi1 < bi2);
                Assert.AreEqual(v1 > v2, bi1 > bi2);
                Assert.AreEqual(v1 <= v2, bi1 <= bi2);
                Assert.AreEqual(v1 >= v2, bi1 >= bi2);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void LeadingZeroCountTest() {
            Random random = new Random(1234);
            
            Accumulator<Pow2.N16> one = new Accumulator<Pow2.N16>(1);
            Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.Zero;

            for(int i = 0; i <= Accumulator<Pow2.N16>.Bits + 100; i++) { 
                Console.WriteLine($"{v.LeadingZeroCount}, {v}");
                
                v.LeftShift(1);

                if(random.Next(2) < 1) { 
                    v = Accumulator<Pow2.N16>.Add(v, one);
                }
            }
        }

        [TestMethod]
        public void MantissaShiftTest() {
            Random random = new Random(1234);
            
            Accumulator<Pow2.N8> one = new Accumulator<Pow2.N8>(1);
            Accumulator<Pow2.N8> v = Accumulator<Pow2.N8>.Zero;

            for(int i = 0; i <= Accumulator<Pow2.N8>.Bits + 10; i++) {
                (Mantissa<Pow2.N8> n, int sft) = v.MantissaShift; 
                
                Console.WriteLine($"{n.LeadingZeroCount}, {sft}, {n}, {v}");
                
                v.LeftShift(1);

                if(random.Next(2) < 1) { 
                    v = Accumulator<Pow2.N8>.Add(v, one);
                }
            }
        }

        [TestMethod]
        public void MantissaShiftFullTest() {
            UInt32[][] vs = {
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFDu, 0x00000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFDu, 0x00000001u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFDu, 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFDu, 0x80000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFDu, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFDu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },

                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0x00000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0x00000001u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0x80000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x00000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x00000001u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x80000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },

                new UInt32[8] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u, 0x00000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u, 0x00000001u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u, 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u, 0x80000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },

                new UInt32[8] { 0x1FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x1FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x1FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },

                new UInt32[8] { 0x3FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x3FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x3FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },

                new UInt32[8] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },

                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0x7FFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFEu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
                new UInt32[8] { 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFu },
            };

            for(uint i = 0; i < vs.Length; i++) { 
                Accumulator<Pow2.N4> v = new Accumulator<Pow2.N4>(vs[i].Reverse().ToArray());

                (Mantissa<Pow2.N4> n, int sft) = v.MantissaShift; 
                
                Console.WriteLine($"{n.LeadingZeroCount}, {sft}, {n}");
            }
        }

        [TestMethod]
        public void ToStringTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 2500; i++) { 
                int digits = random.Next(Accumulator<Pow2.N16>.Length);

                UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.RightShift(new Accumulator<Pow2.N16>(mantissa), (uint)random.Next(UIntUtil.UInt32Bits));
                BigInteger bi = (BigInteger)v;

                Assert.AreEqual(bi.ToString(), v.ToString());

                Console.WriteLine(v);
            }
        }

        [TestMethod]
        public void ToStringFullTest() {
            Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.Full;
            BigInteger bi = (BigInteger)v;

            Assert.AreEqual(bi.ToString(), v.ToString());

            Console.WriteLine(v);
        }
    }
}
