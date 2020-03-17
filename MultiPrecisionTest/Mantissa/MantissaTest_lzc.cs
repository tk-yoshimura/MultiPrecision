using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MantissaTest {

        [TestMethod]
        public void LeadingZeroCountTest() {
            Random random = new Random(1234);
            
            Mantissa<Pow2.N32> one = new Mantissa<Pow2.N32>(1);
            Mantissa<Pow2.N32> v = Mantissa<Pow2.N32>.Zero;

            for(int i = 0; i <= Mantissa<Pow2.N32>.Bits + 100; i++) { 
                Console.WriteLine($"{v.LeadingZeroCount}, {v}");
                
                v <<= 1;

                if(random.Next(2) < 1) { 
                    v = Mantissa<Pow2.N32>.Add(v, one);
                }
            }
        }
    }
}
