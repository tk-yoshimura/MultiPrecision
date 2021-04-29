using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            BesselConvergenceSummary<Pow2.N4>("bessel_limit_convergence_n4.txt");
            BesselConvergenceSummary<Pow2.N8>("bessel_limit_convergence_n8.txt");
            BesselConvergenceSummary<Pow2.N16>("bessel_limit_convergence_n16.txt");
            BesselConvergenceSummary<Pow2.N32>("bessel_limit_convergence_n32.txt");
            BesselConvergenceSummary<Pow2.N64>("bessel_limit_convergence_n64.txt");
            BesselConvergenceSummary<Pow2.N128>("bessel_limit_convergence_n128.txt");
            BesselConvergenceSummary<Pow2.N256>("bessel_limit_convergence_n256.txt");

            Console.WriteLine("END");
            Console.Read();
        }

        private static void BesselConvergenceSummary<N>(string filepath) where N : struct, IConstant {
            using (StreamWriter sw = new StreamWriter(filepath)) {
                sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");
                sw.WriteLine("nu,z,terms");

                for (decimal nu = 0; nu <= 2; nu += 1 / 32m) {
                    Console.WriteLine(nu);

                    int z, terms;
                    for (z = 1; z <= 65536; z *= 2) {
                        terms = BesselLimitApprox<N>.BesselTermConvergence(nu, z);

                        Console.WriteLine($"{z},{terms}");

                        if (terms < int.MaxValue) {
                            break;
                        }
                    }
                    while (z >= 4) {
                        z = z * 7 / 8;

                        terms = BesselLimitApprox<N>.BesselTermConvergence(nu, z);

                        Console.WriteLine($"{z},{terms}");

                        if (terms >= int.MaxValue) {
                            break;
                        }
                    }
                    z = z / 4 * 4;

                    while (true) {
                        z += 4;

                        terms = BesselLimitApprox<N>.BesselTermConvergence(nu, z);

                        Console.WriteLine($"{z},{terms}");

                        if (terms < int.MaxValue) {
                            break;
                        }
                    }

                    sw.WriteLine($"{nu},{z},{terms}");
                    Console.WriteLine($"{nu},{z},{terms}");
                }
            }
        }
    }
}
