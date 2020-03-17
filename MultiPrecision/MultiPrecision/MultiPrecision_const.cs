namespace MultiPrecision {    
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> zero = null, minus_zero = null, one = null, minus_one = null, nan = null;
            public static MultiPrecision<N> max_value = null, min_value = null, positive_inf = null, negative_inf = null;
            public static MultiPrecision<N> epsilon = null;
        }

        public static MultiPrecision<N> Zero {
            get {
                if (Consts.zero is null) {
                    Consts.zero = new MultiPrecision<N>(Sign.Plus, ExponentMin, Mantissa<N>.Zero);
                }

                return Consts.zero;
            }
        }

        public static MultiPrecision<N> MinusZero {
            get {
                if (Consts.minus_zero is null) {
                    Consts.minus_zero = new MultiPrecision<N>(Sign.Minus, ExponentMin, Mantissa<N>.Zero);
                }

                return Consts.minus_zero;
            }
        }

        public static MultiPrecision<N> One {
            get {
                if (Consts.one is null) {
                    Consts.one = new MultiPrecision<N>(Sign.Plus, ExponentZero, Mantissa<N>.One);
                }

                return Consts.one;
            }
        }

        public static MultiPrecision<N> MinusOne {
            get {
                if (Consts.minus_one is null) {
                    Consts.minus_one = new MultiPrecision<N>(Sign.Minus, ExponentZero, Mantissa<N>.One);
                }

                return Consts.minus_one;
            }
        }

        public static MultiPrecision<N> NaN {
            get {
                if (Consts.nan is null) {
                    Consts.nan = new MultiPrecision<N>(Sign.Plus, ExponentMax, Mantissa<N>.Full);
                }

                return Consts.nan;
            }
        }

        public static MultiPrecision<N> MaxValue {
            get {
                if (Consts.max_value is null) {
                    Consts.max_value = new MultiPrecision<N>(Sign.Plus, ExponentMax - 1, Mantissa<N>.Full);
                }

                return Consts.max_value;
            }
        }

        public static MultiPrecision<N> MinValue {
            get {
                if (Consts.min_value is null) {
                    Consts.min_value = new MultiPrecision<N>(Sign.Minus, ExponentMax - 1, Mantissa<N>.Full);
                }

                return Consts.min_value;
            }
        }

        public static MultiPrecision<N> PositiveInfinity {
            get {
                if (Consts.positive_inf is null) {
                    Consts.positive_inf = new MultiPrecision<N>(Sign.Plus, ExponentMax, Mantissa<N>.Zero);
                }

                return Consts.positive_inf;
            }
        }

        public static MultiPrecision<N> NegativeInfinity {
            get {
                if (Consts.negative_inf is null) {
                    Consts.negative_inf = new MultiPrecision<N>(Sign.Minus, ExponentMax, Mantissa<N>.Zero);
                }

                return Consts.negative_inf;
            }
        }

        public static MultiPrecision<N> Epsilon {
            get {
                if (Consts.epsilon is null) {
                    Consts.epsilon = new MultiPrecision<N>(Sign.Plus, 1, Mantissa<N>.One);
                }

                return Consts.epsilon;
            }
        }
    }
}
