using System;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Pow2(MultiPrecision<N> x) {

            if (x.IsNaN) {
                return NaN;
            }

            MultiPrecision<N> x_int = Floor(x);

            if (x_int.Exponent >= UIntUtil.UInt32Bits) {
                if (x.Sign == Sign.Plus) {
                    return PositiveInfinity;
                }
                else {
                    return Zero;
                }
            }

            Int64 exponent = x_int.mantissa.Value.Last() >> (UIntUtil.UInt32Bits - (int)x_int.Exponent - 1);
            if (x_int.Sign == Sign.Minus) {
                exponent = -exponent;
            }

            MultiPrecision<N> x_frac = x - x_int;

            MultiPrecision<N> v = Ln2 * x_frac;

            if (v.IsZero || v.Exponent < int.MinValue) {
                return new MultiPrecision<N>(Sign.Plus, exponent, Mantissa<N>.One, round: false);
            }

            Accumulator<N> a = Accumulator<N>.One, m = new Accumulator<N>(v.mantissa, (int)v.Exponent), w = m;

            foreach (var t in Accumulator<N>.TaylorTable) {
                Accumulator<N> d = w * t;
                if (d.Digits < Length) {
                    break;
                }

                a += d;
                w = Accumulator<N>.RightRoundShift(w * m, Mantissa<N>.Bits - 1);
            }

            (Mantissa<N> n, int sft) = a.Mantissa;

            MultiPrecision<N> y = new MultiPrecision<N>(Sign.Plus, exponent - sft + 1, n, round: false);

            return y;
        }
    }
}
