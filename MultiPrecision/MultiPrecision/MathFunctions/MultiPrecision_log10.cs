namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> rcp_log2_10 = null;
        }

        public static MultiPrecision<N> Log10(MultiPrecision<N> x) {
            if (Consts.rcp_log2_10 is null) {
                Consts.rcp_log2_10 = One / Log2(10);
            }

            MultiPrecision<N> y = Log2(x) * Consts.rcp_log2_10;

            return y;
        }
    }
}
