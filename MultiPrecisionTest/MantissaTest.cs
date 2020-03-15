using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    [TestClass]
    public class MantissaTest {
        [TestMethod]
        public void CreateTest() {
            UInt32[] mantissa_one = (new UInt32[] { 0x80000000u }).Concat(new UInt32[Mantissa<Pow2.N32>.Length - 1]).ToArray();

            Console.WriteLine(new Mantissa<Pow2.N32>());
            Console.WriteLine(new Mantissa<Pow2.N32>(mantissa_one));
            Console.WriteLine(new Mantissa<Pow2.N32>(2));
        }

        [TestMethod]
        public void CarryAddTest() {
            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(2);
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
            UInt32[] mantissa_full = Enumerable.Repeat(0xFFFFFFFFu, Mantissa<Pow2.N32>.Length).ToArray();

            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa_full);
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
            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(2);
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

            UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length]).Select((_) => (UInt32)random.Next()).ToArray();
            
            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa);
            BigInteger bi = (BigInteger)v;

            Mantissa<Pow2.N32> v_full = Mantissa<Pow2.N32>.Full;
            BigInteger bi_full = (BigInteger)v_full;

            for(int sft = 0; sft <= 2500; sft++) { 
                Mantissa<Pow2.N32> v_sft = v << sft;
                BigInteger bi_sft = (bi << sft) & bi_full;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }

        [TestMethod]
        public void RightShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa);
            BigInteger bi = (BigInteger)v;

            for(int sft = 0; sft <= 2500; sft++) { 
                Mantissa<Pow2.N32> v_sft = v >> sft;
                BigInteger bi_sft = bi >> sft;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }

        [TestMethod]
        public void LeftShiftArrayTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length]).Select((_) => (UInt32)random.Next()).ToArray();
            
            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa);
            BigInteger bi = (BigInteger)v;

            Mantissa<Pow2.N32> v_full = Mantissa<Pow2.N32>.Full;
            BigInteger bi_full = (BigInteger)v_full;

            for(uint sft = 0; sft <= 100; sft++) { 
                Mantissa<Pow2.N32> v_sft = v.Copy();
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

            UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa);
            BigInteger bi = (BigInteger)v;

            for(uint sft = 0; sft <= 100; sft++) {
                Mantissa<Pow2.N32> v_sft = v.Copy();
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
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Add(v1, v2);
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
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                try {
                    Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Sub(v1, v2);
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
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Mul(v1, v2);
                BigInteger bi = bi1 * bi2;

                Console.WriteLine(bi);
                Console.WriteLine(v);
                Assert.AreEqual(bi, (BigInteger)v);
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
            BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

            Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Mul(v1, v2);
            BigInteger bi = bi1 * bi2;

            Console.WriteLine(bi);
            Console.WriteLine(v);
            Assert.AreEqual(bi, (BigInteger)v);
        }

        [TestMethod]
        public void DivTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 50000; i++) { 
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
                BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                if (v2.IsZero) {
                    continue;
                }

                (Mantissa<Pow2.N32> vdiv, Mantissa<Pow2.N32> vrem)
                    = Mantissa<Pow2.N32>.Div(v1, v2);
                BigInteger bidiv = bi1 / bi2, birem = bi1 - bi2 * bidiv;
                
                Assert.IsTrue(vrem < v2);

                Assert.AreEqual(birem, (BigInteger)vrem, "rem");
                Assert.AreEqual(bidiv, (BigInteger)vdiv, "div");

                Assert.AreEqual(v1, Mantissa<Pow2.N32>.Add(Mantissa<Pow2.N32>.Mul(vdiv, v2), vrem));
            }
        }

        [TestMethod]
        public void DivFullTest() {

            for (int sft1 = 0; sft1 < Mantissa<Pow2.N8>.Bits; sft1++) {

                for (int sft2 = 0; sft2 < Mantissa<Pow2.N8>.Bits; sft2++) {

                    Mantissa<Pow2.N8> v1 = Mantissa<Pow2.N8>.Full >> sft1;
                    Mantissa<Pow2.N8> v2 = Mantissa<Pow2.N8>.Full >> sft2;
                    BigInteger bi1 = (BigInteger)v1, bi2 = (BigInteger)v2;

                    (Mantissa<Pow2.N8> vdiv, Mantissa<Pow2.N8> vrem)
                        = Mantissa<Pow2.N8>.Div(v1, v2);
                    BigInteger bidiv = bi1 / bi2, birem = bi1 - bi2 * bidiv;

                    Assert.IsTrue(vrem < v2);

                    Assert.AreEqual(birem, (BigInteger)vrem, "rem");
                    Assert.AreEqual(bidiv, (BigInteger)vdiv, "div");

                    Assert.AreEqual(v1, Mantissa<Pow2.N8>.Add(Mantissa<Pow2.N8>.Mul(vdiv, v2), vrem));

                }
            }
        }

        [TestMethod]
        public void MulDivTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 50000; i++) { 
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
                else{
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

        [TestMethod]
        public void CmpTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 2500; i++) { 
                int digits1 = random.Next(Mantissa<Pow2.N32>.Length);
                int digits2 = random.Next(Mantissa<Pow2.N32>.Length);

                UInt32[] mantissa1 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits1 && random.Next(2) < 1 ? (UInt32)random.Next() : 0
                ).ToArray();
                UInt32[] mantissa2 = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits2 && random.Next(2) < 1 ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Mantissa<Pow2.N32> v1 = new Mantissa<Pow2.N32>(mantissa1) >> random.Next(UIntUtil.UInt32Bits);
                Mantissa<Pow2.N32> v2 = random.Next(8) < 7
                                        ? new Mantissa<Pow2.N32>(mantissa2) >> random.Next(UIntUtil.UInt32Bits)
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
            
            Mantissa<Pow2.N32> one = new Mantissa<Pow2.N32>(1);
            Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Zero;

            for(int i = 0; i <= Mantissa<Pow2.N32>.Bits + 100; i++) { 
                Console.WriteLine($"{v.LeadingZeroCount}, {v}");
                
                v.LeftShift(1);

                if(random.Next(2) < 1) { 
                    v = Mantissa<Pow2.N32>.Add(v, one);
                }
            }
        }

        [TestMethod]
        public void ToStringTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 2500; i++) { 
                int digits = random.Next(Mantissa<Pow2.N32>.Length);

                UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.RightShift(new Mantissa<Pow2.N32>(mantissa), (uint)random.Next(UIntUtil.UInt32Bits));
                BigInteger bi = (BigInteger)v;

                Assert.AreEqual(bi.ToString(), v.ToString());

                Console.WriteLine(v);
            }
        }

        [TestMethod]
        public void ToStringFullTest() {
            Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Full;
            BigInteger bi = (BigInteger)v;

            Assert.AreEqual(bi.ToString(), v.ToString());

            Console.WriteLine(v);
        }

        [TestMethod]
        public void ParseTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 2500; i++) { 
                int digits = random.Next(Mantissa<Pow2.N32>.Length);

                UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa) >> random.Next(UIntUtil.UInt32Bits);
                Mantissa<Pow2.N32> v2 = new Mantissa<Pow2.N32>(v.ToString());

                Assert.AreEqual(v, v2);
            }
        }

        [TestMethod]
        public void ParseFullTest() {
            Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Full;
            Mantissa<Pow2.N32> v2 = new Mantissa<Pow2.N32>(v.ToString());

            Assert.AreEqual(v, v2);
        }
    }
}
