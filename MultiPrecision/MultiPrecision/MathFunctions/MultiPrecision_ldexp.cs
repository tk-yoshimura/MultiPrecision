namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Ldexp(MultiPrecision<N> x, int y) {
            return new MultiPrecision<N>(x.sign, x.Exponent + y, x.mantissa, round: false);
        }
    }
}
