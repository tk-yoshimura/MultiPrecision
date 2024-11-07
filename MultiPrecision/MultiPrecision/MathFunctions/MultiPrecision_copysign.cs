namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> CopySign(MultiPrecision<N> value, MultiPrecision<N> sign) {
            
            return new MultiPrecision<N>(sign.Sign, value.exponent, value.mantissa);
        }
    }
}
