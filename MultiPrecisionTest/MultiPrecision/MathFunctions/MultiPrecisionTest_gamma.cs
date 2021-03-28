using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        public static class Approx {
            public static double Gamma(double z) {
                if (z <= 0 && z == Math.Truncate(z)) {
                    return double.NaN;
                }

                if (z < 0.5) {
                    return Math.PI / (Math.Sin(Math.PI * z) * Gamma(1 - z));
                }
                else {
                    double x = Ag(z);
                    double t = z + G - 1.5;

                    double y = Math.Sqrt(2 * Math.PI) * Math.Pow(t, z - 0.5) * Math.Exp(-t) * x;

                    return y;
                }
            }

            public static double LogGamma(double z) {
                if (!(z >= 0)) {
                    return double.NaN;
                }
                if (z == 0) {
                    return double.PositiveInfinity;
                }
                if (z < 4) {
                    return Math.Log(Gamma(z));
                }

                double x = Ag(z);
                double t = z + G - 1.5;

                double y = Math.Log(Math.Sqrt(2 * Math.PI)) + Math.Log(t) * (z - 0.5) - t + Math.Log(x);

                return y;
            }

            private static double Ag(double z) {
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

                double x = 0.99999999999980993;
                for (int i = 0; i < p.Length; i++) {
                    x += p[i] / (z + i);
                }

                return x;
            }

            private static double G => 8;
        }

        [TestMethod]
        public void GammaApproxTest() {
            Assert.AreEqual(1, Approx.Gamma(1), 1e-10);
            Assert.AreEqual(1, Approx.Gamma(2), 1e-10);
            Assert.AreEqual(2, Approx.Gamma(3), 1e-10);
            Assert.AreEqual(6, Approx.Gamma(4), 1e-10);
            Assert.AreEqual(24, Approx.Gamma(5), 1e-10);

            Assert.AreEqual(Math.Sqrt(Math.PI) * 4 / 3, Approx.Gamma(-1.5), 1e-10);
            Assert.AreEqual(Math.Sqrt(Math.PI) * -2, Approx.Gamma(-0.5), 1e-10);
            Assert.AreEqual(Math.Sqrt(Math.PI), Approx.Gamma(0.5), 1e-10);
            Assert.AreEqual(Math.Sqrt(Math.PI) / 2, Approx.Gamma(1.5), 1e-10);
            Assert.AreEqual(Math.Sqrt(Math.PI) * 3 / 4, Approx.Gamma(2.5), 1e-10);
            Assert.AreEqual(Math.Sqrt(Math.PI) * 15 / 8, Approx.Gamma(3.5), 1e-10);
        }

        [TestMethod]
        public void LogGammaApproxTest() {
            Assert.AreEqual(Math.Log(1), Approx.LogGamma(1), 1e-10);
            Assert.AreEqual(Math.Log(1), Approx.LogGamma(2), 1e-10);
            Assert.AreEqual(Math.Log(2), Approx.LogGamma(3), 1e-10);
            Assert.AreEqual(Math.Log(6), Approx.LogGamma(4), 1e-10);
            Assert.AreEqual(Math.Log(24), Approx.LogGamma(5), 1e-10);

            Assert.AreEqual(Math.Log(Math.Sqrt(Math.PI)), Approx.LogGamma(0.5), 1e-10);
            Assert.AreEqual(Math.Log(Math.Sqrt(Math.PI) / 2), Approx.LogGamma(1.5), 1e-10);
            Assert.AreEqual(Math.Log(Math.Sqrt(Math.PI) * 3 / 4), Approx.LogGamma(2.5), 1e-10);
            Assert.AreEqual(Math.Log(Math.Sqrt(Math.PI) * 15 / 8), Approx.LogGamma(3.5), 1e-10);
        }

        [TestMethod]
        public void LogGammaTest() {
            for (int i = 1; i < 200; i++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -2) * i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Approx.LogGamma((double)x), y, rateerr: 1e-10, ignore_sign: true);
            }

            Assert.IsTrue(0 == MultiPrecision<Pow2.N8>.LogGamma(1));
            Assert.IsTrue(0 == MultiPrecision<Pow2.N8>.LogGamma(2));
            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Log(2),
                    MultiPrecision<Pow2.N8>.LogGamma(3),
                    "1e-50"
                ), $"{MultiPrecision<Pow2.N8>.Log(2)}, { MultiPrecision<Pow2.N8>.LogGamma(3)}");

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Log(6),
                    MultiPrecision<Pow2.N8>.LogGamma(4),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Log(24),
                    MultiPrecision<Pow2.N8>.LogGamma(5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Log(MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI)),
                    MultiPrecision<Pow2.N8>.LogGamma(0.5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Log(MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI) / 2),
                    MultiPrecision<Pow2.N8>.LogGamma(1.5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Log(MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI) * 3 / 4),
                    MultiPrecision<Pow2.N8>.LogGamma(2.5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Log(MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI) * 15 / 8),
                    MultiPrecision<Pow2.N8>.LogGamma(3.5),
                    "1e-50"
                ));
        }

        [TestMethod]
        public void GammaTest() {
            for (int i = -200; i < 200; i++) {
                if (i <= 0 && i % 4 == 0) {
                    continue;
                }

                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.One, -2) * i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Approx.Gamma((double)x), y, rateerr: 1e-10, ignore_sign: true);
            }

            Assert.IsTrue(1 == MultiPrecision<Pow2.N8>.Gamma(1));
            Assert.IsTrue(1 == MultiPrecision<Pow2.N8>.Gamma(2));
            Assert.IsTrue(2 == MultiPrecision<Pow2.N8>.Gamma(3));
            Assert.IsTrue(6 == MultiPrecision<Pow2.N8>.Gamma(4));
            Assert.IsTrue(24 == MultiPrecision<Pow2.N8>.Gamma(5));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI) * 4 / 3,
                    MultiPrecision<Pow2.N8>.Gamma(-1.5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI) * -2,
                    MultiPrecision<Pow2.N8>.Gamma(-0.5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI),
                    MultiPrecision<Pow2.N8>.Gamma(0.5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI) / 2,
                    MultiPrecision<Pow2.N8>.Gamma(1.5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI) * 3 / 4,
                    MultiPrecision<Pow2.N8>.Gamma(2.5),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI) * 15 / 8,
                    MultiPrecision<Pow2.N8>.Gamma(3.5),
                    "1e-50"
                ));
        }

        [TestMethod]
        public void LogGammaBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                1 - MultiPrecision<Pow2.N8>.Ldexp(1, -31),
                1 + MultiPrecision<Pow2.N8>.Ldexp(1, -31),
                2 - MultiPrecision<Pow2.N8>.Ldexp(1, -31),
                2 + MultiPrecision<Pow2.N8>.Ldexp(1, -31),
                MultiPrecision<Pow2.N8>.Ldexp(1, -2),
                0, 1, 2, 3, 4, 32, 64 
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    if (x <= 0) {
                        continue;
                    }

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    if ((double)x > 0) {
                        TestTool.Tolerance(Approx.LogGamma((double)x), y, ignore_sign: true);
                    }
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void GammaBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0.5, 1 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Approx.Gamma((double)x), y, ignore_sign: true);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void GammaApproxBorderTest() {
            foreach (MultiPrecision<Pow2.N4> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N4>)150, 4)) {

                MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), MultiPrecision<Pow2.N4>.Log(y), ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N8>)100, 4)) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), MultiPrecision<Pow2.N8>.Log(y), ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N16>)128, 4)) {

                MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), MultiPrecision<Pow2.N16>.Log(y), ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N32> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N32>)250, 4)) {

                MultiPrecision<Pow2.N32> y = MultiPrecision<Pow2.N32>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), MultiPrecision<Pow2.N32>.Log(y), ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N64> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N64>)472, 4)) {

                MultiPrecision<Pow2.N64> y = MultiPrecision<Pow2.N64>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), MultiPrecision<Pow2.N64>.Log(y), ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N128> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N128>)936, 4)) {

                MultiPrecision<Pow2.N128> y = MultiPrecision<Pow2.N128>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), MultiPrecision<Pow2.N128>.Log(y), ignore_sign: true);
            }
        }

        [TestMethod]
        public void LogGammaApproxBorderTest() {
            foreach (MultiPrecision<Pow2.N4> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N4>)150, 4)) {

                MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), y, ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N8>)100, 4)) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), y, ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N16>)128, 4)) {

                MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), y, ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N32> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N32>)250, 4)) {

                MultiPrecision<Pow2.N32> y = MultiPrecision<Pow2.N32>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), y, ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N64> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N64>)472, 4)) {

                MultiPrecision<Pow2.N64> y = MultiPrecision<Pow2.N64>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), y, ignore_sign: true);
            }

            foreach (MultiPrecision<Pow2.N128> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N128>)936, 4)) {

                MultiPrecision<Pow2.N128> y = MultiPrecision<Pow2.N128>.LogGamma(x);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                TestTool.Tolerance(Approx.LogGamma((double)x), y, ignore_sign: true);
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
                MultiPrecision<Plus1<Pow2.N4>> x = i;
                MultiPrecision<Plus1<Pow2.N4>> y = MultiPrecision<Plus1<Pow2.N4>>.Gamma(x);

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
                MultiPrecision<Plus1<Pow2.N8>> x = i;
                MultiPrecision<Plus1<Pow2.N8>> y = MultiPrecision<Plus1<Pow2.N8>>.Gamma(x);

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
                MultiPrecision<Plus1<Pow2.N16>> x = i;
                MultiPrecision<Plus1<Pow2.N16>> y = MultiPrecision<Plus1<Pow2.N16>>.Gamma(x);

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
                MultiPrecision<Plus1<Pow2.N32>> x = i;
                MultiPrecision<Plus1<Pow2.N32>> y = MultiPrecision<Plus1<Pow2.N32>>.Gamma(x);

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

            for (int i = 1, f = 1; i <= 4; i++, f *= i - 1) {
                MultiPrecision<Plus1<Pow2.N64>> x = i;
                MultiPrecision<Plus1<Pow2.N64>> y = MultiPrecision<Plus1<Pow2.N64>>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                Assert.AreEqual(f, y);
            }

            for (int i = 1, f = 1; i <= 4; i++, f *= i - 1) {
                MultiPrecision<Pow2.N128> x = i;
                MultiPrecision<Pow2.N128> y = MultiPrecision<Pow2.N128>.Gamma(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.Write("\n");

                Assert.AreEqual(f, y);
            }
        }

        [TestMethod]
        public void GammaUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] nans = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                0
                -1,
                -2,
                MultiPrecision<Pow2.N8>.NegativeInfinity
            };

            foreach (MultiPrecision<Pow2.N8> v in nans) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(v);

                Assert.IsTrue(y.IsNaN);
            }

            MultiPrecision<Pow2.N8>[] infs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.PositiveInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in infs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Gamma(v);

                Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, y);
            }
        }

        [TestMethod]
        public void LogGammaUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] nans = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                0,
                -0.5,
                -1,
                -2,
                MultiPrecision<Pow2.N8>.NegativeInfinity
            };

            foreach (MultiPrecision<Pow2.N8> v in nans) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(v);

                Assert.IsTrue(y.IsNaN);
            }

            MultiPrecision<Pow2.N8>[] infs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.PositiveInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in infs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.LogGamma(v);

                Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, y);
            }
        }
    }
}
