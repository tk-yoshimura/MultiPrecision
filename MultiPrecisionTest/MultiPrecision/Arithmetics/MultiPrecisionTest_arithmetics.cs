using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Diagnostics;
using static MultiPrecision.Pow2;
using static MultiPrecisionTest.BigUInt.BigUIntTest;

namespace MultiPrecisionTest.Arithmetics {

    public static class MultiPrecisionTest<N> where N : struct, IConstant {
        static readonly int bits = default(N).Value * UIntUtil.UInt32Bits;

        static readonly MultiPrecision<N>[] vs = [
            MultiPrecision<N>.Zero,
            MultiPrecision<N>.MinusZero,
            1, 2, 3, 4, 5, 7, 8, 10, 11, 13, 100, 1000, 255, 256, 65535, 65536, 16777215, 16777216,
            -1, -2, -3, -4, -5, -7, -8, -10, -11, -13, -100, -1000, -255, -256, -65535, -65536, -16777215, -16777216,
            MultiPrecision<N>.One / 2,
            MultiPrecision<N>.One / 3,
            MultiPrecision<N>.One / 4,
            MultiPrecision<N>.One / 5,
            MultiPrecision<N>.One / 7,
            MultiPrecision<N>.MinusOne / 2,
            MultiPrecision<N>.MinusOne / 3,
            MultiPrecision<N>.MinusOne / 4,
            MultiPrecision<N>.MinusOne / 5,
            MultiPrecision<N>.MinusOne / 7,
            MultiPrecision<N>.Ldexp(15, bits / 2),
            MultiPrecision<N>.Ldexp(15, bits - 16),
            MultiPrecision<N>.Ldexp(15, bits - 4),
            MultiPrecision<N>.Ldexp(15, bits - 1),
            MultiPrecision<N>.Ldexp(15, bits),
            MultiPrecision<N>.Ldexp(15, bits + 1),
            MultiPrecision<N>.Ldexp(15, bits + 4),
            MultiPrecision<N>.Ldexp(15, bits + 16),
            MultiPrecision<N>.Ldexp(15, bits * 2),
            MultiPrecision<N>.Ldexp(255, bits / 2),
            MultiPrecision<N>.Ldexp(255, bits - 16),
            MultiPrecision<N>.Ldexp(255, bits - 4),
            MultiPrecision<N>.Ldexp(255, bits - 1),
            MultiPrecision<N>.Ldexp(255, bits),
            MultiPrecision<N>.Ldexp(255, bits + 1),
            MultiPrecision<N>.Ldexp(255, bits + 4),
            MultiPrecision<N>.Ldexp(255, bits + 16),
            MultiPrecision<N>.Ldexp(255, bits * 2),
            MultiPrecision<N>.Ldexp(1, -bits / 4 - 1),
            -MultiPrecision<N>.Ldexp(1, -bits / 4 - 1),
            MultiPrecision<N>.Ldexp(1, bits / 4 - 1),
            -MultiPrecision<N>.Ldexp(1, bits / 4 - 1),
            MultiPrecision<N>.Ldexp(1, -bits / 4),
            -MultiPrecision<N>.Ldexp(1, -bits / 4),
            MultiPrecision<N>.Ldexp(1, bits / 4),
            -MultiPrecision<N>.Ldexp(1, bits / 4),
            MultiPrecision<N>.Ldexp(1, -bits / 4 + 1),
            -MultiPrecision<N>.Ldexp(1, -bits / 4 + 1),
            MultiPrecision<N>.Ldexp(1, bits / 4 + 1),
            -MultiPrecision<N>.Ldexp(1, bits / 4 + 1),
            MultiPrecision<N>.Ldexp(1, -bits / 2 - 1),
            -MultiPrecision<N>.Ldexp(1, -bits / 2 - 1),
            MultiPrecision<N>.Ldexp(1, bits / 2 - 1),
            -MultiPrecision<N>.Ldexp(1, bits / 2 - 1),
            MultiPrecision<N>.Ldexp(1, -bits / 2),
            -MultiPrecision<N>.Ldexp(1, -bits / 2),
            MultiPrecision<N>.Ldexp(1, bits / 2),
            -MultiPrecision<N>.Ldexp(1, bits / 2),
            MultiPrecision<N>.Ldexp(1, -bits / 2 + 1),
            -MultiPrecision<N>.Ldexp(1, -bits / 2 + 1),
            MultiPrecision<N>.Ldexp(1, bits / 2 + 1),
            -MultiPrecision<N>.Ldexp(1, bits / 2 + 1),
            MultiPrecision<N>.Ldexp(1, -bits - 32),
            -MultiPrecision<N>.Ldexp(1, -bits - 32),
            MultiPrecision<N>.Ldexp(1, bits - 32),
            -MultiPrecision<N>.Ldexp(1, bits - 32),
            MultiPrecision<N>.Ldexp(1, -bits - 16),
            -MultiPrecision<N>.Ldexp(1, -bits - 16),
            MultiPrecision<N>.Ldexp(1, bits - 16),
            -MultiPrecision<N>.Ldexp(1, bits - 16),
            MultiPrecision<N>.Ldexp(1, -bits - 4),
            -MultiPrecision<N>.Ldexp(1, -bits - 4),
            MultiPrecision<N>.Ldexp(1, bits - 4),
            -MultiPrecision<N>.Ldexp(1, bits - 4),
            MultiPrecision<N>.Ldexp(1, -bits - 1),
            -MultiPrecision<N>.Ldexp(1, -bits - 1),
            MultiPrecision<N>.Ldexp(1, bits - 1),
            -MultiPrecision<N>.Ldexp(1, bits - 1),
            MultiPrecision<N>.Ldexp(1, -bits),
            -MultiPrecision<N>.Ldexp(1, -bits),
            MultiPrecision<N>.Ldexp(1, bits),
            -MultiPrecision<N>.Ldexp(1, bits),
            MultiPrecision<N>.Ldexp(1, -bits + 1),
            -MultiPrecision<N>.Ldexp(1, -bits + 1),
            MultiPrecision<N>.Ldexp(1, bits + 1),
            -MultiPrecision<N>.Ldexp(1, bits + 1),
            MultiPrecision<N>.Ldexp(1, -bits + 4),
            -MultiPrecision<N>.Ldexp(1, -bits + 4),
            MultiPrecision<N>.Ldexp(1, bits + 4),
            -MultiPrecision<N>.Ldexp(1, bits + 4),
            MultiPrecision<N>.Ldexp(1, -bits + 16),
            -MultiPrecision<N>.Ldexp(1, -bits + 16),
            MultiPrecision<N>.Ldexp(1, bits + 16),
            -MultiPrecision<N>.Ldexp(1, bits + 16),
            MultiPrecision<N>.Ldexp(1, -bits + 32),
            -MultiPrecision<N>.Ldexp(1, -bits + 32),
            MultiPrecision<N>.Ldexp(1, bits + 32),
            -MultiPrecision<N>.Ldexp(1, bits + 32),
            MultiPrecision<N>.Ldexp(1, -bits * 2 - 1),
            -MultiPrecision<N>.Ldexp(1, -bits * 2 - 1),
            MultiPrecision<N>.Ldexp(1, bits * 2 - 1),
            -MultiPrecision<N>.Ldexp(1, bits * 2 - 1),
            MultiPrecision<N>.Ldexp(1, -bits * 2),
            -MultiPrecision<N>.Ldexp(1, -bits * 2),
            MultiPrecision<N>.Ldexp(1, bits * 2),
            -MultiPrecision<N>.Ldexp(1, bits * 2),
            MultiPrecision<N>.Ldexp(1, -bits * 2 + 1),
            -MultiPrecision<N>.Ldexp(1, -bits * 2 + 1),
            MultiPrecision<N>.Ldexp(1, bits * 2 + 1),
            -MultiPrecision<N>.Ldexp(1, bits * 2 + 1),
            MultiPrecision<N>.PositiveInfinity,
            MultiPrecision<N>.NegativeInfinity,
            MultiPrecision<N>.NaN,
            MultiPrecision<N>.Epsilon,
            -MultiPrecision<N>.Epsilon,
        ];

