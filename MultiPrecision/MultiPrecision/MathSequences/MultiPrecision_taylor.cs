using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static ReadOnlyCollection<MultiPrecision<N>> taylor_sequence = null;

        public static ReadOnlyCollection<MultiPrecision<N>> TaylorSequence {
            get {
                if (taylor_sequence is null) {
                    taylor_sequence = GenerateTaylorSequence();
                }

                return taylor_sequence;
            }
        }

        private static ReadOnlyCollection<MultiPrecision<N>> GenerateTaylorSequence() {
            List<MultiPrecision<N>> table = new();

            MultiPrecision<Plus1<N>> v = 1, d = 1, t = 1;

            while (table.Count < 1024 || t.Exponent >= -Bits * 2) {
                t = 1 / v;

                if (t.IsZero) {
                    break;
                }

                table.Add(t.Convert<N>());
                d += 1;
                v *= d;
            }

            return table.AsReadOnly();
        }
    }
}
