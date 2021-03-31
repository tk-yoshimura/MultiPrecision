using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void LogTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Log((double)x), y);
            }

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(
                1, 
                MultiPrecision<Pow2.N8>.Log(MultiPrecision<Pow2.N8>.E), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(
                1, 
                MultiPrecision<Pow2.N16>.Log(MultiPrecision<Pow2.N16>.E), 1));
        }

        [TestMethod]
        public void Log1pTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log1p(x - 1);

                Console.WriteLine(x - 1);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Log((double)x), y);
            }
        }

        [TestMethod]
        public void Log1pBorderTest() {
            MultiPrecision<Pow2.N8>[] borders_n8 = new MultiPrecision<Pow2.N8>[] { -0.25, 0, 0.25 };

            foreach (MultiPrecision<Pow2.N8> b in borders_n8) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, n: 4)) {

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log1p(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Log(1 + (double)x), y, ignore_sign: true);

                    ys.Add(y);
                }

                if (b != 0) {
                    TestTool.NearlyNeighbors(ys, 2);
                }
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            MultiPrecision<Pow2.N16>[] borders_n16 = new MultiPrecision<Pow2.N16>[] { -0.25, 0, 0.25 };

            foreach (MultiPrecision<Pow2.N16> b in borders_n16) {
                List<MultiPrecision<Pow2.N16>> ys = new();

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor(b, n: 4)) {

                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Log1p(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Log(1 + (double)x), y, ignore_sign: true);

                    ys.Add(y);
                }

                if (b != 0) {
                    TestTool.NearlyNeighbors(ys, 2);
                }
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }
    }
}
