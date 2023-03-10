using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        public static MultiPrecision<N> BernoulliSequence(int n) {
            return Bernoulli.Table(n).ToMultiPrecision<N>();
        }
    }

    static class Bernoulli {
        private static readonly List<Fraction> table = new() { 1 };
        private static readonly Fraction p5 = new(1, 2);

        private static BigInteger[] binom = new BigInteger[] { 1, 1 };

        public static Fraction Table(int n) {
            if (n < 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            if (n < table.Count) {
                return table[n];
            }

            for (int j = table.Count; j <= n; j++) {
                BigInteger[] binom_plus1 = new BigInteger[binom.Length + 1];
                BigInteger[] binom_plus2 = new BigInteger[binom.Length + 2];

                binom_plus1[1] = binom_plus1[^2] = binom_plus1.Length - 1;
                binom_plus2[1] = binom_plus2[^2] = binom_plus2.Length - 1;
                binom_plus1[0] = binom_plus1[^1] = binom_plus2[0] = binom_plus2[^1] = 1;

                for (int i = 2; i <= binom_plus1.Length / 2; i++) {
                    binom_plus1[i] = binom_plus1[^(i + 1)] = binom[i - 1] + binom[i];
                }
                for (int i = 2; i <= binom_plus2.Length / 2; i++) {
                    binom_plus2[i] = binom_plus2[^(i + 1)] = binom_plus1[i - 1] + binom_plus1[i];
                }

                binom = binom_plus2;

                Fraction t = 1;
                for (int i = 2; i < binom.Length - 2; i += 2) {
                    t += binom[i] * table[i / 2];
                }
                t /= checked(-(j * 2 + 1));
                t += p5;

                table.Add(t);
            }

            return table[n];
        }
    }
}
