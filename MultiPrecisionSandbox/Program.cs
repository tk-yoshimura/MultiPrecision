using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new StreamWriter("../../erf_approx.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z1024 = -32 * 1024; z1024 <= 32 * 1024; z1024++) {
                    double z = z1024 / 1024.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erf(z);
                    double y_approx = ErrorFunction.Erf(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../erfc_approx.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z1024 = -32 * 1024; z1024 <= 32 * 1024; z1024++) {
                    double z = z1024 / 1024.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erfc(z);
                    double y_approx = ErrorFunction.Erfc(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../inverf_approx.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z1024 = -1 * 1024; z1024 <= 1 * 1024; z1024++) {
                    double z = z1024 / 1024.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.InverseErf(z);
                    double y_approx = ErrorFunction.InverseErf(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../inverfc_approx.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z1024 = 0 * 1024; z1024 <= 2 * 1024; z1024++) {
                    double z = z1024 / 1024.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.InverseErfc(z);
                    double y_approx = ErrorFunction.InverseErfc(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }
        }
    }
}