        static readonly long[] us = [
            long.MinValue, long.MinValue + 1, -9999999999999999, int.MinValue, int.MinValue + 1,
            -99999999, -12345678, -1000000, -1000, -100, -64, -32, -16, -8, -7, -6, -5, -4, -3, -2, -1,
            0,
            long.MaxValue, int.MaxValue,
            9999999999999999, 12345678, 99999999, 1000000, 1000, 100, 64, 32, 16, 8, 7, 6, 5, 4, 3, 2, 1
        ];

        public static void AddTest() {
            long passes = 0;

            foreach (MultiPrecision<N> a in vs) {
                foreach (MultiPrecision<N> b in vs) {
                    MultiPrecision<N> c_actual = a + b;
                    double c_expect = (double)a + (double)b;

                    Trace.WriteLine($"{(double)a} + {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} + {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && double.IsNaN(c_expect)) {
                        continue;
                    }
                    if ((double.IsInfinity((double)a) || double.IsInfinity((double)b)) && !double.IsFinite(c_expect)) {
                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a) || MultiPrecision<N>.IsNaN(b)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
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

                    passes++;
                }
            }

            Console.WriteLine($"{nameof(bits)}:{bits} {passes}tests passed.");
        }

        public static void SubTest() {
            long passes = 0;

            Assert.AreNotEqual(MultiPrecision<N>.Zero, MultiPrecision<N>.One - MultiPrecision<N>.BitDecrement(1));

            foreach (MultiPrecision<N> a in vs) {
                foreach (MultiPrecision<N> b in vs) {
                    MultiPrecision<N> c_actual = a - b;
                    double c_expect = (double)a - (double)b;

                    Trace.WriteLine($"{(double)a} - {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} - {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && double.IsNaN(c_expect)) {
                        continue;
                    }
                    if ((double.IsInfinity((double)a) || double.IsInfinity((double)b)) && !double.IsFinite(c_expect)) {
                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a) || MultiPrecision<N>.IsNaN(b)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
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

                    passes++;
                }
            }

            Console.WriteLine($"{nameof(bits)}:{bits} {passes}tests passed.");
        }

