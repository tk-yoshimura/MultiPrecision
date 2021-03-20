using System;
using System.Collections.Generic;
using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> StirlingSequence(int n) {
            return Stirling.Table(n).ToMultiPrecision<N>();
        }
    }

    static class Stirling {
        private readonly static List<Fraction> table = new() { new Fraction(1, 12) };
        static BigInteger[] stirling = new BigInteger[] { 1 };

        public static Fraction Table(int n) {
            if (n <= 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }
            
            if (n <= table.Count) {
                return table[n - 1];
            }

            for (int j = table.Count + 1; j <= n; j++) {
                BigInteger[] stirling_plus1 = new BigInteger[stirling.Length + 1];

                stirling_plus1[0] = stirling[0] * (j - 1);
                stirling_plus1[^1] = 1;

                for (int i = 1; i < stirling.Length; i++) {
                    stirling_plus1[i] = stirling[i - 1] + stirling[i] * (j - 1);
                }

                stirling = stirling_plus1;

                Fraction t = 0;
                for (long i = 0; i < stirling.Length; i++) {
                    t += new Fraction((i + 1) * stirling[i], checked((i + 2) * (i + 3)));
                }
                t /= 2 * j;

                table.Add(t);
            }

            return table[n - 1];
        }
    }
}
