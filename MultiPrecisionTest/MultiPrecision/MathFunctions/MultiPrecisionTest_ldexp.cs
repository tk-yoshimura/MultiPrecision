using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void LdexpTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ldexp(x, 1);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance((double)x * 2, y);
            }

            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ldexp(x, -1);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance((double)x / 2, y);
            }

            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ldexp(x, 2);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance((double)x * 4, y);
            }

            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ldexp(x, -2);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance((double)x / 4, y);
            }
        }

        [TestMethod]
        public void LdexpUnnormalValueTest() {
            for (int p = -1; p <= +1; p++) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.NaN, p);

                Assert.IsTrue(y.IsNaN);

                MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.PositiveInfinity, p);

                Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);

                MultiPrecision<Pow2.N8> minf = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.NegativeInfinity, p);

                Assert.AreEqual(MultiPrecision<Pow2.N8>.NegativeInfinity, minf);
            }

            MultiPrecision<Pow2.N8> yeps = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Epsilon, -1);

            Assert.IsTrue(yeps.IsZero);
        }
    }
}
