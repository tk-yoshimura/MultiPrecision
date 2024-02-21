using MultiPrecision;
using System;

namespace MultiPrecisionSandbox {
    class Program {
        internal struct N12 : IConstant {
            public int Value => 12;
        }

        static void Main(string[] args) {
            for (double exponent = 0; exponent > -12; exponent -= 1d / 32) {
                MultiPrecision<N12> x = MultiPrecision<N12>.Pow2(exponent);
                MultiPrecision<N12> x_inc = MultiPrecision<N12>.BitIncrement(x);
                MultiPrecision<N12> x_dec = MultiPrecision<N12>.BitDecrement(x);

                MultiPrecision<N12> y = MultiPrecision<N12>.Log2(x);
                MultiPrecision<N12> y_inc = MultiPrecision<N12>.Log2(x_inc);
                MultiPrecision<N12> y_dec = MultiPrecision<N12>.Log2(x_dec);

                Console.WriteLine(y);
                Console.WriteLine(y_inc);
                Console.WriteLine(y_dec);
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}