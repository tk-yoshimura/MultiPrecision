using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPrecisionSandbox {
    public static class BesselYoshidaCoef<N> where N : struct, IConstant {
        public static (MultiPrecision<N>[] cs, MultiPrecision<N>[] ds) Table(MultiPrecision<N> nu, int m) {
            BesselLimitCoef<Plus16<N>> limitcoef = new(nu.Convert<Plus16<N>>());

            MultiPrecision<N>[] cs = new MultiPrecision<N>[m + 1], ds = new MultiPrecision<N>[m + 1];

            for (int j = 0; j <= m; j++) {
                ds[m - j] = ShiftedLegendre.Table(m, j) / ((j + 1) * limitcoef.Value(j + 1)).Convert<N>();

                MultiPrecision<Plus16<N>> sum = 0;

                for (int p = j; p <= m; p++) {
                    sum += ShiftedLegendre.Table(m, p) * limitcoef.Value(p - j) / ((p + 1) * limitcoef.Value(p + 1));
                }

                cs[m - j] = sum.Convert<N>();
            }

            return (cs, ds);
        }
    }
}
