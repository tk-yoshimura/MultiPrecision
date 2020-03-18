﻿using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> operator +(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Add(a, b);
        }

        public static MultiPrecision<N> operator -(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Sub(a, b);
        }

        public static MultiPrecision<N> operator *(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Mul(a, b);
        }

        public static MultiPrecision<N> operator /(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Div(a, b);
        }

        public static MultiPrecision<N> operator +(MultiPrecision<N> x) {
            return x;
        }

        public static MultiPrecision<N> operator -(MultiPrecision<N> x) {
            return Neg(x);
        }

        public static MultiPrecision<N> Add(MultiPrecision<N> a, MultiPrecision<N> b) { 
            if(a.IsNaN || b.IsNaN) { 
                return NaN;
            }

            if (a.IsZero) {
                return b;
            }
            if (b.IsZero) {
                return a;
            }

            if(a.sign == b.sign) { 
                (Mantissa<N> mantissa, Int64 exponent) = Add((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(a.sign, exponent, mantissa, round: false);
            }
            else{
                (Mantissa<N> mantissa, Int64 exponent) = Diff((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>((Abs(a) > Abs(b)) ? a.sign : b.sign, exponent, mantissa, round: false);
            }
        }

        public static MultiPrecision<N> Sub(MultiPrecision<N> a, MultiPrecision<N> b) { 
            if(a.IsNaN || b.IsNaN) { 
                return NaN;
            }

            if (a.IsZero) {
                return Neg(b);
            }
            if (b.IsZero) {
                return a;
            }

            if(a.sign == b.sign){
                (Mantissa<N> mantissa, Int64 exponent) = Diff((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>((a > b) ? Sign.Plus : Sign.Minus, exponent, mantissa, round: false);
            }
            else {
                (Mantissa<N> mantissa, Int64 exponent) = Add((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(a.sign, exponent, mantissa, round: false);
            }
        }

        public static MultiPrecision<N> Mul(MultiPrecision<N> a, MultiPrecision<N> b) { 
            if(a.IsNaN || b.IsNaN) { 
                return NaN;
            }

            if (a.IsZero || b.IsZero) {
                return a.sign == b.sign ? Zero : MinusZero;
            }

            (Mantissa<N> mantissa, Int64 exponent) = Mul((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));
            
            return new MultiPrecision<N>((a.sign == b.sign) ? Sign.Plus : Sign.Minus, exponent, mantissa, round: false);
        }

        public static MultiPrecision<N> Div(MultiPrecision<N> a, MultiPrecision<N> b) { 
            if(a.IsNaN || b.IsNaN) { 
                return NaN;
            }

            if (b.IsZero) {
                if (a.IsZero) { 
                    return NaN;
                }
                else {
                    return (a.sign == b.sign) ? PositiveInfinity : NegativeInfinity;
                }
            }

            (Mantissa<N> mantissa, Int64 exponent, bool round) = Div((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));
            
            return new MultiPrecision<N>((a.sign == b.sign) ? Sign.Plus : Sign.Minus, exponent, mantissa, round);
        }

        public static MultiPrecision<N> Neg(MultiPrecision<N> v) { 
            return new MultiPrecision<N>((v.sign == Sign.Plus) ? Sign.Minus : Sign.Plus, v.exponent, v.mantissa);
        }
    }
}
