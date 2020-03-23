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
            MultiPrecision<Pow2.N8> zero = 0;
            Assert.AreEqual((double)0L, (double)zero);

            MultiPrecision<Pow2.N8> one = 1;
            Assert.AreEqual((double)1L, (double)one);

            MultiPrecision<Pow2.N8> minus_one = -1;
            Assert.AreEqual((double)-1L, (double)minus_one);

            MultiPrecision<Pow2.N8> maxhalf_value = Int64.MaxValue / 2;
            Assert.AreEqual((double)(Int64.MaxValue / 2), (double)maxhalf_value);

            MultiPrecision<Pow2.N8> minhalf_value = Int64.MinValue / 2;
            Assert.AreEqual((double)(Int64.MinValue / 2), (double)minhalf_value);

            MultiPrecision<Pow2.N8> max_value = Int64.MaxValue;
            Assert.AreEqual((double)(Int64.MaxValue), (double)max_value);

            MultiPrecision<Pow2.N8> min_value = Int64.MinValue;
            Assert.AreEqual((double)(Int64.MinValue), (double)min_value);

            for (Int64 i = 10; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Assert.AreEqual((double)i, (double)m);
            }

            for (Int64 i = -10; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Assert.AreEqual((double)i, (double)m);
            }
        }
    }
}
