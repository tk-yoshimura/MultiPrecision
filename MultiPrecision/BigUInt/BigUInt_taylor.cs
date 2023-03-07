using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static ReadOnlyCollection<BigUInt<N>> taylor_table = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static ReadOnlyCollection<BigUInt<N>> TaylorTable {
            get {
                taylor_table ??= GenerateTaylorTable();

                return taylor_table;
            }
        }

        private static ReadOnlyCollection<BigUInt<N>> GenerateTaylorTable() {
            int sft = Mantissa<N>.Bits - 1;

            List<BigUInt<N>> table = new();

            BigUInt<N> v = new BigUInt<N>(1) << sft, d = 1;

            while (table.Count < 1 || !table.Last().IsZero) {
                table.Add(RightRoundShift(v, sft));
                d += 1;
                v /= d;
            }

            table = table.Take(table.Count - 1).ToList();

            return table.AsReadOnly();
        }
    }
}
