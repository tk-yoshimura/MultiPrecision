using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> : IFormattable {

        public override string ToString() {
            if (IsNaN) {
                return double.NaN.ToString();
            }
            if (!IsFinite) {
                return (Sign == Sign.Plus) ? double.PositiveInfinity.ToString() : double.NegativeInfinity.ToString();
            }

            (Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) = ToStringCore(DecimalDigits);

            if (mantissa_dec.IsZero) {
                return sign == Sign.Plus ? "0" : "-0";
            }

            string num = mantissa_dec.ToString().TrimEnd('0');

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
            if (format == null) {
                return ToString();
            }

            if (format.Length < 2 || (format[0] != 'e' && format[0] != 'E')) {
                throw new FormatException();
            }

            if (!(int.TryParse(format[1..], NumberStyles.Integer, CultureInfo.InvariantCulture, out int digits)) || digits <= 1) {
                throw new FormatException();
            }

            if (IsNaN) {
                return double.NaN.ToString();
            }
            if (!IsFinite) {
                return (Sign == Sign.Plus) ? double.PositiveInfinity.ToString() : double.NegativeInfinity.ToString();
            }

            (Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) = ToStringCore(digits);

            if (mantissa_dec.IsZero) {
                return (sign == Sign.Plus ? "0." : "-0.") + new string('0', digits) + $"{format[0]}0";
            }

            string num = mantissa_dec.ToString();
            num = num.Insert(1, ".");

            return $"{(sign == Sign.Plus ? "" : "-")}{num}{format[0]}{exponent_dec}";
        }

        public string ToString(string format) {
            return ToString(format, null);
        }

        internal (Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) ToStringCore(int digits) {
            const int presicion = 2;

            if (digits > DecimalDigits) {
                throw new ArgumentException(nameof(digits));
            }

            if (IsZero) {
                return (Sign, 0, Accumulator<N>.Zero);
            }

            MultiPrecision<N> exponent = Lg2 * Exponent;
            MultiPrecision<N> exponent_int = Floor(exponent);
            Int64 exponent_dec = (Int64)exponent_int;

            MultiPrecision<N> exponent_frac = Ldexp(Pow(5, -exponent_dec), checked(Exponent - exponent_dec));

#if DEBUG
            Debug<ArithmeticException>.Assert(exponent_frac >= 1 && exponent_frac < 10);
#endif


            Accumulator<N> mantissa_dec = new(mantissa);

            mantissa_dec = Accumulator<N>.MulShift(mantissa_dec, Accumulator<N>.Decimal(digits + presicion));
            mantissa_dec = Accumulator<N>.MulShift(mantissa_dec, new Accumulator<N>(exponent_frac.mantissa, (int)exponent_frac.Exponent));

            if (mantissa_dec >= Accumulator<N>.Decimal(digits + presicion + 1)) {
                exponent_dec = checked(exponent_dec + 1);
                mantissa_dec = Accumulator<N>.RoundDiv(mantissa_dec, Accumulator<N>.Decimal(presicion + 1));
            }
            else {
                mantissa_dec = Accumulator<N>.RoundDiv(mantissa_dec, Accumulator<N>.Decimal(presicion));
            }
            if (mantissa_dec == Accumulator<N>.Decimal(digits + 1)) {
                exponent_dec = checked(exponent_dec + 1);
                mantissa_dec = Accumulator<N>.Decimal(digits);
            }

#if DEBUG
            Debug<ArithmeticException>.Assert(mantissa_dec < Accumulator<N>.Decimal(digits + 1));
#endif

            return (Sign, exponent_dec, mantissa_dec);
        }
    }
}
