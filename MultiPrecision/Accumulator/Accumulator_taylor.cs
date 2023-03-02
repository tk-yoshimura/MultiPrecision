using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        private static ReadOnlyCollection<Accumulator<N>> taylor_table = null;

        public static ReadOnlyCollection<Accumulator<N>> TaylorTable {
            get {
                taylor_table ??= GenerateTaylorTable();

                return taylor_table;
            }
        }

        public static int TaylorTableShift => Mantissa<N>.Bits - 1;

        private static ReadOnlyCollection<Accumulator<N>> GenerateTaylorTable() {
            List<Accumulator<N>> table = new();

            Accumulator<N> v = One, d = 1;

            while (table.Count < 1 || !(table.Last().IsZero)) {
                table.Add(RightRoundShift(v, TaylorTableShift));
                d += 1;
                v /= d;
            }

            table = table.Take(table.Count - 1).ToList();

            return table.AsReadOnly();
        }
    }
}
