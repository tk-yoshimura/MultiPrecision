using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            for (int z = 1; z <= 65536; z *= 2) {
                int terms = Bessel0LimitApprox<Pow2.N8>.BesselTermConvergence(z);
                
                Console.WriteLine($"{z},{terms}");
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
