using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void TruncateTest() {
            MultiPrecision<Pow2.N8> p0 = 0;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p1 = (MultiPrecision<Pow2.N8>)(1) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p5 = (MultiPrecision<Pow2.N8>)(5) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p9 = (MultiPrecision<Pow2.N8>)(9) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Truncate((double)x), (double)y, 1e-5);
            }
        }

        [TestMethod]
        public void FloorTest() {
            MultiPrecision<Pow2.N8> p0 = 0;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p1 = (MultiPrecision<Pow2.N8>)(1) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p5 = (MultiPrecision<Pow2.N8>)(5) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p9 = (MultiPrecision<Pow2.N8>)(9) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Floor((double)x), (double)y, 1e-5);
            }
        }

        [TestMethod]
        public void CeilingTest() {
            MultiPrecision<Pow2.N8> p0 = 0;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p0 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p1 = (MultiPrecision<Pow2.N8>)(1) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p5 = (MultiPrecision<Pow2.N8>)(5) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p5 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p9 = (MultiPrecision<Pow2.N8>)(9) / 10;

            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p9 + 1 / (MultiPrecision<Pow2.N8>)i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Ceiling((double)x), (double)y, 1e-5);
            }
        }

        [TestMethod]
        public void TruncateUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(v);

                Assert.IsTrue(y.IsNaN);
            }
        }
    }
}
