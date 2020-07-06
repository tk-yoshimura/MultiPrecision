using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static ReadOnlyCollection<MultiPrecision<Plus1<N>>> taylor_table = null;

        private static ReadOnlyCollection<MultiPrecision<Plus1<N>>> TaylorTable {
            get {
                if (taylor_table is null) {
                    taylor_table = GenerateTaylorTable();
                }

                return taylor_table;
            }
        }

        private static ReadOnlyCollection<MultiPrecision<Plus1<N>>> GenerateTaylorTable() {
            List<MultiPrecision<Plus1<N>>> table = new List<MultiPrecision<Plus1<N>>>();

            MultiPrecision<Plus1<N>> v = MultiPrecision<Plus1<N>>.One;
            MultiPrecision<Plus1<N>> d = MultiPrecision<Plus1<N>>.One;
            MultiPrecision<Plus1<N>> t = MultiPrecision<Plus1<N>>.One;

            while (table.Count < 1024 || t.Exponent >= -Bits * 2) {
                t = MultiPrecision<Plus1<N>>.One / v;

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
