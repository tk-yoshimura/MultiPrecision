namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> rcp_log2_e = null;
        }

        public static MultiPrecision<N> Log(MultiPrecision<N> x) {
            if (Consts.rcp_log2_e is null) {
                Consts.rcp_log2_e = One / Log2(E);
            }

            MultiPrecision<N> y = Log2(x) * Consts.rcp_log2_e;

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

            for(long i = 2, f = 2; i < Bits; i += exp_threshold * 2, f += 2){
                MultiPrecision<Plus1<N>> dy = z * (x_expand * f - MultiPrecision<Plus1<N>>.One * (f + 1)) / checked(f * (f + 1));
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
