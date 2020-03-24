using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void AtanTest() {
            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Atan(x);
                double y_expect = Math.Atan((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }

            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = 65536 + (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Atan(x);
                double y_expect = Math.Atan((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }

            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = -65536 + (MultiPrecision<Pow2.N8>)i / 250;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Atan(x);
                double y_expect = Math.Atan((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }
        }

        [TestMethod]
        public void AsinTest() {
            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 1000;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Asin(x);
                double y_expect = Math.Asin((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }
        }

        [TestMethod]
        public void AcosTest() {
            for (Int64 i = -1000; i <= 1000; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 1000;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Acos(x);
                double y_expect = Math.Acos((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }
        }

        [TestMethod]
        public void SquareAsinTest() {
            for (Int64 i = 0; i <= 250; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 500;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.SquareAsin(x);
                double y_expect = Math.Asin((double)x) * Math.Asin((double)x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y_actual);
                Console.WriteLine((double)y_expect);
                Assert.AreEqual(y_expect, (double)y_actual, Math.Abs(y_expect * 1e-5) + 1e-10);
            }
        }
    }
}
