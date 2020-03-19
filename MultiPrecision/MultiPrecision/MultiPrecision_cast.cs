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

        public static explicit operator Int64(MultiPrecision<N> v){
            if (v.IsNaN) {
                throw new InvalidCastException("NaN");
            }

            if (v.IsZero || v.Exponent < 0) {
                return 0;
            }

            if (v.Exponent >= UIntUtil.UInt64Bits) { 
                throw new OverflowException();
            }
            
            Accumulator<N> acc = new Accumulator<N>(v.mantissa, v.Exponent - Mantissa<N>.Bits + 1);

            if (acc.Digits > 2) {
                throw new OverflowException();
            }

            UInt64 u = UIntUtil.Pack(acc.Value[1], acc.Value[0]);

            if (v.sign == Sign.Plus) {
                if (u > (UInt64)Int64.MaxValue) {
                    throw new OverflowException();
                }

                return unchecked((Int64)u);
            }
            else {
                if(u > (UInt64)Int64.MaxValue + 1) {
                    throw new OverflowException();
                }

                return unchecked(-(Int64)u);
            }
        }

        public static implicit operator MultiPrecision<N>(Int64 v){
            if(v >= 0) { 
                UInt64 v_pos = unchecked((UInt64)v);

                (Mantissa<N> n, int sft) = new Accumulator<N>(v_pos).Mantissa;

                return new MultiPrecision<N>(Sign.Plus, Accumulator<N>.Bits - sft - 1, n, round: false);
            }
            else { 
                UInt64 v_neg = ~(unchecked((UInt64)v)) + 1;

                (Mantissa<N> n, int sft) = new Accumulator<N>(v_neg).Mantissa;

                return new MultiPrecision<N>(Sign.Minus, Accumulator<N>.Bits - sft - 1, n, round: false);
            }
        }
    }
}
