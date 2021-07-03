using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionSpline;
using System;

namespace MultiPrecisionSplineTests {
    [TestClass()]
    public class QuinticHermiteSplineTests {
        [TestMethod()]
        public void QuinticHermiteTest() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6, -3, -0, 4, 5, 9, 11
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4, 5, 2, -4, -2, 0, 1
            };

            QuinticHermiteSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.01m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);
                MultiPrecision<Pow2.N4> gg = spline.SecondGrad(x);

                Console.WriteLine($"{x},{y},{g},{gg}");
            }
        }

        [TestMethod()]
        public void QuinticHermiteN1Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1
            };

            QuinticHermiteSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);
                MultiPrecision<Pow2.N4> gg = spline.SecondGrad(x);

                Console.WriteLine($"{x},{y},{g},{gg}");
            }
        }

        [TestMethod()]
        public void QuinticHermiteN2Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4
            };

            QuinticHermiteSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);
                MultiPrecision<Pow2.N4> gg = spline.SecondGrad(x);

                Console.WriteLine($"{x},{y},{g},{gg}");
            }
        }
    }
}