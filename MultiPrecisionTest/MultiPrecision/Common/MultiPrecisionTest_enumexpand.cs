using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void EnumStatsTest() {
            MultiPrecision<Pow2.N8>[] xs = new MultiPrecision<Pow2.N8>[] { 1, 3, 6, 2, 4 };

            Assert.AreEqual(16, xs.Sum());
            Assert.AreEqual(16 / 5.0, (double)xs.Average(), 1e-10);
            Assert.AreEqual(2.96, (double)xs.Variance(), 1e-10);
        }
    }
}
