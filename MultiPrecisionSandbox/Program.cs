using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            GammaApproxSummary<Pow2.N4>("gamma_n4.txt");
            GammaApproxSummary<Pow2.N8>("gamma_n8.txt");
            GammaApproxSummary<Pow2.N16>("gamma_n16.txt");
            GammaApproxSummary<Pow2.N32>("gamma_n32.txt");
            GammaApproxSummary<Pow2.N64>("gamma_n64.txt");
            GammaApproxSummary<Pow2.N128>("gamma_n128.txt");
            GammaApproxSummary<Pow2.N256>("gamma_n256.txt");
        }

        private static void GammaApproxSummary<N>(string filepath) where N : struct, IConstant {
            Console.WriteLine(MultiPrecision<N>.Length);

            using (StreamWriter sw = new StreamWriter(filepath)) {

                int matchbits = MultiPrecision<N>.Bits;
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
                            if (MultiPrecision<N>.RoundMantissa(y_expected, MultiPrecision<N>.Bits - keepbits) == MultiPrecision<N>.RoundMantissa(y_actual, MultiPrecision<N>.Bits - keepbits)) {
                                sw.WriteLine($"  matchbits : {keepbits}");

                                if (keepbits < matchbits) {
                                    matchbits = keepbits;
                                }
                                break;
                            }
                        }
                    }

                    Console.Write(".");
                }

                sw.WriteLine($"min matchbits : {matchbits}");
            }

            Console.WriteLine(string.Empty);
        }
    }
}


//namespace MultiPrecisionSandbox {
//    class Program {
//        static void Main(string[] args) {
//            {
//                int z = SterlingConvergence<Pow2.N4>("gamma_conv_sterling_n4_plus1.txt");
//                for (int z2 = z * 2; z2 < z * 2 + 32; z2 += 2) {
//                    if (SterlingSummary<Pow2.N4>(z2, $"gamma_conv_sterling_n4_z{z2 / 2}_plus1.txt") == 0) {
//                        break;
//                    }
//                }
//            }

//            {
//                int z = SterlingConvergence<Pow2.N8>("gamma_conv_sterling_n8_plus1.txt");
//                for (int z2 = z * 2; z2 < z * 2 + 32; z2 += 2) {
//                    if (SterlingSummary<Pow2.N8>(z2, $"gamma_conv_sterling_n8_z{z2 / 2}_plus1.txt") == 0) {
//                        break;
//                    }
//                }
//            }

//            {
//                int z = SterlingConvergence<Pow2.N16>("gamma_conv_sterling_n16_plus1.txt");
//                for (int z2 = z * 2; z2 < z * 2 + 32; z2 += 2) {
//                    if (SterlingSummary<Pow2.N16>(z2, $"gamma_conv_sterling_n16_z{z2 / 2}_plus1.txt") == 0) {
//                        break;
//                    }
//                }
//            }

//            {
//                int z = SterlingConvergence<Pow2.N32>("gamma_conv_sterling_n32_plus1.txt");
//                for (int z2 = z * 2; z2 < z * 2 + 32; z2 += 2) {
//                    if (SterlingSummary<Pow2.N32>(z2, $"gamma_conv_sterling_n32_z{z2 / 2}_plus1.txt") == 0) {
//                        break;
//                    }
//                }
//            }

//            {
//                int z = SterlingConvergence<Pow2.N64>("gamma_conv_sterling_n64_plus1.txt");
//                for (int z2 = z * 2; z2 < z * 2 + 32; z2 += 2) {
//                    if (SterlingSummary<Pow2.N64>(z2, $"gamma_conv_sterling_n64_z{z2 / 2}_plus1.txt") == 0) {
//                        break;
//                    }
//                }
//            }

//            {
//                int z = SterlingConvergence<Pow2.N128>("gamma_conv_sterling_n128_plus1.txt");
//                for (int z2 = z * 2; z2 < z * 2 + 32; z2 += 2) {
//                    if (SterlingSummary<Pow2.N128>(z2, $"gamma_conv_sterling_n128_z{z2 / 2}_plus1.txt") == 0) {
//                        break;
//                    }
//                }
//            }

//            {
//                int z = SterlingConvergence<Pow2.N256>("gamma_conv_sterling_n256_plus1.txt");
//                for (int z2 = z * 2; z2 < z * 2 + 32; z2 += 2) {
//                    if (SterlingSummary<Pow2.N256>(z2, $"gamma_conv_sterling_n256_z{z2 / 2}_plus1.txt") == 0) {
//                        break;
//                    }
//                }
//            }
//        }

//        public static int SterlingConvergence<N>(string filepath) where N : struct, IConstant {
//            Console.WriteLine($"bits {MultiPrecision<N>.Bits}");

//            int z;

