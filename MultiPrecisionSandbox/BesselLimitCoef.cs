using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionSandbox {
    public class BesselLimitCoef<N> where N : struct, IConstant {
        private readonly MultiPrecision<N> squa_nu4;
        private readonly List<MultiPrecision<N>> a_table = new() { 1 };

        public BesselLimitCoef(MultiPrecision<N> nu) {
            this.squa_nu4 = 4 * nu * nu; 
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
                    a_table.Last() * MultiPrecision<N>.Div(checked(squa_nu4 - (2 * k - 1) * (2 * k - 1)), checked(k * 8));

                a_table.Add(a);
            }

            return a_table[n];
        }
    }
}
