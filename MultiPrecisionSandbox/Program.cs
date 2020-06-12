using MultiPrecision;

using System;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            for(int n = 1; n <= 10; n++) { 
                for(int m = 1; m <= n; m++) {
                    Console.WriteLine($"{n},{m},{Chebyshev<Pow2.N8>.Table(n, m)}");
                }

                Console.Write("\n");
            }

            MultiPrecision<Pow2.N8>[] ps = GammaCoef<Pow2.N8>.Generate(0.75, 8);

            MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.Pow(2.5, 1.5);

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
