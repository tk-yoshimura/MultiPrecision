using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        internal static MultiPrecision<N> FromStringCore(Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) {
            (Mantissa<N> m, int sft) = mantissa_dec.Mantissa;

            MultiPrecision<N> exponent = Pow10(exponent_dec - Digits);
            MultiPrecision<N> mantissa = new MultiPrecision<N>(sign, Accumulator<N>.Bits - sft - 1, m, round: false);

            return exponent * mantissa;
        }
    }
}
