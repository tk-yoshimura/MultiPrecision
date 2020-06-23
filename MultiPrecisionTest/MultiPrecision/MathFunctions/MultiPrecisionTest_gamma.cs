using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        public static double GammaApprox(double z) {
            double[] p = {
                676.5203681218851,
                -1259.1392167224028,
                771.32342877765313,
                -176.61502916214059,
                12.507343278686905,
                -0.13857109526572012,
                9.9843695780195716e-6,
                1.5056327351493116e-7
            };

            if(z <= 0 && z == Math.Truncate(z)) {
                return double.NaN;
            }

            if (z < 0.5) {
                return Math.PI / (Math.Sin(Math.PI * z) * GammaApprox(1 - z));
            }
            else {
                z -= 1;
                double x = 1 - double.Epsilon;
                for (int i = 0; i < p.Length; i++) {
                    x += p[i] / (z + i + 1);
                }
                double t = z + p.Length - 0.5;

                return Math.Sqrt(2 * Math.PI) * Math.Pow(t, (z + 0.5)) * Math.Exp(-t) * x;
            }
        }

        public static double LogGammaApprox(double z) {
            return z >= 0 ? Math.Log(GammaApprox(z)) : double.NaN;
        }

        [TestMethod]
        public void GammaApproxTest() {
            Assert.AreEqual(1, GammaApprox(1), 1e-5);
            Assert.AreEqual(1, GammaApprox(2), 1e-5);
            Assert.AreEqual(2, GammaApprox(3), 1e-5);
            Assert.AreEqual(6, GammaApprox(4), 1e-5);
            Assert.AreEqual(24, GammaApprox(5), 1e-5);

            Assert.AreEqual(Math.Sqrt(Math.PI) * 4 / 3, GammaApprox(-1.5), 1e-5);
            Assert.AreEqual(Math.Sqrt(Math.PI) * -2, GammaApprox(-0.5), 1e-5);
            Assert.AreEqual(Math.Sqrt(Math.PI), GammaApprox(0.5), 1e-5);
            Assert.AreEqual(Math.Sqrt(Math.PI) / 2, GammaApprox(1.5), 1e-5);
            Assert.AreEqual(Math.Sqrt(Math.PI) * 3 / 4, GammaApprox(2.5), 1e-5);
            Assert.AreEqual(Math.Sqrt(Math.PI) * 15 / 8, GammaApprox(3.5), 1e-5);
        }

        [TestMethod]
        public void LogGammaTest() {
            for (int i = 1; i < 200; i++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -2) * i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(LogGammaApprox((double)x), y, rateerr:1e-5, ignore_sign: true);
            }
        }

        [TestMethod]
        public void GammaTest() {
            for (int i = -200; i < 200; i++) {
                if(i <= 0 && i % 4 == 0) { 
                    continue;
                }

                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -2) * i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(GammaApprox((double)x), y, rateerr:1e-5, ignore_sign: true);
            }
        }

        [TestMethod]
        public void LogGammaBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0, 1, 3, 4, 32, 64 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    if(x <= 0) { 
                        continue;
                    }

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(LogGammaApprox((double)x), y, ignore_sign: true);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void GammaBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 1, 2, 3, 4, 32, 64 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    if(x <= 0 && x == MultiPrecision<Pow2.N8>.Truncate(x)) { 
                        continue;
                    }

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(GammaApprox((double)x), y, ignore_sign: true);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void GammaIntegerTest() {
            for (int i = 1, f = 1; i <= 4; i++, f *= i - 1) {
                MultiPrecision<Pow2.N4> x = i;
                MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");
                
                Assert.AreEqual(f, y);
            }

            for (int i = 1, f = 1; i <= 4; i++, f *= i - 1) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                Assert.AreEqual(f, y);
            }

            for (int i = 1, f = 1; i <= 4; i++, f *= i - 1) {
                MultiPrecision<Pow2.N16> x = i;
                MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                Assert.AreEqual(f, y);
            }

            for (int i = 1, f = 1; i <= 4; i++, f *= i - 1) {
                MultiPrecision<Pow2.N32> x = i;
                MultiPrecision<Pow2.N32> y = MultiPrecision<Pow2.N32>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                Assert.AreEqual(f, y);
            }

            for (int i = 1, f = 1; i <= 4; i++, f *= i - 1) {
                MultiPrecision<Pow2.N64> x = i;
                MultiPrecision<Pow2.N64> y = MultiPrecision<Pow2.N64>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                Assert.AreEqual(f, y);
            }
        }
    }
}
