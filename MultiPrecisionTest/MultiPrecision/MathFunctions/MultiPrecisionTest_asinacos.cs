using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void AtanTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Atan((double)x), y);
            }
        }

        [TestMethod]
        public void AsinTest() {
            for (Int64 i = -99; i <= 99; i++) {

                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 100;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Asin(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Asin((double)x), y);
            }
        }

        [TestMethod]
        public void AcosTest() {
            for (Int64 i = -99; i <= 99; i++) {

                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 100;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Acos(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Acos((double)x), y);
            }
        }

        [TestMethod]
        public void Atan2Test() {
            for (Int64 iy = -10; iy <= 10; iy++) {
                for (Int64 ix = -10; ix <= 10; ix++) {

                    MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)ix / 10;
                    MultiPrecision<Pow2.N8> y = (MultiPrecision<Pow2.N8>)iy / 10;
                    MultiPrecision<Pow2.N8> d = MultiPrecision<Pow2.N8>.Atan2(y, x);

                    Console.WriteLine($"{x}, {y}");
                    Console.WriteLine(d);

                    TestTool.Tolerance(Math.Atan2((double)y, (double)x), d);
                }
            }
        }

        [TestMethod]
        public void SquareAsinTest() {
            for (Int64 i = 0; i <= 250; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 500;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.SquareAsin(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Asin((double)x) * Math.Asin((double)x), y);
            }
        }

        [TestMethod]
        public void AtanBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NegativeInfinity, -2, -1, 0, 1, 2, MultiPrecision<Pow2.N8>.PositiveInfinity
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Atan((double)x), y);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void AsinBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                -MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Sqrt2, -1),
                0,
                MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Sqrt2, -1),
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Asin(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Asin((double)x), y);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void AcosBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                -MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Sqrt2, -1),
                0,
                MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Sqrt2, -1),
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Acos(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Acos((double)x), y);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void Atan2BorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                0,
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(x, 1);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Atan2((double)x, 1), y);
                }

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(x, -1);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Atan2((double)x, -1), y);
                }

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(1, x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Atan2(1, (double)x), y);
                }

                Console.Write("\n");
            }

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(-1, x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Atan2(-1, (double)x), y);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void AtanUnnormalValueTest() {
            MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan(MultiPrecision<Pow2.N8>.NaN);

            Assert.IsTrue(y.IsNaN);
        }

        [TestMethod]
        public void AsinUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.BitDecrement(-1),
                MultiPrecision<Pow2.N8>.BitIncrement(1),
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Asin(v);

                Assert.IsTrue(y.IsNaN);
            }
        }

        [TestMethod]
        public void AcosUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.BitDecrement(-1),
                MultiPrecision<Pow2.N8>.BitIncrement(1),
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Acos(v);

                Assert.IsTrue(y.IsNaN);
            }
        }

        [TestMethod]
        public void Atan2UnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(0, v);

                Assert.IsTrue(y.IsNaN);
            }

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(v, 0);

                Assert.IsTrue(y.IsNaN);
            }

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(v, v);

                Assert.IsTrue(y.IsNaN);
            }
        }
    }
}
