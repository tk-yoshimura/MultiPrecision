namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Sinh(MultiPrecision<N> x) {
            if(x.Exponent < 0) { 
                return Ldexp(Expm1(x) - Expm1(-x), -1);
            }
            else { 
                return Ldexp(Exp(x) - Exp(-x), -1);
            }
        }

        public static MultiPrecision<N> Cosh(MultiPrecision<N> x) {
            return Ldexp(Exp(x) + Exp(-x), -1);
        }

        public static MultiPrecision<N> Tanh(MultiPrecision<N> x) {
            MultiPrecision<N> x2 = Ldexp(x, 1);

            return Expm1(x2) / (Exp(x2) + One);
        }
    }
}
