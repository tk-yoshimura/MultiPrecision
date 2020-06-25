namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        private static partial class Consts {
            public static MultiPrecision<N> rcp_log2 = null;
        }

        public static MultiPrecision<N> Exp(MultiPrecision<N> x) {
            if (Consts.rcp_log2 is null) {
                Consts.rcp_log2 = One / Log(2);
            }

            MultiPrecision<N> y = Pow2(x * Consts.rcp_log2);

            return y;
        }

        public static MultiPrecision<N> Expm1(MultiPrecision<N> x) {
            if(x.Exponent >= 0) { 
                return Exp(x) - 1;
            }

            MultiPrecision<Plus1<N>> x_expand = MultiPrecisionUtil.Convert<Plus1<N>, N>(x);
            MultiPrecision<Plus1<N>> z = x_expand;
            MultiPrecision<Plus1<N>> y = MultiPrecision<Plus1<N>>.Zero;

            foreach(MultiPrecision<Plus1<N>> t in TaylorTable) { 
                MultiPrecision<Plus1<N>> dy = t * z;
                y += dy;

                if(dy.IsZero || y.Exponent - dy.Exponent > Bits) {
                    break;
                }

                z *= x_expand;
            }

            return MultiPrecisionUtil.Convert<N, Plus1<N>>(y);
        }
    }
}
