using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace MultiPrecisionUtilTests {
    [TestClass()]
    public class RombergIntegrateTests {
        [TestMethod()]
        public void LogIntegrateTest() {
            (MultiPrecision<Pow2.N4> actual, _) = MultiPrecisionUtil.RombergIntegrate<Pow2.N4>(MultiPrecision<Pow2.N4>.Log, 1, 2);

            MultiPrecision<Pow2.N4> expected = 2 * MultiPrecision<Pow2.N4>.Log(2) - 1;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 1));
        }

        [TestMethod()]
        public void ExpIntegrateTest() {
            (MultiPrecision<Pow2.N4> actual, _) = MultiPrecisionUtil.RombergIntegrate<Pow2.N4>(MultiPrecision<Pow2.N4>.Exp, 1, 2);

            MultiPrecision<Pow2.N4> expected = MultiPrecision<Pow2.N4>.E * (MultiPrecision<Pow2.N4>.E - 1);

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 1));
        }

        [TestMethod()]
        public void SinIntegrateTest() {
            (MultiPrecision<Pow2.N4> actual, _) = MultiPrecisionUtil.RombergIntegrate<Pow2.N4>(MultiPrecision<Pow2.N4>.SinPI, 0, 1);

            MultiPrecision<Pow2.N4> expected = 2 / MultiPrecision<Pow2.N4>.PI;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 1));
        }

        [TestMethod()]
        public void PolyIntegrateTest() {
            (MultiPrecision<Pow2.N4> actual, _) = MultiPrecisionUtil.RombergIntegrate<Pow2.N4>(MultiPrecision<Pow2.N4>.Square, 0, 1);

            MultiPrecision<Pow2.N4> expected = MultiPrecision<Pow2.N4>.One / 3;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 1));
        }

        [TestMethod()]
        public void ZeroIntegrateTest() {
            (MultiPrecision<Pow2.N4> actual, _) = MultiPrecisionUtil.RombergIntegrate<Pow2.N4>(MultiPrecision<Pow2.N4>.Cube, -1, 1);

            MultiPrecision<Pow2.N4> expected = 0;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 1));
        }
    }
}
