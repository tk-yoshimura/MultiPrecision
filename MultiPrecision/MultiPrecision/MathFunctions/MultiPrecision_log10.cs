using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static MultiPrecision<N> const_rcp_log2_10 = null;

        public static MultiPrecision<N> Log10(MultiPrecision<N> x) {
            if (const_rcp_log2_10 is null) {
                const_rcp_log2_10 = One / Log2(10);
            }

            MultiPrecision<N> y = Log2(x) * const_rcp_log2_10;

            return y;
        }
    }
}
