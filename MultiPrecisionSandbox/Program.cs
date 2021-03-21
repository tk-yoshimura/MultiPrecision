using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            GammaApproxSummary<Pow2.N4>("gamma_2_n4.txt");
            GammaApproxSummary<Pow2.N8>("gamma_2_n8.txt");
            GammaApproxSummary<Pow2.N16>("gamma_2_n16.txt");
            GammaApproxSummary<Pow2.N32>("gamma_2_n32.txt");
            GammaApproxSummary<Pow2.N64>("gamma_2_n64.txt");
            GammaApproxSummary<Pow2.N128>("gamma_2_n128.txt");
        }

        private static void GammaApproxSummary<N>(string filepath) where N : struct, IConstant {
            Console.WriteLine(MultiPrecision<N>.Length);

            using (StreamWriter sw = new StreamWriter(filepath)) {
                for (int z2 = 1; z2 <= 2000; z2++) {
                    MultiPrecision<N> z = MultiPrecision<N>.Ldexp(z2, -1);

                    MultiPrecision<N> y_expected = GammaExpects<N>.Gamma(z2);
                    MultiPrecision<N> y_actual = MultiPrecision<N>.Gamma(z);

                    MultiPrecision<N> err = MultiPrecision<N>.Abs(y_expected - y_actual);

                    sw.WriteLine($"gamma({z2 * 0.5:F1})");
                    sw.WriteLine($"  true   : {y_expected}");
                    sw.WriteLine($"  approx : {y_actual}");
                    sw.WriteLine($"  err    : {err}");

                    if (y_expected != y_actual) {
                        for (int keepbits = MultiPrecision<N>.Bits; keepbits > 0; keepbits--) {
                            if (MultiPrecision<N>.RoundMantissa(y_expected, keepbits) == MultiPrecision<N>.RoundMantissa(y_actual, keepbits)) {
                                sw.WriteLine($"  matchbits : {keepbits}");
                                break;
                            }
                        }
                    }

                    Console.Write(".");
                }
            }

            Console.WriteLine(string.Empty);
        }
    }
}