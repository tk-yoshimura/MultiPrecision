using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CosHalfPITest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosHalfPI(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Cos((double)x * Math.PI / 2), y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void CosPITest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosPI(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Cos((double)x * Math.PI), y, minerr:1e-4, ignore_sign: true);
            }
        }

        [TestMethod]
        public void SinPITest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.SinPI(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Sin((double)x * Math.PI), y, minerr:1e-4, ignore_sign: true);
            }
        }

        [TestMethod]
        public void TanPITest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 
                if(x * 2 == MultiPrecision<Pow2.N8>.Truncate(x * 2)) { 
                    continue;
                }

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.TanPI(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Tan((double)x * Math.PI), y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void CosTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cos(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Cos((double)x), y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void SinTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sin(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Sin((double)x), y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void TanTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Tan(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Tan((double)x), y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void CosHalfPIBorderTest() {
            MultiPrecision<Pow2.N8> half = MultiPrecision<Pow2.N8>.Ldexp(1, -1);

            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] {
                -65536 - half, -65536, -65535 - half, -4 - half, -4, -3 - half, -3, -2 - half, -2, -1 - half, -1, -0 - half,
                0,
                0 + half, 1, 1 + half, 2, 2 + half, 3, 3 + half, 4, 4 + half, 65535 + half, 65536, 65536 + half
            };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosHalfPI(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Cos((double)x * Math.PI / 2), y, ignore_sign: true);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void CosHalfPIUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.CosHalfPI(v);

                Assert.IsTrue(y.IsNaN);
            }
        }
    }
}
