namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static Accumulator<N> Zero { get; } = new Accumulator<N>(BigUInt<N, Pow2.N2>.Zero);

        public static Accumulator<N> One { get; } = new Accumulator<N>(Mantissa<N>.One) * new Accumulator<N>(Mantissa<N>.One);

        public static Accumulator<N> Full { get; } = new Accumulator<N>(BigUInt<N, Pow2.N2>.Full);

        public static Accumulator<N> MaxDecimal { get; } = new Accumulator<N>(Mantissa<N>.MaxDecimal);

        public static Accumulator<N> MaxDecimal_x10 { get; } = MaxDecimal * 10;
    }
}
