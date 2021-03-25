using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Sin(MultiPrecision<N> x) {
            return SinPI(x * RcpPI);
        }

        public static MultiPrecision<N> Cos(MultiPrecision<N> x) {
            return CosPI(x * RcpPI);
        }

        public static MultiPrecision<N> Tan(MultiPrecision<N> x) {
            return TanPI(x * RcpPI);
        }

        public static MultiPrecision<N> SinPI(MultiPrecision<N> x) {
            return CosHalfPI(2 * x + MinusOne);
        }

        public static MultiPrecision<N> CosPI(MultiPrecision<N> x) {
            return CosHalfPI(2 * x);
        }

        public static MultiPrecision<N> TanPI(MultiPrecision<N> x) {
            return SinPI(x) / CosPI(x);
        }

        internal static MultiPrecision<N> CosHalfPI(MultiPrecision<N> x) {
            if (!x.IsFinite) {
                return NaN;
            }

            MultiPrecision<N> x_abs = Abs(x);
            MultiPrecision<N> x_int = Round(x_abs), x_frac = x_abs - x_int, xpi = x_frac * PI / 2, squa_xpi = xpi * xpi;
            Int64 cycle = x_int.Exponent < UIntUtil.UInt32Bits / 2 ? ((Int64)x_int) % 4 : (Int64)(x_int % 4);

            Accumulator<N> a = Accumulator<N>.One, m = new(squa_xpi.mantissa, squa_xpi.Exponent), w = m;
            Sign s = Sign.Minus;

            for (int i = (cycle == 0 || cycle == 2) ? 1 : 2; i + 1 < Accumulator<N>.TaylorTable.Count; i += 2) {
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
                w = Accumulator<N>.MulShift(w, m);
            }

            (Mantissa<N> n, int sft) = a.Mantissa;

            MultiPrecision<N> y;
            if (cycle == 0 || cycle == 2) {
                y = new MultiPrecision<N>((cycle == 0) ? Sign.Plus : Sign.Minus, -sft + 1, n, round: false);
            }
            else {
                y = new MultiPrecision<N>((cycle == 1) ? Sign.Minus : Sign.Plus, -sft + 1, n, round: false);
                y *= xpi;
            }

            return y;
        }
    }
}
