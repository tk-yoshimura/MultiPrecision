using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Log10Test() {
            for(Int64 i = 1; i <= 100000000000; i *= 10) { 
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log10(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log10((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p = 1;
            for(int i = 0; i < 32; i++) { 
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log10(p);

                Console.WriteLine((double)p);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log10((double)p), (double)y, 1e-5);

                p *= 2;
            }

            MultiPrecision<Pow2.N8> n = 1;
            for(int i = 0; i < 32; i++) { 
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log10(n);

                Console.WriteLine((double)n);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log10((double)n), (double)y, 1e-5);

                n /= 2;
            }

            MultiPrecision<Pow2.N8> p2 = 255;
            for(int i = 0; i < 32; i++) { 
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log10(p2);

                Console.WriteLine((double)p2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log10((double)p2), (double)y, 1e-5);

                p2 *= 2;
            }

            MultiPrecision<Pow2.N8> n2 = 3;
            for(int i = 0; i < 32; i++) { 
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log10(n2);

                Console.WriteLine((double)n2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Log10((double)n2), (double)y, 1e-5);

                n2 /= 2;
            }
        }
    }
}
