using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    public partial class AccumulatorTest {

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
    }
}
