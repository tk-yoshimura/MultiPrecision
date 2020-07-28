namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log10(MultiPrecision<N> x) {
            MultiPrecision<N> y = Log2(x) * Lg2;

            return y;
        }
    }
}
