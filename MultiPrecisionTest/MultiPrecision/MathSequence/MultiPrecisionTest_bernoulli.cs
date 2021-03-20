using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Sequences {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void BernoulliTest() {
            foreach (MultiPrecision<Pow2.N8> v in MultiPrecision<Pow2.N8>.BernoulliSequence) {
                Console.WriteLine(v);
            }


        }
    }
}
