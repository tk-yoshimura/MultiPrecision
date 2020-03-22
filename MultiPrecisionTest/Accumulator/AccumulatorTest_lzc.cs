using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Accumulator {
    public partial class AccumulatorTest {

        [TestMethod]
        public void LeadingZeroCountTest() {
            Random random = new Random(1234);

            Accumulator<Pow2.N16> one = new Accumulator<Pow2.N16>(1);
            Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.Zero;

            for (int i = 0; i < Accumulator<Pow2.N16>.Bits; i++) {
                Console.WriteLine($"{v.LeadingZeroCount}, {v}");

                v <<= 1;

                if (random.Next(2) < 1) {
                    v = Accumulator<Pow2.N16>.Add(v, one);
                }
            }
        }
    }
}
