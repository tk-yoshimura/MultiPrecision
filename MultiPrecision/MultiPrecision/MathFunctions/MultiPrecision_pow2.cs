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

            BigUInt<Double<N>> a = new(Mantissa<N>.One.Value, Length);
            BigUInt<N> m = new(v.mantissa.Value), w = m;
            
            foreach (var t in BigUInt<N>.TaylorTable) {
                BigUInt<Double<N>> d = BigUInt<N>.Mul<Double<N>>(w, t);
                if (d.Digits < Length) {
                    break;
                }

                a += d;
                w = BigUInt<Double<N>>.RightRoundShift(
                    BigUInt<N>.Mul<Double<N>>(w, m), 
                    Mantissa<N>.Bits - 1, enable_clone: false)
                    .Convert<N>(check_overflow: false);
            }

            BigUInt<N> n = BigUInt<Double<N>>.RightBlockShift(a, Length, enable_clone: false).Convert<N>(check_overflow: false);

            MultiPrecision<N> y = new(Sign.Plus, exponent + 1, new Mantissa<N>(n), round: false);

            return y;
        }
    }
}
