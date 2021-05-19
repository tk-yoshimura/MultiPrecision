namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        public static MultiPrecision<N> Pow10(MultiPrecision<N> x) {
            MultiPrecision<Plus1<N>> y = MultiPrecision<Plus1<N>>.Pow2(x.Convert<Plus1<N>>() * MultiPrecision<Plus1<N>>.Lb10);

            return y.Convert<N>();
        }
    }
}
