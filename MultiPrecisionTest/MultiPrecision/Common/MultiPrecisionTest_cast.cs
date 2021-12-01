using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Numerics;

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
            Assert.AreEqual(0, (double)zero);

            MultiPrecision<Pow2.N8> one = 1.0;
            Assert.AreEqual(1, (double)one);

            MultiPrecision<Pow2.N8> minus_one = -1.0;
            Assert.AreEqual(-1, (double)minus_one);

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

        [TestMethod]
        public void DecimalTest() {
            MultiPrecision<Pow2.N8> p5 = 0.5m;
            Assert.AreEqual(-1, p5.Exponent);
            CollectionAssert.AreEqual(Mantissa<Pow2.N8>.One.Value, p5.Mantissa);

            MultiPrecision<Pow2.N8> n2p5 = 2.5m;
            Assert.AreEqual(2.5, n2p5);

            MultiPrecision<Pow2.N8> zero = 0.0m;
            Assert.AreEqual(0, (decimal)zero);

            MultiPrecision<Pow2.N8> one = 1.0m;
            Assert.AreEqual(1, (decimal)one);

            MultiPrecision<Pow2.N8> minus_one = -1.0m;
            Assert.AreEqual(-1, (decimal)minus_one);

            MultiPrecision<Pow2.N8> pi = (decimal)Math.PI;
            Assert.AreEqual((decimal)Math.PI, (decimal)pi);

            MultiPrecision<Pow2.N8> p33a = 0.3300m;
            Assert.AreEqual(0.33m, (decimal)p33a);

            MultiPrecision<Pow2.N8> p33b = 0.33000000000000000000000000m;
            Assert.AreEqual(0.33m, (decimal)p33b);

            MultiPrecision<Pow2.N8> p33c = 0.33m;
            Assert.AreEqual(0.33m, (decimal)p33c);

            MultiPrecision<Pow2.N8> max = decimal.MaxValue;
            Assert.AreEqual((double)decimal.MaxValue, (double)max, 1e-5);

            MultiPrecision<Pow2.N8> eps = new decimal(1, 0, 0, false, 28);
            Assert.AreEqual(new decimal(1, 0, 0, false, 28), (decimal)eps);

            for (decimal i = 10; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Assert.AreEqual(i, (decimal)m);
            }

            for (decimal i = -10; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Assert.AreEqual(i, (decimal)m);
            }

            for (decimal i = 10; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = 1 / i;
                Assert.AreEqual(1 / i, (decimal)m);
            }

            for (decimal i = -10; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = 1 / i;
                Assert.AreEqual(1 / i, (decimal)m);
            }

            for (decimal i = 0, v = 1; i >= -28; i--, v /= 2) {
                MultiPrecision<Pow2.N4> m = v;
                Assert.AreEqual(i, m.Exponent);
                CollectionAssert.AreEqual(Mantissa<Pow2.N4>.One.Value, m.Mantissa);
            }

            for (decimal i = 0, v = 1; i >= -28; i--, v /= 2) {
                MultiPrecision<Pow2.N8> m = v;
                Assert.AreEqual(i, m.Exponent);
                CollectionAssert.AreEqual(Mantissa<Pow2.N8>.One.Value, m.Mantissa);
            }

            for (decimal i = 0, v = 1; i >= -28; i--, v /= 2) {
                MultiPrecision<Pow2.N16> m = v;
                Assert.AreEqual(i, m.Exponent);
                CollectionAssert.AreEqual(Mantissa<Pow2.N16>.One.Value, m.Mantissa);
            }

            for (decimal i = 0, v = 1; i <= 28; i++, v *= 2) {
                MultiPrecision<Pow2.N4> m = v;
                Assert.AreEqual(i, m.Exponent);
                CollectionAssert.AreEqual(Mantissa<Pow2.N4>.One.Value, m.Mantissa);
            }

            for (decimal i = 0, v = 1; i <= 28; i++, v *= 2) {
                MultiPrecision<Pow2.N8> m = v;
                Assert.AreEqual(i, m.Exponent);
                CollectionAssert.AreEqual(Mantissa<Pow2.N8>.One.Value, m.Mantissa);
            }

            for (decimal i = 0, v = 1; i <= 28; i++, v *= 2) {
                MultiPrecision<Pow2.N16> m = v;
                Assert.AreEqual(i, m.Exponent);
                CollectionAssert.AreEqual(Mantissa<Pow2.N16>.One.Value, m.Mantissa);
            }
        }

        [TestMethod]
        public void BigIntegerTest() {
            int x = 1234567890;

            BigInteger bigint = new BigInteger(x) * new BigInteger(x) * new BigInteger(x) * new BigInteger(x);

            MultiPrecision<Pow2.N8> mp1 = (MultiPrecision<Pow2.N8>)x * x * x * x;
            MultiPrecision<Pow2.N8> mp2 = bigint;

            Assert.AreEqual(mp1, mp2);
        }
    }
}
