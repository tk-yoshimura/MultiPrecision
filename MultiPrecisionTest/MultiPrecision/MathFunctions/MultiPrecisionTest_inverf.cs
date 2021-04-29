using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void InverseErfTest() {
            MultiPrecision<Pow2.N8>[] xs = new MultiPrecision<Pow2.N8>[] {
                -(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -128)),
                -(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -64)),
                -(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -32)),
                -(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -16)),
                -(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -8)),
                -(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -4)),
                -(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -2)),
                -(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -1)),
                -MultiPrecision<Pow2.N8>.Ldexp(1, -2),
                -MultiPrecision<Pow2.N8>.Ldexp(1, -4),
                -MultiPrecision<Pow2.N8>.Ldexp(1, -8),
                0,
                +MultiPrecision<Pow2.N8>.Ldexp(1, -8),
                +MultiPrecision<Pow2.N8>.Ldexp(1, -4),
                +MultiPrecision<Pow2.N8>.Ldexp(1, -2),
                +(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -1)),
                +(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -2)),
                +(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -4)),
                +(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -8)),
                +(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -16)),
                +(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -32)),
                +(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -64)),
                +(1 - MultiPrecision<Pow2.N8>.Ldexp(1, -128)),
            };

            foreach (MultiPrecision<Pow2.N8> x in xs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.InverseErf(x);
                MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Erf(y);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.WriteLine(z);
                Console.WriteLine(z.ToHexcode());
                Console.Write("\n");

                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(x, z, 1));
            }
        }

        [TestMethod]
        public void InverseErfcTest() {
            MultiPrecision<Pow2.N8>[] xs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.Ldexp(1, -256),
                MultiPrecision<Pow2.N8>.Ldexp(1, -128),
                MultiPrecision<Pow2.N8>.Ldexp(1, -64),
                MultiPrecision<Pow2.N8>.Ldexp(1, -32),
                MultiPrecision<Pow2.N8>.Ldexp(1, -16),
                MultiPrecision<Pow2.N8>.Ldexp(1, -8),
                MultiPrecision<Pow2.N8>.Ldexp(1, -4),
                MultiPrecision<Pow2.N8>.Ldexp(1, -2),
                MultiPrecision<Pow2.N8>.Ldexp(1, -1),
                MultiPrecision<Pow2.N8>.Point5 + MultiPrecision<Pow2.N8>.Ldexp(1, -2),
                MultiPrecision<Pow2.N8>.Point5 + MultiPrecision<Pow2.N8>.Ldexp(1, -4),
                MultiPrecision<Pow2.N8>.Point5 + MultiPrecision<Pow2.N8>.Ldexp(1, -8),
                1,
                1 + MultiPrecision<Pow2.N8>.Ldexp(1, -8),
                1 + MultiPrecision<Pow2.N8>.Ldexp(1, -4),
                1 + MultiPrecision<Pow2.N8>.Ldexp(1, -2),
                (2 - MultiPrecision<Pow2.N8>.Ldexp(1, -1)),
                (2 - MultiPrecision<Pow2.N8>.Ldexp(1, -2)),
                (2 - MultiPrecision<Pow2.N8>.Ldexp(1, -4)),
                (2 - MultiPrecision<Pow2.N8>.Ldexp(1, -8)),
                (2 - MultiPrecision<Pow2.N8>.Ldexp(1, -16)),
                (2 - MultiPrecision<Pow2.N8>.Ldexp(1, -32)),
            };

            foreach (MultiPrecision<Pow2.N8> x in xs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.InverseErfc(x);
                MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Erfc(y);
                MultiPrecision<Pow2.N8> w = MultiPrecision<Pow2.N8>.InverseErfc(z);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.WriteLine(z);
                Console.WriteLine(z.ToHexcode());
                Console.WriteLine(w);
                Console.WriteLine(w.ToHexcode());
                Console.Write("\n");

                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(x, z, 8));
                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y, w, 1));
            }
        }

        [TestMethod]
        public void InverseErfcPercentileTest() {
            for (MultiPrecision<Pow2.N8> x = "0.1"; x > "1e-10"; x *= "0.1") {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.InverseErfc(x);
                MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Erfc(y);
                MultiPrecision<Pow2.N8> w = MultiPrecision<Pow2.N8>.InverseErfc(z);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.WriteLine(z);
                Console.WriteLine(z.ToHexcode());
                Console.WriteLine(w);
                Console.WriteLine(w.ToHexcode());
                Console.Write("\n");

                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y, w, 1));
            }

            for (MultiPrecision<Pow2.N8> x = "1e-10"; x > "1e-100"; x *= "1e-10") {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.InverseErfc(x);
                MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Erfc(y);
                MultiPrecision<Pow2.N8> w = MultiPrecision<Pow2.N8>.InverseErfc(z);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.WriteLine(z);
                Console.WriteLine(z.ToHexcode());
                Console.WriteLine(w);
                Console.WriteLine(w.ToHexcode());
                Console.Write("\n");

                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y, w, 1));
            }

            for (MultiPrecision<Pow2.N8> x = "1e-100"; x > "1e-1000"; x *= "1e-100") {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.InverseErfc(x);
                MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Erfc(y);
                MultiPrecision<Pow2.N8> w = MultiPrecision<Pow2.N8>.InverseErfc(z);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.WriteLine(z);
                Console.WriteLine(z.ToHexcode());
                Console.WriteLine(w);
                Console.WriteLine(w.ToHexcode());
                Console.Write("\n");

                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y, w, 1));
            }

            for (MultiPrecision<Pow2.N8> x = "1e-1000"; x > "1e-10000"; x *= "1e-1000") {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.InverseErfc(x);
                MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Erfc(y);
                MultiPrecision<Pow2.N8> w = MultiPrecision<Pow2.N8>.InverseErfc(z);

                Console.WriteLine(x);
                Console.WriteLine(x.ToHexcode());
                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());
                Console.WriteLine(z);
                Console.WriteLine(z.ToHexcode());
                Console.WriteLine(w);
                Console.WriteLine(w.ToHexcode());
                Console.Write("\n");

                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y, w, 1));
            }
        }

        [TestMethod]
        public void InverseErfBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                0, -1, 1,
                -MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Epsilon, 1),
                +MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Epsilon, 1),
                -MultiPrecision<Pow2.N8>.Ldexp(1, -63),
                +MultiPrecision<Pow2.N8>.Ldexp(1, -63)
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 4)) {

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.InverseErf(x);
                    MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Erf(y);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.WriteLine(z);
                    Console.WriteLine(z.ToHexcode());
                    Console.Write("\n");

                    if (y.IsFinite && !y.IsZero) {
                        Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(x, z, 1));
                    }

                    if (y.IsFinite) {
                        ys.Add(y);
                    }
                }

                if (b != -1 && b != 1) {
                    if (b != 0) {
                        TestTool.NearlyNeighbors(ys, 32);
                    }
                    TestTool.SmoothnessSatisfied(ys, 0.5);
                    TestTool.MonotonicitySatisfied(ys);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void InverseErfcBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                0, MultiPrecision<Pow2.N8>.Point5, 1, 2
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 8)) {

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.InverseErf(x);
                    MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.Erf(y);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.WriteLine(z);
                    Console.WriteLine(z.ToHexcode());
                    Console.Write("\n");

                    if (y.IsFinite && !y.IsZero) {
                        Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(x, z, 1));
                    }

                    if (y.IsFinite) {
                        ys.Add(y);
                    }
                }

                if (b != 2) {
                    if (b != 1) {
                        TestTool.NearlyNeighbors(ys, 2);
                    }
                    TestTool.SmoothnessSatisfied(ys, 1);
                    TestTool.MonotonicitySatisfied(ys);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void InverseErfUnnormalValueTest() {
            MultiPrecision<Pow2.N8> m1 = MultiPrecision<Pow2.N8>.InverseErf(-1);
            MultiPrecision<Pow2.N8> p1 = MultiPrecision<Pow2.N8>.InverseErf(+1);

            Assert.IsTrue(!m1.IsFinite && !m1.IsNaN && m1.Sign == Sign.Minus);
            Assert.IsTrue(!p1.IsFinite && !p1.IsNaN && p1.Sign == Sign.Plus);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.InverseErf(MultiPrecision<Pow2.N8>.NaN).IsNaN);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.InverseErf(-2).IsNaN);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.InverseErf(+2).IsNaN);
        }

        [TestMethod]
        public void InverseErfcUnnormalValueTest() {
            MultiPrecision<Pow2.N8> z = MultiPrecision<Pow2.N8>.InverseErfc(0);
            MultiPrecision<Pow2.N8> p2 = MultiPrecision<Pow2.N8>.InverseErfc(2);

            Assert.IsTrue(!z.IsFinite && !z.IsNaN && z.Sign == Sign.Plus);
            Assert.IsTrue(!p2.IsFinite && !p2.IsNaN && p2.Sign == Sign.Minus);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.InverseErfc(MultiPrecision<Pow2.N8>.NaN).IsNaN);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.InverseErfc(-1).IsNaN);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.InverseErfc(+3).IsNaN);
        }
    }
}
