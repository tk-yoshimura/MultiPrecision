using MultiPrecision;

using System;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N64> x = 1;
            MultiPrecision<Pow2.N64> y = 1;

            for(int i = 0; i < 375000; i++) {
                x += 1;
                y *= x;

                if(i % 1024 == 0) {
                    Console.WriteLine(".");
                }
            }

            while (true) { 
                MultiPrecision<Pow2.N64> v = LogGammaApprox<Pow2.N64>.SterlingApprox(x, 64).hi;
                
                MultiPrecision<Pow2.N64> ly = MultiPrecision<Pow2.N64>.Log(y);
                MultiPrecision<Pow2.N64> e = v - ly;
                
                Console.WriteLine($"{v:E5}, {ly:E5}, {e:E5}");
                
                if(MultiPrecision<Pow2.N64>.NearlyEqualBits(v, ly, 8)) { 
                    Console.WriteLine($"{x}");

                    break;
                }

                x += 1;
                y *= x;
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
