using MultiPrecision;
using System;
using System.IO;
using System.Numerics;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            for (int n = 0; n <= 8; n++) {
                for (int k = 0; k <= n; k++) {
                    BigInteger v = ShiftedLegendre.Table(n, k);

                    Console.Write($"{v} ");
                }
                Console.Write("\n");
            }

            Console.WriteLine("END");
            Console.Read();
        }

        private static void GammaApproxSummary<N>(string filepath) where N : struct, IConstant {
            Console.WriteLine(MultiPrecision<N>.Length);

            using (StreamWriter sw = new StreamWriter(filepath)) {
                for (int terms = 4; terms < 32; terms++) {
                    sw.WriteLine($"terms : {terms}");

                    for (int z2 = 1; z2 <= 20; z2++) {
                        MultiPrecision<N> z = MultiPrecision<N>.Ldexp(z2, -1);

                        MultiPrecision<N> y_expected = GammaExpects<N>.Gamma(z2);
                        MultiPrecision<N> y_actual = SterlingApprox<N>.Gamma(z, terms);

                        MultiPrecision<N> err = MultiPrecision<N>.Abs(y_expected - y_actual);

                        sw.WriteLine($"  gamma({z2 * 0.5:F1})");
                        sw.WriteLine($"    true   : {y_expected}");
                        sw.WriteLine($"    approx : {y_actual}");
                        sw.WriteLine($"    err    : {err}");

                        if (y_expected != y_actual) {
                            for (int roundbits = 0; roundbits < MultiPrecision<N>.Bits; roundbits++) {
                                if (MultiPrecision<N>.RoundMantissa(y_expected, roundbits) == MultiPrecision<N>.RoundMantissa(y_actual, roundbits)) {
                                    sw.WriteLine($"    matchbits : {MultiPrecision<N>.Bits - roundbits}");
                                    break;
                                }
                            }
                        }

                        Console.Write(".");
                    }
                }
            }

            Console.WriteLine(string.Empty);
        }
    }
}