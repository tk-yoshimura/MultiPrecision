﻿using MultiPrecision;
using System;
using System.IO;
using System.Numerics;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            const int m = 28;
            (MultiPrecision<Pow2.N16>[][] dss, MultiPrecision<Pow2.N16>[] es) = BesselYoshidaCoef<Pow2.N16>.Table(m);

            //for (int m = 24; m <= 36; m++) {
            //    (MultiPrecision<Pow2.N16>[] cs, MultiPrecision<Pow2.N16>[] ds) = BesselYoshidaCoef<Pow2.N16>.Table(0, m);
            //    for (MultiPrecision<Pow2.N16> z = 1; z <= 32; z += 0.5) {
            //
            //        MultiPrecision<Pow2.N16> t = 1 / z, tn = 1;
            //        MultiPrecision<Pow2.N16> c = 0, d = 0;
            //
            //        for (int j = 0; j <= m; j++) {
            //            c += cs[j] * tn;
            //            d += ds[j] * tn;
            //            tn *= t;
            //        }
            //
            //        MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Sqrt(t * MultiPrecision<Pow2.N16>.PI / 2) * c / d;
            //        y *= MultiPrecision<Pow2.N16>.Exp(-z);
            //
            //        Console.WriteLine($"{m},{z},{y}");
            //    }
            //
            //    Console.Write("\n");
            //}

            (MultiPrecision<Pow2.N16>[] cs, MultiPrecision<Pow2.N16>[] ds) = BesselYoshidaCoef<Pow2.N16>.Table(0.25, dss, es);

            for (MultiPrecision<Pow2.N16> z = 1; z <= 32; z += 0.5) {
                MultiPrecision<Pow2.N16> t = 1 / z, tn = 1;
                MultiPrecision<Pow2.N16> c = 0, d = 0;

                for (int j = 0; j <= m; j++) {
                    c += cs[j] * tn;
                    d += ds[j] * tn;
                    tn *= t;
                }

                MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Sqrt(t * MultiPrecision<Pow2.N16>.PI / 2) * c / d;
                y *= MultiPrecision<Pow2.N16>.Exp(-z);

                Console.WriteLine($"{z},{y}");
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}