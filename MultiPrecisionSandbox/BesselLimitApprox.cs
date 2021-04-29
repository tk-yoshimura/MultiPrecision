using MultiPrecision;
using System.Collections.Generic;

namespace MultiPrecisionSandbox {
    public static class BesselLimitApprox<N> where N : struct, IConstant {
        readonly static Dictionary<MultiPrecision<N>, BesselLimitCoef<Plus1<N>>> coef_table = new();

        public static int BesselTermConvergence(MultiPrecision<N> nu, MultiPrecision<N> z) {
            BesselLimitCoef<Plus1<N>> table;
            if (coef_table.ContainsKey(nu)) {
                table = coef_table[nu];
            }
            else {
                table = new BesselLimitCoef<Plus1<N>>(nu.Convert<Plus1<N>>());
                coef_table.Add(nu, table);
            }

            MultiPrecision<Plus1<N>> v = 1 / z.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> w = v * v;

            MultiPrecision<Plus1<N>> x = 0, u = 1, prev_c = null;

            Sign sign = Sign.Plus;

            for (int terms = 1; terms < int.MaxValue; terms++) {
                MultiPrecision<Plus1<N>> c = u * table.Value(terms * 2);

                if (sign == Sign.Plus) {
                    x += c;
                }
                else { 
                    x -= c;
                }

                u *= w;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    return terms;
                }
                if (prev_c is not null && prev_c.Exponent < c.Exponent) {
                    return int.MaxValue;
                }

                prev_c = c; 
             }

            return int.MaxValue;
        }
    }
}
