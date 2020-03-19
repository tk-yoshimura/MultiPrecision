namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Pow(MultiPrecision<N> x, MultiPrecision<N> y) {
            return Pow2(x * Log2(y));
        }
    }
}
