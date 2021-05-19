namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log10(MultiPrecision<N> x) {
            MultiPrecision<Plus1<N>> y = MultiPrecision<Plus1<N>>.Log2(x.Convert<Plus1<N>>()) * MultiPrecision<Plus1<N>>.Lg2;

            return y.Convert<N>();
        }
    }
}
