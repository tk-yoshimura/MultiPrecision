using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Sequences {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void BernoulliTest() {
            for (int n = 0; n <= 64; n += 4) {
                Console.WriteLine($"B({2 * n}) = {MultiPrecision<Pow2.N8>.BernoulliSequence(n)}");
            }

            for (int n = 0; n <= 64; n++) {
                Console.WriteLine($"B({2 * n}) = {MultiPrecision<Pow2.N8>.BernoulliSequence(n)}");
            }

            Assert.AreEqual(1, MultiPrecision<Pow2.N8>.BernoulliSequence(0));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(1, 6), MultiPrecision<Pow2.N8>.BernoulliSequence(1));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(-1, 30), MultiPrecision<Pow2.N8>.BernoulliSequence(2));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(1, 42), MultiPrecision<Pow2.N8>.BernoulliSequence(3));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(-1, 30), MultiPrecision<Pow2.N8>.BernoulliSequence(4));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(5, 66), MultiPrecision<Pow2.N8>.BernoulliSequence(5));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(-691, 2730), MultiPrecision<Pow2.N8>.BernoulliSequence(6));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.Div(7, 6), MultiPrecision<Pow2.N8>.BernoulliSequence(7));
        }
    }
}
