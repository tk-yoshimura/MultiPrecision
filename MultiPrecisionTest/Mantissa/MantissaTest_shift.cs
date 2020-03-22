using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.Mantissa {
    public partial class MantissaTest {

        [TestMethod]
        public void LeftShiftTest() {
            Random random = new Random(1234);

            for (int sft = 0; sft < Mantissa<Pow2.N32>.Bits; sft++) {
                UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length]).Select((_, idx) => idx < Mantissa<Pow2.N32>.Length - sft / UIntUtil.UInt32Bits ? (UInt32)random.Next() : 0u).ToArray();
                mantissa[Mantissa<Pow2.N32>.Length - sft / UIntUtil.UInt32Bits - 1] >>= sft % UIntUtil.UInt32Bits;

                Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa);
                BigInteger bi = v;

                Mantissa<Pow2.N32> v_sft = v << sft;
                BigInteger bi_sft = bi << sft;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, v_sft);
            }

            Assert.ThrowsException<OverflowException>(() => {
                Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(1u);

                Mantissa<Pow2.N32> v_sft = Mantissa<Pow2.N32>.LeftBlockShift(v, Mantissa<Pow2.N32>.Length);
            });
        }

        [TestMethod]
        public void RightShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa);
            BigInteger bi = v;

            for (int sft = 33; sft <= 2500; sft++) {
                Mantissa<Pow2.N32> v_sft = v >> sft;
                BigInteger bi_sft = bi >> sft;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, v_sft);
            }
        }

        [TestMethod]
        public void LeftBlockShiftTest() {
            Random random = new Random(1234);

            for (int sft = 0; sft < Mantissa<Pow2.N32>.Length; sft++) {
                UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length]).Select((_, idx) => idx < Mantissa<Pow2.N32>.Length - sft ? (UInt32)random.Next() : 0u).ToArray();

                Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa);
                BigInteger bi = v;

                Mantissa<Pow2.N32> v_sft = Mantissa<Pow2.N32>.LeftBlockShift(v, sft);
                BigInteger bi_sft = bi << (sft * 32);

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, v_sft);
            }

            Assert.ThrowsException<OverflowException>(() => {
                Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(0x12345678u);

                Mantissa<Pow2.N32> v_sft = Mantissa<Pow2.N32>.LeftBlockShift(v, Mantissa<Pow2.N32>.Length);
            });
        }

        [TestMethod]
        public void RightBlockShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Mantissa<Pow2.N32> v = new Mantissa<Pow2.N32>(mantissa);
            BigInteger bi = v;

            for (int sft = 0; sft <= 100; sft++) {
                Mantissa<Pow2.N32> v_sft = Mantissa<Pow2.N32>.RightBlockShift(v, sft);
                BigInteger bi_sft = bi >> (sft * 32);

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, v_sft);
            }
        }
    }
}
