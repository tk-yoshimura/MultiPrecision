using System;
using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public override string ToString() {
            (Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) = ToStringCore();

            if (mantissa_dec.IsZero) {
                return sign == Sign.Plus ? "0e0" : "-0e0";
            }

            string num = mantissa_dec.ToString().TrimEnd('0');

            if (num.Length >= 2) {
                num = num.Insert(1, ".");
            }

            return $"{(sign == Sign.Plus ? "" : "-")}{num}e{exponent_dec}";
        }

        internal (Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) ToStringCore() {
            if (Consts.log10_2 is null) {
                Consts.log10_2 = One / Log2(10);
            }

            if (IsZero) {
                return (Sign, 0, Accumulator<N>.Zero);
            }

            MultiPrecision<N> exponent = Consts.log10_2 * Exponent;
            MultiPrecision<N> exponent_int = Floor(exponent), exponent_frac = Pow10(exponent - exponent_int);

            Int64 exponent_dec = (Int64)exponent_int;

            Accumulator<N> mantissa_dec = new Accumulator<N>(mantissa, 2);

            mantissa_dec = Accumulator<N>.MulShift(mantissa_dec, Accumulator<N>.MaxDecimal);
            mantissa_dec = Accumulator<N>.MulShift(mantissa_dec, new Accumulator<N>(exponent_frac.mantissa, (int)exponent_frac.Exponent));

            if (mantissa_dec >= Accumulator<N>.MaxDecimal_x10) {
                exponent_dec = checked(exponent_dec + 1);
                mantissa_dec = Accumulator<N>.RoundDiv(mantissa_dec, 10);
            }

            return (Sign, exponent_dec, mantissa_dec);
        }
    }
}
