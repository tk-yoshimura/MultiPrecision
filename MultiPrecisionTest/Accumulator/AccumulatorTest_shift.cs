using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.Accumulator {
    public partial class AccumulatorTest {

        [TestMethod]
        public void LeftShiftTest() {
            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(1u);
            BigInteger bi = (BigInteger)v;

            for (int sft = 0; sft < Accumulator<Pow2.N16>.Bits; sft++) {
                Accumulator<Pow2.N16> v_sft = v << sft;
                BigInteger bi_sft = bi << sft;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }

            Assert.ThrowsException<OverflowException>(() => {
                Accumulator<Pow2.N16> v_sft = Accumulator<Pow2.N16>.LeftBlockShift(v, Accumulator<Pow2.N16>.Length);
            });
        }

        [TestMethod]
        public void RightShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa);
            BigInteger bi = (BigInteger)v;

            for (int sft = 0; sft <= 2500; sft++) {
                Accumulator<Pow2.N16> v_sft = v >> sft;
                BigInteger bi_sft = bi >> sft;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }

        [TestMethod]
        public void LeftBlockShiftTest() {
            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(0x12345678u);
            BigInteger bi = (BigInteger)v;

            for (int sft = 0; sft < Accumulator<Pow2.N16>.Length; sft++) {
                Accumulator<Pow2.N16> v_sft = Accumulator<Pow2.N16>.LeftBlockShift(v, sft);
                BigInteger bi_sft = bi << ((int)sft * 32);

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }

            Assert.ThrowsException<OverflowException>(() => {
                Accumulator<Pow2.N16> v_sft = Accumulator<Pow2.N16>.LeftBlockShift(v, Accumulator<Pow2.N16>.Length);
            });
        }

        [TestMethod]
        public void RightBlockShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa);
            BigInteger bi = (BigInteger)v;

            for (int sft = 0; sft <= 100; sft++) {
                Accumulator<Pow2.N16> v_sft = Accumulator<Pow2.N16>.RightBlockShift(v, sft);
                BigInteger bi_sft = bi >> ((int)sft * 32);

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }
    }
}
