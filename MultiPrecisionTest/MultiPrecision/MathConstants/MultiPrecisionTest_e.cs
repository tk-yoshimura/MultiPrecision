using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Constants {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ETest() {
            MultiPrecision<Pow2.N8> e = MultiPrecision<Pow2.N8>.E;

            Console.WriteLine(e);
            Console.WriteLine(e.ToHexcode());

            TestTool.Tolerance(Math.E, e);
        }
    }
}
