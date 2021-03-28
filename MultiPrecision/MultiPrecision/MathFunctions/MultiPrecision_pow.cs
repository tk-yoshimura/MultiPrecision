namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Pow(MultiPrecision<N> x, MultiPrecision<N> y) {
            if (x.Sign == Sign.Minus) {
                return NaN;
            }

            if (y.IsZero) {
                return x.IsNaN ? NaN : 1;
            }

            return Pow2(y * Log2(x));
        }

        public static MultiPrecision<N> Pow(MultiPrecision<N> x, long n) {
            if (x.IsNaN) {
                return NaN;
            }

            if (n == 0) {
                return One;
            }

            ulong n_abs = UIntUtil.Abs(n);
            MultiPrecision<N> y = 1, z = x;

            while (n_abs > 0) {
                if ((n_abs & 1) == 1) {
                    y *= z;
                }

                z *= z;
                n_abs >>= 1;
            }

            return (n > 0) ? y : (1 / y);
        }
    }
}
