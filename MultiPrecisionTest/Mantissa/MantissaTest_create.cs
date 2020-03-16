using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest {
    [TestClass]
    public partial class MantissaTest {
        [TestMethod]
        public void CreateTest() {
            UInt32[] mantissa_one = (new UInt32[] { 0x80000000u }).Concat(new UInt32[Mantissa<Pow2.N32>.Length - 1]).ToArray();

            Console.WriteLine(new Mantissa<Pow2.N32>());
            Console.WriteLine(new Mantissa<Pow2.N32>(mantissa_one));
            Console.WriteLine(new Mantissa<Pow2.N32>(2));
        }
    }
}
