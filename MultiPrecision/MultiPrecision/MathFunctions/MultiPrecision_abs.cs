﻿namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Abs(MultiPrecision<N> x) {
            MultiPrecision<N> y = new MultiPrecision<N>(Sign.Plus, x.exponent, x.mantissa);

            return y;
        }
    }
}