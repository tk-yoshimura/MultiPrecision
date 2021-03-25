using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Diagnostics;

namespace MultiPrecisionTest.Arithmetics {

    [TestClass]
    public partial class MultiPrecisionTest {
        readonly MultiPrecision<Pow2.N8>[] vs = {
            MultiPrecision<Pow2.N8>.Zero,
            MultiPrecision<Pow2.N8>.MinusZero,
            1, 2, 3, 4, 5, 7, 10, 11, 13, 100, 1000,
            -1, -2, -3, -4, -5, -7, -10, -11, -13, -100, -1000,
            MultiPrecision<Pow2.N8>.One / 2,
            MultiPrecision<Pow2.N8>.One / 3,
            MultiPrecision<Pow2.N8>.One / 4,
            MultiPrecision<Pow2.N8>.One / 5,
            MultiPrecision<Pow2.N8>.MinusOne / 2,
            MultiPrecision<Pow2.N8>.MinusOne / 3,
            MultiPrecision<Pow2.N8>.MinusOne / 4,
            MultiPrecision<Pow2.N8>.MinusOne / 5,
            MultiPrecision<Pow2.N8>.PositiveInfinity,
            MultiPrecision<Pow2.N8>.NegativeInfinity,
            MultiPrecision<Pow2.N8>.NaN,
        };

        readonly long[] us = {
            long.MinValue, long.MinValue + 1, -99999999, -1000, -100, -64, -32, -16, -8, -7, -6, -5, -4, -3, -2, -1,
            0,
            long.MaxValue, 99999999, 1000, 100, 64, 32, 16, 8, 7, 6, 5, 4, 3, 2, 1
        };


