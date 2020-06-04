using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void TanhTest() {
            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Tanh(x);
                double y_expect = Math.Tanh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }

            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = 65536 + (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Tanh(x);
                double y_expect = Math.Tanh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }

            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = -65536 + (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Tanh(x);
                double y_expect = Math.Tanh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }
        }

        [TestMethod]
        public void SinhTest() {
            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Sinh(x);
                double y_expect = Math.Sinh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }

            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = 65536 + (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Sinh(x);
                double y_expect = Math.Sinh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }

            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = -65536 + (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Sinh(x);
                double y_expect = Math.Sinh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }
        }

        [TestMethod]
        public void CoshTest() {
            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Cosh(x);
                double y_expect = Math.Cosh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }

            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = 65536 + (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Cosh(x);
                double y_expect = Math.Cosh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }

            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = -65536 + (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Cosh(x);
                double y_expect = Math.Cosh((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }
        }

        [TestMethod]
        public void TanhBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Tanh(x);

                    if (y.IsNaN) {
                        continue;
                    }

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    if (double.IsInfinity(Math.Tanh((double)x))) {
                        continue;
                    }

                    Assert.AreEqual(Math.Tanh((double)x), (double)y, 1e-10);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void SinhBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0, -1, 1 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sinh(x);

                    if (y.IsNaN) {
                        continue;
                    }

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    if (double.IsInfinity(Math.Sinh((double)x))) {
                        continue;
                    }

                    Assert.AreEqual(Math.Sinh((double)x), (double)y, 1e-10);
                }

                Console.Write("\n");
            }
        }
    }
}