        public static void MulTest() {
            long passes = 0;

            foreach (MultiPrecision<N> a in vs) {
                foreach (MultiPrecision<N> b in vs) {
                    MultiPrecision<N> c_actual = a * b;
                    double c_expect = (double)a * (double)b;

                    Trace.WriteLine($"{(double)a} * {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} * {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && double.IsNaN(c_expect)) {
                        continue;
                    }
                    if ((double.IsInfinity((double)a) || double.IsInfinity((double)b)) && !double.IsFinite(c_expect)) {
                        continue;
                    }
                    if (c_expect < 1e-304) {
                        continue;
                    }
                    if ((!MultiPrecision<N>.IsZero(a) && (double)a == 0) || (!MultiPrecision<N>.IsZero(b) && (double)b == 0)
                        || (!MultiPrecision<N>.IsZero(c_actual) && (double)c_actual == 0 && Math.Abs(c_expect) < 1e-308)) {
                        Console.WriteLine($"{(double)a} * {(double)b} = {c_expect}");
                        Console.WriteLine($"{a} * {b} = {c_actual}");

                        Console.WriteLine("ignore epsilon");

                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a) || MultiPrecision<N>.IsNaN(b)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
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

                    passes++;
                }
            }

            Console.WriteLine($"{nameof(bits)}:{bits} {passes}tests passed.");
        }

