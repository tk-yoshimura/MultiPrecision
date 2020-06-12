namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Pow(MultiPrecision<N> x, MultiPrecision<N> y) {
            if(x.Sign == Sign.Minus) { 
                return NaN;
            }

            if (y.IsZero) { 
                return x.IsNaN ? NaN : 1;
            }

            return Pow2(y * Log2(x));
        }
    }
}
