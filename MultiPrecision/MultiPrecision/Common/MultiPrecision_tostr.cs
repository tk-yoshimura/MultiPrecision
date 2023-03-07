using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace MultiPrecision {
    //[DebuggerDisplay("{Convert<MultiPrecision.Pow2.N4>().ToString(),nq}")]
    [DebuggerDisplay("{ToDouble(),nq}")]
    public sealed partial class MultiPrecision<N> : IFormattable {

        public override string ToString() {
            if (IsNaN) {
                return double.NaN.ToString();
            }
            if (!IsFinite) {
                return (Sign == Sign.Plus) ? double.PositiveInfinity.ToString() : double.NegativeInfinity.ToString();
            }

            (Sign sign, Int64 exponent_dec, BigUInt<N> n) = ToStringCore(DecimalDigits);

            if (n.IsZero) {
                return sign == Sign.Plus ? "0" : "-0";
            }

            string num = n.ToString().TrimEnd('0');

            if (exponent_dec >= 8 || exponent_dec <= -4 || exponent_dec == 0) {
                if (num.Length >= 2) {
                    num = num.Insert(1, ".");
                }

                if (exponent_dec != 0) {
                    return $"{(sign == Sign.Plus ? "" : "-")}{num}e{exponent_dec}";
                }
                else {
                    return $"{(sign == Sign.Plus ? "" : "-")}{num}";
                }
            }
            else if (exponent_dec < 0) {
                num = new string('0', checked((int)-exponent_dec)) + num;
                num = num.Insert(1, ".");

                return $"{(sign == Sign.Plus ? "" : "-")}{num}";
            }
            else {
                if (num.Length < checked((int)exponent_dec + 1)) {
                    num += new string('0', checked((int)exponent_dec - num.Length + 1));
                }
                else if (num.Length > checked((int)exponent_dec + 1)) {
                    num = num.Insert(checked((int)exponent_dec + 1), ".");
                }

                return $"{(sign == Sign.Plus ? "" : "-")}{num}";
            }
        }

        public string ToString([AllowNull] string format, [AllowNull] IFormatProvider formatProvider) {
            if (format is null) {
                return ToString();
            }

            format = format.Trim();

            if (format.Length < 2 || (format[0] != 'e' && format[0] != 'E')) {
                throw new FormatException(format);
            }

            if (!(int.TryParse(format[1..], NumberStyles.Integer, CultureInfo.InvariantCulture, out int digits)) || digits <= 1) {
                throw new FormatException(format);
            }

            if (IsNaN) {
                return double.NaN.ToString();
            }
            if (!IsFinite) {
                return (Sign == Sign.Plus) ? double.PositiveInfinity.ToString() : double.NegativeInfinity.ToString();
            }

            (Sign sign, Int64 exponent_dec, BigUInt<N> n) = ToStringCore(digits);

            if (n.IsZero) {
                return (sign == Sign.Plus ? "0." : "-0.") + new string('0', digits) + $"{format[0]}0";
            }

            string num = n.ToString();
            num = num.Insert(1, ".");

            return $"{(sign == Sign.Plus ? "" : "-")}{num}{format[0]}{exponent_dec}";
        }

        public string ToString(string format) {
            return ToString(format, null);
        }

        internal (Sign sign, Int64 exponent_dec, BigUInt<N> n) ToStringCore(int digits) {
            const int presicion = 2;

            if (digits > DecimalDigits) {
                throw new ArgumentOutOfRangeException(nameof(digits));
            }

            if (IsZero) {
                return (Sign, 0, BigUInt<N>.Zero);
            }

            MultiPrecision<N> exponent = Lg2 * Exponent;
            MultiPrecision<N> exponent_int = Floor(exponent);
            Int64 exponent_dec = (Int64)exponent_int;

            MultiPrecision<N> exponent_frac = Ldexp(Pow(5, -exponent_dec), checked(Exponent - exponent_dec));

#if DEBUG

            if (!(exponent_frac >= 1 && exponent_frac < 10)) {
                Console.WriteLine();
            }

            Debug<ArithmeticException>.Assert(exponent_frac >= 1 && exponent_frac < 10, exponent_frac.ToHexcode());
#endif

            BigUInt<N> n = new(mantissa.Value), f = new(exponent_frac.mantissa.Value);

            n = BigUInt<Double<N>>.RightRoundShift(
                BigUInt<N>.Mul<Double<N>>(n, BigUInt<N>.Decimal(digits + presicion)), 
                Bits - 1, enable_clone: false).Convert<N>(check_overflow: false);

            n = BigUInt<Double<N>>.RightRoundShift(
                BigUInt<N>.Mul<Double<N>>(n, f), 
                Bits - 1 - (int)exponent_frac.Exponent, enable_clone: false).Convert<N>(check_overflow: false);

            int n_length = n.ToString().Length;

            if (n_length > (digits + 1)) {
                int trunc_digits = n_length - (digits + 1);
                exponent_dec = checked(exponent_dec + trunc_digits - presicion);
                n = BigUInt<N>.RoundDiv(n, BigUInt<N>.Decimal(trunc_digits));
            }
            if (n == BigUInt<N>.Decimal(digits + 1)) {
                exponent_dec = checked(exponent_dec + 1);
                n = BigUInt<N>.Decimal(digits);
            }

#if DEBUG
            Debug<ArithmeticException>.Assert(n < BigUInt<N>.Decimal(digits + 1));
            Debug<ArithmeticException>.Assert(n.ToString().Length == (digits + 1));
#endif

            return (Sign, exponent_dec, n);
        }
    }
}
