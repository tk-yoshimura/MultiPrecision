using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Common {

    [TestClass]
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CreateTest() {
            Int64 min_exponent = (Int64)MultiPrecision<Pow2.N8>.ExponentMin - (Int64)MultiPrecision<Pow2.N8>.ExponentZero;
            Int64 one_exponent = min_exponent + 1;
            Int64 inf_exponent = (Int64)MultiPrecision<Pow2.N8>.ExponentMax - (Int64)MultiPrecision<Pow2.N8>.ExponentZero;
            Int64 max_exponent = inf_exponent - 1;

            {
                MultiPrecision<Pow2.N8> n1 = new MultiPrecision<Pow2.N8>(Sign.Plus, 0, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n2 = new MultiPrecision<Pow2.N8>(Sign.Minus, 0, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n3 = new MultiPrecision<Pow2.N8>(Sign.Plus, +1, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n4 = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n5 = new MultiPrecision<Pow2.N8>(Sign.Plus, -1, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n6 = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n7 = new MultiPrecision<Pow2.N8>(Sign.Plus, min_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n8 = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n9 = new MultiPrecision<Pow2.N8>(Sign.Plus, one_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus, max_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus, inf_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.Zero, round: false);

                Assert.AreEqual(0, (double)n1);
                Assert.AreEqual(-0, (double)n2);
                Assert.AreEqual(0, (double)n3);
                Assert.AreEqual(-0, (double)n4);
                Assert.AreEqual(0, (double)n5);
                Assert.AreEqual(-0, (double)n6);
                Assert.AreEqual(0, (double)n7);
                Assert.AreEqual(-0, (double)n8);
                Assert.AreEqual(0, (double)n9);
                Assert.AreEqual(-0, (double)n10);
                Assert.AreEqual(0, (double)n11);
                Assert.AreEqual(-0, (double)n12);
                Assert.AreEqual(0, (double)n13);
                Assert.AreEqual(-0, (double)n14);
            }

            {
                MultiPrecision<Pow2.N8> n1 = new MultiPrecision<Pow2.N8>(Sign.Plus, 0, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n2 = new MultiPrecision<Pow2.N8>(Sign.Minus, 0, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n3 = new MultiPrecision<Pow2.N8>(Sign.Plus, +1, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n4 = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n5 = new MultiPrecision<Pow2.N8>(Sign.Plus, -1, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n6 = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n7 = new MultiPrecision<Pow2.N8>(Sign.Plus, min_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n8 = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n9 = new MultiPrecision<Pow2.N8>(Sign.Plus, one_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus, max_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus, inf_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.One, round: false);

                Assert.AreEqual(1, (double)n1);
                Assert.AreEqual(-1, (double)n2);
                Assert.AreEqual(2, (double)n3);
                Assert.AreEqual(-2, (double)n4);
                Assert.AreEqual(0.5, (double)n5);
                Assert.AreEqual(-0.5, (double)n6);
                Assert.AreEqual(0, (double)n7);
                Assert.AreEqual(-0, (double)n8);
                Assert.AreEqual(0, (double)n9);
                Assert.AreEqual(-0, (double)n10);
                Assert.AreEqual(double.PositiveInfinity, (double)n11);
                Assert.AreEqual(double.NegativeInfinity, (double)n12);
                Assert.AreEqual(double.PositiveInfinity, (double)n13);
                Assert.AreEqual(double.NegativeInfinity, (double)n14);
            }

            {
                MultiPrecision<Pow2.N8> n1 = new MultiPrecision<Pow2.N8>(Sign.Plus, 0, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n2 = new MultiPrecision<Pow2.N8>(Sign.Minus, 0, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n3 = new MultiPrecision<Pow2.N8>(Sign.Plus, +1, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n4 = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n5 = new MultiPrecision<Pow2.N8>(Sign.Plus, -1, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n6 = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n7 = new MultiPrecision<Pow2.N8>(Sign.Plus, min_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n8 = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n9 = new MultiPrecision<Pow2.N8>(Sign.Plus, one_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus, max_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus, inf_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.Full, round: false);

                Assert.AreEqual(2, (double)n1);
                Assert.AreEqual(-2, (double)n2);
                Assert.AreEqual(4, (double)n3);
                Assert.AreEqual(-4, (double)n4);
                Assert.AreEqual(1, (double)n5);
                Assert.AreEqual(-1, (double)n6);
                Assert.AreEqual(0, (double)n7);
                Assert.AreEqual(-0, (double)n8);
                Assert.AreEqual(0, (double)n9);
                Assert.AreEqual(-0, (double)n10);
                Assert.AreEqual(double.PositiveInfinity, (double)n11);
                Assert.AreEqual(double.NegativeInfinity, (double)n12);
                Assert.AreEqual(double.PositiveInfinity, (double)n13);
                Assert.AreEqual(double.NegativeInfinity, (double)n14);
            }

            {
                MultiPrecision<Pow2.N8> n1 = new MultiPrecision<Pow2.N8>(Sign.Plus, 0, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n2 = new MultiPrecision<Pow2.N8>(Sign.Minus, 0, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n3 = new MultiPrecision<Pow2.N8>(Sign.Plus, +1, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n4 = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n5 = new MultiPrecision<Pow2.N8>(Sign.Plus, -1, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n6 = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n7 = new MultiPrecision<Pow2.N8>(Sign.Plus, min_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n8 = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n9 = new MultiPrecision<Pow2.N8>(Sign.Plus, one_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus, max_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus, inf_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.Zero, round: true);

                Assert.AreEqual(0, (double)n1);
                Assert.AreEqual(-0, (double)n2);
                Assert.AreEqual(0, (double)n3);
                Assert.AreEqual(-0, (double)n4);
                Assert.AreEqual(0, (double)n5);
                Assert.AreEqual(-0, (double)n6);
                Assert.AreEqual(0, (double)n7);
                Assert.AreEqual(-0, (double)n8);
                Assert.AreEqual(0, (double)n9);
                Assert.AreEqual(-0, (double)n10);
                Assert.AreEqual(0, (double)n11);
                Assert.AreEqual(-0, (double)n12);
                Assert.AreEqual(0, (double)n13);
                Assert.AreEqual(-0, (double)n14);
            }

            {
                MultiPrecision<Pow2.N8> n1 = new MultiPrecision<Pow2.N8>(Sign.Plus, 0, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n2 = new MultiPrecision<Pow2.N8>(Sign.Minus, 0, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n3 = new MultiPrecision<Pow2.N8>(Sign.Plus, +1, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n4 = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n5 = new MultiPrecision<Pow2.N8>(Sign.Plus, -1, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n6 = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n7 = new MultiPrecision<Pow2.N8>(Sign.Plus, min_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n8 = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n9 = new MultiPrecision<Pow2.N8>(Sign.Plus, one_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus, max_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus, inf_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.One, round: true);

                Assert.AreEqual(1, (double)n1);
                Assert.AreEqual(-1, (double)n2);
                Assert.AreEqual(2, (double)n3);
                Assert.AreEqual(-2, (double)n4);
                Assert.AreEqual(0.5, (double)n5);
                Assert.AreEqual(-0.5, (double)n6);
                Assert.AreEqual(0, (double)n7);
                Assert.AreEqual(-0, (double)n8);
                Assert.AreEqual(0, (double)n9);
                Assert.AreEqual(-0, (double)n10);
                Assert.AreEqual(double.PositiveInfinity, (double)n11);
                Assert.AreEqual(double.NegativeInfinity, (double)n12);
                Assert.AreEqual(double.PositiveInfinity, (double)n13);
                Assert.AreEqual(double.NegativeInfinity, (double)n14);
            }

            {
                MultiPrecision<Pow2.N8> n1 = new MultiPrecision<Pow2.N8>(Sign.Plus, 0, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n2 = new MultiPrecision<Pow2.N8>(Sign.Minus, 0, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n3 = new MultiPrecision<Pow2.N8>(Sign.Plus, +1, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n4 = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n5 = new MultiPrecision<Pow2.N8>(Sign.Plus, -1, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n6 = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n7 = new MultiPrecision<Pow2.N8>(Sign.Plus, min_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n8 = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n9 = new MultiPrecision<Pow2.N8>(Sign.Plus, one_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus, max_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus, inf_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.Full, round: true);

                Assert.AreEqual(2, (double)n1);
                Assert.AreEqual(-2, (double)n2);
                Assert.AreEqual(4, (double)n3);
                Assert.AreEqual(-4, (double)n4);
                Assert.AreEqual(1, (double)n5);
                Assert.AreEqual(-1, (double)n6);
                Assert.AreEqual(0, (double)n7);
                Assert.AreEqual(-0, (double)n8);
                Assert.AreEqual(0, (double)n9);
                Assert.AreEqual(-0, (double)n10);
                Assert.AreEqual(double.PositiveInfinity, (double)n11);
                Assert.AreEqual(double.NegativeInfinity, (double)n12);
                Assert.AreEqual(double.PositiveInfinity, (double)n13);
                Assert.AreEqual(double.NegativeInfinity, (double)n14);
            }
        }
    }
}
