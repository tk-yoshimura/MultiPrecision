using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static MultiPrecision<N> const_rcp_log2_e = null;

        public static MultiPrecision<N> Log(MultiPrecision<N> x) {
            if (const_rcp_log2_e is null) {
                const_rcp_log2_e = One / Log2(E);
            }

            MultiPrecision<N> y = Log2(x) * const_rcp_log2_e;

            return y;
        }
    }
}
