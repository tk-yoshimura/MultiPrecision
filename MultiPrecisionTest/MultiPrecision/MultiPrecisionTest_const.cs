using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ConstTest() {
            MultiPrecision<Pow2.N8> zero = MultiPrecision<Pow2.N8>.Zero;
            Console.WriteLine(zero);
            Assert.IsTrue(zero.IsZero);
            Assert.IsTrue(zero.IsNormal);
            Assert.IsTrue(zero.IsFinite);
            Assert.IsFalse(zero.IsNaN);
            Assert.AreEqual(Sign.Plus, zero.Sign);

            MultiPrecision<Pow2.N8> minus_zero = MultiPrecision<Pow2.N8>.MinusZero;
            Console.WriteLine(minus_zero);
            Assert.IsTrue(minus_zero.IsZero);
            Assert.IsTrue(minus_zero.IsNormal);
            Assert.IsTrue(minus_zero.IsFinite);
            Assert.IsFalse(minus_zero.IsNaN);
            Assert.AreEqual(Sign.Minus, minus_zero.Sign);

            MultiPrecision<Pow2.N8> one = MultiPrecision<Pow2.N8>.One;
            Console.WriteLine(one);
            Assert.IsFalse(one.IsZero);
            Assert.IsTrue(one.IsNormal);
            Assert.IsTrue(one.IsFinite);
            Assert.IsFalse(one.IsNaN);
            Assert.AreEqual(Sign.Plus, one.Sign);

            MultiPrecision<Pow2.N8> minus_one = MultiPrecision<Pow2.N8>.MinusOne;
            Console.WriteLine(minus_one);
            Assert.IsFalse(minus_one.IsZero);
            Assert.IsTrue(minus_one.IsNormal);
            Assert.IsTrue(minus_one.IsFinite);
            Assert.IsFalse(minus_one.IsNaN);
            Assert.AreEqual(Sign.Minus, minus_one.Sign);

            MultiPrecision<Pow2.N8> nan = MultiPrecision<Pow2.N8>.NaN;
            Console.WriteLine(nan);
            Assert.IsFalse(nan.IsZero);
            Assert.IsFalse(nan.IsNormal);
            Assert.IsFalse(nan.IsFinite);
            Assert.IsTrue(nan.IsNaN);

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.PositiveInfinity;
            Console.WriteLine(inf);
            Assert.IsFalse(inf.IsZero);
            Assert.IsFalse(inf.IsNormal);
            Assert.IsFalse(inf.IsFinite);
            Assert.IsFalse(inf.IsNaN);
            Assert.AreEqual(Sign.Plus, inf.Sign);

            MultiPrecision<Pow2.N8> minus_inf = MultiPrecision<Pow2.N8>.NegativeInfinity;
            Console.WriteLine(minus_inf);
            Assert.IsFalse(minus_inf.IsZero);
            Assert.IsFalse(minus_inf.IsNormal);
            Assert.IsFalse(minus_inf.IsFinite);
            Assert.IsFalse(minus_inf.IsNaN);
            Assert.AreEqual(Sign.Minus, minus_inf.Sign);

            MultiPrecision<Pow2.N8> eps = MultiPrecision<Pow2.N8>.Epsilon;
            Console.WriteLine(eps);
            Assert.IsFalse(eps.IsZero);
            Assert.IsTrue(eps.IsNormal);
            Assert.IsTrue(eps.IsFinite);
            Assert.IsFalse(eps.IsNaN);
            Assert.AreEqual(Sign.Plus, eps.Sign);
        }
    }
}
