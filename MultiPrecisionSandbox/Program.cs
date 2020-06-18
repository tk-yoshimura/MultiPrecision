using MultiPrecision;

using System;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.Exp(LogGammaApprox<Pow2.N8>.SterlingApprox(2, 5).hi);

            LogGammaApprox<Pow2.N8>.ConvergenceTimes(32);

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
