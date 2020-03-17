﻿using System;
using System.Diagnostics;

namespace MultiPrecision {

    [DebuggerDisplay("{ToDouble()}")]
    public sealed partial class MultiPrecision<N> {

        public static explicit operator double(MultiPrecision<N> v){
            return v.ToDouble();
        }

        public double ToDouble(){
            if (IsFinite) { 
                UInt64 n = mantissa.MostSignificantDigits;

                return (double)n * Math.Pow(2, (double)(Exponent - UIntUtil.UInt64Bits + 1)) * ((sign == Sign.Plus) ? 1 : -1);
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
