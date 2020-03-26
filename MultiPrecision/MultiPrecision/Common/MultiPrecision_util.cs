using System;
using System.Linq;

namespace MultiPrecision {

    public static class MultiPrecisionUtil {

        public static MultiPrecision<Ndst> Convert<Ndst, Nsrc>(MultiPrecision<Nsrc> v) where Nsrc : struct, IConstant where Ndst : struct, IConstant { 
            if(typeof(Nsrc) == typeof(Ndst)) { 
                return new MultiPrecision<Ndst>(v.Sign, v.Exponent, v.Mantissa.ToArray());
            }
            
            UInt32[] mantissa = new UInt32[MultiPrecision<Ndst>.Length];

            for(int i = 0, n = Math.Min(MultiPrecision<Nsrc>.Length, MultiPrecision<Ndst>.Length); i < n; i++) {
                mantissa[mantissa.Length - i - 1] = v.Mantissa[MultiPrecision<Nsrc>.Length - i - 1];
            }

            if(MultiPrecision<Nsrc>.Length >= MultiPrecision<Ndst>.Length && UIntUtil.IsFull(mantissa)) {
                return new MultiPrecision<Ndst>(v.Sign, v.Exponent + 1, Mantissa<Ndst>.One, round: false);
            }
            else { 
                return new MultiPrecision<Ndst>(v.Sign, v.Exponent, new Mantissa<Ndst>(mantissa), round: false);
            }
        }
    }
}
