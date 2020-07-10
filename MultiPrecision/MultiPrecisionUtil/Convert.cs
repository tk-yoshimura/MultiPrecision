using System;
using System.Linq;

namespace MultiPrecision {

    public static partial class MultiPrecisionUtil {

        public static MultiPrecision<Ndst> Convert<Ndst, Nsrc>(MultiPrecision<Nsrc> v) where Nsrc : struct, IConstant where Ndst : struct, IConstant {
            if (!v.IsFinite) {
                if (v.IsNaN) {
                    return MultiPrecision<Ndst>.NaN;
                }
                return v.Sign == Sign.Plus ? MultiPrecision<Ndst>.PositiveInfinity : MultiPrecision<Ndst>.NegativeInfinity;
            }

            if (MultiPrecision<Nsrc>.Length == MultiPrecision<Ndst>.Length) {
                return new MultiPrecision<Ndst>(v.Sign, v.Exponent, v.Mantissa.ToArray());
            }

            UInt32[] mantissa = new UInt32[MultiPrecision<Ndst>.Length];

            if (MultiPrecision<Nsrc>.Length <= MultiPrecision<Ndst>.Length) {
                Array.Copy(v.Mantissa.ToArray(), 0, mantissa, MultiPrecision<Ndst>.Length - MultiPrecision<Nsrc>.Length, MultiPrecision<Nsrc>.Length);

                return new MultiPrecision<Ndst>(v.Sign, v.Exponent, new Mantissa<Ndst>(mantissa, enable_clone: false), round: false);
            }
            else {
                Array.Copy(v.Mantissa.ToArray(), MultiPrecision<Nsrc>.Length - MultiPrecision<Ndst>.Length, mantissa, 0, MultiPrecision<Ndst>.Length);

                return new MultiPrecision<Ndst>(v.Sign, v.Exponent, new Mantissa<Ndst>(mantissa, enable_clone: false), round: v.Mantissa[0] > UIntUtil.UInt32Round);
            }
        }
    }
}
