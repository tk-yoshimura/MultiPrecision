namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Arsinh(MultiPrecision<N> x) {
            MultiPrecision<N> y = Log(x + Sqrt(x * x + One));

            return y;
        }

        public static MultiPrecision<N> Arcosh(MultiPrecision<N> x) {
            if (x == One) {
                return Zero;
            }

            MultiPrecision<N> y = Log(x + Sqrt(x * x + MinusOne));

            return y;
        }

        public static MultiPrecision<N> Artanh(MultiPrecision<N> x) {
            if (x.IsNaN || x.Exponent > 1 || x < MinusOne || x > One) {
                return NaN;
            }

            if (x == MinusOne) {
                return NegativeInfinity;
            }
            if (x == One) {
                return PositiveInfinity;
            }

            MultiPrecision<N> y = Ldexp(Log(One + x) - Log(One - x), -1);

            return y;
        }
    }
}
