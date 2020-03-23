using System;
using System.Linq;

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

        public string ToString(string format, IFormatProvider formatProvider) {
            if (format == null) {
                return ToString();
            }

            if (format.Length < 2 || (format[0] != 'e' && format[0] != 'E')) {
                throw new FormatException();
            }

            if (!(int.TryParse(format[1..], out int digits)) || digits <= 1) {
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

        internal (Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) ToStringCore(int digits) {
            const int presicion = 2;
            const UInt64 presicion_pow10 = 100, presicion_p1_pow10 = presicion_pow10 * 10;

            if (Consts.log10_2 is null) {
                Consts.log10_2 = One / Log2(10);
            }

            if (digits > DecimalDigits) {
                throw new ArgumentException(nameof(digits));
            }

            if (IsZero) {
                return (Sign, 0, Accumulator<N>.Zero);
            }

            MultiPrecision<N> exponent = Consts.log10_2 * Exponent;
            MultiPrecision<N> exponent_int = Floor(exponent), exponent_frac = Pow10(exponent - exponent_int);

            Int64 exponent_dec = (Int64)exponent_int;

            Accumulator<N> mantissa_dec = new Accumulator<N>(mantissa);

            mantissa_dec = Accumulator<N>.MulShift(mantissa_dec, Accumulator<N>.Decimal(digits + presicion));
            mantissa_dec = Accumulator<N>.MulShift(mantissa_dec, new Accumulator<N>(exponent_frac.mantissa, (int)exponent_frac.Exponent));

            if (mantissa_dec >= Accumulator<N>.Decimal(digits + presicion + 1)) {
                exponent_dec = checked(exponent_dec + 1);
                mantissa_dec = Accumulator<N>.RoundDiv(mantissa_dec, Accumulator<N>.Integer(presicion_p1_pow10));
            }
            else {
                mantissa_dec = Accumulator<N>.RoundDiv(mantissa_dec, Accumulator<N>.Integer(presicion_pow10));
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
