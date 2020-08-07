using MultiPrecision;

using System;
using System.IO;
using System.Numerics;

namespace MultiPrecisionSandbox {

    class Program {

        static void Main(string[] args) {

            for (int n = 2; n <= 1024; n = (n < 32) ? n + 1 : n * 2) {

                Fraction[,] c = CoefMatrix(n);
                Fraction[,] v = Inverse(c);

                using (StreamWriter sw = new StreamWriter($"cn_{n}.txt")) {
                    for (int i = 0; i < n; i++) {
                        sw.WriteLine(v[0, i]);
                    }
                }

                Console.WriteLine(n);
            }

            Console.WriteLine("END");
            Console.Read();
        }

        public static Fraction[,] CoefMatrix(int n) {

            BigInteger[] m = new BigInteger[n + 1];
            m[0] = 1;

            for (int i = 1; i <= n; i++) {
                for (int j = i - 2; j >= 0; j--) {
                    m[j + 1] = m[j] + m[j + 1] * i;
                }
                m[0] *= i;
                m[i] = 1;
            }

            Fraction[,] c = new Fraction[n, n];

            for (int i = 0; i < n; i++) {
                BigInteger r = m[n];
                
                c[i, n - 1] = 1;

                for (int j = n - 2; j >= 0; j--) {
                    r = m[j + 1] - r * (i + 1);

                    c[i, j] = r;
                }
            }

            return c;
        }

         public static Fraction[,] Inverse(Fraction[,] m) {
            if (m.GetLength(0) != m.GetLength(1)) {
                throw new ArgumentException();
            }

            int i, j, k, n = m.GetLength(0);
            Fraction inv_mii, mul;

            Fraction[,] v = new Fraction[n, n];
            for (i = 0; i < n; i++) { 
                for (j = 0; j < n; j++) {
                    v[i, j] = (i == j) ? 1 : 0;
                }
            }

            for(i = 0; i < n; i++) {
                inv_mii = 1 / m[i, i];
                m[i, i] = 1;
                for(j = i + 1; j < n; j++) {
                    m[i, j] *= inv_mii;
                }
                for(j = 0; j < n; j++) {
                    v[i, j] *= inv_mii;
                }

                for(j = i + 1; j < n; j++) {
                    mul = m[j, i];
                    m[j, i] = 0;
                    for(k = i + 1; k < n; k++) {
                        m[j, k] -= m[i, k] * mul;
                    }
                    for(k = 0; k < n; k++) {
                        v[j, k] -= v[i, k] * mul;
                    }
                }
            }

            for(i = n - 1; i >= 0; i--) {
                for(j = i - 1; j >= 0; j--) {
                    mul = m[j, i];
                    for(k = i; k < n; k++) {
                        m[j, k] = 0;
                    }
                    for(k = 0; k < n; k++) {
                        v[j, k] -= v[i, k] * mul;
                    }
                }
            }

            return v;
        }
    }
}
