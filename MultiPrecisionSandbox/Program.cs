using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            GammaNearOnes<Pow2.N4>("gamma_n4_nearones.csv");
            GammaNearOnes<Pow2.N8>("gamma_n8_nearones.csv");
            GammaNearOnes<Pow2.N16>("gamma_n16_nearones.csv");
            GammaNearOnes<Pow2.N32>("gamma_n32_nearones.csv");
            GammaNearOnes<Pow2.N64>("gamma_n64_nearones.csv");
            GammaNearOnes<Pow2.N128>("gamma_n128_nearones.csv");
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

        private static void GammaNearOnes<N>(string filepath) where N : struct, IConstant {
            Console.WriteLine(MultiPrecision<N>.Length);

            using (StreamWriter sw = new StreamWriter(filepath)) {
                sw.WriteLine("z,gamma(z)");
                
                for (int i = 500; i <= 2000; i++) {
                    MultiPrecision<N> z = ((MultiPrecision<N>)i) / 1000;

                    MultiPrecision<N> y = MultiPrecision<N>.Gamma(z);

                    sw.WriteLine($"{z},{y}");

                    Console.Write(".");
                }
            }

            Console.WriteLine(string.Empty);
        }
    }
}
