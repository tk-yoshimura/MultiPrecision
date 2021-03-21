using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            SterlingSummary<Pow2.N4>(300, "gamma_sterling_n4.txt");
            SterlingSummary<Pow2.N8>(200, "gamma_sterling_n8.txt");
            SterlingSummary<Pow2.N16>(256, "gamma_sterling_n16.txt");
            SterlingSummary<Pow2.N32>(500, "gamma_sterling_n32.txt");
            SterlingSummary<Pow2.N64>(944, "gamma_sterling_n64.txt");
            SterlingSummary<Pow2.N128>(1872, "gamma_sterling_n128.txt");
        }

        private static void SterlingSummary<N>(int z2_check, string filepath) where N : struct, IConstant {
            for (int z2 = 1; z2 < z2_check; z2++) {
                GammaExpects<N>.Gamma(z2);
            }

            int max_matchbits = 0, sat_cnt = 0, maxacc_terms = 1;

            using (StreamWriter sw = new StreamWriter(filepath)) {
                for (int terms = 2; terms <= 65536; terms += 2) {

                    int matchbits = MultiPrecision<N>.Bits;
                    for (int z2 = z2_check; z2 <= z2_check + 20; z2++) {
                        MultiPrecision<N> z = MultiPrecision<N>.Ldexp(z2, -1);

                        MultiPrecision<N> y_expected = GammaExpects<N>.Gamma(z2);
                        MultiPrecision<N> y_actual = SterlingApprox<N>.Gamma(z, terms);

                        if (y_expected != y_actual) {
                            for (int keepbits = MultiPrecision<N>.Bits; keepbits > 0; keepbits--) {
                                if (MultiPrecision<N>.RoundMantissa(y_expected, keepbits) == MultiPrecision<N>.RoundMantissa(y_actual, keepbits)) {
                                    if (keepbits < matchbits) {
                                        matchbits = keepbits;
                                    }
                                    break;
                                }
                                else if (keepbits == 1) {
                                    matchbits = 0;
                                }
                            }
                        }
                    }

                    sw.WriteLine($"terms: {terms}, min_matchbits : {matchbits}");

                    Console.WriteLine($"{terms},{matchbits}");

                    if (matchbits > max_matchbits) {
                        max_matchbits = matchbits;
                        maxacc_terms = terms;
                        sat_cnt = 0;
                    }
                    else{
                        sat_cnt++;

                        if (sat_cnt >= 4) { 
                            break;
                        }
                    }
                }

                sw.WriteLine($"maxacc_terms: {maxacc_terms}");

                for (int z2 = z2_check; z2 <= z2_check + 200; z2++) {
                    MultiPrecision<N> z = MultiPrecision<N>.Ldexp(z2, -1);

                    MultiPrecision<N> y_expected = GammaExpects<N>.Gamma(z2);
                    MultiPrecision<N> y_actual = SterlingApprox<N>.Gamma(z, maxacc_terms);

                    MultiPrecision<N> err = MultiPrecision<N>.Abs(y_expected - y_actual);

                    sw.WriteLine($"  gamma({z2 * 0.5:F1})");
                    sw.WriteLine($"    true   : {y_expected}");
                    sw.WriteLine($"    approx : {y_actual}");
                    sw.WriteLine($"    err    : {err}");

                    if (y_expected != y_actual) {
                        for (int keepbits = MultiPrecision<N>.Bits; keepbits > 0; keepbits--) {
                            if (MultiPrecision<N>.RoundMantissa(y_expected, keepbits) == MultiPrecision<N>.RoundMantissa(y_actual, keepbits)) {
                                sw.WriteLine($"    matchbits : {keepbits}");
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
