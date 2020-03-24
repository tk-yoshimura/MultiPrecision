namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Sinh(MultiPrecision<N> x) {
            return Ldexp(Exp(x) - Exp(-x), -1);
        }

        public static MultiPrecision<N> Cosh(MultiPrecision<N> x) {
            return Ldexp(Exp(x) + Exp(-x), -1);
        }

        public static MultiPrecision<N> Tanh(MultiPrecision<N> x) {
            MultiPrecision<N> exp_2x = Exp(Ldexp(x, 1));

            return (exp_2x + MinusOne) / (exp_2x + One);
        }
    }
}
