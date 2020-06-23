using MultiPrecision;

using System;

namespace MultiPrecisionSandbox {
    using MP = MultiPrecision<Pow2.N8>;

    class Program {

        static void Main(string[] args) {
            MP x = 2;

            for(int i = 0; i < 4098; i++) { 
                MP y = MP.Erfc(x);

                Console.WriteLine(y);
                Console.WriteLine(y.ToHexcode());

                x = MP.BitDecrement(x);
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
