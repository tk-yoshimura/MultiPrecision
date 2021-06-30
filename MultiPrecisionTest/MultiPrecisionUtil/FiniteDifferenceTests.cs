using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionUtilTests {
    [TestClass()]
    public class FiniteDifferenceTests {
        [TestMethod()]
        public void LogDiffTest() {
            (MultiPrecision<Pow2.N4> actual, MultiPrecision<Pow2.N4> error) = MultiPrecisionUtil.FiniteDifference<Pow2.N4>(MultiPrecision<Pow2.N4>.Log, 1);

            MultiPrecision<Pow2.N4> expected = 1;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 16));
        }

        [TestMethod()]
        public void ExpDiffTest() {
            (MultiPrecision<Pow2.N4> actual, MultiPrecision<Pow2.N4> error) = MultiPrecisionUtil.FiniteDifference<Pow2.N4>(MultiPrecision<Pow2.N4>.Exp, 1);

            MultiPrecision<Pow2.N4> expected = MultiPrecision<Pow2.N4>.E;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 16));
        }

        [TestMethod()]
        public void SinDiffTest() {
            (MultiPrecision<Pow2.N4> actual, MultiPrecision<Pow2.N4> error) = MultiPrecisionUtil.FiniteDifference<Pow2.N4>(MultiPrecision<Pow2.N4>.SinPI, 1);

            MultiPrecision<Pow2.N4> expected = -MultiPrecision<Pow2.N4>.PI;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 16));
        }

        [TestMethod()]
        public void PolyDiffTest() {
            (MultiPrecision<Pow2.N4> actual, MultiPrecision<Pow2.N4> error) = MultiPrecisionUtil.FiniteDifference<Pow2.N4>(MultiPrecision<Pow2.N4>.Square, 1);

            MultiPrecision<Pow2.N4> expected = 2;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 16));
        }

        [TestMethod()]
        public void ZeroDiffTest() {
            (MultiPrecision<Pow2.N4> actual, MultiPrecision<Pow2.N4> error) = MultiPrecisionUtil.FiniteDifference<Pow2.N4>(MultiPrecision<Pow2.N4>.Cube, 0);

            MultiPrecision<Pow2.N4> expected = 0;

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, actual, 16));
        }

        [TestMethod()]
        public void UndefineDiffTest() {
            static MultiPrecision<Pow2.N4> f(MultiPrecision<Pow2.N4> x) {
                return x.IsZero ? 0 : MultiPrecision<Pow2.N4>.Sin(1 / x);
            };

            (MultiPrecision<Pow2.N4> actual, MultiPrecision<Pow2.N4> error) = MultiPrecisionUtil.FiniteDifference<Pow2.N4>(f, 0);

            Console.WriteLine(actual);
            Console.WriteLine(error);
        }
    }
}
