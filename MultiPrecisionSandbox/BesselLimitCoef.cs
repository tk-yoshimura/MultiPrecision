using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecisionSandbox {
    public class BesselLimitCoef<N> where N : struct, IConstant {
        private readonly MultiPrecision<N> squa_nu4;
        private readonly List<MultiPrecision<N>> a_table = new();

        public BesselLimitCoef(MultiPrecision<N> nu) {
            this.squa_nu4 = 4 * MultiPrecision<N>.Square(nu);

            MultiPrecision<N> a1 = (squa_nu4 - 1) / 8;

            this.a_table.Add(1);
            this.a_table.Add(a1);
        }

        public MultiPrecision<N> Value(int n) {
            if (n < 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            if (n < a_table.Count) {
                return a_table[n];
            }

            for (long k = a_table.Count; k <= n; k++) {
                MultiPrecision<N> a =
                    a_table.Last() * MultiPrecision<N>.Div(squa_nu4 - checked((2 * k - 1) * (2 * k - 1)), checked(k * 8));

                a_table.Add(a);
            }

            return a_table[n];
        }
    }
}