        [TestMethod]
        public void AddTest() {
            foreach (MultiPrecision<Pow2.N8> a in vs) {
                foreach (MultiPrecision<Pow2.N8> b in vs) {
                    MultiPrecision<Pow2.N8> c_actual = a + b;
                    double c_expect = (double)a + (double)b;

                    Trace.WriteLine($"{(double)a} + {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} + {b} = {c_actual}");

                    if (c_actual.IsNaN && double.IsNaN(c_expect)) {
                        continue;
                    }

                    if (a.IsNaN || b.IsNaN) {
                        if (!c_actual.IsNaN) {
                            Console.WriteLine($"{(double)a} + {(double)b} = {c_expect}");
                            Console.WriteLine($"{a} + {b} = {c_actual}");
                            Console.Write("\n");

                            Assert.Fail();
                        }
                    }
                    else {
                        if (c_expect > 0) {
                            if (!(c_expect >= (double)c_actual * 0.999 && c_expect <= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} + {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} + {b} = {c_actual}");
                                Console.Write("\n");

                                Assert.Fail();
                            }
                        }
                        else {
                            if (!(c_expect <= (double)c_actual * 0.999 && c_expect >= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} + {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} + {b} = {c_actual}");
                                Console.Write("\n");

                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void SubTest() {
            foreach (MultiPrecision<Pow2.N8> a in vs) {
                foreach (MultiPrecision<Pow2.N8> b in vs) {
                    MultiPrecision<Pow2.N8> c_actual = a - b;
                    double c_expect = (double)a - (double)b;

                    Trace.WriteLine($"{(double)a} - {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} - {b} = {c_actual}");

                    if (c_actual.IsNaN && double.IsNaN(c_expect)) {
                        continue;
                    }

                    if (a.IsNaN || b.IsNaN) {
                        if (!c_actual.IsNaN) {
                            Console.WriteLine($"{(double)a} - {(double)b} = {c_expect}");
                            Console.WriteLine($"{a} - {b} = {c_actual}");
                            Console.Write("\n");

                            Assert.Fail();
                        }
                    }
                    else {
                        if (c_expect > 0) {
                            if (!(c_expect >= (double)c_actual * 0.999 && c_expect <= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} - {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} - {b} = {c_actual}");
                                Console.Write("\n");

                                Assert.Fail();
                            }
                        }
                        else {
                            if (!(c_expect <= (double)c_actual * 0.999 && c_expect >= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} - {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} - {b} = {c_actual}");
                                Console.Write("\n");

                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void MulTest() {
            foreach (MultiPrecision<Pow2.N8> a in vs) {
                foreach (MultiPrecision<Pow2.N8> b in vs) {
                    MultiPrecision<Pow2.N8> c_actual = a * b;
                    double c_expect = (double)a * (double)b;

                    Trace.WriteLine($"{(double)a} * {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} * {b} = {c_actual}");

                    if (c_actual.IsNaN && double.IsNaN(c_expect)) {
                        continue;
                    }

                    if (a.IsNaN || b.IsNaN) {
                        if (!c_actual.IsNaN) {
                            Console.WriteLine($"{(double)a} * {(double)b} = {c_expect}");
                            Console.WriteLine($"{a} * {b} = {c_actual}");
                            Console.Write("\n");

                            Assert.Fail();
                        }
                    }
                    else {
                        if (c_expect > 0) {
                            if (!(c_expect >= (double)c_actual * 0.999 && c_expect <= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} * {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} * {b} = {c_actual}");
                                Console.Write("\n");

                                Assert.Fail();
                            }
                        }
                        else {
                            if (!(c_expect <= (double)c_actual * 0.999 && c_expect >= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} * {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} * {b} = {c_actual}");
                                Console.Write("\n");

                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void DivTest() {
            foreach (MultiPrecision<Pow2.N8> a in vs) {
                foreach (MultiPrecision<Pow2.N8> b in vs) {
                    MultiPrecision<Pow2.N8> c_actual = a / b;
                    double c_expect = (double)a / (double)b;

                    Trace.WriteLine($"{(double)a} / {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} / {b} = {c_actual}");

                    if (c_actual.IsNaN && double.IsNaN(c_expect)) {
                        continue;
                    }

                    if (a.IsNaN || b.IsNaN) {
                        if (!c_actual.IsNaN) {
                            Console.WriteLine($"{(double)a} / {(double)b} = {c_expect}");
                            Console.WriteLine($"{a} / {b} = {c_actual}");
                            Console.Write("\n");

                            Assert.Fail();
                        }
                    }
                    else {
                        if (c_expect > 0) {
                            if (!(c_expect >= (double)c_actual * 0.999 && c_expect <= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} / {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} / {b} = {c_actual}");
                                Console.Write("\n");

                                Assert.Fail();
                            }
                        }
                        else {
                            if (!(c_expect <= (double)c_actual * 0.999 && c_expect >= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} / {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} / {b} = {c_actual}");
                                Console.Write("\n");

                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void RemTest() {
            foreach (MultiPrecision<Pow2.N8> a in vs) {
                foreach (MultiPrecision<Pow2.N8> b in vs) {
                    MultiPrecision<Pow2.N8> c_actual = a % b;
                    double c_expect = (double)a % (double)b;

                    Trace.WriteLine($"{(double)a} % {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} % {b} = {c_actual}");

                    if (c_actual.IsNaN && double.IsNaN(c_expect)) {
                        continue;
                    }

                    if (a.IsNaN || b.IsNaN) {
                        if (!c_actual.IsNaN) {
                            Console.WriteLine($"{(double)a} % {(double)b} = {c_expect}");
                            Console.WriteLine($"{a} % {b} = {c_actual}");
                            Console.Write("\n");
                        }
                    }
                    else {
                        if (c_expect > 0) {
                            if (!(c_expect >= (double)c_actual * 0.999 && c_expect <= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} % {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} % {b} = {c_actual}");
                                Console.Write("\n");
                            }
                        }
                        else {
                            if (!(c_expect <= (double)c_actual * 0.999 && c_expect >= (double)c_actual * 1.001)) {
                                Console.WriteLine($"{(double)a} % {(double)b} = {c_expect}");
                                Console.WriteLine($"{a} % {b} = {c_actual}");
                                Console.Write("\n");
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void MulLongTest() {
            foreach (MultiPrecision<Pow2.N8> a in vs) {
                foreach (long b in us) {
                    MultiPrecision<Pow2.N8> c_actual = MultiPrecision<Pow2.N8>.Mul(a, b);
                    MultiPrecision<Pow2.N8> c_expect = MultiPrecision<Pow2.N8>.Mul(a, (MultiPrecision<Pow2.N8>)b);

                    Trace.WriteLine($"{a} * {b} = {c_expect}");
                    Trace.WriteLine($"{a} * {b} = {c_actual}");

                    if (c_actual.IsNaN && c_expect.IsNaN) {
                        continue;
                    }

                    if (a.IsNaN) {
                        if (!c_actual.IsNaN) {
                            Console.WriteLine($"{a} * {b} = {c_expect}");
                            Console.WriteLine($"{a} * {b} = {c_actual}");
                            Console.Write("\n");

                            Assert.Fail();
                        }
                    }
                    else {
                        Console.WriteLine($"{a} * {b} = {c_expect}");
                        Console.WriteLine($"{a} * {b} = {c_actual}");
                        Console.Write("\n");

                        Assert.AreEqual(c_expect, c_actual);
                    }
                }
            }
        }

        [TestMethod]
        public void DivLongTest() {
            foreach (MultiPrecision<Pow2.N8> a in vs) {
                foreach (long b in us) {
                    MultiPrecision<Pow2.N8> c_actual = MultiPrecision<Pow2.N8>.Div(a, b);
                    MultiPrecision<Pow2.N8> c_expect = MultiPrecision<Pow2.N8>.Div(a, (MultiPrecision<Pow2.N8>)b);

                    Trace.WriteLine($"{a} / {b} = {c_expect}");
                    Trace.WriteLine($"{a} / {b} = {c_actual}");

                    if (c_actual.IsNaN && c_expect.IsNaN) {
                        continue;
                    }

                    if (a.IsNaN) {
                        if (!c_actual.IsNaN) {
                            Console.WriteLine($"{a} / {b} = {c_expect}");
                            Console.WriteLine($"{a} / {b} = {c_actual}");
                            Console.Write("\n");

                            Assert.Fail();
                        }
                    }
                    else {
                        Console.WriteLine($"{a} / {b} = {c_expect}");
                        Console.WriteLine($"{a} / {b} = {c_actual}");
                        Console.Write("\n");

                        Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(c_expect, c_actual, 1));
                    }
                }
            }
        }
    }
}
