using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> rcp_log2_e = null;
        }

        public static MultiPrecision<N> Log(MultiPrecision<N> x) {
            if (Consts.rcp_log2_e is null) {
                Consts.rcp_log2_e = One / Log2(E);
            }

            MultiPrecision<N> y = Log2(x) * Consts.rcp_log2_e;

            return y;
        }
    }
}
