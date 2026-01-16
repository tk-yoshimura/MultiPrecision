using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System.Linq;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void EnumStatsTest() {
            MultiPrecision<Pow2.N8>[] xs = [1, 3, 6, 2, 4];

            Assert.AreEqual(16, xs.Sum());
            Assert.AreEqual(16 / 5.0, (double)xs.Average(), 1e-10);
            Assert.AreEqual(2.96, (double)xs.Variance(), 1e-10);
        }

        [TestMethod]
        public void EnumStatsMinMaxTest() {
            MultiPrecision<Pow2.N8>[] xs = [3, 1, 6, 2, 4];
            MultiPrecision<Pow2.N8>[] xs_withnan = [3, 1, 6, 2, 4, double.NaN];
            MultiPrecision<Pow2.N8>[] xs_none = Enumerable.Empty<MultiPrecision<Pow2.N8>>().ToArray();

            Assert.AreEqual(16, xs.Sum());
            Assert.AreEqual(16 / 5.0, (double)xs.Average(), 1e-10);
            Assert.AreEqual(1, xs.Min());
            Assert.AreEqual(6, xs.Max());

            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(xs_withnan.Min()));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(xs_withnan.Max()));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(xs_none.Min()));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(xs_none.Max()));

            Assert.AreEqual(1, xs.MinIndex());
            Assert.AreEqual(2, xs.MaxIndex());

            Assert.AreEqual(-1, xs_withnan.MinIndex());
            Assert.AreEqual(-1, xs_withnan.MaxIndex());

            Assert.AreEqual(-1, xs_none.MinIndex());
            Assert.AreEqual(-1, xs_none.MaxIndex());
        }
    }
}
