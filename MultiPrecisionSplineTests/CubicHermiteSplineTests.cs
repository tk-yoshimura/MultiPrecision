using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionSpline;
using System;

namespace MultiPrecisionSplineTests {
    [TestClass()]
    public class CubicHermiteSplineTests {
        [TestMethod()]
        public void CubicHermiteTest() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6, -3, -0, 4, 5, 9, 11
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4, 5, 2, -4, -2, 0, 1
            };

            CubicHermiteSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }

        [TestMethod()]
        public void CubicHermiteN1Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1
            };

            CubicHermiteSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }

        [TestMethod()]
        public void CubicHermiteN2Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4
            };

            CubicHermiteSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }
    }
}