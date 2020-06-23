using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void TanhTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Tanh(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Tanh((double)x), y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void SinhTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sinh(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Sinh((double)x), y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void CoshTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Cosh(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Cosh((double)x), y, ignore_sign: true);
            }
        }

        [TestMethod]
        public void TanhBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Tanh(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Tanh((double)x), y);
                }

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void SinhBorderTest() {
            MultiPrecision<Pow2.N8>[] borders = new MultiPrecision<Pow2.N8>[] { 0, -1, 1 };

            foreach (MultiPrecision<Pow2.N8> b in borders) {
                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor(b, 2)) {
                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Sinh(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    TestTool.Tolerance(Math.Sinh((double)x), y);
                }

                Console.Write("\n");
            }
        }
    }
}
