using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Regex parse_regex = ParserRegex();

        public static implicit operator MultiPrecision<N>(string num) {
            return Parse(num);
        }

        public static MultiPrecision<N> Parse(string s) {
            if (string.IsNullOrEmpty(s) || !parse_regex.IsMatch(s)) {
                return FromIrregularString(s);
            }

            Sign sign = Sign.Plus;

            if (s[0] == '+' || s[0] == '-') {
                if (s[0] == '-') {
                    sign = Sign.Minus;
                }

                s = s[1..];
            }

            int exponent_symbol_index = s.Length;

            if (s.Contains('e')) {
                exponent_symbol_index = s.IndexOf('e');
            }
            else if (s.Contains('E')) {
                exponent_symbol_index = s.IndexOf('E');
            }

            string mantissa = s[..exponent_symbol_index].TrimStart('0');

            if (string.IsNullOrEmpty(mantissa)) {
                return sign == Sign.Plus ? Zero : MinusZero;
            }

            int point_symbol_index = mantissa.Contains('.') ? (mantissa.IndexOf('.') - 1) : (mantissa.Length - 1);

            string dec = mantissa.Replace(".", string.Empty);
            string dec_trim = dec.TrimStart('0');
            if (dec_trim.Length == 0) {
                dec_trim = "0";
            }

            int leading_zeros = dec.Length - dec_trim.Length;
            dec = dec_trim;

            if (dec.Length > BigUInt<Plus1<N>>.MaxDecimalDigits) {
                dec = dec[..BigUInt<Plus1<N>>.MaxDecimalDigits];
            }
            int digits = dec.Length - 1;

            BigUInt<Plus1<N>> mantissa_dec = new(dec);

            string exponent = (exponent_symbol_index + 1 < s.Length) ? s[(exponent_symbol_index + 1)..] : "0";
            if (!Int64.TryParse(exponent, NumberStyles.Integer, CultureInfo.InvariantCulture, out Int64 exponent_dec)) {
                throw new FormatException(nameof(s));
            }

            exponent_dec = checked(exponent_dec + point_symbol_index - leading_zeros);

            return FromStringCore(sign, exponent_dec, mantissa_dec, digits);
        }

        public static bool TryParse(string s, out MultiPrecision<N> result) {
            try {
                result = (MultiPrecision<N>)s;
                return true;
            }
            catch (FormatException) {
                result = Zero;
                return false;
            }
        }

        internal static MultiPrecision<N> FromStringCore(Sign sign, Int64 exponent_dec, BigUInt<Plus1<N>> n, int digits) {
            Int64 p = checked(exponent_dec - digits);

            MultiPrecision<Plus1<N>> mantissa = MultiPrecision<Plus1<N>>.CreateInteger(sign, n);

            if (p == 0) {
                return mantissa.Convert<N>();
            }

            MultiPrecision<Plus1<N>> exponent = MultiPrecision<Plus1<N>>.Pow(5, p);

            return MultiPrecision<Plus1<N>>.Ldexp(mantissa * exponent, p).Convert<N>();
        }

        private static MultiPrecision<N> FromIrregularString(string str) {
            if (str == double.NaN.ToString() || str.Equals("nan", StringComparison.CurrentCultureIgnoreCase)) {
                return NaN;
            }
            if (str == double.PositiveInfinity.ToString() || str.Equals("inf", StringComparison.CurrentCultureIgnoreCase) || str.Equals("+inf", StringComparison.CurrentCultureIgnoreCase)) {
                return PositiveInfinity;
            }
            if (str == double.NegativeInfinity.ToString() || str.Equals("-inf", StringComparison.CurrentCultureIgnoreCase)) {
                return NegativeInfinity;
            }

            throw new FormatException($"Invalid numeric string. : {str}");
        }

        [GeneratedRegex(@"^[\+-]?\d+(\.\d+)?([eE][\+-]?\d+)?$")]
        private static partial Regex ParserRegex();
    }
}
