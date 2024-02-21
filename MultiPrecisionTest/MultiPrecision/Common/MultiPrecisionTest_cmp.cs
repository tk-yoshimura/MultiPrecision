using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CmpTest() {
            MultiPrecision<Pow2.N8>[] vs = [
                MultiPrecision<Pow2.N8>.Zero,
                MultiPrecision<Pow2.N8>.MinusZero,
                1, 2, 3, 4, 5, 7, 10, 11, 13, 100, 1000,
                -1, -2, -3, -4, -5, -7, -10, -11, -13, -100, -1000,
                MultiPrecision<Pow2.N8>.One / 2,
                MultiPrecision<Pow2.N8>.One / 3,
                MultiPrecision<Pow2.N8>.One / 4,
                MultiPrecision<Pow2.N8>.One / 5,
                MultiPrecision<Pow2.N8>.One / 7,
                MultiPrecision<Pow2.N8>.One / 10,
                MultiPrecision<Pow2.N8>.One / 11,
                MultiPrecision<Pow2.N8>.One / 13,
                MultiPrecision<Pow2.N8>.One / 100,
                MultiPrecision<Pow2.N8>.One / 1000,
                MultiPrecision<Pow2.N8>.MinusOne / 2,
                MultiPrecision<Pow2.N8>.MinusOne / 3,
                MultiPrecision<Pow2.N8>.MinusOne / 4,
                MultiPrecision<Pow2.N8>.MinusOne / 5,
                MultiPrecision<Pow2.N8>.MinusOne / 7,
                MultiPrecision<Pow2.N8>.MinusOne / 10,
                MultiPrecision<Pow2.N8>.MinusOne / 11,
                MultiPrecision<Pow2.N8>.MinusOne / 13,
                MultiPrecision<Pow2.N8>.MinusOne / 100,
                MultiPrecision<Pow2.N8>.MinusOne / 1000,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
                MultiPrecision<Pow2.N8>.NaN,
            ];


            foreach (MultiPrecision<Pow2.N8> v in vs) {
                foreach (MultiPrecision<Pow2.N8> u in vs) {

                    double dv = (double)v, du = (double)u;

                    try {
                        Assert.AreEqual(v.Equals(u), dv.Equals(du), "equal");
                        Assert.AreEqual(v == u, dv == du, "==");
                        Assert.AreEqual(v != u, dv != du, "!=");
                        Assert.AreEqual(v < u, dv < du, "<");
                        Assert.AreEqual(v > u, dv > du, ">");
                        Assert.AreEqual(v <= u, dv <= du, "<=");
                        Assert.AreEqual(v >= u, dv >= du, ">=");
                    }
                    catch (AssertFailedException e) {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(v);
                        Console.WriteLine(u);

                        Console.WriteLine($"equal {v.Equals(u)}");
                        Console.WriteLine($"== {v == u}");
                        Console.WriteLine($"!= {v != u}");
                        Console.WriteLine($"< {v < u}");
                        Console.WriteLine($"> {v > u}");
                        Console.WriteLine($"<= {v <= u}");
                        Console.WriteLine($">= {v >= u}");

                        throw;
                    }
                }
            }
        }

        [TestMethod]
        public void NearlyEqualsTest() {
            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEquals("1.5", "1.45", "0.1"));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEquals("1.5", "1.55", "0.1"));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEquals("1.5", "1.45", "0.06"));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEquals("1.5", "1.55", "0.06"));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.NearlyEquals("1.5", "1.45", "0.04"));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.NearlyEquals("1.5", "1.55", "0.04"));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.NearlyEquals("1.5", "1.45", "0.01"));
            Assert.IsFalse(MultiPrecision<Pow2.N8>.NearlyEquals("1.5", "1.55", "0.01"));
        }

        [TestMethod]
        public void NearlyEqualBitsTest() {
            const int g = 8, n = 5;

            MultiPrecision<Pow2.N8>[][] vss = [
                [
                    -MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.BitDecrement(4)),
                    -MultiPrecision<Pow2.N8>.BitDecrement(4),
                    -4,
                    -MultiPrecision<Pow2.N8>.BitIncrement(4),
                    -MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.BitIncrement(4)),
                ],

                [
                    MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.BitDecrement(4)),
                    MultiPrecision<Pow2.N8>.BitDecrement(4),
                    4,
                    MultiPrecision<Pow2.N8>.BitIncrement(4),
                    MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.BitIncrement(4)),
                ],

                [
                    -MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.BitDecrement(3)),
                    -MultiPrecision<Pow2.N8>.BitDecrement(3),
                    -3,
                    -MultiPrecision<Pow2.N8>.BitIncrement(3),
                    -MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.BitIncrement(3)),
                ],

                [
                    MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.BitDecrement(3)),
                    MultiPrecision<Pow2.N8>.BitDecrement(3),
                    3,
                    MultiPrecision<Pow2.N8>.BitIncrement(3),
                    MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.BitIncrement(3)),
                ],

                [
                    -MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.BitDecrement(2)),
                    -MultiPrecision<Pow2.N8>.BitDecrement(2),
                    -2,
                    -MultiPrecision<Pow2.N8>.BitIncrement(2),
                    -MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.BitIncrement(2)),
                ],

                [
                    MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.BitDecrement(2)),
                    MultiPrecision<Pow2.N8>.BitDecrement(2),
                    2,
                    MultiPrecision<Pow2.N8>.BitIncrement(2),
                    MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.BitIncrement(2)),
                ],

                [
                    -MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.Sqrt2)),
                    -MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.Sqrt2),
                    -MultiPrecision<Pow2.N8>.Sqrt2,
                    -MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.Sqrt2),
                    -MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.Sqrt2)),
                ],

                [
                    MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.Sqrt2)),
                    MultiPrecision<Pow2.N8>.BitDecrement(MultiPrecision<Pow2.N8>.Sqrt2),
                    MultiPrecision<Pow2.N8>.Sqrt2,
                    MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.Sqrt2),
                    MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.BitIncrement(MultiPrecision<Pow2.N8>.Sqrt2)),
                ],
            ];

            for (int i = 0; i < g; i++) {
                for (int j = 0; j < n; j++) {
                    MultiPrecision<Pow2.N8> v = vss[i][j];

                    for (int k = 0; k < g; k++) {
                        for (int m = 0; m < n; m++) {
                            MultiPrecision<Pow2.N8> u = vss[k][m];

                            if (i == k) {
                                Console.WriteLine(v.ToHexcode());
                                Console.WriteLine(u.ToHexcode());

                                int dist = Math.Abs(j - m);

                                Console.WriteLine($"dist {dist}");

                                if (MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 0)) {
                                    Console.WriteLine("match");

                                    Assert.IsTrue(dist == 0, $"\n{v.ToHexcode()}\n{u.ToHexcode()}");
                                }
                                else if (MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 1)) {
                                    Console.WriteLine("nearly 1bits");

                                    Assert.IsTrue(dist == 1, $"\n{v.ToHexcode()}\n{u.ToHexcode()}");
                                }
                                else if (MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 2)) {
                                    Console.WriteLine("nearly 2bits");

                                    Assert.IsTrue(dist >= 2 && dist < 4, $"\n{v.ToHexcode()}\n{u.ToHexcode()}");
                                }
                                else if (MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 3)) {
                                    Console.WriteLine("nearly 3bits");

                                    Assert.IsTrue(dist >= 3 && dist < 8, $"\n{v.ToHexcode()}\n{u.ToHexcode()}");
                                }
                                else {
                                    Console.WriteLine("nearly more 4bits");
                                }

                                Console.WriteLine(string.Empty);
                            }
                            else {
                                Assert.IsFalse(MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 0), "bits 0");
                                Assert.IsFalse(MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 1), "bits 1");
                                Assert.IsFalse(MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 2), "bits 2");
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void NullCmpTest() {
            MultiPrecision<Pow2.N8> x = 1;
            MultiPrecision<Pow2.N8> n = null;

            Assert.IsTrue(x != null);
            Assert.IsTrue(x != n);
            Assert.IsTrue(null != x);
            Assert.IsTrue(n != x);

            Assert.IsFalse(x == null);
            Assert.IsFalse(x == n);
            Assert.IsFalse(null == x);
            Assert.IsFalse(n == x);

            Assert.IsFalse(n != null);
            Assert.IsFalse(null != n);

            Assert.IsTrue(n == null);
            Assert.IsTrue(null == n);

#pragma warning disable CS1718
            Assert.IsTrue(n == n);
            Assert.IsFalse(n != n);
#pragma warning restore CS1718
        }
    }
}
