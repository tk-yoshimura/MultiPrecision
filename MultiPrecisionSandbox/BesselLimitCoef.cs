using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionSandbox {
    static class Bessel0LimitCoef {
        private readonly static List<Fraction> a_table = new() { 1 };
        private readonly static List<BigInteger> p_table = new() { 1 };
        private readonly static List<BigInteger> f_table = new() { 1 };

        public static Fraction Table(int n) {
            if (n < 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            if (n < a_table.Count) {
                return a_table[n];
            }

            for (int k = a_table.Count; k <= n; k++) {
                BigInteger p = p_table.Last() * checked(-(2 * k - 1) * (2 * k - 1));
                BigInteger f = f_table.Last() * checked(k * 8);

                p_table.Add(p);
                f_table.Add(f);
                a_table.Add(new Fraction(p, f));
            }

            return a_table[n];
        }
    }

    static class Bessel1LimitCoef {
        private readonly static List<Fraction> a_table = new() { 1 };
        private readonly static List<BigInteger> p_table = new() { 1 };
        private readonly static List<BigInteger> f_table = new() { 1 };

        public static Fraction Table(int n) {
            if (n < 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            if (n < a_table.Count) {
                return a_table[n];
            }

            for (int k = a_table.Count; k <= n; k++) {
                BigInteger p = p_table.Last() * checked(4 - (2 * k - 1) * (2 * k - 1));
                BigInteger f = f_table.Last() * checked(k * 8);

                p_table.Add(p);
                f_table.Add(f);
                a_table.Add(new Fraction(p, f));
            }

            return a_table[n];
        }
    }
}
