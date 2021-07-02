using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionSpline;
using System;

namespace MultiPrecisionSplineTests {
    [TestClass()]
    public class AkimaSplineTests {
        [TestMethod()]
        public void AkimaSplineTest() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6, -3, -0, 4, 5, 9, 11
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4, 5, 2, -4, -2, 0, 1
            };

            AkimaSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }

        [TestMethod()]
        public void AkimaSplineTest2() {
            MultiPrecision<Pow2.N4>[] xs = {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
            };

            MultiPrecision<Pow2.N4>[] ys = {
                12, 15, 15, 10, 10, 10, 10.5, 15, 50, 60, 85
            };

            AkimaSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }

        [TestMethod()]
        public void AkimaSplineTest3() {
            MultiPrecision<Pow2.N4>[] xs = {
                0, 3, 6, 10, 14
            };

            MultiPrecision<Pow2.N4>[] ys = {
                12, 15, 18, 26, 34
            };

            AkimaSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }

        [TestMethod()]
        public void AkimaSplineTest4() {
            MultiPrecision<Pow2.N4>[] xs = {
                3, 6, 10, 14
            };

            MultiPrecision<Pow2.N4>[] ys = {
                15, 18, 26, 34
            };

            AkimaSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }

        [TestMethod()]
        public void AkimaSplineN1Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1
            };

            AkimaSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }

        [TestMethod()]
        public void AkimaSplineN2Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4
            };

            AkimaSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }

        [TestMethod()]
        public void AkimaSplineN3Test() {
            MultiPrecision<Pow2.N4>[] xs = {
                -8, -6, -3
            };

            MultiPrecision<Pow2.N4>[] ys = {
                1, 4, 5
            };

            AkimaSpline<Pow2.N4> spline = new(xs, ys);

            for (decimal x = -10; x <= 14; x += 0.1m) {
                MultiPrecision<Pow2.N4> y = spline.Value(x);
                MultiPrecision<Pow2.N4> g = spline.Grad(x);

                Console.WriteLine($"{x},{y},{g}");
            }
        }
    }
}