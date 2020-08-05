﻿using MultiPrecision;

using System;
using System.IO;
using System.Linq;

namespace MultiPrecisionSandbox {
    using MP = MultiPrecision<Pow2.N8>;

    class Program {

        static void Main(string[] args) {
            var seq = MP.StirlingSequence;

            using(StreamWriter sw = new StreamWriter("stirling.txt")) { 
                foreach(var s in seq) { 
                    sw.WriteLine(s);
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
