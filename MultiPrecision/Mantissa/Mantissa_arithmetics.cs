﻿using System;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> operator +(Mantissa<N> a, Mantissa<N> b) {
            return Add(a, b);
        }

        public static Mantissa<N> operator -(Mantissa<N> a, Mantissa<N> b) {
            return Sub(a, b);
        }

        public static Mantissa<N> operator +(Mantissa<N> a, UInt32 b) {
            return Add(a, b);
        }

        public static Mantissa<N> operator -(Mantissa<N> a, UInt32 b) {
            return Sub(a, b);
        }

        public static Accumulator<N> operator *(Mantissa<N> a, Mantissa<N> b) {
            return Mul(a, b);
        }

        public static Mantissa<N> operator /(Mantissa<N> a, Mantissa<N> b) {
            return Div(a, b).div;
        }

        public static Mantissa<N> operator %(Mantissa<N> a, Mantissa<N> b) {
            return Div(a, b).rem;
        }

        public static Mantissa<N> Add(Mantissa<N> v1, Mantissa<N> v2) {
            return new Mantissa<N>(BigUInt<N>.Add(v1.value, v2.value));
        }

        public static Mantissa<N> Sub(Mantissa<N> v1, Mantissa<N> v2) {
            return new Mantissa<N>(BigUInt<N>.Sub(v1.value, v2.value));
        }

        public static Mantissa<N> Add(Mantissa<N> v1, UInt32 v2) {
            return new Mantissa<N>(BigUInt<N>.Add(v1.value, v2));
        }

        public static Mantissa<N> Sub(Mantissa<N> v1, UInt32 v2) {
            return new Mantissa<N>(BigUInt<N>.Sub(v1.value, v2));
        }

        public static Accumulator<N> Mul(Mantissa<N> v1, Mantissa<N> v2) {
            return new Accumulator<N>(BigUInt<N>.ExpandMul(v1.value, v2.value));
        }

        public static (Mantissa<N> div, Mantissa<N> rem) Div(Mantissa<N> v1, Mantissa<N> v2) {
            (BigUInt<N> div, BigUInt<N> rem) = BigUInt<N>.Div(v1.value, v2.value);

            return (new Mantissa<N>(div), new Mantissa<N>(rem));
        }

        public static Mantissa<N> RoundDiv(Mantissa<N> v1, Mantissa<N> v2) {
            return new Mantissa<N>(BigUInt<N>.RoundDiv(v1.value, v2.value));
        }
    }
}