//            using (StreamWriter sw = new StreamWriter(filepath)) {
//                sw.WriteLine("z,terms");

//                for (z = 16; z <= 65536; z = (z * 5 / 4) / 4 * 4) {
//                    int terms = SterlingApprox<N>.SterlingTermConvergence(z);

//                    sw.WriteLine($"{z},{((terms < int.MaxValue) ? terms : "n/a")}");
//                    Console.WriteLine($"{z},{((terms < int.MaxValue) ? terms : "n/a")}");

//                    if (terms < int.MaxValue) {
//                        break;
//                    }
//                }

//                while (true) {
//                    z -= 4;

//                    int terms = SterlingApprox<N>.SterlingTermConvergence(z);

//                    sw.WriteLine($"{z},{((terms < int.MaxValue) ? terms : "n/a")}");
//                    Console.WriteLine($"{z},{((terms < int.MaxValue) ? terms : "n/a")}");

//                    if (terms == int.MaxValue) {
//                        break;
//                    }
//                }
//            }

//            return z + 4;
//        }

//        private static int SterlingSummary<N>(int z2_check, string filepath) where N : struct, IConstant {
//            for (int z2 = 1; z2 < z2_check; z2++) {
//                GammaExpects<N>.Gamma(z2);
//            }

//            int max_matchbits = 0, sat_cnt = 0, maxacc_terms = 1;

//            int terms_init = SterlingApprox<N>.SterlingTermConvergence(MultiPrecision<N>.Ldexp(z2_check, -1).Convert<Plus1<N>>());

//            if (terms_init == int.MaxValue) {
//                return -1;
//            }

//            terms_init = terms_init / 2 * 2;

//            using (StreamWriter sw = new StreamWriter(filepath)) {
//                for (int terms = terms_init; terms <= 65536; terms += 2) {

//                    int matchbits = MultiPrecision<N>.Bits;
//                    for (int z2 = z2_check; z2 <= z2_check + 20; z2++) {
//                        MultiPrecision<N> z = MultiPrecision<N>.Ldexp(z2, -1);

//                        MultiPrecision<N> y_expected = GammaExpects<N>.Gamma(z2);
//                        MultiPrecision<N> y_actual = SterlingApprox<N>.Gamma(z, terms);

//                        if (y_expected != y_actual) {
//                            for (int keepbits = MultiPrecision<N>.Bits; keepbits > 0; keepbits--) {
//                                if (MultiPrecision<N>.RoundMantissa(y_expected, MultiPrecision<N>.Bits - keepbits) == MultiPrecision<N>.RoundMantissa(y_actual, MultiPrecision<N>.Bits - keepbits)) {
//                                    if (keepbits < matchbits) {
//                                        matchbits = keepbits;
//                                    }
//                                    break;
//                                }
//                                else if (keepbits == 1) {
//                                    matchbits = 0;
//                                }
//                            }
//                        }
//                    }

//                    sw.WriteLine($"terms: {terms}, min_matchbits : {matchbits}");

//                    Console.WriteLine($"{terms},{matchbits}");

//                    if (matchbits > max_matchbits) {
//                        max_matchbits = matchbits;
//                        maxacc_terms = terms;
//                        sat_cnt = 0;
//                    }
//                    else{
//                        sat_cnt++;

//                        if (sat_cnt >= 4) { 
//                            break;
//                        }
//                    }
//                }

//                sw.WriteLine($"maxacc_terms: {maxacc_terms}");

//                for (int z2 = z2_check; z2 <= z2_check + 200; z2++) {
//                    MultiPrecision<N> z = MultiPrecision<N>.Ldexp(z2, -1);

//                    MultiPrecision<N> y_expected = GammaExpects<N>.Gamma(z2);
//                    MultiPrecision<N> y_actual = SterlingApprox<N>.Gamma(z, maxacc_terms);

//                    MultiPrecision<N> err = MultiPrecision<N>.Abs(y_expected - y_actual);

//                    sw.WriteLine($"  gamma({z2 * 0.5:F1})");
//                    sw.WriteLine($"    true   : {y_expected}");
//                    sw.WriteLine($"    approx : {y_actual}");
//                    sw.WriteLine($"    err    : {err}");

//                    if (y_expected != y_actual) {
//                        for (int keepbits = MultiPrecision<N>.Bits; keepbits > 0; keepbits--) {
//                            if (MultiPrecision<N>.RoundMantissa(y_expected, MultiPrecision<N>.Bits - keepbits) == MultiPrecision<N>.RoundMantissa(y_actual, MultiPrecision<N>.Bits - keepbits)) {
//                                sw.WriteLine($"    matchbits : {keepbits}");
//                                break;
//                            }
//                        }
//                    }
//                }
//            }

//            return 0;
//        }
//    }
//}
