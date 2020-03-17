namespace MultiPrecision {    
    public sealed partial class MultiPrecision<N> {

        private static MultiPrecision<N> zero = null, minus_zero = null, one = null, minus_one = null, nan = null;
        private static MultiPrecision<N> max_value = null, min_value = null, positive_inf = null, negative_inf = null;
        private static MultiPrecision<N> epsilon = null;

        public static MultiPrecision<N> Zero {
            get {
                if (zero is null) {
                    zero = new MultiPrecision<N>(Sign.Plus, ExponentMin, Mantissa<N>.Zero);
                }

                return zero;
            }
        }

        public static MultiPrecision<N> MinusZero {
            get {
                if (minus_zero is null) {
                    minus_zero = new MultiPrecision<N>(Sign.Minus, ExponentMin, Mantissa<N>.Zero);
                }

                return minus_zero;
            }
        }

        public static MultiPrecision<N> One {
            get {
                if (one is null) {
                    one = new MultiPrecision<N>(Sign.Plus, ExponentZero, Mantissa<N>.One);
                }

                return one;
            }
        }

        public static MultiPrecision<N> MinusOne {
            get {
                if (minus_one is null) {
                    minus_one = new MultiPrecision<N>(Sign.Minus, ExponentZero, Mantissa<N>.One);
                }

                return minus_one;
            }
        }

        public static MultiPrecision<N> NaN {
            get {
                if (nan is null) {
                    nan = new MultiPrecision<N>(Sign.Plus, ExponentMax, Mantissa<N>.Full);
                }

                return nan;
            }
        }

        public static MultiPrecision<N> MaxValue {
            get {
                if (max_value is null) {
                    max_value = new MultiPrecision<N>(Sign.Plus, ExponentMax - 1, Mantissa<N>.Full);
                }

                return max_value;
            }
        }

        public static MultiPrecision<N> MinValue {
            get {
                if (min_value is null) {
                    min_value = new MultiPrecision<N>(Sign.Minus, ExponentMax - 1, Mantissa<N>.Full);
                }

                return min_value;
            }
        }

        public static MultiPrecision<N> PositiveInfinity {
            get {
                if (positive_inf is null) {
                    positive_inf = new MultiPrecision<N>(Sign.Plus, ExponentMax, Mantissa<N>.Zero);
                }

                return positive_inf;
            }
        }

        public static MultiPrecision<N> NegativeInfinity {
            get {
                if (negative_inf is null) {
                    negative_inf = new MultiPrecision<N>(Sign.Minus, ExponentMax, Mantissa<N>.Zero);
                }

                return negative_inf;
            }
        }

        public static MultiPrecision<N> Epsilon {
            get {
                if (epsilon is null) {
                    epsilon = new MultiPrecision<N>(Sign.Plus, 1, Mantissa<N>.One);
                }

                return epsilon;
            }
        }
    }
}
