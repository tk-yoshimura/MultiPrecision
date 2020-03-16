using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    public partial class AccumulatorTest {

        [TestMethod]
        public void ToStringTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 2500; i++) { 
                int digits = random.Next(Accumulator<Pow2.N16>.Length);

                UInt32[] mantissa = (new UInt32[Accumulator<Pow2.N16>.Length])
                    .Select((_, idx) => idx < digits ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Accumulator<Pow2.N16> v = Accumulator<Pow2.N16>.RightShift(new Accumulator<Pow2.N16>(mantissa), random.Next(UIntUtil.UInt32Bits));
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
