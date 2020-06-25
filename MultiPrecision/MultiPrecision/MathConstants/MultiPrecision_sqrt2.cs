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
            Accumulator<Plus1<N>> x = 1, y = 0;

            while (x.LeadingZeroCount >= 2) {
                Accumulator<Plus1<N>> x_next = x + (y << 1);
                Accumulator<Plus1<N>> y_next = x + y;

                x = x_next;
                y = y_next;
            }

            y = Accumulator<Plus1<N>>.RightRoundBlockShift(y, Mantissa<Plus1<N>>.Length);

            Accumulator<Plus1<N>> acc = Accumulator<Plus1<N>>.RoundDiv(x, y);
            (Mantissa<Plus1<N>> n, int _) = acc.Mantissa;

            return MultiPrecisionUtil.Convert<N, Plus1<N>>(new MultiPrecision<Plus1<N>>(Sign.Plus, exponent: 0, n, round: false));
        }
    }
}
