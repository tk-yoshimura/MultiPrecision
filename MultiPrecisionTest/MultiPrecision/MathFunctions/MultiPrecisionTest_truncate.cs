using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void TruncateTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.TruncateSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Truncate((double)x), y);
            }
        }

        [TestMethod]
        public void FloorTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.TruncateSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Floor(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Floor((double)x), y);
            }
        }

        [TestMethod]
        public void CeilingTest() {
            foreach (MultiPrecision<Pow2.N8> x in TestTool.TruncateSet<Pow2.N8>()) {

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Ceiling(x);

                Console.WriteLine(x);
                Console.WriteLine(y);

                TestTool.Tolerance(Math.Ceiling((double)x), y);
            }
        }

        [TestMethod]
        public void RoundTest() {
            for (int i = -10; i <= 10; i++) {

                MultiPrecision<Pow2.N4> x1 = i - MultiPrecision<Pow2.N4>.Point5;
                MultiPrecision<Pow2.N4> x2 = MultiPrecision<Pow2.N4>.BitDecrement(x1);
                MultiPrecision<Pow2.N4> x3 = i + MultiPrecision<Pow2.N4>.Point5;
                MultiPrecision<Pow2.N4> x4 = MultiPrecision<Pow2.N4>.BitDecrement(x3);

                Console.WriteLine(x1.ToHexcode());
                Console.WriteLine(x2.ToHexcode());
                Console.WriteLine(x3.ToHexcode());
                Console.WriteLine(x4.ToHexcode());

                Assert.AreEqual(i, MultiPrecision<Pow2.N4>.Round(x1));
                Assert.AreEqual(i - 1, MultiPrecision<Pow2.N4>.Round(x2));
                Assert.AreEqual(i + 1, MultiPrecision<Pow2.N4>.Round(x3));
                Assert.AreEqual(i, MultiPrecision<Pow2.N4>.Round(x4));
            }
        }

        [TestMethod]
        public void RoundMantissaTest() {
            const int cases = 8;

            MultiPrecision<Pow2.N4>[] tests = new MultiPrecision<Pow2.N4>[cases] {
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFE, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFF8, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7F, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7E, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF78, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
            };

            MultiPrecision<Pow2.N4>[] expects = new MultiPrecision<Pow2.N4>[cases] {
                new MultiPrecision<Pow2.N4>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFF8, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF80, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF80, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF78, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
            };

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Pow2.N4> actual = MultiPrecision<Pow2.N4>.RoundMantissa(tests[i], 2);

                Console.WriteLine(expects[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects[i], actual);
            }
        }

        [TestMethod]
        public void TruncateMantissaTest() {
            const int cases = 8;

            MultiPrecision<Pow2.N4>[] tests = new MultiPrecision<Pow2.N4>[cases] {
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFE, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFF8, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7F, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7E, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF78, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
            };

            MultiPrecision<Pow2.N4>[] expects = new MultiPrecision<Pow2.N4>[cases] {
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFFF8, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xFFFFFF78, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
            };

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Pow2.N4> actual = MultiPrecision<Pow2.N4>.TruncateMantissa(tests[i], 2);

                Console.WriteLine(expects[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects[i], actual);
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
