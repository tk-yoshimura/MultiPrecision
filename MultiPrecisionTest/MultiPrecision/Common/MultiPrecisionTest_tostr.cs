using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void ToStringTest() {

            foreach(MultiPrecision<Pow2.N8> x in TestTool.AllRangeSet<Pow2.N8>().Concat(TestTool.LimitSet<Pow2.N8>())) {
                
                foreach (MultiPrecision<Pow2.N8> v in TestTool.EnumerateNeighbor(x, x.IsZero ? 0 : 2)) {

                    (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore(MultiPrecision<Pow2.N8>.DecimalDigits);

                    Console.WriteLine(sign);
                    Console.WriteLine(exponent_dec);
                    Console.WriteLine(mantissa_dec);
                    Console.WriteLine(v.ToString());
                    Console.WriteLine($"{v:E10}");
                    Console.WriteLine(v.ToString("e10"));

                    MultiPrecision<Pow2.N8> u = MultiPrecision<Pow2.N8>.FromStringCore(sign, exponent_dec, mantissa_dec, MultiPrecision<Pow2.N8>.DecimalDigits);
                    Console.WriteLine(u.ToString());

                    Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(v, u, 20));
                    Assert.AreEqual((double)v, double.Parse(v.ToString()), Math.Abs((double)v) * 1e-8);
                }
            }

            foreach(MultiPrecision<Pow2.N16> x in TestTool.AllRangeSet<Pow2.N16>().Concat(TestTool.LimitSet<Pow2.N16>())) { 
                foreach (MultiPrecision<Pow2.N16> v in TestTool.EnumerateNeighbor(x, x.IsZero ? 0 : 2)) {

                    (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N16> mantissa_dec) = v.ToStringCore(MultiPrecision<Pow2.N16>.DecimalDigits);

                    Console.WriteLine(sign);
                    Console.WriteLine(exponent_dec);
                    Console.WriteLine(mantissa_dec);
                    Console.WriteLine(v.ToString());
                    Console.WriteLine($"{v:E10}");
                    Console.WriteLine(v.ToString("e10"));

                    MultiPrecision<Pow2.N16> u = MultiPrecision<Pow2.N16>.FromStringCore(sign, exponent_dec, mantissa_dec, MultiPrecision<Pow2.N16>.DecimalDigits);
                    Console.WriteLine(u.ToString());

                    Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(v, u, 20));
                    Assert.AreEqual((double)v, double.Parse(v.ToString()), Math.Abs((double)v) * 1e-8);
                }
            }
        }
    }
}
