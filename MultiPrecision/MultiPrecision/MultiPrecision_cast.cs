using System;
using System.Diagnostics;

namespace MultiPrecision {

    [DebuggerDisplay("{ToDouble()}")]
    public sealed partial class MultiPrecision<N> {

        public static explicit operator double(MultiPrecision<N> v){
            return v.ToDouble();
        }

        public double ToDouble(){
            if (IsFinite) { 
                UInt64 n = UIntUtil.Pack(mantissa.arr[MultiPrecision<N>.Length - 1], mantissa.arr[MultiPrecision<N>.Length - 2]);

                return (double)n * Math.Pow(2, (double)(Exponent - UIntUtil.UInt32Bits * 2 + 1)) * ((sign == Sign.Plus) ? 1 : -1);
            }
            else if (mantissa.IsZero){
                return (sign == Sign.Plus) ? double.PositiveInfinity : double.NegativeInfinity;
            }
            else{
                return double.NaN;
            }
        }
    }
}
