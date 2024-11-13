using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CosHalfPiTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.ShortRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosHalfPi(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Cos((double)x * Math.PI / 2), y, minerr: 1e-5, ignore_sign: true);
            }

            for (int x = -31; x <= 31; x += 2) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosHalfPi(x);

                Assert.AreEqual(MultiPrecision<Pow2.N8>.Zero, y);
                Assert.AreEqual(Sign.Plus, y.Sign);
            }

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(
                MultiPrecision<Pow2.N8>.Sqrt2 / 2,
                MultiPrecision<Pow2.N8>.CosHalfPi(MultiPrecision<Pow2.N8>.Div(1, 2)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(
                MultiPrecision<Pow2.N8>.Sqrt(3) / 2,
                MultiPrecision<Pow2.N8>.CosHalfPi(MultiPrecision<Pow2.N8>.Div(1, 3)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(
                MultiPrecision<Pow2.N8>.Point5,
                MultiPrecision<Pow2.N8>.CosHalfPi(MultiPrecision<Pow2.N8>.Div(2, 3)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(
                MultiPrecision<Pow2.N16>.Sqrt2 / 2,
                MultiPrecision<Pow2.N16>.CosHalfPi(MultiPrecision<Pow2.N16>.Div(1, 2)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(
                MultiPrecision<Pow2.N16>.Sqrt(3) / 2,
                MultiPrecision<Pow2.N16>.CosHalfPi(MultiPrecision<Pow2.N16>.Div(1, 3)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(
                MultiPrecision<Pow2.N16>.Point5,
                MultiPrecision<Pow2.N16>.CosHalfPi(MultiPrecision<Pow2.N16>.Div(2, 3)), 1));
        }

        [TestMethod]
        public void SinHalfPiTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.ShortRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.SinHalfPi(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Sin((double)x * Math.PI / 2), y, minerr: 1e-5, ignore_sign: true);
            }

            for (int x = -32; x <= 32; x += 2) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.SinHalfPi(x);

                Assert.AreEqual(MultiPrecision<Pow2.N8>.Zero, y);
            }

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(
                MultiPrecision<Pow2.N8>.Sqrt2 / 2,
                MultiPrecision<Pow2.N8>.SinHalfPi(MultiPrecision<Pow2.N8>.Div(1, 2)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(
                MultiPrecision<Pow2.N8>.Point5,
                MultiPrecision<Pow2.N8>.SinHalfPi(MultiPrecision<Pow2.N8>.Div(1, 3)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(
                MultiPrecision<Pow2.N8>.Sqrt(3) / 2,
                MultiPrecision<Pow2.N8>.SinHalfPi(MultiPrecision<Pow2.N8>.Div(2, 3)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(
                MultiPrecision<Pow2.N16>.Sqrt2 / 2,
                MultiPrecision<Pow2.N16>.SinHalfPi(MultiPrecision<Pow2.N16>.Div(1, 2)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(
                MultiPrecision<Pow2.N16>.Point5,
                MultiPrecision<Pow2.N16>.SinHalfPi(MultiPrecision<Pow2.N16>.Div(1, 3)), 1));

            Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(
                MultiPrecision<Pow2.N16>.Sqrt(3) / 2,
                MultiPrecision<Pow2.N16>.SinHalfPi(MultiPrecision<Pow2.N16>.Div(2, 3)), 1));
        }

        [TestMethod]
        public void CosPiTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.ShortRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosPi(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Cos((double)x * Math.PI), y, minerr: 1e-5, ignore_sign: true);
            }

            for (MultiPrecision<Pow2.N8> n = 0; n <= 32; n++) {
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPlusZero(MultiPrecision<Pow2.N8>.CosPi(n + 0.5)), "cos intn");
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPlusZero(MultiPrecision<Pow2.N8>.CosPi(-n - 0.5)), "cos intn");
            }
        }

        [TestMethod]
        public void SinPiTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.ShortRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.SinPi(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Sin((double)x * Math.PI), y, minerr: 1e-5, ignore_sign: true);
            }

            for (MultiPrecision<Pow2.N8> n = 0; n <= 32; n++) {
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPlusZero(MultiPrecision<Pow2.N8>.SinPi(n)), "sin intn");
                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsMinusZero(MultiPrecision<Pow2.N8>.SinPi(-n)), "sin intn");
            }
        }

        [TestMethod]
        public void TanPiTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.ShortRangeSet<Pow2.N8>()) {
                if (x * 2 == MultiPrecision<Pow2.N8>.Truncate(x * 2)) {
                    continue;
                }

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.TanPi(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Tan((double)x * Math.PI), y, minerr: 1e-5, ignore_sign: true);
            }
        }

        [TestMethod]
        public void CosTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.ShortRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cos(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Cos((double)x), y, minerr: 1e-5, ignore_sign: true);
            }
        }

        [TestMethod]
        public void SinTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.ShortRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sin(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Sin((double)x), y, minerr: 1e-5, ignore_sign: true);
            }
        }

        [TestMethod]
        public void TanTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.ShortRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Tan(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Tan((double)x), y, minerr: 1e-5, ignore_sign: true);
            }
        }

        [TestMethod]
        public void CosHalfPiBorderTest() {
            MultiPrecision<Pow2.N8> half_n8 = MultiPrecision<Pow2.N8>.Point5;

            MultiPrecision<Pow2.N8>[] borders_n8 = [
                -4 - half_n8, -4, -3 - half_n8, -3, -2 - half_n8, -2, -1 - half_n8, -1, -0 - half_n8,
                0,
                0 + half_n8, 1, 1 + half_n8, 2, 2 + half_n8, 3, 3 + half_n8, 4, 4 + half_n8
            ];

            foreach (MultiPrecision<Pow2.N8> b in borders_n8) {
                List<MultiPrecision<Pow2.N8>> ys = [];

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosHalfPi(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Cos((double)x * Math.PI / 2), y, ignore_sign: true);

                    ys.Add(y);
                }

                if (MultiPrecision<Pow2.N8>.Abs(b % 2) == 1) {
                    TestTool.SmoothnessSatisfied(ys, 1);
                    TestTool.MonotonicitySatisfied(ys);
                }
                else if (b % 2 == 0) {
                    TestTool.NearlyNeighbors(ys, 3);
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                }
                else {
                    TestTool.NearlyNeighbors(ys, 4);
                    TestTool.SmoothnessSatisfied(ys, 1);
                    TestTool.MonotonicitySatisfied(ys);
                }

                Console.Write("\n");
            }

            MultiPrecision<Pow2.N16> half_n16 = MultiPrecision<Pow2.N16>.Point5;

            MultiPrecision<Pow2.N16>[] borders_n16 = [
                -4 - half_n16, -4, -3 - half_n16, -3, -2 - half_n16, -2, -1 - half_n16, -1, -0 - half_n16,
                0,
                0 + half_n16, 1, 1 + half_n16, 2, 2 + half_n16, 3, 3 + half_n16, 4, 4 + half_n16
            ];

            foreach (MultiPrecision<Pow2.N16> b in borders_n16) {
                List<MultiPrecision<Pow2.N16>> ys = [];

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.CosHalfPi(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Cos((double)x * Math.PI / 2), y, ignore_sign: true);

                    ys.Add(y);
                }

                if (MultiPrecision<Pow2.N16>.Abs(b % 2) == 1) {
                    TestTool.SmoothnessSatisfied(ys, 1);
                    TestTool.MonotonicitySatisfied(ys);
                }
                else if (b % 2 == 0) {
                    TestTool.NearlyNeighbors(ys, 3);
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                }
                else {
                    TestTool.NearlyNeighbors(ys, 4);
                    TestTool.SmoothnessSatisfied(ys, 1);
                    TestTool.MonotonicitySatisfied(ys);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void CosHalfPiLargeValueTest() {
            MultiPrecision<Pow2.N8> half = MultiPrecision<Pow2.N8>.Point5;

            MultiPrecision<Pow2.N8>[] borders = [
                -65538, -65537 - half, -65537, -65536 - half, -65536, -65535 - half,
                65535 + half, 65536, 65536 + half, 65537, 65537 + half, 65538
            ];

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = [];

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosHalfPi(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Cos((double)x * Math.PI / 2), y, ignore_sign: true);

                    ys.Add(y);
                }

                if (MultiPrecision<Pow2.N8>.Abs(b % 2) == 1) {
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                    TestTool.MonotonicitySatisfied(ys);
                }
                else if (b % 2 == 0) {
                    TestTool.NearlyNeighbors(ys, 19);
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                }
                else {
                    TestTool.NearlyNeighbors(ys, 20);
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                    TestTool.MonotonicitySatisfied(ys);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void CosHalfPiUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = [
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            ];

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosHalfPi(v);

                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(y));
            }
        }

        [TestMethod]
        public void SinHalfPiBorderTest() {
            MultiPrecision<Pow2.N8> half_n8 = MultiPrecision<Pow2.N8>.Point5;

            MultiPrecision<Pow2.N8>[] borders_n8 = [
                -4 - half_n8, -4, -3 - half_n8, -3, -2 - half_n8, -2, -1 - half_n8, -1, -0 - half_n8,
                0,
                0 + half_n8, 1, 1 + half_n8, 2, 2 + half_n8, 3, 3 + half_n8, 4, 4 + half_n8
            ];

            foreach (MultiPrecision<Pow2.N8> b in borders_n8) {
                List<MultiPrecision<Pow2.N8>> ys = [];

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.SinHalfPi(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Sin((double)x * Math.PI / 2), y, ignore_sign: true);

                    ys.Add(y);
                }

                if (b % 2 == 0) {
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                    TestTool.MonotonicitySatisfied(ys);
                }
                else if (MultiPrecision<Pow2.N8>.Abs(b % 2) == 1) {
                    TestTool.NearlyNeighbors(ys, 3);
                    TestTool.SmoothnessSatisfied(ys, 1);
                }
                else {
                    TestTool.NearlyNeighbors(ys, 4);
                    TestTool.SmoothnessSatisfied(ys, 1);
                    TestTool.MonotonicitySatisfied(ys);
                }

                Console.Write("\n");
            }

            MultiPrecision<Pow2.N16> half_n16 = MultiPrecision<Pow2.N16>.Point5;

            MultiPrecision<Pow2.N16>[] borders_n16 = [
                -4 - half_n16, -4, -3 - half_n16, -3, -2 - half_n16, -2, -1 - half_n16, -1, -0 - half_n16,
                0,
                0 + half_n16, 1, 1 + half_n16, 2, 2 + half_n16, 3, 3 + half_n16, 4, 4 + half_n16
            ];

            foreach (MultiPrecision<Pow2.N16> b in borders_n16) {
                List<MultiPrecision<Pow2.N16>> ys = [];

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.SinHalfPi(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Sin((double)x * Math.PI / 2), y, ignore_sign: true);

                    ys.Add(y);
                }

                if (b % 2 == 0) {
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                    TestTool.MonotonicitySatisfied(ys);
                }
                else if (MultiPrecision<Pow2.N16>.Abs(b % 2) == 1) {
                    TestTool.NearlyNeighbors(ys, 3);
                    TestTool.SmoothnessSatisfied(ys, 1);
                }
                else {
                    TestTool.NearlyNeighbors(ys, 4);
                    TestTool.SmoothnessSatisfied(ys, 1);
                    TestTool.MonotonicitySatisfied(ys);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void SinHalfPiLargeValueTest() {
            MultiPrecision<Pow2.N8> half = MultiPrecision<Pow2.N8>.Point5;

            MultiPrecision<Pow2.N8>[] borders = [
                -65538, -65537 - half, -65537, -65536 - half, -65536, -65535 - half,
                65535 + half, 65536, 65536 + half, 65537, 65537 + half, 65538
            ];

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = [];

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.SinHalfPi(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Sin((double)x * Math.PI / 2), y, ignore_sign: true);

                    ys.Add(y);
                }

                if (b % 2 == 0) {
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                    TestTool.MonotonicitySatisfied(ys);
                }
                else if (MultiPrecision<Pow2.N8>.Abs(b % 2) == 1) {
                    TestTool.NearlyNeighbors(ys, 19);
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                }
                else {
                    TestTool.NearlyNeighbors(ys, 20);
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                    TestTool.MonotonicitySatisfied(ys);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void SinHalfPiUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = [
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            ];

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.SinHalfPi(v);

                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(y));
            }
        }



        [TestMethod]
        public void TanPiBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = [
                0, -1, 1, 0.25, 0.75, -0.25, -0.75
            ];

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.TanPi(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Tan((double)x * Math.PI), y, ignore_sign: true);
                }

                Console.Write("\n");
            }
        }

        internal struct N18 : IConstant {
            public readonly int Value => 18;
        }

        [TestMethod]
        public void SinPiMistakeSftValueTest() {
            MultiPrecision<N18>[] borders = [
                0.4580078125
            ];

            foreach (MultiPrecision<N18> b in borders) {
                List<MultiPrecision<N18>> ys = [];

                foreach (MultiPrecision<N18> x in TestTool.EnumerateNeighbor(b, 2048)) {
                    Console.WriteLine($"x = {x.ToHexcode()}");
                    try {
                        _ = MultiPrecision<N18>.SinPi(x);
                    }
                    catch (Exception) {
                        Console.WriteLine($"err = {x.ToHexcode()}");
                        throw;
                    }
                }

                Console.Write("\n");
            }
        }
    }
}
