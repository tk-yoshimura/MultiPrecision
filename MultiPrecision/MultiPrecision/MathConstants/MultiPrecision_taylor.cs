using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static ReadOnlyCollection<MultiPrecision<N>> taylor_table = null;

        public static ReadOnlyCollection<MultiPrecision<N>> TaylorTable {
            get {
                if (taylor_table is null) {
                    taylor_table = GenerateTaylorTable();
                }

                return taylor_table;
            }
        }

        public static int TaylorTableShift => Mantissa<N>.Bits - 1;

        private static ReadOnlyCollection<MultiPrecision<N>> GenerateTaylorTable() {
            List<MultiPrecision<N>> table = new List<MultiPrecision<N>>();

            MultiPrecision<N> v = One;
            MultiPrecision<N> d = 1;
            MultiPrecision<N> t = One;

            while (table.Count < 1024 || t.Exponent >= -Bits * 2) {
                t = One / v;

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
