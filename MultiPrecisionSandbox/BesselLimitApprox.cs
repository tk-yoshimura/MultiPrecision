using MultiPrecision;
using System.Collections.Generic;

namespace MultiPrecisionSandbox {
    static class Bessel0LimitApprox<N> where N : struct, IConstant {
        static readonly Dictionary<int, MultiPrecision<Plus1<N>>> table = new();

        public static int BesselTermConvergence(int z) {
            MultiPrecision<Plus1<N>> v = 1 / (MultiPrecision<Plus1<N>>)z;
            MultiPrecision<Plus1<N>> w = v * v;

            MultiPrecision<Plus1<N>> x = 0, u = 1, prev_c = null;

            Sign sign = Sign.Plus;

            for (int terms = 1; terms < int.MaxValue; terms++) {
                MultiPrecision<Plus1<N>> c = u * Coef(terms * 2);

                if (sign == Sign.Plus) {
                    x += c;
                }
                else { 
                    x -= c;
                }

                u *= w;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<N>.Bits) {
                    return terms;
                }
                if (prev_c is not null && prev_c.Exponent < c.Exponent) {
                    return int.MaxValue;
                }

                prev_c = c; 
             }

            return int.MaxValue;
        }

        private static MultiPrecision<Plus1<N>> Coef(int k) {
            if (!table.ContainsKey(k)) {
                MultiPrecision<Plus1<N>> c = Bessel0LimitCoef.Table(k).ToMultiPrecision<Plus1<N>>();

                table.Add(k, c);
            }

            return table[k];
        }
    }
}
