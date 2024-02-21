using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Log2Test() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Log2((double)x), y);
            }
        }

        [TestMethod]
        public void Log2BorderTest() {
            MultiPrecision<Pow2.N8>[] borders = [0, 1, 2, 4];

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = [];

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    if (x.Sign == Sign.Minus) {
                        continue;
                    }

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(x);

                    if (MultiPrecision<Pow2.N8>.IsFinite(y)) {
                        ys.Add(y);
                    }

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    if ((double)x == 0) {
                        continue;
                    }

                    TestTool.Tolerance(Math.Log2((double)x), y, ignore_sign: true);
                }

                if (b != 1 && b != 2) {
                    TestTool.NearlyNeighbors(ys, 2);
                }
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void Log2UnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = [
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.BitDecrement(0),
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            ];

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(v);

                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(y));
            }

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Log2(MultiPrecision<Pow2.N8>.PositiveInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);
        }

        internal struct N12 : IConstant {
            public readonly int Value => 12;
        }

        [TestMethod]
        public void Log2OverFlowTest() {
            for (double exponent = -1; exponent >= -32; exponent -= 1d / 32) {
                MultiPrecision<N12> x = MultiPrecision<N12>.Pow2(exponent);
                MultiPrecision<N12> x_inc = MultiPrecision<N12>.BitIncrement(x);
                MultiPrecision<N12> x_dec = MultiPrecision<N12>.BitDecrement(x);

                MultiPrecision<N12> y = MultiPrecision<N12>.Log2(x);
                MultiPrecision<N12> y_inc = MultiPrecision<N12>.Log2(x_inc);
                MultiPrecision<N12> y_dec = MultiPrecision<N12>.Log2(x_dec);

                Console.WriteLine(y);
                Console.WriteLine(y_inc);
                Console.WriteLine(y_dec);

                Assert.AreEqual(exponent, (double)y);
                Assert.AreEqual(exponent, (double)y_inc);
                Assert.AreEqual(exponent, (double)y_dec);

                Assert.IsTrue(MultiPrecision<N12>.NearlyEqualBits(exponent, y, 1));
                Assert.IsTrue(MultiPrecision<N12>.NearlyEqualBits(exponent, y_inc, 2));
                Assert.IsTrue(MultiPrecision<N12>.NearlyEqualBits(exponent, y_dec, 2));
            }
        }
    }
}
