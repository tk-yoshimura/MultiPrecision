using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void ArsinhTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Arsinh(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Asinh((double)x), y);
            }
        }

        [TestMethod]
        public void ArcoshTest() {
            for (Int64 i = 100; i <= 400; i++) {

                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 100;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Arcosh(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Acosh((double)x), y);
            }
        }

        [TestMethod]
        public void ArtanhTest() {
            for (Int64 i = -99; i <= 99; i++) {

                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 100;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Artanh(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Atanh((double)x), y);
            }
        }

        [TestMethod]
        public void ArsinhBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                0,
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Arsinh(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Asinh((double)x), y);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void ArcoshBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                1
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Arcosh(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Acosh((double)x), y, ignore_expected_nan: true);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void ArtanhBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { -1, 0, 1 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Arcosh(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Atanh((double)x), y, ignore_expected_nan: true);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void ArsinhUnnormalValueTest() {
            MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Arsinh(MultiPrecision<Pow2.N8>.NaN);

            Assert.IsTrue(y.IsNaN);
        }


        [TestMethod]
        public void ArcoshUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.BitDecrement(1),
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Arcosh(v);

                Assert.IsTrue(y.IsNaN);
            }

            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.Arcosh(MultiPrecision<Pow2.N8>.PositiveInfinity);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);
        }

        [TestMethod]
        public void ArtanhUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.BitDecrement(-1),
                MultiPrecision<Pow2.N8>.BitIncrement(1),
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Artanh(v);

                Assert.IsTrue(y.IsNaN);
            }
        }
    }
}
