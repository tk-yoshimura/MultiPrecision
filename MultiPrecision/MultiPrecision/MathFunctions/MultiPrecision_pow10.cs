using System;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        private static partial class Consts {
            public static MultiPrecision<N> log_2_10 = null;
        }

        public static MultiPrecision<N> Pow10(MultiPrecision<N> x) {
            if (Consts.log_2_10 is null) {
                Consts.log_2_10 = Log2(10);
            }

            MultiPrecision<N> y = Pow2(x * Consts.log_2_10);

            return y;
        }
    }
}
