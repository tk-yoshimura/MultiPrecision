using MultiPrecision;

using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace MultiPrecisionSandbox {

    class Program {

        static void Main(string[] args) {
            Console.WriteLine($"Convergence N4 : {ConvergenceError<Pow2.N4>()}");
            Console.WriteLine($"Convergence N8 : {ConvergenceError<Pow2.N8>()}");
            Console.WriteLine($"Convergence N16 : {ConvergenceError<Pow2.N16>()}");
            Console.WriteLine($"Convergence N32 : {ConvergenceError<Pow2.N32>()}");
            Console.WriteLine($"Convergence N64 : {ConvergenceError<Pow2.N64>()}");

            Console.WriteLine("END");
            Console.Read();
        }

        static int ConvergenceError<N>() where N : struct, IConstant { 
            MultiPrecision<Plus1<N>> x = MultiPrecision<Plus1<N>>.BitDecrement(2);
            MultiPrecision<N> erfc2 = MultiPrecisionUtil.Convert<N, Plus1<N>>(1 - MultiPrecision<Plus1<N>>.Erf(x));

            int min_i = 1;

            for(int step = 4096; step >= 1; step /= 2) { 
                MultiPrecision<N> min_err = MultiPrecision<N>.PositiveInfinity;

                int i = min_i;

                while(true) {
                    MultiPrecision<Plus1<N>> z = x * MultiPrecision<Plus1<N>>.Sqrt2;
                    MultiPrecision<Plus1<N>> a = 0;

                    for (long n = i; n > 0; n--) {
                        a = n / (z + a);
                    }

                    MultiPrecision<Plus1<N>> y = MultiPrecision<Plus1<N>>.Exp(-x * x) / (z + a) * MultiPrecision<Plus1<N>>.Sqrt(2 / MultiPrecision<Plus1<N>>.PI);

                    MultiPrecision<N> err = erfc2 - MultiPrecisionUtil.Convert<N, Plus1<N>>(y);

                    //Console.WriteLine($"{i},{err:E12}");

                    if(min_err > err) { 
                        min_err = err;
                        min_i = i;
                    }
                    else {
                        min_i -= step;
                        break;
                    }

                    i += step;
                }

                //Console.WriteLine($"{min_i}");
            }

            return min_i;
        }
    }
}
