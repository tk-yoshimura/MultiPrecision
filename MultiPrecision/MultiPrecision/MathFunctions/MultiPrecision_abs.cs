using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Abs(MultiPrecision<N> x) {
            return new MultiPrecision<N>(Sign.Plus, x.exponent, x.mantissa);
        }
    }
}
