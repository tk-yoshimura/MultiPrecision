using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new StreamWriter("../../erf_approx_nearzero.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z64 = 0; z64 <= 1 * 64; z64++) {
                    double z = z64 / 64.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erf(z);
                    double y_approx = ErrorFunction.ErfNearZero(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../erfc_approx_range12.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z64 = 1 * 64; z64 <= 2 * 64; z64++) {
                    double z = z64 / 64.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erfc(z);
                    double y_approx = ErrorFunction.ErfcRange12(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../erfc_approx_range23.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z64 = 2 * 64; z64 <= 3 * 64; z64++) {
                    double z = z64 / 64.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erfc(z);
                    double y_approx = ErrorFunction.ErfcRange23(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../erfc_approx_range34.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z64 = 3 * 64; z64 <= 4 * 64; z64++) {
                    double z = z64 / 64.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erfc(z);
                    double y_approx = ErrorFunction.ErfcRange34(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../erfc_approx_larger4.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z64 = 4 * 64; z64 <= 32 * 64; z64++) {
                    double z = z64 / 64.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erfc(z);
                    double y_approx = ErrorFunction.ErfcLarger4(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }
        }
    }
}
