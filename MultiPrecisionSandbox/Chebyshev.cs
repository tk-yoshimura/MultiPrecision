using System;
using System.Collections.Generic;

using MultiPrecision;

namespace MultiPrecisionSandbox {
    public static class Chebyshev<N> where N : struct, IConstant {
        private readonly static Dictionary<(int m, int n), MultiPrecision<N>> table;

        static Chebyshev() {
            table = new Dictionary<(int n, int m), MultiPrecision<N>> {
                { (1, 1), 1 },
                { (2, 2), 1 }
            };
        }

        public static MultiPrecision<N> Table(int n, int m) {
            if(n < 1 || m < 1 || n < m) { 
                throw new ArgumentOutOfRangeException();
            }

            if((checked(m + n) & 1) == 1) { 
                return 0;
            }
            if(m == 1) {
                return (((n / 2) & 1) == 0) ? 1 : -1;
            }
            if(n == m) { 
                return (n >= 2) ? MultiPrecision<N>.Ldexp(1, n - 2) : 1;
            }

            if(table.ContainsKey((n, m))) { 
                return table[(n, m)];
            }

            MultiPrecision<N> v = MultiPrecision<N>.Ldexp(Table(n - 1, m - 1), 1) - Table(n - 2, m);
            table.Add((n, m), v);

            return v;
        }
    }
}
