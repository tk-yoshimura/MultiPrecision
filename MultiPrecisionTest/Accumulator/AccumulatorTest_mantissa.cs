using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest.Accumulator {
    public partial class AccumulatorTest {

        [TestMethod]
        public void MantissaTest() {
            Random random = new Random(1234);

            Accumulator<Pow2.N8> one = new Accumulator<Pow2.N8>(1);
            Accumulator<Pow2.N8> v = Accumulator<Pow2.N8>.Zero;

            for (int i = 0; i < Accumulator<Pow2.N8>.Bits; i++) {
                (Mantissa<Pow2.N8> n, int sft) = v.Mantissa;

                Console.WriteLine($"{n.LeadingZeroCount}, {sft}, {n}, {v}");

                v <<= 1;

                if (random.Next(2) < 1) {
                    v = Accumulator<Pow2.N8>.Add(v, one);
                }
            }
        }

        [TestMethod]
        public void MantissaFullTest() {
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

            for (uint i = 0; i < vs.Length; i++) {
                Accumulator<Pow2.N4> v = new Accumulator<Pow2.N4>(vs[i].Reverse().ToArray());

                (Mantissa<Pow2.N4> n, int sft) = v.Mantissa;

                Console.WriteLine($"{n.LeadingZeroCount}, {sft}, {n}");
            }
        }
    }
}
