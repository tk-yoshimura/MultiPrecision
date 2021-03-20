using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static ReadOnlyCollection<MultiPrecision<N>> stirling_sequence = null;

        public static ReadOnlyCollection<MultiPrecision<N>> StirlingSequence {
            get {
                if (stirling_sequence is null) {
                    stirling_sequence = GenerateStirlingSequence();
                }

                return stirling_sequence;
            }
        }

        private static ReadOnlyCollection<MultiPrecision<N>> GenerateStirlingSequence() {
            List<Fraction> table = new List<Fraction>() { new Fraction(1, 12) };
            BigInteger[] stirling = new BigInteger[] { 1 };

            for (int n = 2; n <= 256; n++) {
                BigInteger[] stirling_plus1 = new BigInteger[stirling.Length + 1];

                stirling_plus1[0] = stirling[0] * (n - 1);
                stirling_plus1[^1] = 1;

                for (int i = 1; i < stirling.Length; i++) {
                    stirling_plus1[i] = stirling[i - 1] + stirling[i] * (n - 1);
                }

                stirling = stirling_plus1;

                Fraction t = 0;
                for (int i = 0; i < stirling.Length; i++) {
                    t += new Fraction((i + 1) * stirling[i], checked((i + 2) * (i + 3)));
                }
                t /= 2 * n;

                table.Add(t);
            }

            return table.Select((v) => v.ToMultiPrecision<N>()).ToList().AsReadOnly();
        }
    }
}
