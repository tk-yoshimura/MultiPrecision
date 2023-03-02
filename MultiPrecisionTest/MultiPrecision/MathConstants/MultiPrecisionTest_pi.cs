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

        [TestMethod]
        public void PIDigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.PI);
            Console.WriteLine(MultiPrecision<Pow2.N8>.PI.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.PI);
            Console.WriteLine(MultiPrecision<Pow2.N16>.PI.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.PI);
            Console.WriteLine(MultiPrecision<Pow2.N32>.PI.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.PI);
            Console.WriteLine(MultiPrecision<Pow2.N64>.PI.ToHexcode());
        }

        [TestMethod]
        public void RcpPITest() {
            MultiPrecision<Pow2.N8> rcp_pi = MultiPrecision<Pow2.N8>.RcpPI;

            Console.WriteLine(rcp_pi);
            Console.WriteLine(rcp_pi.ToHexcode());

            TestTool.Tolerance(1 / Math.PI, rcp_pi);
        }

        [TestMethod]
        public void RcpPIDigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.RcpPI);
            Console.WriteLine(MultiPrecision<Pow2.N8>.RcpPI.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.RcpPI);
            Console.WriteLine(MultiPrecision<Pow2.N16>.RcpPI.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.RcpPI);
            Console.WriteLine(MultiPrecision<Pow2.N32>.RcpPI.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.RcpPI);
            Console.WriteLine(MultiPrecision<Pow2.N64>.RcpPI.ToHexcode());
        }
    }
}
