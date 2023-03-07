using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static ReadOnlyCollection<MultiPrecision<N>> taylor_sequence = null;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static ReadOnlyCollection<MultiPrecision<N>> TaylorSequence {
            get {
                Consts.taylor_sequence ??= GenerateTaylorSequence();

                return Consts.taylor_sequence;
            }
        }

        private static ReadOnlyCollection<MultiPrecision<N>> GenerateTaylorSequence() {
            List<MultiPrecision<N>> table = new() {
                One,
                One,
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

            return table.AsReadOnly();
        }
    }
}
