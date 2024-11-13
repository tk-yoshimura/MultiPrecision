using System.Collections.Concurrent;
using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> ChebyshevCoef(int n, int m) {
            return Chebyshev.Table(n, m);
        }
    }

    static class Chebyshev {
        private static readonly ConcurrentDictionary<(int m, int n), BigInteger> table = [];

        static Chebyshev() {
            table[(1, 1)] = 1;
            table[(2, 2)] = 1;
        }

        public static BigInteger Table(int n, int m) {
            if (n < 1 || m < 1 || n < m) {
                throw new ArgumentOutOfRangeException($"{nameof(n)},{nameof(m)}", $"{nameof(n)}>={nameof(m)}>=1");
            }

            if ((checked(m + n) & 1) == 1) {
                return 0;
            }
            if (m == 1) {
                return (((n / 2) & 1) == 0) ? 1 : -1;
            }
            if (n == m) {
                return (n >= 2) ? (BigInteger.One << (n - 2)) : 1;
            }

            if (!table.TryGetValue((n, m), out BigInteger v)) {
                v = Table(n - 1, m - 1) * 2 - Table(n - 2, m);
                table[(n, m)] = v;
            }

            return v;
        }
    }
}