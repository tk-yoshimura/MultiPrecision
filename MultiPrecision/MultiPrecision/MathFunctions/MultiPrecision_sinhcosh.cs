namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Sinh(MultiPrecision<N> x) {
            if (x.Exponent < 0) {
                return (Expm1(x) - Expm1(-x)) / 2;
            }
            else {
                return (Exp(x) - Exp(-x)) / 2;
            }
        }

        public static MultiPrecision<N> Cosh(MultiPrecision<N> x) {
            return (Exp(x) + Exp(-x)) / 2;
        }

        public static MultiPrecision<N> Tanh(MultiPrecision<N> x) {
            if (x.IsNaN) {
                return NaN;
            }

            MultiPrecision<N> x2 = 2 * x;
            MultiPrecision<N> x2_expm1 = Expm1(x2), x2_expp1 = Exp(x2) + 1;

            if (x2_expm1.IsFinite && x2_expp1.IsFinite) {
                return x2_expm1 / x2_expp1;
            }
            else {
                return 1;
            }
        }
    }
}
