using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest.Mantissa {
    public partial class MantissaTest {

        [TestMethod]
        public void ParseTest() {
            Random random = new Random(1234);

            for (int i = 0; i <= 2500; i++) {
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
