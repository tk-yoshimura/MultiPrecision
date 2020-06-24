using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static ReadOnlyCollection<MultiPrecision<Next<N>>> taylor_table = null;

        private static ReadOnlyCollection<MultiPrecision<Next<N>>> TaylorTable {
            get {
                if (taylor_table is null) {
                    taylor_table = GenerateTaylorTable();
                }

                return taylor_table;
            }
        }

        private static ReadOnlyCollection<MultiPrecision<Next<N>>> GenerateTaylorTable() {
            List<MultiPrecision<Next<N>>> table = new List<MultiPrecision<Next<N>>>();

            MultiPrecision<Next<N>> v = MultiPrecision<Next<N>>.One;
            MultiPrecision<Next<N>> d = MultiPrecision<Next<N>>.One;
            MultiPrecision<Next<N>> t = MultiPrecision<Next<N>>.One;

            while (table.Count < 1024 || t.Exponent >= -Bits * 2) {
                t = MultiPrecision<Next<N>>.One / v;

                if (t.IsZero) { 
                    break;
                }

                table.Add(t);
                d += 1;
                v *= d;
            }

            return table.AsReadOnly();
        }
    }
}
