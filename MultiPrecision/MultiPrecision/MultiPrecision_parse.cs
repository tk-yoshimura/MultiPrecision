using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        internal static MultiPrecision<N> FromStringCore(Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) {
            MultiPrecision<N> exponent = Pow10(checked(exponent_dec - DecimalDigits));
            MultiPrecision<N> mantissa = CreateInteger(sign, mantissa_dec);

            return exponent * mantissa;
        }
    }
}
