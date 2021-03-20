using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        private static readonly Regex parse_regex = new(@"^[\+-]?\d+(\.\d+)?([eE][\+-]?\d+)?$");

        public static implicit operator MultiPrecision<N>(string num) {
            if (!parse_regex.IsMatch(num)) {
                throw new FormatException();
            }

            Sign sign = Sign.Plus;

            if (num[0] == '+' || num[0] == '-') {
                if (num[0] == '-') {
                    sign = Sign.Minus;
                }

                num = num[1..];
            }

            int exponent_symbol_index = num.Length;

            if (num.Contains('e')) {
                exponent_symbol_index = num.IndexOf('e');
            }
            else if (num.Contains('E')) {
                exponent_symbol_index = num.IndexOf('E');
            }

            string mantissa = num[..exponent_symbol_index].TrimStart('0');

            if (string.IsNullOrEmpty(mantissa)) {
                return sign == Sign.Plus ? Zero : MinusZero;
            }

            int point_symbol_index = mantissa.Contains('.') ? (mantissa.IndexOf('.') - 1) : (mantissa.Length - 1);

            string mantissa_withoutpoint = mantissa.Replace(".", string.Empty);

            if (mantissa_withoutpoint.Length > Accumulator<N>.MaxDecimalDigits) {
                mantissa_withoutpoint = mantissa_withoutpoint[..Accumulator<N>.MaxDecimalDigits];
            }

            Accumulator<N> mantissa_dec = new(mantissa_withoutpoint);

            string exponent = (exponent_symbol_index + 1 < num.Length) ? num[(exponent_symbol_index + 1)..] : "0";
            if (!Int64.TryParse(exponent, NumberStyles.Integer, CultureInfo.InvariantCulture, out Int64 exponent_dec)) {
                throw new FormatException(nameof(num));
            }

            exponent_dec = checked(exponent_dec + point_symbol_index);

            int digits = mantissa_withoutpoint.Length - 1;

            return FromStringCore(sign, exponent_dec, mantissa_dec, digits);
        }

        internal static MultiPrecision<N> FromStringCore(Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec, int digits) {
            Int64 p = checked(exponent_dec - digits);

            MultiPrecision<N> mantissa = CreateInteger(sign, mantissa_dec);

            if (p == 0) {
                return mantissa;
            }

            MultiPrecision<N> exponent = Pow(5, p);

            return Ldexp(mantissa * exponent, p);
        }
    }
}
