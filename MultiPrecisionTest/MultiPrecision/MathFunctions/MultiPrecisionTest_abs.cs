using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    [TestClass]
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void AbsTest() {
            for (Int64 i = 1; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Abs((double)x), (double)y, 1e-5);
            }

            for (Int64 i = -1; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(x);

                Console.WriteLine((double)x);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Abs((double)x), (double)y, 1e-5);
            }

            MultiPrecision<Pow2.N8> p = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(p);

                Console.WriteLine((double)p);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Abs((double)p), (double)y, 1e-5);

                p *= 2;
            }

            MultiPrecision<Pow2.N8> n = 1;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(n);

                Console.WriteLine((double)n);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Abs((double)n), (double)y, 1e-5);

                n /= 2;
            }

            MultiPrecision<Pow2.N8> p2 = 255;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(p2);

                Console.WriteLine((double)p2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Abs((double)p2), (double)y, 1e-5);

                p2 *= 2;
            }

            MultiPrecision<Pow2.N8> n2 = 3;
            for (int i = 0; i < 32; i++) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(n2);

                Console.WriteLine((double)n2);
                Console.WriteLine((double)y);
                Assert.AreEqual(Math.Abs((double)n2), (double)y, 1e-5);

                n2 /= 2;
            }
        }

        [TestMethod]
        public void AbsBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0 };
            
            foreach(MultiPrecision<Pow2.N8> b in borders) { 
                foreach(MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) { 
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Abs(x);

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(Math.Abs((double)x), (double)y, 1e-10);
                    Assert.AreEqual(Math.Sign(Math.Abs((double)x)), Math.Sign((double)y), 1e-10);
                }

                Console.Write("\n");
            }
        }
    }
}
