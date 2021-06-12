using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace MultiPrecisionUtilTests {
    [TestClass()]
    public class RombergIntegrateTests {
        [TestMethod()]
        public void LogIntegrateTest() {
            (MultiPrecision<Pow2.N4> autual, _) = MultiPrecisionUtil.RombergIntegrate<Pow2.N4>(MultiPrecision<Pow2.N4>.Log, 1, 2);

            MultiPrecision<Pow2.N4> expected = 2 * MultiPrecision<Pow2.N4>.Log(2) - 1;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, autual, 8));
        }
    }
}
