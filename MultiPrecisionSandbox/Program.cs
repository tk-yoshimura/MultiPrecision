using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionSandbox {
    class Program {

        static void Main() {
            MultiPrecision<Pow2.N8> x = "1.9106332362490185563277142050315155084868293900200109819194";

            Console.WriteLine($"{ToFP128(x)},");

            Console.WriteLine("END");
            Console.Read();
        }

        public static string ToFP128(MultiPrecision<Pow2.N8> x) {
            Sign sign = x.Sign;
            long exponent = x.Exponent;
            uint[] mantissa = x.Mantissa.Reverse().ToArray();

            string code = $"({(sign == Sign.Plus ? "+1" : "-1")}, {exponent}, 0x{mantissa[0]:X8}{mantissa[1]:X8}uL, 0x{mantissa[2]:X8}{mantissa[3]:X8}uL)";

            return code;
        }
    }
}