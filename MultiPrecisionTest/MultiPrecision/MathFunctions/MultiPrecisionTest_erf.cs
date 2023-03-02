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
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2, ignore_sign: true);
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
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(ErfcApprox((double)x), y, minerr: 1e-2, ignore_sign: true);
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

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "2.088487583762544757000786294957788611560818119321163727012213714e-45",
                    MultiPrecision<Pow2.N8>.Erfc(10),
                    "1e-95"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "2.070920778841656048448447875165788792932250920995399683766235534e-1088",
                    MultiPrecision<Pow2.N8>.Erfc(50),
                    "1e-1138"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "6.405961424921732039021339148586394148214414399460338057767107650e-4346",
                    MultiPrecision<Pow2.N8>.Erfc(100),
                    "1e-4396"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "2.703823745452247856854931734255359377805490749772707768226770978e-108577",
                    MultiPrecision<Pow2.N8>.Erfc(500),
                    "1e-108627"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.860037048632323370908471162289984373206233539926488472200712370e-434298",
                    MultiPrecision<Pow2.N8>.Erfc(1000),
                    "1e-434348"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.011285440970039477031542173737947576392096400158950800837668262e-10857366",
                    MultiPrecision<Pow2.N8>.Erfc(5000),
                    "1e-10857416"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "3.639987386564198052838435239316275600299066983050320050544858948e-43429453",
                    MultiPrecision<Pow2.N8>.Erfc(10000),
                    "1e-43429303"
                ));

            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEquals(
                    "1.969361711624327154389174953238654441985348792810296010912403861e-1085736210",
                    MultiPrecision<Pow2.N8>.Erfc(50000),
                    "1e-1085736260"
                ));
        }

        [TestMethod]
        public void ErfBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0, 1 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N4> b in new MultiPrecision<Pow2.N4>[] { 8 }) {
                List<MultiPrecision<Pow2.N4>> ys = new();

                foreach (MultiPrecision<Pow2.N4> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 1);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N8> b in new MultiPrecision<Pow2.N8>[] { 8 }) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 1);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N16> b in new MultiPrecision<Pow2.N16>[] { 8 }) {
                List<MultiPrecision<Pow2.N16>> ys = new();

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 1);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N32> b in new MultiPrecision<Pow2.N32>[] { 8 }) {
                List<MultiPrecision<Pow2.N32>> ys = new();

                foreach (MultiPrecision<Pow2.N32> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N32> y = MultiPrecision<Pow2.N32>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 1);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N64> b in new MultiPrecision<Pow2.N64>[] { 8 }) {
                List<MultiPrecision<Pow2.N64>> ys = new();

                foreach (MultiPrecision<Pow2.N64> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N64> y = MultiPrecision<Pow2.N64>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 1);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N128> b in new MultiPrecision<Pow2.N128>[] { 8 }) {
                List<MultiPrecision<Pow2.N128>> ys = new();

                foreach (MultiPrecision<Pow2.N128> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N128> y = MultiPrecision<Pow2.N128>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 1);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N256> b in new MultiPrecision<Pow2.N256>[] { 8 }) {
                List<MultiPrecision<Pow2.N256>> ys = new();

                foreach (MultiPrecision<Pow2.N256> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N256> y = MultiPrecision<Pow2.N256>.Erf(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 1);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void ErfcBorderTest() {
            foreach (MultiPrecision<Pow2.N4> b in new MultiPrecision<Pow2.N4>[] { 8 }) {
                List<MultiPrecision<Pow2.N4>> ys = new();

                foreach (MultiPrecision<Pow2.N4> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.Erfc(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfcApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 12);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N8> b in new MultiPrecision<Pow2.N8>[] { 8 }) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Erfc(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfcApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 12);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N16> b in new MultiPrecision<Pow2.N16>[] { 8 }) {
                List<MultiPrecision<Pow2.N16>> ys = new();

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Erfc(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfcApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 12);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N32> b in new MultiPrecision<Pow2.N32>[] { 8 }) {
                List<MultiPrecision<Pow2.N32>> ys = new();

                foreach (MultiPrecision<Pow2.N32> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N32> y = MultiPrecision<Pow2.N32>.Erfc(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfcApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 12);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N64> b in new MultiPrecision<Pow2.N64>[] { 8 }) {
                List<MultiPrecision<Pow2.N64>> ys = new();

                foreach (MultiPrecision<Pow2.N64> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N64> y = MultiPrecision<Pow2.N64>.Erfc(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfcApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 12);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N128> b in new MultiPrecision<Pow2.N128>[] { 8 }) {
                List<MultiPrecision<Pow2.N128>> ys = new();

                foreach (MultiPrecision<Pow2.N128> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N128> y = MultiPrecision<Pow2.N128>.Erfc(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfcApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 12);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N256> b in new MultiPrecision<Pow2.N256>[] { 8 }) {
                List<MultiPrecision<Pow2.N256>> ys = new();

                foreach (MultiPrecision<Pow2.N256> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N256> y = MultiPrecision<Pow2.N256>.Erfc(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(ErfcApprox((double)x), y, minerr: 1e-2);

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 12);
                TestTool.SmoothnessSatisfied(ys, 0.5);
                TestTool.MonotonicitySatisfied(ys);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void ErfUnnormalValueTest() {
            Assert.AreEqual(-1, MultiPrecision<Pow2.N8>.Erf(MultiPrecision<Pow2.N8>.NegativeInfinity));
            Assert.AreEqual(+1, MultiPrecision<Pow2.N8>.Erf(MultiPrecision<Pow2.N8>.PositiveInfinity));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Erf(MultiPrecision<Pow2.N8>.NaN).IsNaN);
        }

        [TestMethod]
        public void ErfcUnnormalValueTest() {
            Assert.AreEqual(2, MultiPrecision<Pow2.N8>.Erfc(MultiPrecision<Pow2.N8>.NegativeInfinity));
            Assert.AreEqual(0, MultiPrecision<Pow2.N8>.Erfc(MultiPrecision<Pow2.N8>.PositiveInfinity));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Erfc(MultiPrecision<Pow2.N8>.NaN).IsNaN);
        }

        [TestMethod]
        public void ErfLargeValueTest() {
            for (decimal z = 2; z <= 20; z += 0.125m) {
                MultiPrecision<Pow2.N8> y_n8 = MultiPrecision<Pow2.N8>.Erf(z);
                MultiPrecision<Pow2.N16> y_n16 = MultiPrecision<Pow2.N16>.Erf(z);

                MultiPrecision<Pow2.N8> y_n8_m = 1 - MultiPrecision<Pow2.N8>.Erfc(z);
                MultiPrecision<Pow2.N16> y_n16_m = 1 - MultiPrecision<Pow2.N16>.Erfc(z);

                Console.WriteLine($"erf({z})=");
                Console.WriteLine(y_n8);
                Console.WriteLine(y_n16);
                Console.WriteLine(y_n8.ToHexcode());
                Console.WriteLine(y_n16.ToHexcode());

                Console.WriteLine(y_n8_m.ToHexcode());
                Console.WriteLine(y_n16_m.ToHexcode());

                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y_n8, y_n8_m, 1));
                Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(y_n16, y_n16_m, 1));
            }
        }
    }
}