        public static void DivTest() {
            long passes = 0;

            foreach (MultiPrecision<N> a in vs) {
                foreach (MultiPrecision<N> b in vs) {
                    MultiPrecision<N> c_actual = a / b;
                    double c_expect = (double)a / (double)b;

                    Trace.WriteLine($"{(double)a} / {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} / {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && double.IsNaN(c_expect)) {
                        continue;
                    }
                    if (double.IsInfinity((double)b) && c_expect == 0) {
                        continue;
                    }
                    if (double.IsInfinity((double)a) && double.IsInfinity(c_expect)) {
                        continue;
                    }
                    if (double.IsInfinity((double)a) && double.IsInfinity((double)b) && double.IsNaN(c_expect)) {
                        continue;
                    }
                    if (c_expect < 1e-304) {
                        continue;
                    }

                    if ((!MultiPrecision<N>.IsZero(a) && (double)a == 0) || (!MultiPrecision<N>.IsZero(b) && (double)b == 0)
                        || (!MultiPrecision<N>.IsZero(c_actual) && (double)c_actual == 0 && Math.Abs(c_expect) < 1e-308)) {
                        Console.WriteLine($"{(double)a} / {(double)b} = {c_expect}");
                        Console.WriteLine($"{a} / {b} = {c_actual}");

                        Console.WriteLine("ignore epsilon");

                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a) || MultiPrecision<N>.IsNaN(b)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
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

                    passes++;
                }
            }

            Console.WriteLine($"{nameof(bits)}:{bits} {passes}tests passed.");
        }

