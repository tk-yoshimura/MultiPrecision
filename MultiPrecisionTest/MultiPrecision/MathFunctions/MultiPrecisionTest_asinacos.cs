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
        public void Atan2Test() {
            for (Int64 iy = -10; iy <= 10; iy++) {
                for (Int64 ix = -10; ix <= 10; ix++) {
                    MultiPrecision<Pow2.N8> x = (MultiPrecision<Pow2.N8>)ix / 10;
                    MultiPrecision<Pow2.N8> y = (MultiPrecision<Pow2.N8>)iy / 10;
                    MultiPrecision<Pow2.N8> d_actual = MultiPrecision<Pow2.N8>.Atan2(y, x);
                    double d_expect = Math.Atan2((double)y, (double)x);

                    Console.WriteLine($"{(double)x}, {(double)y}");
                    Console.WriteLine((double)d_actual);
                    Console.WriteLine((double)d_expect);
                    Assert.AreEqual(d_expect, (double)d_actual, Math.Abs(d_expect * 1e-5) + 1e-10);
                }
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

        [TestMethod]
        public void AtanBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { -2, -1, 0, 1, 2 };
            
            foreach(MultiPrecision<Pow2.N8> b in borders) { 
                foreach(MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) { 
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan(x);

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(Math.Atan((double)x), (double)y, 1e-10);
                    Assert.AreEqual(Math.Sign(Math.Atan((double)x)), Math.Sign((double)y), 1e-10);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void AsinBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 
                -MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Sqrt2, -1), 
                0, 
                MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Sqrt2, -1), 
            };
            
            foreach(MultiPrecision<Pow2.N8> b in borders) { 
                foreach(MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) { 
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Asin(x);

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(Math.Asin((double)x), (double)y, 1e-10);
                    Assert.AreEqual(Math.Sign(Math.Asin((double)x)), Math.Sign((double)y), 1e-10);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void AcosBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 
                -MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Sqrt2, -1), 
                0, 
                MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Sqrt2, -1), 
            };
            
            foreach(MultiPrecision<Pow2.N8> b in borders) { 
                foreach(MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) { 
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Acos(x);

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(Math.Acos((double)x), (double)y, 1e-10);
                    Assert.AreEqual(Math.Sign(Math.Acos((double)x)), Math.Sign((double)y), 1e-10);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void Atan2BorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 
                0, 
            };
            
            foreach(MultiPrecision<Pow2.N8> b in borders) { 
                foreach(MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) { 
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(x, 1);

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(Math.Atan2((double)x, 1), (double)y, 1e-10);
                    Assert.AreEqual(Math.Sign(Math.Atan2((double)x, 1)), Math.Sign((double)y), 1e-10);
                }

                Console.Write("\n");
            }

            foreach(MultiPrecision<Pow2.N8> b in borders) { 
                foreach(MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) { 
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(x, -1);

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(Math.Atan2((double)x, -1), (double)y, 1e-10);
                    Assert.AreEqual(Math.Sign(Math.Atan2((double)x, -1)), Math.Sign((double)y), 1e-10);
                }

                Console.Write("\n");
            }

            foreach(MultiPrecision<Pow2.N8> b in borders) { 
                foreach(MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) { 
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(1, x);

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(Math.Atan2(1, (double)x), (double)y, 1e-10);
                    Assert.AreEqual(Math.Sign(Math.Atan2(1, (double)x)), Math.Sign((double)y), 1e-10);
                }

                Console.Write("\n");
            }

            foreach(MultiPrecision<Pow2.N8> b in borders) { 
                foreach(MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) { 
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Atan2(-1, x);

                    Console.WriteLine(x);
                    Console.WriteLine($"{x.Sign} {x.Exponent}, {UIntUtil.ToHexcode(x.Mantissa)}");
                    Console.WriteLine(y);
                    Console.WriteLine($"{y.Sign} {y.Exponent}, {UIntUtil.ToHexcode(y.Mantissa)}");
                    Console.Write("\n");

                    Assert.AreEqual(Math.Atan2(-1, (double)x), (double)y, 1e-10);
                    Assert.AreEqual(Math.Sign(Math.Atan2(-1, (double)x)), Math.Sign((double)y), 1e-10);
                }

                Console.Write("\n");
            }
        }
    }
}
