namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> BitIncrement(MultiPrecision<N> x) {
            if (!x.IsFinite) {
                if (x.IsNaN) { 
                    return NaN;
                }
                return x.Sign == Sign.Plus ? PositiveInfinity : MinValue;
            }
            if (x.IsZero) { 
                return Epsilon;
            }
            if (x == -Epsilon) { 
                return MinusZero;
            }

            Mantissa<N> m = x.mantissa;

            if(x.Sign == Sign.Plus) { 
                if (m.IsFull) { 
                    return new MultiPrecision<N>(Sign.Plus, x.Exponent + 1, Mantissa<N>.One, round: false);
                }
                return new MultiPrecision<N>(Sign.Plus, x.Exponent, m + 1, round: false);
            }
            else { 
                if (m == Mantissa<N>.One) { 
                    return new MultiPrecision<N>(Sign.Minus, x.Exponent - 1, Mantissa<N>.Full, round: false);
                }
                return new MultiPrecision<N>(Sign.Minus, x.Exponent, m - 1, round: false);
            }
        }

        public static MultiPrecision<N> BitDecrement(MultiPrecision<N> x) {
            if (!x.IsFinite) {
                if (x.IsNaN) { 
                    return NaN;
                }
                return x.Sign == Sign.Minus ? NegativeInfinity : MaxValue;
            }
            if (x.IsZero) { 
                return -Epsilon;
            }
            if (x == Epsilon) { 
                return Zero;
            }

            Mantissa<N> m = x.mantissa;

            if(x.Sign == Sign.Plus) { 
                if (m == Mantissa<N>.One) { 
                    return new MultiPrecision<N>(Sign.Plus, x.Exponent - 1, Mantissa<N>.Full, round: false);
                }
                return new MultiPrecision<N>(Sign.Plus, x.Exponent, m - 1, round: false);
            }
            else { 
                if (m.IsFull) { 
                    return new MultiPrecision<N>(Sign.Minus, x.Exponent + 1, Mantissa<N>.One, round: false);
                }
                return new MultiPrecision<N>(Sign.Minus, x.Exponent, m + 1, round: false);
            }
        }
    }
}
