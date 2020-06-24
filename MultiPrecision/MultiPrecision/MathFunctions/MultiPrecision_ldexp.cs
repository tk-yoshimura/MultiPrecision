namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Ldexp(MultiPrecision<N> x, int y) {
            if (!x.IsFinite) {
                return x;
            }

            return new MultiPrecision<N>(x.Sign, x.Exponent + y, x.mantissa, round: false);
        }
    }
}
