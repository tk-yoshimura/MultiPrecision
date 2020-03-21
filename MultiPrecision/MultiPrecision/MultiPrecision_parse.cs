using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        internal static MultiPrecision<N> FromStringCore(Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec, int digits) {
            MultiPrecision<N> exponent = Pow10(checked(exponent_dec - digits));
            MultiPrecision<N> mantissa = CreateInteger(sign, mantissa_dec);

            return exponent * mantissa;
        }
    }
}
