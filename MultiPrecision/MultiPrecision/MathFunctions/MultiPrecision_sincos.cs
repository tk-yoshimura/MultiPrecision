using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        internal static MultiPrecision<N> CosHalfPI(MultiPrecision<N> x) {

            MultiPrecision<N> x_abs = Abs(x);
            MultiPrecision<N> x_int = Round(x_abs), x_frac = x_abs - x_int, xpi = Ldexp(x_frac * PI, -1), squa_xpi = xpi * xpi;
            Int64 cycle = x_int.Exponent < UIntUtil.UInt32Bits / 2 ? ((Int64)x_int) % 4 : (Int64)(x_int % 4);
            
            MultiPrecision<N> y;
            if (cycle == 0 || cycle == 2) {
                Accumulator<N> a = Accumulator<N>.One, m = new Accumulator<N>(squa_xpi.mantissa, squa_xpi.Exponent), w = m;

                Sign s = Sign.Minus;
                for (int i = 1; i + 1 < Accumulator<N>.TaylorTable.Count; i += 2) {
                    Accumulator<N> t = Accumulator<N>.TaylorTable[i];
                    Accumulator<N> d = w * t;
                    if (d.IsZero) {
                        break;
                    }

                    if (s == Sign.Plus) {
                        a += d;
                        s = Sign.Minus;
                    }
                    else {
                        a -= d;
                        s = Sign.Plus;
                    }
                    w = Accumulator<N>.RightRoundShift(w * m, Mantissa<N>.Bits - 1);
                }

                (Mantissa<N> n, int sft) = a.Mantissa;

                y = new MultiPrecision<N>((cycle == 0) ? Sign.Plus : Sign.Minus, - sft + 1, n, round: false);
            }
            else { 
                Accumulator<N> a = Accumulator<N>.One, m = new Accumulator<N>(squa_xpi.mantissa, squa_xpi.Exponent), w = m;

                Sign s = Sign.Minus;
                for (int i = 2; i + 1 < Accumulator<N>.TaylorTable.Count; i += 2) {
                    Accumulator<N> t = Accumulator<N>.TaylorTable[i];
                    Accumulator<N> d = w * t;
                    if (d.Digits < Length) {
                        break;
                    }

                    if (s == Sign.Plus) {
                        a += d;
                        s = Sign.Minus;
                    }
                    else {
                        a -= d;
                        s = Sign.Plus;
                    }
                    w = Accumulator<N>.RightRoundShift(w * m, Mantissa<N>.Bits - 1);
                }

                (Mantissa<N> n, int sft) = a.Mantissa;

                y = new MultiPrecision<N>((cycle == 1) ? Sign.Minus : Sign.Plus, - sft + 1, n, round: false);
                y *= xpi;
            }

            return y;
        }
    }
}
