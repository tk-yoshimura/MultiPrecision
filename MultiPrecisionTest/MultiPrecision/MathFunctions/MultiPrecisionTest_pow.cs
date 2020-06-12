using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void PowTest() {
            { 
                for (Int64 i = 0; i <= 100; i++) {
                    MultiPrecision<Pow2.N8> x = i;
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(x, 1.5);

                    Console.WriteLine((double)x);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)x, 1.5), (double)y, Math.Pow((double)x, 1.5) * 1e-5);
                }

                MultiPrecision<Pow2.N8> p = 1;
                for (int i = 0; i < 32; i++) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(p, 1.5);

                    Console.WriteLine((double)p);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)p, 1.5), (double)y, Math.Pow((double)p, 1.5) * 1e-5);

                    p *= 2;
                }

                MultiPrecision<Pow2.N8> n = 1;
                for (int i = 0; i < 32; i++) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(n, 1.5);

                    Console.WriteLine((double)n);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)n, 1.5), (double)y, Math.Pow((double)n, 1.5) * 1e-5);

                    n /= 2;
                }
            }

            { 
                for (Int64 i = 0; i <= 100; i++) {
                    MultiPrecision<Pow2.N8> x = i;
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(x, -1.5);

                    Console.WriteLine((double)x);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)x, -1.5), (double)y, Math.Pow((double)x, -1.5) * 1e-5);
                }

                MultiPrecision<Pow2.N8> p = 1;
                for (int i = 0; i < 32; i++) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(p, -1.5);

                    Console.WriteLine((double)p);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)p, -1.5), (double)y, Math.Pow((double)p, -1.5) * 1e-5);

                    p *= 2;
                }

                MultiPrecision<Pow2.N8> n = 1;
                for (int i = 0; i < 32; i++) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(n, -1.5);

                    Console.WriteLine((double)n);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)n, -1.5), (double)y, Math.Pow((double)n, -1.5) * 1e-5);

                    n /= 2;
                }
            }

            { 
                for (Int64 i = 0; i <= 100; i++) {
                    MultiPrecision<Pow2.N8> x = i;
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(x, 0);

                    Console.WriteLine((double)x);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)x, 0), (double)y, Math.Pow((double)x, 0) * 1e-5);
                }

                MultiPrecision<Pow2.N8> p = 1;
                for (int i = 0; i < 32; i++) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(p, 0);

                    Console.WriteLine((double)p);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)p, 0), (double)y, Math.Pow((double)p, 0) * 1e-5);

                    p *= 2;
                }

                MultiPrecision<Pow2.N8> n = 1;
                for (int i = 0; i < 32; i++) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Pow(n, 0);

                    Console.WriteLine((double)n);
                    Console.WriteLine((double)y);
                    Assert.AreEqual(Math.Pow((double)n, 0), (double)y, Math.Pow((double)n, 0) * 1e-5);

                    n /= 2;
                }
            }
        }

        [TestMethod]
        public void PowUnnormalValueTest() {
            MultiPrecision<Pow2.N8> nz = MultiPrecision<Pow2.N8>.Pow(MultiPrecision<Pow2.N8>.NaN, 0);
            Assert.IsTrue(nz.IsNaN);

            MultiPrecision<Pow2.N8> zn = MultiPrecision<Pow2.N8>.Pow(0, MultiPrecision<Pow2.N8>.NaN);
            Assert.IsTrue(zn.IsNaN);

            MultiPrecision<Pow2.N8> zz = MultiPrecision<Pow2.N8>.Pow(0, 0);
            Assert.AreEqual(1, zz);

            MultiPrecision<Pow2.N8> nn = MultiPrecision<Pow2.N8>.Pow(MultiPrecision<Pow2.N8>.NaN, MultiPrecision<Pow2.N8>.NaN);
            Assert.IsTrue(nn.IsNaN);
        }
    }
}
