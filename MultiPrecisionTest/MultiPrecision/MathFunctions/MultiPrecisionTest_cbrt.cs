using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CbrtTest() {
            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Cbrt((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Cbrt((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(p);

                Console.WriteLine((double)p);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Cbrt((double)p), (double)y, 1e-5);

                p *= 2;
            }

            MultiPrecision<Pow2.N8> n = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(n);

                Console.WriteLine((double)n);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Cbrt((double)n), (double)y, 1e-5);

                n /= 2;
            }

            MultiPrecision<Pow2.N8> p2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(p2);

                Console.WriteLine((double)p2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Cbrt((double)p2), (double)y, 1e-5);

                p2 *= 2;
            }

            MultiPrecision<Pow2.N8> n2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(n2);

                Console.WriteLine((double)n2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Cbrt((double)n2), (double)y, 1e-5);

                n2 /= 2;
            }

            for (int i = 0; i <= 1024; i++) {
                MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)i / 512;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cbrt(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Cbrt((double)x), (double)y, 1e-5);
            }
        }
    }
}
