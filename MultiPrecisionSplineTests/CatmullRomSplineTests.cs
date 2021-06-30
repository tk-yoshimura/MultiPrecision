using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionSpline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPrecisionSplineTests {
    [TestClass()]
    public class CatmullRomSplineTests {
        [TestMethod()]
        public void CatmullRomSplineTest() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6, -3, -0, 4, 5, 9, 11
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4, 5, 2, -4, -2, 0, 1
            };

            CatmullRomSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);

                Console.WriteLine($"{x},{y}");
            }
        }

        [TestMethod()]
        public void CatmullRomSplineN1Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1
            };

            CatmullRomSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);

                Console.WriteLine($"{x},{y}");
            }
        }

        [TestMethod()]
        public void CatmullRomSplineN2Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4
            };

            CatmullRomSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);

                Console.WriteLine($"{x},{y}");
            }
        }
    }
}