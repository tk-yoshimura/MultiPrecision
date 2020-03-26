using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CmpTest() {
            MultiPrecision<Pow2.N8>[] vs = {
                MultiPrecision<Pow2.N8>.Zero,
                MultiPrecision<Pow2.N8>.MinusZero,
                1, 2, 3, 4, 5, 7, 10, 11, 13, 100, 1000,
                -1, -2, -3, -4, -5, -7, -10, -11, -13, -100, -1000,
                MultiPrecision<Pow2.N8>.One / 2,
                MultiPrecision<Pow2.N8>.One / 3,
                MultiPrecision<Pow2.N8>.One / 4,
                MultiPrecision<Pow2.N8>.One / 5,
                MultiPrecision<Pow2.N8>.One / 7,
                MultiPrecision<Pow2.N8>.One / 10,
                MultiPrecision<Pow2.N8>.One / 11,
                MultiPrecision<Pow2.N8>.One / 13,
                MultiPrecision<Pow2.N8>.One / 100,
                MultiPrecision<Pow2.N8>.One / 1000,
                MultiPrecision<Pow2.N8>.MinusOne / 2,
                MultiPrecision<Pow2.N8>.MinusOne / 3,
                MultiPrecision<Pow2.N8>.MinusOne / 4,
                MultiPrecision<Pow2.N8>.MinusOne / 5,
                MultiPrecision<Pow2.N8>.MinusOne / 7,
                MultiPrecision<Pow2.N8>.MinusOne / 10,
                MultiPrecision<Pow2.N8>.MinusOne / 11,
                MultiPrecision<Pow2.N8>.MinusOne / 13,
                MultiPrecision<Pow2.N8>.MinusOne / 100,
                MultiPrecision<Pow2.N8>.MinusOne / 1000,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
                MultiPrecision<Pow2.N8>.NaN,
            };


            foreach (MultiPrecision<Pow2.N8> v in vs) {
                foreach (MultiPrecision<Pow2.N8> u in vs) {

                    double dv = (double)v, du = (double)u;

                    try {
                        Assert.AreEqual(v.Equals(u), dv.Equals(du), "equal");
                        Assert.AreEqual(v == u, dv == du, "==");
                        Assert.AreEqual(v != u, dv != du, "!=");
                        Assert.AreEqual(v < u, dv < du, "<");
                        Assert.AreEqual(v > u, dv > du, ">");
                        Assert.AreEqual(v <= u, dv <= du, "<=");
                        Assert.AreEqual(v >= u, dv >= du, ">=");
                    }
                    catch (AssertFailedException e) {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(v);
                        Console.WriteLine(u);

                        Console.WriteLine($"equal {v.Equals(u)}");
                        Console.WriteLine($"== {v == u}");
                        Console.WriteLine($"!= {v != u}");
                        Console.WriteLine($"< {v < u}");
                        Console.WriteLine($"> {v > u}");
                        Console.WriteLine($"<= {v <= u}");
                        Console.WriteLine($">= {v >= u}");

                        throw;
                    }
                }
            }
        }
    }
}