using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MultiPrecision;
using System;

namespace MultiPrecisionBesselTest {
    [TestClass]
    public class BesselYTest {
        private const string outdir = "../../../results_bessel/bessel_y/";

        static BesselYTest() {
            Directory.CreateDirectory(outdir);
        }

        private static void CheckGridPoints<N>(string filepath) where N : struct, IConstant {
            using (StreamWriter sw = new StreamWriter(filepath)) {
                sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");

                MultiPrecision<N> z_threshold = MultiPrecision<N>.BesselJYApproxThreshold;

                sw.WriteLine($"z threshold: {z_threshold}");

                for (decimal nu = -64; nu <= 64; nu += 1 / 8m) {
                    sw.WriteLine($"nu: {nu}");

                    for (MultiPrecision<N> z = 0; z < MultiPrecision<N>.Min(4 + 1 / 8m, z_threshold - 4); z += 1 / 8m) {
                        Check(sw, nu, z);
                    }
                    for (MultiPrecision<N> z = MultiPrecision<N>.Max(0, z_threshold - 4); z <= z_threshold + 4; z += 1 / 8m) {
                        Check(sw, nu, z);
                    }
                }
            }
        }

        private static void CheckNearlyThreshold<N>(string filepath) where N : struct, IConstant {
            using (StreamWriter sw = new StreamWriter(filepath)) {
                sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");

                MultiPrecision<N> z_threshold = MultiPrecision<N>.BesselJYApproxThreshold;

                sw.WriteLine($"z threshold: {z_threshold}");

                for (decimal nu = -64; nu <= 64; nu += 1 / 8m) {
                    sw.WriteLine($"nu: {nu}");

                    Check(sw, nu, MultiPrecision<N>.BitDecrement(z_threshold));
                    Check(sw, nu, z_threshold);
                    Check(sw, nu, MultiPrecision<N>.BitIncrement(z_threshold));
                }
            }
        }

        private static void Check<N>(StreamWriter sw, decimal nu, MultiPrecision<N> z) where N : struct, IConstant {
            MultiPrecision<N> t = MultiPrecision<N>.BesselY(nu, z);
            MultiPrecision<Plus1<N>> s = MultiPrecision<Plus1<N>>.BesselY(nu, z.Convert<Plus1<N>>());

            sw.WriteLine($"  z: {z}");
            sw.WriteLine($"  {z.ToHexcode()}");
            sw.WriteLine($"  f: {t}");
            sw.WriteLine($"  {t.ToHexcode()}");
            sw.WriteLine($"  {s.ToHexcode()}");

            if (t.IsNaN && s.IsNaN) {
                return;
            }
            if (!t.IsFinite && !s.IsFinite && t.Sign == s.Sign) {
                return;
            }

            Assert.IsTrue(MultiPrecision<N>.NearlyEqualBits(t, s.Convert<N>(), 1));
        }

        [TestMethod]
        public void Length4GridPointsTest() {
            CheckGridPoints<Pow2.N4>(outdir + "n4_grid.txt");
        }

        [TestMethod]
        public void Length4NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N4>(outdir + "n4_near_threshold.txt");
        }

        [TestMethod]
        public void Length5GridPointsTest() {
            CheckGridPoints<Plus1<Pow2.N4>>(outdir + "n5_grid.txt");
        }

        [TestMethod]
        public void Length5NearlyThresholdTest() {
            CheckNearlyThreshold<Plus1<Pow2.N4>>(outdir + "n5_near_threshold.txt");
        }

        [TestMethod]
        public void Length8GridPointsTest() {
            CheckGridPoints<Pow2.N8>(outdir + "n8_grid.txt");
        }

        [TestMethod]
        public void Length8NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N8>(outdir + "n8_near_threshold.txt");
        }

        [TestMethod]
        public void Length16GridPointsTest() {
            CheckGridPoints<Pow2.N16>(outdir + "n16_grid.txt");
        }

        [TestMethod]
        public void Length16NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N16>(outdir + "n16_near_threshold.txt");
        }

        [TestMethod]
        public void Length32GridPointsTest() {
            CheckGridPoints<Pow2.N32>(outdir + "n32_grid.txt");
        }

        [TestMethod]
        public void Length32NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N32>(outdir + "n32_near_threshold.txt");
        }

        [TestMethod]
        public void Length64GridPointsTest() {
            CheckGridPoints<Pow2.N64>(outdir + "n64_grid.txt");
        }

        [TestMethod]
        public void Length64NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N64>(outdir + "n64_near_threshold.txt");
        }
    }
}
