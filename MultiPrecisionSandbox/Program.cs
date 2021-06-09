using MultiPrecision;
using System;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N4> n = 0.75, k = 0;

            MultiPrecision<Pow2.N4> a = 1;
            MultiPrecision<Pow2.N4> b = MultiPrecision<Pow2.N4>.Sqrt(1 - k * k);
            MultiPrecision<Pow2.N4> p = MultiPrecision<Pow2.N4>.Sqrt(1 - n);
            MultiPrecision<Pow2.N4> q = 1;
            MultiPrecision<Pow2.N4> sum_q = 1;

            for (int i = 1; i <= 10; i++) {
                MultiPrecision<Pow2.N4> a_next = (a + b) / 2;
                MultiPrecision<Pow2.N4> b_next = MultiPrecision<Pow2.N4>.Sqrt(a * b);
                MultiPrecision<Pow2.N4> p_next = (p * p + a * b) / (2 * p);
                MultiPrecision<Pow2.N4> q_next = q / 2 * (p * p - a * b) / (p * p + a * b);

                a = a_next;
                b = b_next;
                p = p_next;
                q = q_next;

                sum_q += q;

                Console.WriteLine(q);
            }

            MultiPrecision<Pow2.N4> y = (2 + sum_q * n / (1 - n)) * MultiPrecision<Pow2.N4>.EllipticK(k) / 2;

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
