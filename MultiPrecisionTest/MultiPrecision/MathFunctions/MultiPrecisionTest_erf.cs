using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        public static double ErfApprox(double x) { 
            double z = Math.Exp(-x * x);

            double c = Math.Sqrt(Math.PI) / 2 + z * 31 / 200 - z * z * 341 / 8000;

            return 2 / Math.Sqrt(Math.PI) * Math.Sign(x) * Math.Sqrt(1 - z) * c;
        }

        public static double ErfcApprox(double x) { 
            return 1 - ErfApprox(x);
        }

        [TestMethod]
        public void ErfApproxTest() {
            Assert.AreEqual(0, ErfApprox(0));
            Assert.AreEqual(0.84270079294971487, ErfApprox(1), 1e-2);
            Assert.AreEqual(0.99532226501895273, ErfApprox(2), 1e-2);
            Assert.AreEqual(-0.84270079294971487, ErfApprox(-1), 1e-2);
            Assert.AreEqual(-0.99532226501895273, ErfApprox(-2), 1e-2);
        }

        [TestMethod]
        public void ErfcApproxTest() {
            Assert.AreEqual(1, ErfcApprox(0));
            Assert.AreEqual(0.15729920705028513, ErfcApprox(1), 1e-2);
            Assert.AreEqual(0.0046777349810472658, ErfcApprox(2), 1e-2);
            Assert.AreEqual(2 - 0.15729920705028513, ErfcApprox(-1), 1e-2);
            Assert.AreEqual(2 - 0.0046777349810472658, ErfcApprox(-2), 1e-2);
        }

        [TestMethod]
        public void ErfTest() {
            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                Console.WriteLine((double)x);
                Console.WriteLine(y);
                Assert.AreEqual(ErfApprox((double)x), (double)y, 1e-2);
            }

            MultiPrecision<Pow2.N8> p = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(p);

                Console.WriteLine((double)p);
                Console.WriteLine(y);
                Assert.AreEqual(ErfApprox((double)p), (double)y, 1e-2);

                p *= 2;
            }

            MultiPrecision<Pow2.N8> n = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(n);

                Console.WriteLine((double)n);
                Console.WriteLine(y);
                Assert.AreEqual(ErfApprox((double)n), (double)y, 1e-2);

                n /= 2;
            }

            MultiPrecision<Pow2.N8> p2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(p2);

                Console.WriteLine((double)p2);
                Console.WriteLine(y);
                Assert.AreEqual(ErfApprox((double)p2), (double)y, 1e-2);

                p2 *= 2;
            }

            MultiPrecision<Pow2.N8> n2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(n2);

                Console.WriteLine((double)n2);
                Console.WriteLine((double)y);
                Assert.AreEqual(ErfApprox((double)n2), (double)y, 1e-2);

                n2 /= 2;
            }

            MultiPrecision<Pow2.N8> p1 = (MultiPrecision<Pow2.N8>)(1) / 10;
            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(ErfApprox((double)x), (double)y, 1e-2);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(ErfApprox((double)x), (double)y, 1e-2);
            }
        }

        [TestMethod]
        public void ErfcTest() {
            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                Console.WriteLine((double)x);
                Console.WriteLine(y);
                Assert.AreEqual(ErfcApprox((double)x), (double)y, 1e-2);
            }

            MultiPrecision<Pow2.N8> p = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(p);

                Console.WriteLine((double)p);
                Console.WriteLine(y);
                Assert.AreEqual(ErfcApprox((double)p), (double)y, 1e-2);

                p *= 2;
            }

            MultiPrecision<Pow2.N8> n = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(n);

                Console.WriteLine((double)n);
                Console.WriteLine(y);
                Assert.AreEqual(ErfcApprox((double)n), (double)y, 1e-2);

                n /= 2;
            }

            MultiPrecision<Pow2.N8> p2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(p2);

                Console.WriteLine((double)p2);
                Console.WriteLine(y);
                Assert.AreEqual(ErfcApprox((double)p2), (double)y, 1e-2);

                p2 *= 2;
            }

            MultiPrecision<Pow2.N8> n2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(n2);

                Console.WriteLine((double)n2);
                Console.WriteLine((double)y);
                Assert.AreEqual(ErfcApprox((double)n2), (double)y, 1e-2);

                n2 /= 2;
            }

            MultiPrecision<Pow2.N8> p1 = (MultiPrecision<Pow2.N8>)(1) / 10;
            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(ErfcApprox((double)x), (double)y, 1e-2);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(ErfcApprox((double)x), (double)y, 1e-2);
            }
        }

        [TestMethod]
        public void ErfBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0, -1, 1, -2, 2 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                    if (y.IsNaN) {
                        continue;
                    }

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(ErfApprox((double)x), (double)y, 1e-2);
                    Assert.AreEqual(Math.Sign(ErfApprox((double)x)), Math.Sign((double)y));
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void ErfcBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 2, 0, -1, 1, -2, 2 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                    if (y.IsNaN) {
                        continue;
                    }

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(ErfcApprox((double)x), (double)y, 1e-2);
                    Assert.AreEqual(Math.Sign(ErfcApprox((double)x)), Math.Sign((double)y));
                }

                Console.Write("\n");
            }
        }
    }
}
