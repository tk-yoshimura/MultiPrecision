using MultiPrecision;

using System;

namespace MultiPrecisionSandbox {
    using MP = MultiPrecision<Pow2.N8>;

    class Program {

        static void Main(string[] args) {
            MP omega = 1;

            for(int i = 0; i < 256; i++) { 
                omega = MP.Exp(-omega);

                Console.WriteLine(omega);
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
