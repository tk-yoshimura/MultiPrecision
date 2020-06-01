using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Int64Test() {
            MultiPrecision<Pow2.N8> zero = 0;
            Assert.AreEqual(0L, (Int64)zero);

            MultiPrecision<Pow2.N8> one = 1;
            Assert.AreEqual(1L, (Int64)one);

            MultiPrecision<Pow2.N8> minus_one = -1;
            Assert.AreEqual(-1L, (Int64)minus_one);

            MultiPrecision<Pow2.N8> maxhalf_value = Int64.MaxValue / 2;
            Assert.AreEqual(Int64.MaxValue / 2, (Int64)maxhalf_value);

            MultiPrecision<Pow2.N8> minhalf_value = Int64.MinValue / 2;
            Assert.AreEqual(Int64.MinValue / 2, (Int64)minhalf_value);

            MultiPrecision<Pow2.N8> max_value = Int64.MaxValue;
            Assert.AreEqual(Int64.MaxValue, (Int64)max_value);

            MultiPrecision<Pow2.N8> min_value = Int64.MinValue;
            Assert.AreEqual(Int64.MinValue, (Int64)min_value);

            for (Int64 i = 10; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Assert.AreEqual(i, (Int64)m);
            }

            for (Int64 i = -10; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Assert.AreEqual(i, (Int64)m);
            }

            Assert.ThrowsException<OverflowException>(() => {
                Int64 v = (Int64)(min_value - 1);
            });

            Assert.ThrowsException<OverflowException>(() => {
                Int64 v = (Int64)(max_value + 1);
            });
        }

        [TestMethod]
        public void DoubleTest() {
            MultiPrecision<Pow2.N8> p5 = 0.5;
            Assert.AreEqual(-1, p5.Exponent);
            CollectionAssert.AreEqual(Mantissa<Pow2.N8>.One.Value, p5.Mantissa);

            MultiPrecision<Pow2.N8> zero = 0.0;
            Assert.AreEqual((double)0, (double)zero);

            MultiPrecision<Pow2.N8> one = 1.0;
            Assert.AreEqual((double)1, (double)one);

            MultiPrecision<Pow2.N8> minus_one = -1.0;
            Assert.AreEqual((double)-1, (double)minus_one);

            MultiPrecision<Pow2.N8> max_value = double.MaxValue;
            Assert.AreEqual(double.MaxValue, (double)max_value);

            MultiPrecision<Pow2.N8> min_value = double.MinValue;
            Assert.AreEqual(double.MinValue, (double)min_value);

            MultiPrecision<Pow2.N8> inf_value = double.PositiveInfinity;
            Assert.AreEqual(double.PositiveInfinity, (double)inf_value);

            MultiPrecision<Pow2.N8> minf_value = double.NegativeInfinity;
            Assert.AreEqual(double.NegativeInfinity, (double)minf_value);

            MultiPrecision<Pow2.N8> nan_value = double.NaN;
            Assert.AreEqual(double.NaN, (double)nan_value);

            MultiPrecision<Pow2.N8> pi = Math.PI;
            Assert.AreEqual(Math.PI, (double)pi);

            for (double i = 10; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Assert.AreEqual((double)i, (double)m);
            }

            for (double i = -10; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Assert.AreEqual((double)i, (double)m);
            }
        }
    }
}
