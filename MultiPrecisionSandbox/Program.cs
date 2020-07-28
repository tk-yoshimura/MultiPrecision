using MultiPrecision;

using System;
using System.IO;
using System.Linq;

namespace MultiPrecisionSandbox {
    using MP = MultiPrecision<Plus1<Plus1<Pow2.N1024>>>;

    class Program {

        static void Main(string[] args) {
            const int sets = 16;

            MP x = MP.Log(2);

            using (StreamWriter sw = new StreamWriter($"log2_hex.txt")) {
                sw.WriteLine(x.ToHexcode());

                var xs = x.Mantissa.Reverse().ToArray();

                for (int i = 0, k = 0; i < xs.Length; i += sets) {
                    for (int j = 0; j < sets && k < xs.Length; j++, k++) {
                        sw.Write($"0x{xs[k]:X8}u, ");
                    }

                    sw.Write("\n");
                }

                sw.Flush();

                sw.WriteLine(x);
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
