using System;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        private static partial class Consts {
            public static MultiPrecision<N> rcp_log2 = null;
        }

        public static MultiPrecision<N> Exp(MultiPrecision<N> x) {
            if (Consts.rcp_log2 is null) {
                Consts.rcp_log2 = One / Log(2);
            }

            MultiPrecision<N> y = Pow2(x * Consts.rcp_log2);

            return y;
        }
    }
}
