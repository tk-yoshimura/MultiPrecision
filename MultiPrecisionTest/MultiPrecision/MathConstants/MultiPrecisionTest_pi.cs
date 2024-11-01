using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Constants {
    [TestClass]
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void PiTest() {
            MultiPrecision<Pow2.N8> pi = MultiPrecision<Pow2.N8>.Pi;

            Console.WriteLine(pi);
            Console.WriteLine(pi.ToHexcode());

            TestTool.Tolerance(Math.PI, pi);
        }

        [TestMethod]
        public void PiDigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.Pi);
            Console.WriteLine(MultiPrecision<Pow2.N8>.Pi.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.Pi);
            Console.WriteLine(MultiPrecision<Pow2.N16>.Pi.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.Pi);
            Console.WriteLine(MultiPrecision<Pow2.N32>.Pi.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.Pi);
            Console.WriteLine(MultiPrecision<Pow2.N64>.Pi.ToHexcode());
        }

        [TestMethod]
        public void RcpPiTest() {
            MultiPrecision<Pow2.N8> rcp_pi = MultiPrecision<Pow2.N8>.RcpPi;

            Console.WriteLine(rcp_pi);
            Console.WriteLine(rcp_pi.ToHexcode());

            TestTool.Tolerance(1 / Math.PI, rcp_pi);
        }

        [TestMethod]
        public void RcpPiDigitsTest() {
            Console.WriteLine(MultiPrecision<Pow2.N8>.RcpPi);
            Console.WriteLine(MultiPrecision<Pow2.N8>.RcpPi.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N16>.RcpPi);
            Console.WriteLine(MultiPrecision<Pow2.N16>.RcpPi.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N32>.RcpPi);
            Console.WriteLine(MultiPrecision<Pow2.N32>.RcpPi.ToHexcode());

            Console.WriteLine(MultiPrecision<Pow2.N64>.RcpPi);
            Console.WriteLine(MultiPrecision<Pow2.N64>.RcpPi.ToHexcode());
        }
    }
}
