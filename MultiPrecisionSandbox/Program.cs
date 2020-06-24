using MultiPrecision;

using System;

namespace MultiPrecisionSandbox {
    using MP = MultiPrecision<Pow2.N8>;

    class Program {

        static void Main(string[] args) {
            MP x = "1e10";

            MP a = MP.Exp(x), b = MP.Exp(-x), c = a + b;

            MP y = MP.Ldexp(c, -1);

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
