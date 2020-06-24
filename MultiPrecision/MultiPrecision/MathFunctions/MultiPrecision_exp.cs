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

            MultiPrecision<Next<N>> x_next = MultiPrecisionUtil.Convert<Next<N>, N>(x);
            MultiPrecision<Next<N>> z = x_next;
            MultiPrecision<Next<N>> y = MultiPrecision<Next<N>>.Zero;

            foreach(MultiPrecision<Next<N>> t in TaylorTable) { 
                MultiPrecision<Next<N>> dy = t * z;
                y += dy;

                if(dy.IsZero || y.Exponent - dy.Exponent > Bits) {
                    break;
                }

                z *= x_next;
            }

            return MultiPrecisionUtil.Convert<N, Next<N>>(y);
        }
    }
}
