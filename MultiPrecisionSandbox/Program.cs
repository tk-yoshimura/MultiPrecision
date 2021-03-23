using MultiPrecision;
using System;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            for (int z = 2; z <= 10; z++) { 
                MultiPrecision<Pow2.N4> erfc = ErfcContinuedFraction<Pow2.N4>.Erfc(z, 1);
            }

            for (int n = 0; n <= 100; n++) {
                MultiPrecision<Pow2.N4> erfc = ErfcContinuedFraction<Pow2.N4>.Erfc(2, n);

                Console.WriteLine($"{n} : {erfc}");
            }

            Console.WriteLine(string.Empty);

            for (int n = 0; n <= 100; n++) {
                MultiPrecision<Pow2.N4> erfc = ErfcContinuedFraction<Pow2.N4>.Erfc(3, n);

                Console.WriteLine($"{n} : {erfc}");
            }

            Console.WriteLine(string.Empty);

            for (int n = 0; n <= 100; n++) {
                MultiPrecision<Pow2.N4> erfc = ErfcContinuedFraction<Pow2.N4>.Erfc(4, n);

                Console.WriteLine($"{n} : {erfc}");
            }

            Console.WriteLine(string.Empty);

            for (int n = 0; n <= 100; n++) {
                MultiPrecision<Pow2.N4> erfc = ErfcContinuedFraction<Pow2.N4>.Erfc(8, n);

                Console.WriteLine($"{n} : {erfc}");
            }

            Console.WriteLine(string.Empty);

            Console.WriteLine("END");
            Console.Read();
        }
    }
}