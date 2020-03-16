using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    public partial class AccumulatorTest {

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
        public void LeftBlockShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length]).Select((_) => (UInt32)random.Next()).ToArray();
            
            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa);
            BigInteger bi = (BigInteger)v;

            Accumulator<Pow2.N16> v_full = Accumulator<Pow2.N16>.Full;
            BigInteger bi_full = (BigInteger)v_full;

            for(int sft = 0; sft <= 100; sft++) { 
                Accumulator<Pow2.N16> v_sft = v.Copy();
                v_sft.LeftBlockShift(sft);
                BigInteger bi_sft = (bi << ((int)sft * 32)) & bi_full;

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }

        [TestMethod]
        public void RightBlockShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length]).Select((_) => (UInt32)random.Next()).ToArray();

            Accumulator<Pow2.N16> v = new Accumulator<Pow2.N16>(mantissa);
            BigInteger bi = (BigInteger)v;

            for(int sft = 0; sft <= 100; sft++) {
                Accumulator<Pow2.N16> v_sft = v.Copy();
                v_sft.RightBlockShift(sft);
                BigInteger bi_sft = bi >> ((int)sft * 32);

                Console.WriteLine(sft);
                Console.WriteLine(v_sft);
                Assert.AreEqual(bi_sft, (BigInteger)v_sft);
            }
        }
    }
}
