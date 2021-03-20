using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Sequences {
    [TestClass]
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void StirlingTest() {
            foreach (MultiPrecision<Pow2.N8> v in MultiPrecision<Pow2.N8>.StirlingSequence) {
                Console.WriteLine(v);
            }
        }
    }
}
