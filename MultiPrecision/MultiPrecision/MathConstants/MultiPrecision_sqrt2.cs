namespace MultiPrecision {    
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> sqrt2 = null;
        }

        public static MultiPrecision<N> Sqrt2 {
            get {
                if (Consts.sqrt2 is null) {
                    Consts.sqrt2 = GenerateSqrt2();
                }

                return Consts.sqrt2;
            }
        }

        private static MultiPrecision<N> GenerateSqrt2() { 
            Accumulator<N> x = 1, y = 0;

            while(x.LeadingZeroCount >= 2) { 
                Accumulator<N> x_next = x + (y << 1);
                Accumulator<N> y_next = x + y;

                x = x_next;
                y = y_next;
            }

            y = Accumulator<N>.RightRoundBlockShift(y, Mantissa<N>.Length);

            Accumulator<N> acc = Accumulator<N>.RoundDiv(x, y);
            (Mantissa<N> n, int _) = acc.Mantissa;

            return new MultiPrecision<N>(Sign.Plus, exponent: 0, n, round: false);
        }
    }
}
