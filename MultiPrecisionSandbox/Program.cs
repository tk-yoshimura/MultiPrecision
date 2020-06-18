using MultiPrecision;

using System;

namespace MultiPrecisionSandbox {
    using MP = MultiPrecision<Pow2.N64>;

    class Program {

        static void Main(string[] args) {
            MP x = MP.Gamma(1.5);

            Console.WriteLine(x);
            Console.WriteLine(x.ToHexcode());

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
