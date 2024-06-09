using System.Diagnostics;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> zero = null, minus_zero = null, one = null, minus_one = null, nan = null, p5 = null;
            public static MultiPrecision<N> max_value = null, min_value = null, positive_inf = null, negative_inf = null;
            public static MultiPrecision<N> epsilon = null;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> Zero {
            get {
                Consts.zero ??= new MultiPrecision<N>(Sign.Plus, ExponentMin, Mantissa<N>.Zero);

                return Consts.zero;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> PlusZero => Zero;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> MinusZero {
            get {
                Consts.minus_zero ??= new MultiPrecision<N>(Sign.Minus, ExponentMin, Mantissa<N>.Zero);

                return Consts.minus_zero;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> One {
            get {
                Consts.one ??= new MultiPrecision<N>(Sign.Plus, ExponentZero, Mantissa<N>.One);

                return Consts.one;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> Point5 {
            get {
                Consts.p5 ??= new MultiPrecision<N>(Sign.Plus, ExponentZero - 1u, Mantissa<N>.One);

                return Consts.p5;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> MinusOne {
            get {
                Consts.minus_one ??= new MultiPrecision<N>(Sign.Minus, ExponentZero, Mantissa<N>.One);

                return Consts.minus_one;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> NaN {
            get {
                Consts.nan ??= new MultiPrecision<N>(Sign.Plus, ExponentMax, Mantissa<N>.Full);

                return Consts.nan;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> MaxValue {
            get {
                Consts.max_value ??= new MultiPrecision<N>(Sign.Plus, ExponentMax - 1, Mantissa<N>.Full);

                return Consts.max_value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> MinValue {
            get {
                Consts.min_value ??= new MultiPrecision<N>(Sign.Minus, ExponentMax - 1, Mantissa<N>.Full);

                return Consts.min_value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> PositiveInfinity {
            get {
                Consts.positive_inf ??= new MultiPrecision<N>(Sign.Plus, ExponentMax, Mantissa<N>.Zero);

                return Consts.positive_inf;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> NegativeInfinity {
            get {
                Consts.negative_inf ??= new MultiPrecision<N>(Sign.Minus, ExponentMax, Mantissa<N>.Zero);

                return Consts.negative_inf;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> Epsilon {
            get {
                Consts.epsilon ??= new MultiPrecision<N>(Sign.Plus, 1, Mantissa<N>.One);

                return Consts.epsilon;
            }
        }
    }
}
