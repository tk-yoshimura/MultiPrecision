using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    public partial class MantissaTest {

        [TestMethod]
        public void ToStringTest() {
            Random random = new Random(1234);

            for(int i = 0; i <= 2500; i++) { 
                int digits = random.Next(Mantissa<Pow2.N32>.Length);

                UInt32[] mantissa = (new UInt32[Mantissa<Pow2.N32>.Length])
                    .Select((_, idx) => idx < digits ? (UInt32)random.Next() : 0
                ).ToArray();
                
                Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.RightShift(new Mantissa<Pow2.N32>(mantissa), random.Next(UIntUtil.UInt32Bits));
                BigInteger bi = v;

                Assert.AreEqual(bi.ToString(), v.ToString());

                Console.WriteLine(v);
            }
        }

        [TestMethod]
        public void ToStringFullTest() {
            Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Full;
            BigInteger bi = v;

            Assert.AreEqual(bi.ToString(), v.ToString());

            Console.WriteLine(v);
        }
    }
}
