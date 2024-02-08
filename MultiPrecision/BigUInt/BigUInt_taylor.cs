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
            List<BigUInt<N>> table = [];

            BigUInt<Plus1<N>> v = BigUInt<Plus1<N>>.Top80000000u;
            BigUInt<Plus1<N>> d = 1;

            while (table.Count < 1 || !table.Last().IsZero) {
                table.Add(BigUInt<Plus1<N>>.RightRoundBlockShift(v, 1).Convert<N>(check_overflow: false));
                d += 1;
                v /= d;
            }

            table = table.Take(table.Count - 1).ToList();

            return table.AsReadOnly();
        }
    }
}
