using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new StreamWriter("consts.txt")) {
                sw.WriteLine("ln2=");
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.Ln2.ToHexcode()}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.Ln2.ToHexcode()}");
                sw.Flush();
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.Ln2}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.Ln2}");
                sw.Flush();

                sw.WriteLine("lb10=");
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.Lb10.ToHexcode()}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.Lb10.ToHexcode()}");
                sw.Flush();
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.Lb10}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.Lb10}");
                sw.Flush();

                sw.WriteLine("lg2=");
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.Lg2.ToHexcode()}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.Lg2.ToHexcode()}");
                sw.Flush();
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.Lg2}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.Lg2}");
                sw.Flush();

                sw.WriteLine("lbe=");
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.LbE.ToHexcode()}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.LbE.ToHexcode()}");
                sw.Flush();
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.LbE}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.LbE}");
                sw.Flush();

                sw.WriteLine("pi=");
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.PI.ToHexcode()}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.PI.ToHexcode()}");
                sw.Flush();
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.PI}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.PI}");
                sw.Flush();

                sw.WriteLine("rcp_pi=");
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.RcpPI.ToHexcode()}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.RcpPI.ToHexcode()}");
                sw.Flush();
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.RcpPI}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.RcpPI}");
                sw.Flush();

                sw.WriteLine("sqrt2=");
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.Sqrt2.ToHexcode()}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.Sqrt2.ToHexcode()}");
                sw.Flush();
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.Sqrt2}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.Sqrt2}");
                sw.Flush();

                sw.WriteLine("e=");
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.E.ToHexcode()}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.E.ToHexcode()}");
                sw.Flush();
                sw.WriteLine($"{MultiPrecision<Pow2.N1024>.E}");
                sw.WriteLine($"{MultiPrecision<Double<Pow2.N1024>>.E}");
                sw.Flush();
            }
        }
    }
}
