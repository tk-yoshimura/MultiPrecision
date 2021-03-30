using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N8> x1 = MultiPrecision<Pow2.N8>.Point5;
            MultiPrecision<Pow2.N8> x2 = MultiPrecision<Pow2.N8>.BitDecrement(x1);
            MultiPrecision<Pow2.N8> x3 = MultiPrecision<Pow2.N8>.BitDecrement(x2);
            MultiPrecision<Pow2.N8> x4 = MultiPrecision<Pow2.N8>.BitDecrement(x3);
            MultiPrecision<Pow2.N8> x5 = MultiPrecision<Pow2.N8>.BitDecrement(x4);

            long n1 = (long)MultiPrecision<Pow2.N8>.Round(x1);
            long n2 = (long)MultiPrecision<Pow2.N8>.Round(x2);
            long n3 = (long)MultiPrecision<Pow2.N8>.Round(x3);
            long n4 = (long)MultiPrecision<Pow2.N8>.Round(x4);
            long n5 = (long)MultiPrecision<Pow2.N8>.Round(x5);

            MultiPrecision<Pow2.N8> y1 = MultiPrecision<Pow2.N8>.CosHalfPI(x1);
            MultiPrecision<Pow2.N8> y2 = MultiPrecision<Pow2.N8>.CosHalfPI(x2);
            MultiPrecision<Pow2.N8> y3 = MultiPrecision<Pow2.N8>.CosHalfPI(x3);
            MultiPrecision<Pow2.N8> y4 = MultiPrecision<Pow2.N8>.CosHalfPI(x4);
            MultiPrecision<Pow2.N8> y5 = MultiPrecision<Pow2.N8>.CosHalfPI(x5);

            Console.WriteLine(y1.ToHexcode());
            Console.WriteLine(y2.ToHexcode());
            Console.WriteLine(y3.ToHexcode());
            Console.WriteLine(y4.ToHexcode());
            Console.WriteLine(y5.ToHexcode());

            Console.Read();
        }
    }
}
