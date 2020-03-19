namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> log10_2 = null;
        }

        public static MultiPrecision<N> Log10(MultiPrecision<N> x) {
            if (Consts.log10_2 is null) {
                Consts.log10_2 = One / Log2(10);
            }

            MultiPrecision<N> y = Log2(x) * Consts.log10_2;

            return y;
        }
    }
}
