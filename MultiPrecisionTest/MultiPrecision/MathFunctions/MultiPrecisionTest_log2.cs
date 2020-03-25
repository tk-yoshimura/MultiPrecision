using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Log2Test() {
            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log2((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(p);

                Console.WriteLine((double)p);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log2((double)p), (double)y, 1e-5);

                p *= 2;
            }

            MultiPrecision<Pow2.N8> n = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(n);

                Console.WriteLine((double)n);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log2((double)n), (double)y, 1e-5);

                n /= 2;
            }

            MultiPrecision<Pow2.N8> p2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(p2);

                Console.WriteLine((double)p2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log2((double)p2), (double)y, 1e-5);

                p2 *= 2;
            }

            MultiPrecision<Pow2.N8> n2 = 257;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(n2);

                Console.WriteLine((double)n2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log2((double)n2), (double)y, 1e-5);

                n2 /= 2;
            }
        }

        [TestMethod]
        public void Log2BorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0, 1, 2, 4 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(x);

                    if (y.IsNaN) {
                        continue;
                    }

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    if (double.IsInfinity(Math.Log2((double)x))) {
                        continue;
                    }

                    Assert.AreEqual(Math.Log2((double)x), (double)y, 1e-10);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void Log2UnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.BitDecrement(0),
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log2(v);

                Assert.IsTrue(y.IsNaN);
            }

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Log2(MultiPrecision<Pow2.N8>.PositiveInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);
        }
    }
}
