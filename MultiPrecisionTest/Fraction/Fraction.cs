using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

namespace MultiPrecisionTest.Fractions {
    [TestClass]
    public partial class FractionTest {
        [TestMethod]
        public void CreateTest() {
            Fraction v1 = new Fraction(6, 12);

            Assert.AreEqual(1, v1.Numer);
            Assert.AreEqual(2, v1.Denom);

            Fraction v2 = new Fraction(-6, 12);

            Assert.AreEqual(-1, v2.Numer);
            Assert.AreEqual(2, v2.Denom);

            Fraction v3 = new Fraction(6, 12);

            Assert.AreEqual(1, v3.Numer);
            Assert.AreEqual(2, v3.Denom);

            Fraction v4 = new Fraction(6, -12);

            Assert.AreEqual(-1, v4.Numer);
            Assert.AreEqual(2, v4.Denom);
        }

        [TestMethod]
        public void AddTest() {
            Fraction v1 = new Fraction(1, 2) + new Fraction(1, 3);

            Assert.AreEqual(5, v1.Numer);
            Assert.AreEqual(6, v1.Denom);

            Fraction v2 = new Fraction(1, 2) + new Fraction(1, 2);

            Assert.AreEqual(1, v2.Numer);
            Assert.AreEqual(1, v2.Denom);

            Fraction v3 = new Fraction(2, 1) + new Fraction(1, 2);

            Assert.AreEqual(5, v3.Numer);
            Assert.AreEqual(2, v3.Denom);

            Fraction v4 = new Fraction(1, 6) + new Fraction(1, 4);

            Assert.AreEqual(5, v4.Numer);
            Assert.AreEqual(12, v4.Denom);

            Fraction v5 = new Fraction(1, 2) + new Fraction(-1, 3);

            Assert.AreEqual(1, v5.Numer);
            Assert.AreEqual(6, v5.Denom);

            Fraction v6 = new Fraction(1, 2) + new Fraction(-1, 2);

            Assert.AreEqual(0, v6.Numer);
            Assert.AreEqual(1, v6.Denom);

            Fraction v7 = new Fraction(2, 1) + new Fraction(-1, 2);

            Assert.AreEqual(3, v7.Numer);
            Assert.AreEqual(2, v7.Denom);

            Fraction v8 = new Fraction(1, 6) + new Fraction(-1, 4);

            Assert.AreEqual(-1, v8.Numer);
            Assert.AreEqual(12, v8.Denom);
        }

        [TestMethod]
        public void SubTest() {
            Fraction v1 = new Fraction(1, 2) - new Fraction(1, 3);

            Assert.AreEqual(1, v1.Numer);
            Assert.AreEqual(6, v1.Denom);

            Fraction v2 = new Fraction(1, 2) - new Fraction(1, 2);

            Assert.AreEqual(0, v2.Numer);
            Assert.AreEqual(1, v2.Denom);

            Fraction v3 = new Fraction(2, 1) - new Fraction(1, 2);

            Assert.AreEqual(3, v3.Numer);
            Assert.AreEqual(2, v3.Denom);

            Fraction v4 = new Fraction(1, 6) - new Fraction(1, 4);

            Assert.AreEqual(-1, v4.Numer);
            Assert.AreEqual(12, v4.Denom);

            Fraction v5 = new Fraction(1, 2) - new Fraction(-1, 3);

            Assert.AreEqual(5, v5.Numer);
            Assert.AreEqual(6, v5.Denom);

            Fraction v6 = new Fraction(1, 2) - new Fraction(-1, 2);

            Assert.AreEqual(1, v6.Numer);
            Assert.AreEqual(1, v6.Denom);

            Fraction v7 = new Fraction(2, 1) - new Fraction(-1, 2);

            Assert.AreEqual(5, v7.Numer);
            Assert.AreEqual(2, v7.Denom);

            Fraction v8 = new Fraction(1, 6) - new Fraction(-1, 4);

            Assert.AreEqual(5, v8.Numer);
            Assert.AreEqual(12, v8.Denom);
        }

        [TestMethod]
        public void MulTest() {
            Fraction v1 = new Fraction(1, 2) * new Fraction(1, 3);

            Assert.AreEqual(1, v1.Numer);
            Assert.AreEqual(6, v1.Denom);

            Fraction v2 = new Fraction(1, 2) * new Fraction(1, 2);

            Assert.AreEqual(1, v2.Numer);
            Assert.AreEqual(4, v2.Denom);

            Fraction v3 = new Fraction(2, 1) * new Fraction(1, 2);

            Assert.AreEqual(1, v3.Numer);
            Assert.AreEqual(1, v3.Denom);

            Fraction v4 = new Fraction(1, 6) * new Fraction(1, 4);

            Assert.AreEqual(1, v4.Numer);
            Assert.AreEqual(24, v4.Denom);

            Fraction v5 = new Fraction(1, 2) * new Fraction(-1, 3);

            Assert.AreEqual(-1, v5.Numer);
            Assert.AreEqual(6, v5.Denom);

            Fraction v6 = new Fraction(1, 2) * new Fraction(-1, 2);

            Assert.AreEqual(-1, v6.Numer);
            Assert.AreEqual(4, v6.Denom);

            Fraction v7 = new Fraction(2, 1) * new Fraction(-1, 2);

            Assert.AreEqual(-1, v7.Numer);
            Assert.AreEqual(1, v7.Denom);

            Fraction v8 = new Fraction(1, 6) * new Fraction(-1, 4);

            Assert.AreEqual(-1, v8.Numer);
            Assert.AreEqual(24, v8.Denom);
        }

        [TestMethod]
        public void DivTest() {
            Fraction v1 = new Fraction(1, 2) / new Fraction(1, 3);

            Assert.AreEqual(3, v1.Numer);
            Assert.AreEqual(2, v1.Denom);

            Fraction v2 = new Fraction(1, 2) / new Fraction(1, 2);

            Assert.AreEqual(1, v2.Numer);
            Assert.AreEqual(1, v2.Denom);

            Fraction v3 = new Fraction(2, 1) / new Fraction(1, 2);

            Assert.AreEqual(4, v3.Numer);
            Assert.AreEqual(1, v3.Denom);

            Fraction v4 = new Fraction(1, 6) / new Fraction(1, 4);

            Assert.AreEqual(2, v4.Numer);
            Assert.AreEqual(3, v4.Denom);

            Fraction v5 = new Fraction(1, 2) / new Fraction(-1, 3);

            Assert.AreEqual(-3, v5.Numer);
            Assert.AreEqual(2, v5.Denom);

            Fraction v6 = new Fraction(1, 2) / new Fraction(-1, 2);

            Assert.AreEqual(-1, v6.Numer);
            Assert.AreEqual(1, v6.Denom);

            Fraction v7 = new Fraction(2, 1) / new Fraction(-1, 2);

            Assert.AreEqual(-4, v7.Numer);
            Assert.AreEqual(1, v7.Denom);

            Fraction v8 = new Fraction(1, 6) / new Fraction(-1, 4);

            Assert.AreEqual(-2, v8.Numer);
            Assert.AreEqual(3, v8.Denom);
        }
    }
}
