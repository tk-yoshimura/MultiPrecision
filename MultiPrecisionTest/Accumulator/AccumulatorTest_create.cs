using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest.Accumulator {
    [TestClass]
    public partial class AccumulatorTest {
        [TestMethod]
        public void CreateTest() {
            UInt32[] mantissa_one = (new UInt32[] { 0x80000000u }).Concat(new UInt32[Accumulator<Pow2.N16>.Length - 1]).ToArray();

            Console.WriteLine(new Accumulator<Pow2.N16>());
            Console.WriteLine(new Accumulator<Pow2.N16>(mantissa_one));
            Console.WriteLine(new Accumulator<Pow2.N16>(2));

            Assert.AreEqual(Mantissa<Pow2.N16>.Length * 2, Accumulator<Pow2.N16>.Length);
        }
    }
}
