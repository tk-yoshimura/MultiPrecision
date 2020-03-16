namespace MultiPrecision {    
    public sealed partial class MultiPrecision<N> {

        private static MultiPrecision<N> zero = null, one = null, minus_one = null, nan = null;
        private static MultiPrecision<N> max_value = null, min_value = null, positive_inf = null, negative_inf = null;
        private static MultiPrecision<N> epsilon = null;

        public static MultiPrecision<N> Zero {
            get {
                if (zero is null) {
                    zero = Create(Sign.Plus, ExponentMin, Mantissa<N>.Zero);
                }

                return zero.Copy();
            }
        }

        public static MultiPrecision<N> One {
            get {
                if (one is null) {
                    one = Create(Sign.Plus, ExponentZero, Mantissa<N>.One);
                }

                return one.Copy();
            }
        }

        public static MultiPrecision<N> MinusOne {
            get {
                if (minus_one is null) {
                    minus_one = Create(Sign.Minus, ExponentZero, Mantissa<N>.One);
                }

                return minus_one.Copy();
            }
        }

        public static MultiPrecision<N> NaN {
            get {
                if (nan is null) {
                    nan = Create(Sign.Plus, ExponentMax, Mantissa<N>.Full);
                }

                return nan.Copy();
            }
        }

        public static MultiPrecision<N> MaxValue {
            get {
                if (max_value is null) {
                    max_value = Create(Sign.Plus, ExponentMax - 1, Mantissa<N>.Full);
                }

                return max_value.Copy();
            }
        }

        public static MultiPrecision<N> MinValue {
            get {
                if (min_value is null) {
                    min_value = Create(Sign.Minus, ExponentMax - 1, Mantissa<N>.Full);
                }

                return min_value.Copy();
            }
        }

        public static MultiPrecision<N> PositiveInfinity {
            get {
                if (positive_inf is null) {
                    positive_inf = Create(Sign.Plus, ExponentMax, Mantissa<N>.Zero);
                }

                return positive_inf.Copy();
            }
        }

        public static MultiPrecision<N> NegativeInfinity {
            get {
                if (negative_inf is null) {
                    negative_inf = Create(Sign.Minus, ExponentMax, Mantissa<N>.Zero);
                }

                return negative_inf.Copy();
            }
        }

        public static MultiPrecision<N> Epsilon {
            get {
                if (epsilon is null) {
                    epsilon = Create(Sign.Plus, 1, Mantissa<N>.One);
                }

                return epsilon.Copy();
            }
        }
    }
}
