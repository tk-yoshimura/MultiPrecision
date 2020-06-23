using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Constants {
    [TestClass]
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void PITest() {
            MultiPrecision<Pow2.N8> pi = MultiPrecision<Pow2.N8>.PI;

            Console.WriteLine(pi);
            Console.WriteLine(pi.ToHexcode());

            TestTool.Tolerance(Math.PI, pi);
        }
    }
}
