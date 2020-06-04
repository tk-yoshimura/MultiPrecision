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

            MultiPrecision<N> z = x;
            MultiPrecision<N> y = Zero;

            foreach(MultiPrecision<N> t in TaylorTable) { 
                MultiPrecision<N> dy = t * z;
                y += dy;

                if(dy.IsZero || dy.Exponent + Bits < y.Exponent) {
                    break;
                }

                z *= x;
            }

            return y;
        }
    }
}
