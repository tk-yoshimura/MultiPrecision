using System.Collections.Generic;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static IReadOnlyList<MultiPrecision<N>> taylor_sequence = null;

        public static IReadOnlyList<MultiPrecision<N>> TaylorSequence {
            get {
                if (taylor_sequence is null) {
                    taylor_sequence = GenerateTaylorSequence();
                }

                return taylor_sequence;
            }
        }

        private static IReadOnlyList<MultiPrecision<N>> GenerateTaylorSequence() {
            List<MultiPrecision<N>> table = new() {
                One, One,
            };

            MultiPrecision<Plus1<N>> v = 2, d = 2, t = 1;

            while (table.Count < 1024 || t.Exponent >= -Bits * 2) {
                t = 1 / v;

                if (t.IsZero) {
                    break;
                }

                table.Add(t.Convert<N>());
                d += 1;
                v *= d;
            }

            return table;
        }
    }
}
