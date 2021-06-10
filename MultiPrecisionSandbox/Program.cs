using MultiPrecision;
using System;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            //MultiPrecisionUtil.RichardsonExtrapolation<Pow2.N4>.R(5);

            //MultiPrecisionUtil.RichardsonExtrapolation<Pow2.N4> extrapolation = new();
            //
            //extrapolation.Append(1 / 2m);
            //extrapolation.Append(17 / 64m);
            //extrapolation.Append(197 / 1024m);

            (var value, var error) = MultiPrecisionUtil.RombergIntegrate<Pow2.N4>(MultiPrecision<Pow2.N4>.Log, 1, 2, min_iterations:8, epsilon:1e-4);

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
