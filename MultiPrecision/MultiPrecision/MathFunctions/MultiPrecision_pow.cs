using System;

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

            long abs_n = Math.Abs(n);
            MultiPrecision<N> y = One, z = x;

            while (abs_n > 0) {
                if ((abs_n & 1) == 1) {
                    y *= z;
                }

                z *= z;
                abs_n >>= 1;
            }

            return (n > 0) ? y : (One / y);
        }
    }
}
