﻿namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Arsinh(MultiPrecision<N> x) {
            if (x.Sign == Sign.Minus) {
                return -Arsinh(Abs(x));
            }

            MultiPrecision<N> y = Log1p(x + (Sqrt(x * x + 1) - 1));

            return y;
        }

        public static MultiPrecision<N> Arcosh(MultiPrecision<N> x) {
            if (x == One) {
                return Zero;
            }

            MultiPrecision<N> y = Log(x + Sqrt(x * x - 1));

            return y;
        }

        public static MultiPrecision<N> Artanh(MultiPrecision<N> x) {
            if (IsNaN(x) || x.Exponent > 1 || x < MinusOne || x > One) {
                return NaN;
            }

            if (x == MinusOne) {
                return NegativeInfinity;
            }
            if (x == One) {
                return PositiveInfinity;
            }

            MultiPrecision<N> y = (Log1p(x) - Log1p(-x)) / 2;

            return y;
        }
    }
}
