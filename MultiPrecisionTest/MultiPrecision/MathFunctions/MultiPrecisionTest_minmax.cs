using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void MinMaxTest() {
            MultiPrecision<Pow2.N8>[] vs = {
                MultiPrecision<Pow2.N8>.NegativeInfinity,
                MultiPrecision<Pow2.N8>.MinValue,
                "-1e100",
                "-1e-100",
                -MultiPrecision<Pow2.N8>.Epsilon,
                MultiPrecision<Pow2.N8>.MinusZero,
                MultiPrecision<Pow2.N8>.Zero,
                MultiPrecision<Pow2.N8>.Epsilon,
                "1e-100",
                "1e100",
                MultiPrecision<Pow2.N8>.MaxValue,
                MultiPrecision<Pow2.N8>.PositiveInfinity
            };

            foreach (var u in vs) {
                foreach (var v in vs) {
                    var max = MultiPrecision<Pow2.N8>.Max(u, v);
                    var min = MultiPrecision<Pow2.N8>.Min(u, v);

                    Console.WriteLine(u);
                    Console.WriteLine(v);

                    Console.WriteLine($"max : {max}");
                    Console.WriteLine($"min : {min}");

                    Assert.IsTrue(max >= min);

                    Console.Write("\n");
                }
            }
        }
    }
}
