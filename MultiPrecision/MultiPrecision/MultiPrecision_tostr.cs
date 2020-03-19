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

            if(num.Length >= 2) { 
                num = num.Insert(1, ".");
            }

            return $"{(sign == Sign.Plus ? "" : "-")}{num}e{exponent_dec}";
        }

        internal (Sign sign, Int64 exponent_dec, Accumulator<N> mantissa_dec) ToStringCore() {
            if (Consts.log10_2 is null) {
                Consts.log10_2 = One / Log2(10);
            }

            if (IsZero) { 
                return (sign, 0, Accumulator<N>.Zero);
            }

            MultiPrecision<N> exponent = Consts.log10_2 * Exponent;
            MultiPrecision<N> exponent_int = Floor(exponent), exponent_frac = Pow10(exponent - exponent_int);

            Accumulator<N> exponent_acc = new Accumulator<N>(exponent_int.mantissa, exponent_int.Exponent - Mantissa<N>.Bits + 1);

            if(exponent_acc.Digits > 1) { 
                throw new OverflowException();
            }

            Int64 exponent_dec = exponent_acc.Value[0];
            if(exponent_int.sign == Sign.Minus) { 
                exponent_dec = -exponent_dec;
            }

            Accumulator<N> mantissa_acc = new Accumulator<N>(mantissa);

            mantissa_acc = (mantissa_acc * Accumulator<N>.MaxDecimal) >> (Mantissa<N>.Bits - 2);

            if((mantissa_acc.Value[0] & 1) == 1) { 
                mantissa_acc = (mantissa_acc >> 1) + 1;
            }
            else { 
                mantissa_acc >>= 1;
            }

            mantissa_acc = (mantissa_acc * new Accumulator<N>(exponent_frac.mantissa)) >> (Mantissa<N>.Bits - (int)exponent_frac.Exponent - 2);

            if((mantissa_acc.Value[0] & 1) == 1) { 
                mantissa_acc = (mantissa_acc >> 1) + 1;
            }
            else { 
                mantissa_acc >>= 1;
            }

            if(mantissa_acc >= Accumulator<N>.MaxDecimal_x10) { 
                (Accumulator<N> div, Accumulator<N> rem) = Accumulator<N>.Div(mantissa_acc, 10);
                if(rem.Value[0] >= 5) { 
                    div += 1;
                }

                exponent_dec += 1;
                mantissa_acc = div;
            }

            return (sign, exponent_dec, mantissa_acc);
        }
    }
}
