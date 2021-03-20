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
            List<MultiPrecision<N>> table = new List<MultiPrecision<N>>();

            MultiPrecision<Plus1<N>> v = MultiPrecision<Plus1<N>>.One;
            MultiPrecision<Plus1<N>> d = MultiPrecision<Plus1<N>>.One;
            MultiPrecision<Plus1<N>> t = MultiPrecision<Plus1<N>>.One;

            while (table.Count < 1024 || t.Exponent >= -Bits * 2) {
                t = MultiPrecision<Plus1<N>>.One / v;

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
