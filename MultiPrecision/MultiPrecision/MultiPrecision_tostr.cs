using System;
using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

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

        internal (Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) ToStringCore(int digits) {
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

            Accumulator<N> mantissa_dec = new Accumulator<N>(mantissa, 2);

            mantissa_dec = Accumulator<N>.MulShift(mantissa_dec, Accumulator<N>.Decimal(digits + 1));
            mantissa_dec = Accumulator<N>.MulShift(mantissa_dec, new Accumulator<N>(exponent_frac.mantissa, (int)exponent_frac.Exponent));

            if (mantissa_dec >= Accumulator<N>.Decimal(digits + 2)) {
                exponent_dec = checked(exponent_dec + 1);
                mantissa_dec = Accumulator<N>.RoundDiv(mantissa_dec, Accumulator<N>.Integer(100));
            }
            else { 
                mantissa_dec = Accumulator<N>.RoundDiv(mantissa_dec, Accumulator<N>.Integer(10));
            }

            return (Sign, exponent_dec, mantissa_dec);
        }
    }
}
