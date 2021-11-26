using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Sequences {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void TaylorTest() {
            for (int n = 0; n <= 64; n++) {
                Console.WriteLine($"1/{n}! = {MultiPrecision<Pow2.N8>.TaylorSequence[n]}");
            }

            Assert.AreEqual(1, MultiPrecision<Pow2.N8>.TaylorSequence[0]);
            Assert.AreEqual(1, MultiPrecision<Pow2.N8>.TaylorSequence[1]);
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Rcp(2), MultiPrecision<Pow2.N8>.TaylorSequence[2]);
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Rcp(6), MultiPrecision<Pow2.N8>.TaylorSequence[3]);
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Rcp(24), MultiPrecision<Pow2.N8>.TaylorSequence[4]);
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Rcp(120), MultiPrecision<Pow2.N8>.TaylorSequence[5]);
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Rcp(720), MultiPrecision<Pow2.N8>.TaylorSequence[6]);
        }
    }
}
