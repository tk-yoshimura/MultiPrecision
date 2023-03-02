using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void SqrtTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sqrt(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Sqrt((double)x), y);
            }

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(
                MultiPrecision<Pow2.N8>.Sqrt2,
                MultiPrecision<Pow2.N8>.Sqrt(2), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(
                MultiPrecision<Pow2.N16>.Sqrt2,
                MultiPrecision<Pow2.N16>.Sqrt(2), 1));
        }

        [TestMethod]
        public void SqrtBorderTest() {
            MultiPrecision<Pow2.N8>[] borders_n8 = new MultiPrecision<Pow2.N8>[] { 0, 1, 4 };

            foreach (MultiPrecision<Pow2.N8> b in borders_n8) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    if (x.Sign == Sign.Minus) {
                        continue;
                    }

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sqrt(x);
                    MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Square(y);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.WriteLine(z);
                    Console.WriteLine(z.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Sqrt((double)x), y);

                    if (b != 0) {
                        Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(x, z, 1));
                    }

                    ys.Add(y);
                }

                if (b != 0) {
                    TestTool.NearlyNeighbors(ys, 1);
                }
                TestTool.SmoothnessSatisfied(ys, (b != 0) ? 1 : 10);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            MultiPrecision<Pow2.N16>[] borders_n16 = new MultiPrecision<Pow2.N16>[] { 0, 1, 4 };

            foreach (MultiPrecision<Pow2.N16> b in borders_n16) {
                List<MultiPrecision<Pow2.N16>> ys = new();

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor(b)) {
                    if (x.Sign == Sign.Minus) {
                        continue;
                    }

                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Sqrt(x);
                    MultiPrecision<Pow2.N16> z = MultiPrecision<Pow2.N16>.Square(y);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.WriteLine(z);
                    Console.WriteLine(z.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Sqrt((double)x), y);

                    if (b != 0) {
                        Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(x, z, 1));
                    }

                    ys.Add(y);
                }

                if (b != 0) {
                    TestTool.NearlyNeighbors(ys, 1);
                }
                TestTool.SmoothnessSatisfied(ys, (b != 0) ? 1 : 10);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }
    }
}
