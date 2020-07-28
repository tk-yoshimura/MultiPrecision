namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log(MultiPrecision<N> x) {
            MultiPrecision<N> y = Log2(x) * Ln2;

            return y;
        }

        public static MultiPrecision<N> Log1p(MultiPrecision<N> x) {
            const int exp_threshold = 2;

            if (x.Exponent >= -exp_threshold) {
                return Log(One + x);
            }

            MultiPrecision<Plus1<N>> x_expand = MultiPrecisionUtil.Convert<Plus1<N>, N>(x);
            MultiPrecision<Plus1<N>> w = x_expand * x_expand;
            MultiPrecision<Plus1<N>> y = x_expand;
            MultiPrecision<Plus1<N>> z = w;
            MultiPrecision<Plus1<N>> s = x_expand - MultiPrecision<Plus1<N>>.One;

            for(long i = 2, f = 2; i < Bits; i += exp_threshold * 2, f += 2){
                MultiPrecision<Plus1<N>> dy = z * (s * f - MultiPrecision<Plus1<N>>.One) / checked(f * (f + 1));
                y += dy;

                if (dy.IsZero || y.Exponent - dy.Exponent > Bits) {
                    break;
                }

                z *= w;
            }

            return MultiPrecisionUtil.Convert<N, Plus1<N>>(y);
        }
    }
}
