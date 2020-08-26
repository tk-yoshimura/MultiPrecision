using MultiPrecision;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Numerics;

namespace MultiPrecisionSandbox {
    using MP = MultiPrecision<Pow2.N16>;

    class Program {

        static void Main(string[] args) {
            const int n = 16, gmax = 32;

            MP p5 = MP.Ldexp(1, -1);

            MP[] ls = new MP[n + 1];

            ls[0] = -2 * MP.Sqrt(MP.PI);
            for(int i = 1; i <= n; i++) { 
                ls[i] = ls[i - 1] * MP.Ldexp(2 * i - 3, -1);
            }

            MP[] es = new MP[n + gmax * 4 + 2];

            es[0] = MP.Sqrt(MP.E);
            es[1] = MP.E;
            for(int i = 2; i < es.Length - 1; i += 2) { 
                es[i] = es[i - 2] * MP.E;
                es[i + 1] = es[i - 1] * MP.E;
            }

            for(int g2 = 1; g2 <= gmax * 2; g2++) { 
                MP g = MP.Ldexp(g2, -1);
                MP[] ps = new MP[n + 1];

                for(int i = 0; i <= n; i++) { 
                    MP c = $"{Chebyshev.Table(2 * n + 1, 2 * i + 1)}";
                    
                    MP f = i + g + p5;
                    MP fp = 1 / (MP.Pow(f, i) * MP.Sqrt(f));

                    MP l = ls[i];
                    MP e = es[i * 2 + g2];

                    ps[i] = c * fp * l * e;
                }

                for(int i = 1; i <= n; i++) {
                    ps[i] += ps[i - 1];
                }

                Console.WriteLine(ps);
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
