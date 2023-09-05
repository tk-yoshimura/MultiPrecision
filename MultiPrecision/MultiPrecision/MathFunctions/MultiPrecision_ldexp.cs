namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Ldexp(MultiPrecision<N> x, int y) {
            if (!IsFinite(x)) {
                return x;
            }

            return new MultiPrecision<N>(x.Sign, x.Exponent + y, x.mantissa, round: false);
        }

        public static MultiPrecision<N> Ldexp(MultiPrecision<N> x, long y) {
            if (!IsFinite(x)) {
                return x;
            }

            return new MultiPrecision<N>(x.Sign, checked(x.Exponent + y), x.mantissa, round: false);
        }
    }
}
