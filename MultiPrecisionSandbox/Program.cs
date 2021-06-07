using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new StreamWriter("ellipticK.csv")) {
                sw.WriteLine("k,y");

                for (decimal k = 0; k <= 1; k += 1 / 64m) {
                    MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.EllipticK(k);

                    sw.WriteLine($"{k},{y}");
                }
            }

            using (StreamWriter sw = new StreamWriter("ellipticE_n48.txt")) {
                for (decimal k = 0; k <= 1; k += 1 / 65536m) {
                    MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticE(k);
                    MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticE(k);

                    sw.WriteLine(k);
                    sw.WriteLine(y4.ToHexcode());
                    sw.WriteLine(y8.ToHexcode());
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }

        private static void BesselConvergenceSummary<N>(string filepath) where N : struct, IConstant {
            using (StreamWriter sw = new StreamWriter(filepath)) {
                sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");
                sw.WriteLine("nu,z,terms");

                for (decimal nu = 0; nu <= 4; nu += 1 / 32m) {
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
