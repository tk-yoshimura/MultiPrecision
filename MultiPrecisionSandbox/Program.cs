using MultiPrecision;
using System;
using System.IO;
using System.Numerics;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            const int m = 5;
            //(MultiPrecision<Pow2.N16>[] cs, MultiPrecision<Pow2.N16>[] ds) = BesselYoshidaCoef<Pow2.N16>.Table(0, m);
            MultiPrecision<Pow2.N16>[][] dss = BesselYoshidaCoef<Pow2.N16>.Table(m);
            (MultiPrecision<Pow2.N16>[] cs, MultiPrecision<Pow2.N16>[] ds) = BesselYoshidaCoef<Pow2.N16>.Table(0.25, dss);

            for (int i = 0; i <= m; i++) {
                Console.WriteLine($"c{i} | {cs[i]:e8}");
            }

            for (int i = 0; i <= m; i++) {
                Console.WriteLine($"d{i} | {ds[i]:e8}");
            }

            for (MultiPrecision<Pow2.N16> z = 1; z <= 32; z += 0.5) {
                MultiPrecision<Pow2.N16> y = BesselYoshidaCoef<Pow2.N16>.Value(z, cs, ds);

                Console.WriteLine($"{z},{y}");
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}