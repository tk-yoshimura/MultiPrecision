using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace MultiPrecisionUtilTests {
    [TestClass()]
    public class RootFinderTests {
        [TestMethod()]
        public void NewtonRaphsonRootFindingTest() {
            MultiPrecision<Pow2.N8> f(MultiPrecision<Pow2.N8> x) {
                return x * x * x - 2;
            }
            MultiPrecision<Pow2.N8> df(MultiPrecision<Pow2.N8> x) {
                return 3 * x * x;
            }

            MultiPrecision<Pow2.N8> y = MultiPrecisionUtil.NewtonRaphsonRootFinding<Pow2.N8>(2, f, df);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y, MultiPrecision<Pow2.N8>.Cbrt(2), 1));
        }

        [TestMethod()]
        public void HalleyRootFindingTest() {
            MultiPrecision<Pow2.N8> f(MultiPrecision<Pow2.N8> x) {
                return x * x * x - 2;
            }
            (MultiPrecision<Pow2.N8> d1, MultiPrecision<Pow2.N8> d2) df(MultiPrecision<Pow2.N8> x) {
                return (3 * x * x, 6 * x);
            }

            MultiPrecision<Pow2.N8> y = MultiPrecisionUtil.HalleyRootFinding<Pow2.N8>(2, f, df);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y, MultiPrecision<Pow2.N8>.Cbrt(2), 1));
        }
    }
}