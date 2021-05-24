namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Pow(MultiPrecision<N> x, MultiPrecision<N> y) {
            if (x.Sign == Sign.Minus) {
                return NaN;
            }

            if (y.IsZero) {
                return x.IsNaN ? NaN : 1;
            }

            MultiPrecision<Plus1<N>> z = MultiPrecision<Plus1<N>>.Pow2(y.Convert<Plus1<N>>() * MultiPrecision<Plus1<N>>.Log2(x.Convert<Plus1<N>>()));

            return z.Convert<N>();
        }

        public static MultiPrecision<N> Pow(MultiPrecision<N> x, long n) {
            if (x.IsNaN) {
                return NaN;
            }

            if (n == 0) {
                return One;
            }

            ulong n_abs = UIntUtil.Abs(n);
            MultiPrecision<Plus1<N>> y = 1, z = x.Convert<Plus1<N>>();

            while (n_abs > 0) {
                if ((n_abs & 1) == 1) {
                    y *= z;
                }

                z *= z;
                n_abs >>= 1;
            }

            return ((n > 0) ? y : (1 / y)).Convert<N>();
        }

        public static MultiPrecision<N> Square(MultiPrecision<N> x) {
            return x * x;
        }

        public static MultiPrecision<N> Cube(MultiPrecision<N> x) {
            return x * x * x;
        }
    }
}
