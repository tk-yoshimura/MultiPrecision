using MultiPrecision;

using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace MultiPrecisionSandbox {

    class Program {

        static void Main(string[] args) {
            Console.WriteLine($"Convergence N4");
            for(int i = 4; i <= 20; i++) { 
                Console.WriteLine($"{(i * 0.5)},{ConvergenceError<Pow2.N4>(i * 0.5)}");
            }

            Console.WriteLine($"Convergence N8");
            for(int i = 4; i <= 20; i++) { 
                Console.WriteLine($"{(i * 0.5)},{ConvergenceError<Pow2.N8>(i * 0.5)}");
            }

            Console.WriteLine($"Convergence N16");
            for(int i = 4; i <= 20; i++) { 
                Console.WriteLine($"{(i * 0.5)},{ConvergenceError<Pow2.N16>(i * 0.5)}");
            }

            Console.WriteLine($"Convergence N32");
            for(int i = 4; i <= 20; i++) { 
                Console.WriteLine($"{(i * 0.5)},{ConvergenceError<Pow2.N32>(i * 0.5)}");
            }

            Console.WriteLine($"Convergence N64");
            for(int i = 4; i <= 20; i++) { 
                Console.WriteLine($"{(i * 0.5)},{ConvergenceError<Pow2.N64>(i * 0.5)}");
            }

            Console.WriteLine("END");
            Console.Read();
        }

        static int ConvergenceError<N>(MultiPrecision<Plus1<N>> x) where N : struct, IConstant { 
            int convergence_i = 1;

            for(int step = 4096; step >= 1; step /= 2) { 
                MultiPrecision<N> prev_y = MultiPrecision<N>.NaN;

                int i = convergence_i;

                while(true) {
                    MultiPrecision<Plus1<N>> z = x * MultiPrecision<Plus1<N>>.Sqrt2;
                    MultiPrecision<Plus1<N>> a = 0;

                    for (long n = i; n > 0; n--) {
                        a = n / (z + a);
                    }

                    MultiPrecision<N> y = 
                        MultiPrecisionUtil.Convert<N, Plus1<N>>(
                            MultiPrecision<Plus1<N>>.Exp(-x * x) / (z + a) * MultiPrecision<Plus1<N>>.Sqrt(2 / MultiPrecision<Plus1<N>>.PI)
                        );

                    if(!MultiPrecision<N>.NearlyEqualBits(prev_y, y, 2)) { 
                        prev_y = y;
                        convergence_i = i;
                    }
                    else {
                        convergence_i -= step;
                        break;
                    }

                    i += step;
                }
            }

            return convergence_i;
        }
    }
}
