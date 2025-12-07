using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Numerics;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void IsNormalTest() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNormal(0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNormal(-0d));
            Assert.AreEqual(double.IsNormal(1d), MultiPrecision<Pow2.N8>.IsNormal(1d));
            Assert.AreEqual(double.IsNormal(-1d), MultiPrecision<Pow2.N8>.IsNormal(-1d));
            Assert.AreEqual(double.IsNormal(double.MaxValue), MultiPrecision<Pow2.N8>.IsNormal(double.MaxValue));
            Assert.AreEqual(double.IsNormal(double.MinValue), MultiPrecision<Pow2.N8>.IsNormal(double.MinValue));
            Assert.AreEqual(double.IsNormal(double.PositiveInfinity), MultiPrecision<Pow2.N8>.IsNormal(double.PositiveInfinity));
            Assert.AreEqual(double.IsNormal(double.NegativeInfinity), MultiPrecision<Pow2.N8>.IsNormal(double.NegativeInfinity));
            Assert.AreEqual(double.IsNormal(double.NaN), MultiPrecision<Pow2.N8>.IsNormal(double.NaN));
        }

        [TestMethod]
        public void IsCanonicalTest() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsCanonical(0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsCanonical(-0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsCanonical(1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsCanonical(-1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsCanonical(double.MaxValue));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsCanonical(double.MinValue));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsCanonical(double.PositiveInfinity));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsCanonical(double.NegativeInfinity));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsCanonical(double.NaN));
        }

        [TestMethod]
        public void IsComplexTest() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsRealNumber(0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsRealNumber(-0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsRealNumber(1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsRealNumber(-1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsRealNumber(double.MaxValue));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsRealNumber(double.MinValue));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsRealNumber(double.PositiveInfinity));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsRealNumber(double.NegativeInfinity));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsRealNumber(double.NaN));

            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(0d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(-0d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(1d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(-1d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(double.MaxValue));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(double.MinValue));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(double.PositiveInfinity));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(double.NegativeInfinity));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsImaginaryNumber(double.NaN));

            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(0d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(-0d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(1d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(-1d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(double.MaxValue));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(double.MinValue));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(double.PositiveInfinity));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(double.NegativeInfinity));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsComplexNumber(double.NaN));
        }

        [TestMethod]
        public void IsPositiveNegativeTest() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositive(0d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsPositive(-0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositive(0f));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsPositive(-0f));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositive(1d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsPositive(-1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositive(double.MaxValue));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsPositive(double.MinValue));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositive(double.PositiveInfinity));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsPositive(double.NegativeInfinity));

            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNegative(0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegative(-0d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNegative(0f));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegative(-0f));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNegative(1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegative(-1d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNegative(double.MaxValue));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegative(double.MinValue));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsNegative(double.PositiveInfinity));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegative(double.NegativeInfinity));
        }

        [TestMethod]
        public void IsIntegerTest() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(-0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(-1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(2d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(-2d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(3d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(-3d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(4d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(-4d));

            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsOddInteger(0d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsOddInteger(-0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsOddInteger(1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsOddInteger(-1d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsOddInteger(2d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsOddInteger(-2d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsOddInteger(3d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsOddInteger(-3d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsOddInteger(4d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsOddInteger(-4d));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsEvenInteger(0d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsEvenInteger(-0d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsEvenInteger(1d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsEvenInteger(-1d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsEvenInteger(2d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsEvenInteger(-2d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsEvenInteger(3d));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.IsEvenInteger(-3d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsEvenInteger(4d));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsEvenInteger(-4d));

            for (BigInteger n = 10; n.ToString().Length <= 40; n *= 10) {
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(n));
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsInteger(-n));

                Assert.IsFalse(MultiPrecision<Pow2.N8>.IsOddInteger(n));
                Assert.IsFalse(MultiPrecision<Pow2.N8>.IsOddInteger(-n));

                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsEvenInteger(n));
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsEvenInteger(-n));
            }

            for (BigInteger n = 10; n.ToString().Length <= 31; n *= 10) {
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsOddInteger(n + 1));
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsOddInteger(-n - 1));

                Assert.IsFalse(MultiPrecision<Pow2.N8>.IsEvenInteger(n + 1));
                Assert.IsFalse(MultiPrecision<Pow2.N8>.IsEvenInteger(-n - 1));
            }
        }

        [TestMethod]
        public void MagnitudeTest() {
            Assert.AreEqual((MultiPrecision<Pow2.N8>)4, MultiPrecision<Pow2.N8>.MaxMagnitude(3, 4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)(-4), MultiPrecision<Pow2.N8>.MaxMagnitude(3, -4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)4, MultiPrecision<Pow2.N8>.MaxMagnitude(-3, 4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)(-4), MultiPrecision<Pow2.N8>.MaxMagnitude(-3, -4));

            Assert.AreEqual(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.MaxMagnitude(3, MultiPrecision<Pow2.N8>.NaN));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.MaxMagnitude(MultiPrecision<Pow2.N8>.NaN, 4));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.MaxMagnitude(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.NaN));

            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, MultiPrecision<Pow2.N8>.MinMagnitude(3, 4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, MultiPrecision<Pow2.N8>.MinMagnitude(3, -4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)(-3), MultiPrecision<Pow2.N8>.MinMagnitude(-3, 4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)(-3), MultiPrecision<Pow2.N8>.MinMagnitude(-3, -4));

            Assert.AreEqual(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.MinMagnitude(3, MultiPrecision<Pow2.N8>.NaN));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.MinMagnitude(MultiPrecision<Pow2.N8>.NaN, 4));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.MinMagnitude(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.NaN));

            Assert.AreEqual((MultiPrecision<Pow2.N8>)4, MultiPrecision<Pow2.N8>.MaxMagnitudeNumber(3, 4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)(-4), MultiPrecision<Pow2.N8>.MaxMagnitudeNumber(3, -4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)4, MultiPrecision<Pow2.N8>.MaxMagnitudeNumber(-3, 4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)(-4), MultiPrecision<Pow2.N8>.MaxMagnitudeNumber(-3, -4));

            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, MultiPrecision<Pow2.N8>.MaxMagnitudeNumber(3, MultiPrecision<Pow2.N8>.NaN));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)4, MultiPrecision<Pow2.N8>.MaxMagnitudeNumber(MultiPrecision<Pow2.N8>.NaN, 4));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.MaxMagnitudeNumber(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.NaN));

            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, MultiPrecision<Pow2.N8>.MinMagnitudeNumber(3, 4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, MultiPrecision<Pow2.N8>.MinMagnitudeNumber(3, -4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)(-3), MultiPrecision<Pow2.N8>.MinMagnitudeNumber(-3, 4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)(-3), MultiPrecision<Pow2.N8>.MinMagnitudeNumber(-3, -4));

            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, MultiPrecision<Pow2.N8>.MinMagnitudeNumber(3, MultiPrecision<Pow2.N8>.NaN));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)4, MultiPrecision<Pow2.N8>.MinMagnitudeNumber(MultiPrecision<Pow2.N8>.NaN, 4));
            Assert.AreEqual(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.MinMagnitudeNumber(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.NaN));
        }

        [TestMethod]
        public void TryConvertFromChecked() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertFromChecked((int)3, out MultiPrecision<Pow2.N8> v1));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, v1);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertFromChecked((long)3, out MultiPrecision<Pow2.N8> v2));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, v2);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertFromChecked((float)3, out MultiPrecision<Pow2.N8> v3));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, v3);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertFromChecked((double)3, out MultiPrecision<Pow2.N8> v4));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, v4);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertFromChecked((BigInteger)3, out MultiPrecision<Pow2.N8> v5));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)3, v5);

            Assert.IsFalse(MultiPrecision<Pow2.N8>.TryConvertFromChecked((char)3, out MultiPrecision<Pow2.N8> v6));
            Assert.AreEqual((MultiPrecision<Pow2.N8>)0, v6);
        }

        [TestMethod]
        public void TryConvertToChecked() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToChecked(3, out int v1));
            Assert.AreEqual((int)3, v1);
            Assert.ThrowsExactly<OverflowException>(() => {
                _ = MultiPrecision<Pow2.N8>.TryConvertToChecked(MultiPrecision<Pow2.N8>.MaxValue, out int _);
            });

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToChecked(3, out long v2));
            Assert.AreEqual((long)3, v2);
            Assert.ThrowsExactly<OverflowException>(() => {
                _ = MultiPrecision<Pow2.N8>.TryConvertToChecked(MultiPrecision<Pow2.N8>.MaxValue, out long _);
            });

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToChecked(3, out float v3));
            Assert.AreEqual((float)3, v3);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToChecked(MultiPrecision<Pow2.N8>.MaxValue, out float vmax3));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(vmax3));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToChecked(3, out double v4));
            Assert.AreEqual((double)3, v4);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToChecked(MultiPrecision<Pow2.N8>.MaxValue, out double vmax4));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(vmax4));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToChecked(3, out decimal v5));
            Assert.AreEqual((decimal)3, v5);
            Assert.ThrowsExactly<OverflowException>(() => {
                _ = MultiPrecision<Pow2.N8>.TryConvertToChecked(MultiPrecision<Pow2.N8>.MaxValue, out decimal _);
            });
        }

        [TestMethod]
        public void TryConvertToSaturating() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(3, out int v1));
            Assert.AreEqual((int)3, v1);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MaxValue, out int vmax1));
            Assert.AreEqual(int.MaxValue, vmax1);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MinValue, out int vmin1));
            Assert.AreEqual(int.MinValue, vmin1);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(3, out long v2));
            Assert.AreEqual((long)3, v2);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MaxValue, out long vmax2));
            Assert.AreEqual(long.MaxValue, vmax2);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MinValue, out long vmin2));
            Assert.AreEqual(long.MinValue, vmin2);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(3, out float v3));
            Assert.AreEqual((float)3, v3);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MaxValue, out float vmax3));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(vmax3));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MinValue, out float vmin3));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegativeInfinity(vmin3));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(3, out double v4));
            Assert.AreEqual((double)3, v4);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MaxValue, out double vmax4));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(vmax4));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MinValue, out double vmin4));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegativeInfinity(vmin4));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(3, out decimal v5));
            Assert.AreEqual((decimal)3, v5);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MaxValue, out decimal vmax5));
            Assert.AreEqual(decimal.MaxValue, vmax5);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.TryConvertToSaturating(MultiPrecision<Pow2.N8>.MinValue, out decimal vmin5));
            Assert.AreEqual(decimal.MinValue, vmin5);
        }
    }
}
