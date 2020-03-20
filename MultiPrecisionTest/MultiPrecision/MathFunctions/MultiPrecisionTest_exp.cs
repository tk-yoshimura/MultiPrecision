using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ExpTest() {
            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Exp((double)x), (double)y, Math.Exp((double)x) * 1e-5);
            }

            MultiPrecision<Pow2.N8> p = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(p);

                Console.WriteLine((double)p);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Exp((double)p), (double)y, Math.Exp((double)p) * 1e-5);

                p *= 2;
            }

            MultiPrecision<Pow2.N8> n = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(n);

                Console.WriteLine((double)n);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Exp((double)n), (double)y, Math.Exp((double)n) * 1e-5);

                n /= 2;
            }

            MultiPrecision<Pow2.N8> p2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(p2);

                Console.WriteLine((double)p2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Exp((double)p2), (double)y, Math.Exp((double)p2) * 1e-5);

                p2 *= 2;
            }

            MultiPrecision<Pow2.N8> n2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(n2);

                Console.WriteLine((double)n2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Exp((double)n2), (double)y, Math.Exp((double)n2) * 1e-5);

                n2 /= 2;
            }

            MultiPrecision<Pow2.N8> p1 = (MultiPrecision<Pow2.N8>)(1) / 10;
            for (Int64 i = 1; i <= 1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Exp((double)x), (double)y, Math.Exp((double)x) * 1e-5);
            }

            for (Int64 i = -1; i >= -1000; i *= 10) {
                MultiPrecision<Pow2.N8> x = p1 + i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Exp(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Exp((double)x), (double)y, Math.Exp((double)x) * 1e-5);
            }
        }
    }
}
