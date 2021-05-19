using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void ToStringTest() {

            foreach (MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>().Concat(TestTool.LimitSet<Pow2.N8>())) {

                foreach (MultiPrecision<Pow2.N8> v in TestTool.EnumerateNeighbor(x, x.IsZero ? 0 : 2)) {

                    (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore(MultiPrecision<Pow2.N8>.DecimalDigits);

                    Console.WriteLine(sign);
                    Console.WriteLine(exponent_dec);
                    Console.WriteLine(mantissa_dec);
                    Console.WriteLine(v.ToString());
                    Console.WriteLine($"{v:E10}");
                    Console.WriteLine(v.ToString("e10"));

                    MultiPrecision<Pow2.N8> u = v.ToString();
                    Console.WriteLine(u.ToString());

                    Console.WriteLine(v.ToHexcode());
                    Console.WriteLine(u.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 14));
                    Assert.AreEqual((double)v, double.Parse(v.ToString()), Math.Abs((double)v) * 1e-8);
                }
            }

            foreach (MultiPrecision<Pow2.N16> x in TestTool.AllRangeSet<Pow2.N16>().Concat(TestTool.LimitSet<Pow2.N16>())) {
                foreach (MultiPrecision<Pow2.N16> v in TestTool.EnumerateNeighbor(x, x.IsZero ? 0 : 2)) {

                    (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N16> mantissa_dec) = v.ToStringCore(MultiPrecision<Pow2.N16>.DecimalDigits);

                    Console.WriteLine(sign);
                    Console.WriteLine(exponent_dec);
                    Console.WriteLine(mantissa_dec);
                    Console.WriteLine(v.ToString());
                    Console.WriteLine($"{v:E10}");
                    Console.WriteLine(v.ToString("e10"));

                    MultiPrecision<Pow2.N16> u = v.ToString();
                    Console.WriteLine(u.ToString());

                    Console.WriteLine(v.ToHexcode());
                    Console.WriteLine(u.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(v, u, 14));
                    Assert.AreEqual((double)v, double.Parse(v.ToString()), Math.Abs((double)v) * 1e-8);
                }
            }

            for (long i = int.MinValue; i < int.MinValue + 120; i++) {
                MultiPrecision<Pow2.N8> v = new MultiPrecision<Pow2.N8>(Sign.Plus, i, Mantissa<Pow2.N8>.One, round: false);

                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore(MultiPrecision<Pow2.N8>.DecimalDigits);

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
                Console.WriteLine($"{v:E10}");
                Console.WriteLine(v.ToString("e10"));

                MultiPrecision<Pow2.N8> u = v.ToString();
                Console.WriteLine(u.ToString());

                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(u.ToHexcode());

                Assert.AreEqual((double)v, double.Parse(v.ToString()), Math.Abs((double)v) * 1e-8);
            }

            for (long i = int.MaxValue - 120; i < int.MaxValue; i++) {
                MultiPrecision<Pow2.N8> v = new MultiPrecision<Pow2.N8>(Sign.Plus, i, Mantissa<Pow2.N8>.One, round: false);

                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore(MultiPrecision<Pow2.N8>.DecimalDigits);

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
                Console.WriteLine($"{v:E10}");
                Console.WriteLine(v.ToString("e10"));

                MultiPrecision<Pow2.N8> u = v.ToString();
                Console.WriteLine(u.ToString());

                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(u.ToHexcode());

                Assert.AreEqual((double)v, double.Parse(v.ToString()), Math.Abs((double)v) * 1e-8);
            }

            {
                MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.PI;

                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore(MultiPrecision<Pow2.N8>.DecimalDigits);

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
                Console.WriteLine($"{v:E10}");
                Console.WriteLine(v.ToString("e10"));

                MultiPrecision<Pow2.N8> u = v.ToString();
                Console.WriteLine(u.ToString());

                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(u.ToHexcode());

                Assert.AreEqual((double)v, double.Parse(v.ToString()), Math.Abs((double)v) * 1e-8);
            }

            string p99 = "0.999";
            for (int i = 0; i < 100; i++) {
                p99 += "9";

                MultiPrecision<Pow2.N8> u = p99;
                Console.WriteLine(u.ToString());
                Console.WriteLine(u.ToHexcode());

                Assert.AreEqual(double.Parse(p99), (double)u, 1e-100);
            }
        }

        [TestMethod]
        public void ToStringFormatTest() {
            MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.PI;

            Console.WriteLine(v.ToString());
            Console.WriteLine($"{v:E10}");
            Console.WriteLine(v.ToString("e10"));
            Console.WriteLine($"{v: E10}");
            Console.WriteLine(v.ToString(" e10"));
            Console.WriteLine(v.ToString("e10 "));
            Console.WriteLine(v.ToString("e" + MultiPrecision<Pow2.N8>.DecimalDigits));

            Assert.ThrowsException<FormatException>(() => {
                Console.WriteLine($"{v:E-1}");
            });

            Assert.ThrowsException<FormatException>(() => {
                Console.WriteLine($"{v:E0}");
            });

            Assert.ThrowsException<FormatException>(() => {
                Console.WriteLine($"{v:F10}");
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                Console.WriteLine(v.ToString("e" + MultiPrecision<Pow2.N8>.DecimalDigits + 1));
            });
        }
    }
}
