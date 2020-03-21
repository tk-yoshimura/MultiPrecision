using System;
using System.Text.RegularExpressions;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static Regex parse_regex = new Regex("[+-]?[0-9]+(?:[.0-9]+)?(?:[eE][+-]?[0-9]+)?");

        //public static implicit operator MultiPrecision<N>(string num) {
        //    if (!parse_regex.IsMatch(num)) {
        //        throw new ArgumentException(nameof(num));
        //    }

        //    Sign sign = Sign.Plus;

        //    if (num[0] == '+' || num[0] == '-') { 
        //        sign = 
        //    }
        //}

        internal static MultiPrecision<N> FromStringCore(Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec, int digits) {
            MultiPrecision<N> exponent = Pow10(checked(exponent_dec - digits));
            MultiPrecision<N> mantissa = CreateInteger(sign, mantissa_dec);

            return exponent * mantissa;
        }
    }
}
