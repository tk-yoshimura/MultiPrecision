using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecisionSandbox {
    public class BesselNearZeroCoef<N> where N : struct, IConstant {
        private readonly MultiPrecision<N> nu;
        private readonly List<MultiPrecision<N>> a_table = new();

        public BesselNearZeroCoef(MultiPrecision<N> nu) {
            this.nu = nu;

            MultiPrecision<N> a0 = MultiPrecision<N>.Pow2(-nu) / MultiPrecision<N>.Gamma(nu + 1);
            this.a_table.Add(a0);
        }

        public MultiPrecision<N> Value(int n) {
            if (n < 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            if (n < a_table.Count) {
                return a_table[n];
            }

            for (int k = a_table.Count; k <= n; k++) {
                MultiPrecision<N> a =
                    -a_table.Last() / (checked(4 * k) * (nu + k));

                a_table.Add(a);
            }

            return a_table[n];
        }
    }
}
