using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ConstTest() {
            MultiPrecision<Pow2.N8> zero = MultiPrecision<Pow2.N8>.Zero;
            Console.WriteLine(zero);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsZero(zero));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNormal(zero));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsFinite(zero));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNaN(zero));
            Assert.AreEqual(Sign.Plus, zero.Sign);

            MultiPrecision<Pow2.N8> minus_zero = MultiPrecision<Pow2.N8>.MinusZero;
            Console.WriteLine(minus_zero);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsZero(minus_zero));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNormal(minus_zero));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsFinite(minus_zero));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNaN(minus_zero));
            Assert.AreEqual(Sign.Minus, minus_zero.Sign);

            MultiPrecision<Pow2.N8> one = MultiPrecision<Pow2.N8>.One;
            Console.WriteLine(one);
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsZero(one));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNormal(one));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsFinite(one));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNaN(one));
            Assert.AreEqual(Sign.Plus, one.Sign);

            MultiPrecision<Pow2.N8> minus_one = MultiPrecision<Pow2.N8>.MinusOne;
            Console.WriteLine(minus_one);
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsZero(minus_one));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNormal(minus_one));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsFinite(minus_one));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNaN(minus_one));
            Assert.AreEqual(Sign.Minus, minus_one.Sign);

            MultiPrecision<Pow2.N8> nan = MultiPrecision<Pow2.N8>.NaN;
            Console.WriteLine(nan);
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsZero(nan));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNormal(nan));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsFinite(nan));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(nan));

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.PositiveInfinity;
            Console.WriteLine(inf);
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsZero(inf));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNormal(inf));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsFinite(inf));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNaN(inf));
            Assert.AreEqual(Sign.Plus, inf.Sign);

            MultiPrecision<Pow2.N8> minus_inf = MultiPrecision<Pow2.N8>.NegativeInfinity;
            Console.WriteLine(minus_inf);
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsZero(minus_inf));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNormal(minus_inf));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsFinite(minus_inf));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNaN(minus_inf));
            Assert.AreEqual(Sign.Minus, minus_inf.Sign);

            MultiPrecision<Pow2.N8> eps = MultiPrecision<Pow2.N8>.Epsilon;
            Console.WriteLine(eps);
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsZero(eps));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNormal(eps));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsFinite(eps));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNaN(eps));
            Assert.AreEqual(Sign.Plus, eps.Sign);
        }
    }
}
