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
            Accumulator<Next<N>> x = 1, y = 0;

            while (x.LeadingZeroCount >= 2) {
                Accumulator<Next<N>> x_next = x + (y << 1);
                Accumulator<Next<N>> y_next = x + y;

                x = x_next;
                y = y_next;
            }

            y = Accumulator<Next<N>>.RightRoundBlockShift(y, Mantissa<Next<N>>.Length);

            Accumulator<Next<N>> acc = Accumulator<Next<N>>.RoundDiv(x, y);
            (Mantissa<Next<N>> n, int _) = acc.Mantissa;

            return MultiPrecisionUtil.Convert<N, Next<N>>(new MultiPrecision<Next<N>>(Sign.Plus, exponent: 0, n, round: false));
        }
    }
}