        public static void RemTest() {
            foreach (MultiPrecision<N> a in vs) {
                foreach (MultiPrecision<N> b in vs) {
                    if ((a / b).Exponent >= MultiPrecision<N>.Bits / 2) {
                        continue;
                    }

                    MultiPrecision<N> c_actual = a % b;
                    double c_expect = (double)a % (double)b;

                    Trace.WriteLine($"{(double)a} % {(double)b} = {c_expect}");
                    Trace.WriteLine($"{a} % {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && double.IsNaN(c_expect)) {
                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a) || MultiPrecision<N>.IsNaN(b)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
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

        public static void MulLongTest() {
            foreach (MultiPrecision<N> a in vs) {
                foreach (long b in us) {
                    MultiPrecision<N> c_actual = MultiPrecision<N>.Mul(a, b);
                    MultiPrecision<N> c_expect = MultiPrecision<N>.Mul(a, (MultiPrecision<N>)b);

                    Trace.WriteLine($"{a} * {b} = {c_expect}");
                    Trace.WriteLine($"{a} * {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && MultiPrecision<N>.IsNaN(c_expect)) {
                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
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

        public static void DivLongTest() {
            foreach (MultiPrecision<N> a in vs) {
                foreach (long b in us) {
                    MultiPrecision<N> c_actual = MultiPrecision<N>.Div(a, b);
                    MultiPrecision<N> c_expect = MultiPrecision<N>.Div(a, (MultiPrecision<N>)b);

                    Trace.WriteLine($"{a} / {b} = {c_expect}");
                    Trace.WriteLine($"{a} / {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && MultiPrecision<N>.IsNaN(c_expect)) {
                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
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

                        Assert.IsTrue(MultiPrecision<N>.NearlyEqualBits(c_expect, c_actual, 1));
                    }
                }
            }
        }

        public static void AddLongTest() {
            foreach (MultiPrecision<N> a in vs) {
                foreach (long b in us) {
                    MultiPrecision<N> c_actual = MultiPrecision<N>.Add(a, b);
                    MultiPrecision<N> c_expect = MultiPrecision<N>.Add(a, (MultiPrecision<N>)b);

                    Trace.WriteLine($"{a} + {b} = {c_expect}");
                    Trace.WriteLine($"{a} + {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && MultiPrecision<N>.IsNaN(c_expect)) {
                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
                            Console.WriteLine($"{a} + {b} = {c_expect}");
                            Console.WriteLine($"{a} + {b} = {c_actual}");
                            Console.Write("\n");

                            Assert.Fail();
                        }
                    }
                    else {
                        Console.WriteLine($"{a} + {b} = {c_expect}");
                        Console.WriteLine($"{a} + {b} = {c_actual}");
                        Console.Write("\n");

                        Assert.AreEqual(c_expect, c_actual);
                    }
                }
            }
        }

        public static void SubLongTest() {
            foreach (MultiPrecision<N> a in vs) {
                foreach (long b in us) {
                    MultiPrecision<N> c_actual = MultiPrecision<N>.Sub(a, b);
                    MultiPrecision<N> c_expect = MultiPrecision<N>.Sub(a, (MultiPrecision<N>)b);

                    Trace.WriteLine($"{a} - {b} = {c_expect}");
                    Trace.WriteLine($"{a} - {b} = {c_actual}");

                    if (MultiPrecision<N>.IsNaN(c_actual) && MultiPrecision<N>.IsNaN(c_expect)) {
                        continue;
                    }

                    if (MultiPrecision<N>.IsNaN(a)) {
                        if (!MultiPrecision<N>.IsNaN(c_actual)) {
                            Console.WriteLine($"{a} - {b} = {c_expect}");
                            Console.WriteLine($"{a} - {b} = {c_actual}");
                            Console.Write("\n");

                            Assert.Fail();
                        }
                    }
                    else {
                        Console.WriteLine($"{a} - {b} = {c_expect}");
                        Console.WriteLine($"{a} - {b} = {c_actual}");
                        Console.Write("\n");

                        Assert.AreEqual(c_expect, c_actual);
                    }
                }
            }
        }
    }

    [TestClass]
    public class MultiPrecisionTest {
        [TestMethod]
        public void AddTest() {
            Assert.AreEqual(double.CopySign(1, 0d + double.NegativeZero), MultiPrecision<N4>.CopySign(1, MultiPrecision<N4>.PlusZero + MultiPrecision<N4>.MinusZero));
            Assert.AreEqual(double.CopySign(1, double.NegativeZero + 0d), MultiPrecision<N4>.CopySign(1, MultiPrecision<N4>.MinusZero + MultiPrecision<N4>.PlusZero));
            Assert.AreEqual(double.CopySign(1, double.NegativeZero + double.NegativeZero), MultiPrecision<N4>.CopySign(1, MultiPrecision<N4>.MinusZero + MultiPrecision<N4>.MinusZero));

            MultiPrecisionTest<N4>.AddTest();
            MultiPrecisionTest<N8>.AddTest();
            MultiPrecisionTest<N9>.AddTest();
            MultiPrecisionTest<N10>.AddTest();
            MultiPrecisionTest<N11>.AddTest();
            MultiPrecisionTest<N12>.AddTest();
            MultiPrecisionTest<N16>.AddTest();
            MultiPrecisionTest<N32>.AddTest();
        }

        [TestMethod]
        public void SubTest() {
            Assert.AreEqual(double.CopySign(1, 0d - double.NegativeZero), MultiPrecision<N4>.CopySign(1, MultiPrecision<N4>.PlusZero - MultiPrecision<N4>.MinusZero));
            Assert.AreEqual(double.CopySign(1, double.NegativeZero - 0d), MultiPrecision<N4>.CopySign(1, MultiPrecision<N4>.MinusZero - MultiPrecision<N4>.PlusZero));
            Assert.AreEqual(double.CopySign(1, double.NegativeZero - double.NegativeZero), MultiPrecision<N4>.CopySign(1, MultiPrecision<N4>.MinusZero - MultiPrecision<N4>.MinusZero));

            MultiPrecisionTest<N4>.SubTest();
            MultiPrecisionTest<N8>.SubTest();
            MultiPrecisionTest<N9>.SubTest();
            MultiPrecisionTest<N10>.SubTest();
            MultiPrecisionTest<N11>.SubTest();
            MultiPrecisionTest<N12>.SubTest();
            MultiPrecisionTest<N16>.SubTest();
            MultiPrecisionTest<N32>.SubTest();
        }

        [TestMethod]
        public void MulTest() {
            MultiPrecisionTest<N4>.MulTest();
            MultiPrecisionTest<N8>.MulTest();
            MultiPrecisionTest<N9>.MulTest();
            MultiPrecisionTest<N10>.MulTest();
            MultiPrecisionTest<N11>.MulTest();
            MultiPrecisionTest<N12>.MulTest();
            MultiPrecisionTest<N16>.MulTest();
            MultiPrecisionTest<N32>.MulTest();
        }

        [TestMethod]
        public void DivTest() {
            MultiPrecisionTest<N4>.DivTest();
            MultiPrecisionTest<N8>.DivTest();
            MultiPrecisionTest<N9>.DivTest();
            MultiPrecisionTest<N10>.DivTest();
            MultiPrecisionTest<N11>.DivTest();
            MultiPrecisionTest<N12>.DivTest();
            MultiPrecisionTest<N16>.DivTest();
            MultiPrecisionTest<N32>.DivTest();
        }

        [TestMethod]
        public void RemTest() {
            MultiPrecisionTest<N4>.RemTest();
            MultiPrecisionTest<N8>.RemTest();
            MultiPrecisionTest<N9>.RemTest();
            MultiPrecisionTest<N10>.RemTest();
            MultiPrecisionTest<N11>.RemTest();
            MultiPrecisionTest<N12>.RemTest();
            MultiPrecisionTest<N16>.RemTest();
            MultiPrecisionTest<N32>.RemTest();
        }

        [TestMethod]
        public void AddLongTest() {
            MultiPrecisionTest<N4>.AddLongTest();
            MultiPrecisionTest<N8>.AddLongTest();
            MultiPrecisionTest<N9>.AddLongTest();
            MultiPrecisionTest<N10>.AddLongTest();
            MultiPrecisionTest<N11>.AddLongTest();
            MultiPrecisionTest<N12>.AddLongTest();
            MultiPrecisionTest<N16>.AddLongTest();
            MultiPrecisionTest<N32>.AddLongTest();
        }

        [TestMethod]
        public void SubLongTest() {
            MultiPrecisionTest<N4>.SubLongTest();
            MultiPrecisionTest<N8>.SubLongTest();
            MultiPrecisionTest<N9>.SubLongTest();
            MultiPrecisionTest<N10>.SubLongTest();
            MultiPrecisionTest<N11>.SubLongTest();
            MultiPrecisionTest<N12>.SubLongTest();
            MultiPrecisionTest<N16>.SubLongTest();
            MultiPrecisionTest<N32>.SubLongTest();
        }

        [TestMethod]
        public void MulLongTest() {
            MultiPrecisionTest<N4>.MulLongTest();
            MultiPrecisionTest<N8>.MulLongTest();
            MultiPrecisionTest<N9>.MulLongTest();
            MultiPrecisionTest<N10>.MulLongTest();
            MultiPrecisionTest<N11>.MulLongTest();
            MultiPrecisionTest<N12>.MulLongTest();
            MultiPrecisionTest<N16>.MulLongTest();
            MultiPrecisionTest<N32>.MulLongTest();
        }

        [TestMethod]
        public void DivLongTest() {
            MultiPrecisionTest<N4>.DivLongTest();
            MultiPrecisionTest<N8>.DivLongTest();
            MultiPrecisionTest<N9>.DivLongTest();
            MultiPrecisionTest<N10>.DivLongTest();
            MultiPrecisionTest<N11>.DivLongTest();
            MultiPrecisionTest<N12>.DivLongTest();
            MultiPrecisionTest<N16>.DivLongTest();
            MultiPrecisionTest<N32>.DivLongTest();
        }
    }
}
