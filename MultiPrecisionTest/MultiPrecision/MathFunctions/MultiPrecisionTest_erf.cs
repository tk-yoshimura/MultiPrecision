using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Collections.Generic;

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
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(ErfApprox((double)x), y, minerr:1e-2, ignore_sign: true);
            }
        }

        [TestMethod]
        public void ErfcTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(ErfcApprox((double)x), y, minerr:1e-2, ignore_sign: true);
            }
        }

        [TestMethod]
        public void ErfBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { -2, 2, 1, 0, -1, -2 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr:1e-2);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void ErfcBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 2, 1, 0, -1, -2 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfcApprox((double)x), y, minerr:1e-2);
                }

                Console.Write("\n");
            }
        }

        //[TestMethod]
        public void ErfcConvergenceTest() {
            MultiPrecision<Pow2.N8> x = 2;
            MultiPrecision<Pow2.N8> erfc2 = 1 - MultiPrecision<Pow2.N8>.Erf(x);

            List<MultiPrecision<Pow2.N8>> errs = new List<MultiPrecision<Pow2.N8>>();

            for (int i = MultiPrecision<Pow2.N8>.Length * MultiPrecision<Pow2.N8>.Length * 14; i <= MultiPrecision<Pow2.N8>.Length * MultiPrecision<Pow2.N8>.Length * 16; i++) {
                MultiPrecision<Pow2.N8> z = x * MultiPrecision<Pow2.N8>.Sqrt2;
                MultiPrecision<Pow2.N8> a = 0;

                for (long n = i; n > 0; n--) {
                    a = n / (z + a);
                }

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(-x * x) / (z + a) * MultiPrecision<Pow2.N8>.Sqrt(2 / MultiPrecision<Pow2.N8>.PI);

                MultiPrecision<Pow2.N8> err = erfc2 - y;

                errs.Add(err);

                Console.WriteLine($"{i},{err:E12}");
            }
        }

        //[TestMethod]
        public void ErfcConvergence2Test() {

            const int max_cnt = 5;

            for (decimal d = 2; d <= 5; d += 0.0625m) {
                MultiPrecision<Pow2.N8> x = d;

                MultiPrecision<Pow2.N8> prev = 0;
                int cnt = 0;

                for (int i = MultiPrecision<Pow2.N8>.Length * MultiPrecision<Pow2.N8>.Length * 4; i <= MultiPrecision<Pow2.N8>.Length * MultiPrecision<Pow2.N8>.Length * 16; i++) {
                    MultiPrecision<Pow2.N8> z = x * MultiPrecision<Pow2.N8>.Sqrt2;
                    MultiPrecision<Pow2.N8> a = 0;

                    for (long n = i; n > 0; n--) {
                        a = n / (z + a);
                    }

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(-x * x) / (z + a) * MultiPrecision<Pow2.N8>.Sqrt(2 / MultiPrecision<Pow2.N8>.PI);

                    if (prev == y) {
                        cnt++;
                    }
                    else {
                        prev = y;
                        cnt = 0;
                    }

                    if (cnt >= max_cnt) {
                        Console.WriteLine($"{x},{i - max_cnt},{y.ToHexcode()}");
                        break;
                    }
                }

                if (cnt < max_cnt) {
                    Console.WriteLine($"{x},N/A");
                }
            }
        }
    }
}
