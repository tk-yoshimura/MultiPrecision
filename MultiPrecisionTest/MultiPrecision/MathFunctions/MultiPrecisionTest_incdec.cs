using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void BitIncrementTest() {
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

            foreach (var v in vs) {
                var vinc = MultiPrecision<Pow2.N8>.BitIncrement(v);

                Console.WriteLine(v);
                Console.WriteLine(vinc);

                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(vinc.ToHexcode());

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void BitDecrementTest() {
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

            foreach (var v in vs) {
                var vdec = MultiPrecision<Pow2.N8>.BitDecrement(v);

                Console.WriteLine(v);
                Console.WriteLine(vdec);

                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(vdec.ToHexcode());

                Console.Write("\n");
            }
        }
    }
}
