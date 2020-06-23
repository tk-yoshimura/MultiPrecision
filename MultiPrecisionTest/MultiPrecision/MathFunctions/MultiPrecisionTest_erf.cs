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

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "-9.999999845827420997199811478403265131159514278547464108088316571e-1",
                    MultiPrecision<Pow2.N8>.Erf(-4),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "-9.999779095030014145586272238704176796201522929126007503427610452e-1",
                    MultiPrecision<Pow2.N8>.Erf(-3),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "-9.953222650189527341620692563672529286108917970400600767383523262e-1",
                    MultiPrecision<Pow2.N8>.Erf(-2),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "-8.427007929497148693412206350826092592960669979663029084599378978e-1",
                    MultiPrecision<Pow2.N8>.Erf(-1),
                    "1e-50"
                ));

            Assert.AreEqual(0, MultiPrecision<Pow2.N8>.Erf(0));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "8.427007929497148693412206350826092592960669979663029084599378978e-1",
                    MultiPrecision<Pow2.N8>.Erf(1),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "9.953222650189527341620692563672529286108917970400600767383523262e-1",
                    MultiPrecision<Pow2.N8>.Erf(2),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "9.999779095030014145586272238704176796201522929126007503427610452e-1",
                    MultiPrecision<Pow2.N8>.Erf(3),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "9.999999845827420997199811478403265131159514278547464108088316571e-1",
                    MultiPrecision<Pow2.N8>.Erf(4),
                    "1e-50"
                ));
        }

        [TestMethod]
        public void ErfcTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(ErfcApprox((double)x), y, minerr:1e-2, ignore_sign: true);
            }

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.9999999845827420997199811478403265131159514278547464108088316571e0",
                    MultiPrecision<Pow2.N8>.Erfc(-4),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.9999779095030014145586272238704176796201522929126007503427610452e0",
                    MultiPrecision<Pow2.N8>.Erfc(-3),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.9953222650189527341620692563672529286108917970400600767383523262e0",
                    MultiPrecision<Pow2.N8>.Erfc(-2),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.8427007929497148693412206350826092592960669979663029084599378978e0",
                    MultiPrecision<Pow2.N8>.Erfc(-1),
                    "1e-50"
                ));

            Assert.AreEqual(1, MultiPrecision<Pow2.N8>.Erfc(0));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.572992070502851306587793649173907407039330020336970915400621022e-1",
                    MultiPrecision<Pow2.N8>.Erfc(1),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "4.677734981047265837930743632747071389108202959939923261647673800e-3",
                    MultiPrecision<Pow2.N8>.Erfc(2),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "2.209049699858544137277612958232037984770708739924965723895484294e-5",
                    MultiPrecision<Pow2.N8>.Erfc(3),
                    "1e-50"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.541725790028001885215967348688404857214525358919116834290499421e-8",
                    MultiPrecision<Pow2.N8>.Erfc(4),
                    "1e-50"
                ));
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
