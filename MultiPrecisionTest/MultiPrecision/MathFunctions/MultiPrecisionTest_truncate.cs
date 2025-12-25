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
            for (MultiPrecision<Pow2.N8> x = -32; x <= 32; x += 0.25) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Round(x);
                double z = double.Round((double)x);

                Assert.AreEqual((MultiPrecision<Pow2.N8>)z, y);
            }

            for (int n = -16; n <= 16; n++) {
                Assert.AreEqual((MultiPrecision<Pow2.N8>)(n), MultiPrecision<Pow2.N8>.Round(MultiPrecision<Pow2.N8>.BitDecrement(n)));
                Assert.AreEqual((MultiPrecision<Pow2.N8>)(n), MultiPrecision<Pow2.N8>.Round(MultiPrecision<Pow2.N8>.BitIncrement(n)));
            }

            for (int n = -16; n <= 16; n++) {
                Assert.AreEqual((MultiPrecision<Pow2.N8>)(n), MultiPrecision<Pow2.N8>.Round(MultiPrecision<Pow2.N8>.BitDecrement(n + 0.5)));
                Assert.AreEqual((MultiPrecision<Pow2.N8>)(n + 1), MultiPrecision<Pow2.N8>.Round(MultiPrecision<Pow2.N8>.BitIncrement(n + 0.5)));
            }

            for (int exp = 4; exp <= 100; exp++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(1, exp);

                Assert.AreEqual(x, MultiPrecision<Pow2.N8>.Round(x));
                Assert.AreEqual(x, MultiPrecision<Pow2.N8>.Round(x + 0.5));
                Assert.AreEqual(x + 2, MultiPrecision<Pow2.N8>.Round(x + 1.5));

                Assert.AreEqual((-x), MultiPrecision<Pow2.N8>.Round(-x));
                Assert.AreEqual((-x), MultiPrecision<Pow2.N8>.Round(-x - 0.5));
                Assert.AreEqual((-x - 2), MultiPrecision<Pow2.N8>.Round(-x - 1.5));
            }

            for (int exp = 4; exp <= 100; exp++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(1, exp);

                Assert.AreEqual(x, MultiPrecision<Pow2.N8>.Round(MultiPrecision<Pow2.N8>.BitDecrement(x)));
                Assert.AreEqual(x, MultiPrecision<Pow2.N8>.Round(x + 0.4));
                Assert.AreEqual(x + 1, MultiPrecision<Pow2.N8>.Round(x + 1.4));

                Assert.AreEqual((-x), MultiPrecision<Pow2.N8>.Round(MultiPrecision<Pow2.N8>.BitIncrement(-x)));
                Assert.AreEqual((-x), MultiPrecision<Pow2.N8>.Round(-x - 0.4));
                Assert.AreEqual((-x - 1), MultiPrecision<Pow2.N8>.Round(-x - 1.4));
            }

            for (int exp = 4; exp <= 100; exp++) {
                MultiPrecision<Pow2.N8> x = MultiPrecision<Pow2.N8>.Ldexp(1, exp);

                Assert.AreEqual(x, MultiPrecision<Pow2.N8>.Round(MultiPrecision<Pow2.N8>.BitDecrement(x)));
                Assert.AreEqual(x + 1, MultiPrecision<Pow2.N8>.Round(x + 0.6));
                Assert.AreEqual(x + 2, MultiPrecision<Pow2.N8>.Round(x + 1.6));

                Assert.AreEqual((-x), MultiPrecision<Pow2.N8>.Round(MultiPrecision<Pow2.N8>.BitIncrement(-x)));
                Assert.AreEqual((-x - 1), MultiPrecision<Pow2.N8>.Round(-x - 0.6));
                Assert.AreEqual((-x - 2), MultiPrecision<Pow2.N8>.Round(-x - 1.6));
            }
        }

        [TestMethod]
        public void RoundMantissaTest() {
            const int cases = 8;

            MultiPrecision<Pow2.N4>[] tests = [
                new(Sign.Plus, 0, [0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFFE, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFF8, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7F, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7E, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF78, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
            ];

            MultiPrecision<Pow2.N4>[] expects = [
                new(Sign.Plus, 1, [0x00000000, 0x00000000, 0x00000000, 0x80000000]),
                new(Sign.Plus, 1, [0x00000000, 0x00000000, 0x00000000, 0x80000000]),
                new(Sign.Plus, 0, [0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFF8, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF80, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF80, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF78, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
            ];

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

            MultiPrecision<Pow2.N4>[] tests = [
                new(Sign.Plus, 0, [0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFFE, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFF8, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7F, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7E, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF78, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
            ];

            MultiPrecision<Pow2.N4>[] expects = [
                new(Sign.Plus, 0, [0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFFC, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFFF8, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF7C, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
                new(Sign.Plus, 0, [0xFFFFFF78, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF]),
            ];

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Pow2.N4> actual = MultiPrecision<Pow2.N4>.TruncateMantissa(tests[i], 2);

                Console.WriteLine(expects[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects[i], actual);
            }
        }


        [TestMethod]
        public void TruncateUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] vs = [
                MultiPrecision<Pow2.N8>.NaN,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
            ];

            foreach (MultiPrecision<Pow2.N8> v in vs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Truncate(v);

                Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(y));
            }
        }
    }
}
