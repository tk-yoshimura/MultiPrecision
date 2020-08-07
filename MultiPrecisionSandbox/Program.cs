using MultiPrecision;

using System;
using System.IO;
using System.Numerics;

namespace MultiPrecisionSandbox {

    class Program {

        static void Main(string[] args) {

            for (int n = 1; n <= 1024; n++) {

                (Fraction[,] c, Fraction[] u) = Coef(n);
                Fraction[,] w = Inverse(c);

                Fraction[] r = new Fraction[n];

                for (int i = 0; i < n; i++) {
                    Fraction x = 0;

                    for (int j = 0; j < n; j++) {
                        x += w[j, i] * u[j];
                    }

                    r[i] = x;
                }

                using (StreamWriter sw = new StreamWriter($"lanczos_{n}.txt")) {
                    for (int i = 0; i < n; i++) {
                        sw.WriteLine(r[i]);
                    }
                }

                Console.WriteLine(n);
            }

            Console.WriteLine("END");
            Console.Read();
        }

        public static (Fraction[,] c, Fraction[] u) Coef(int n) {

            BigInteger[] m = new BigInteger[n + 1];
            BigInteger[] v = new BigInteger[n + 1];
            m[0] = 1;

            v[0] = 0;

            for (int i = 1; i <= n; i++) {
                for (int j = i - 2; j >= 0; j--) {
                    m[j + 1] = m[j] + m[j + 1] * i;

                    v[j + 2] = v[j + 1] - v[j + 2] * (i - 1);
                }
                m[0] *= i;
                m[i] = 1;

                v[1] *= -(i - 1);
                v[i] = 1;
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

            Fraction[] u = new Fraction[n];

            for (int i = 0; i < n; i++) {
                u[i] = v[i] - m[i];
            }

            return (c, u);
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
