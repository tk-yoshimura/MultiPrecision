using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static ReadOnlyCollection<MultiPrecision<N>> bernoulli_sequence = null;

        public static ReadOnlyCollection<MultiPrecision<N>> BernoulliSequence {
            get {
                if (bernoulli_sequence is null) {
                    bernoulli_sequence = GenerateBernoulliSequence();
                }

                return bernoulli_sequence;
            }
        }

        private static ReadOnlyCollection<MultiPrecision<N>> GenerateBernoulliSequence() {
            List<MultiPrecision<N>> table = new List<MultiPrecision<N>>() { 
                One
            };

            MultiPrecision<N>[] binom = new MultiPrecision<N>[]{ One, One };
            MultiPrecision<N> p5 = Ldexp(One, -1);

            for(int n = 2; n < 2048; n += 2) {
                MultiPrecision<N>[] binom_plus1 = new MultiPrecision<N>[binom.Length + 1];
                MultiPrecision<N>[] binom_plus2 = new MultiPrecision<N>[binom.Length + 2];

                binom_plus1[1] = binom_plus1[^2] = binom_plus1.Length - 1;
                binom_plus2[1] = binom_plus2[^2] = binom_plus2.Length - 1;
                binom_plus1[0] = binom_plus1[^1] = binom_plus2[0] = binom_plus2[^1] = One;

                for(int i = 2; i <= binom_plus1.Length / 2; i++) { 
                    binom_plus1[i] = binom_plus1[^(i + 1)] = binom[i - 1] + binom[i];
                } 
                for(int i = 2; i <= binom_plus2.Length / 2; i++) { 
                    binom_plus2[i] = binom_plus2[^(i + 1)] = binom_plus1[i - 1] + binom_plus1[i];
                }

                binom = binom_plus2;

                MultiPrecision<N> t = One;
                for(int i = 2; i < binom.Length - 2; i += 2) {
                    t += binom[i] * table[i / 2];
                }
                t /= -(n + 1);
                t += p5;

                table.Add(t);
            }

            return table.AsReadOnly();
        }
    }
}
