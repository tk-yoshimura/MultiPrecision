namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        public static MultiPrecision<N> Pow10(MultiPrecision<N> x) {
            MultiPrecision<N> y = Pow2(x * Lb10);

            return y;
        }
    }
}
