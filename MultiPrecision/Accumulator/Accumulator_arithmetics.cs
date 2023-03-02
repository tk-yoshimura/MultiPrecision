using System;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static Accumulator<N> operator +(Accumulator<N> a, Accumulator<N> b) {
            return Add(a, b);
        }

        public static Accumulator<N> operator -(Accumulator<N> a, Accumulator<N> b) {
            return Sub(a, b);
        }

        public static Accumulator<N> operator +(Accumulator<N> a, UInt32 b) {
            return Add(a, b);
        }

        public static Accumulator<N> operator -(Accumulator<N> a, UInt32 b) {
            return Sub(a, b);
        }

        public static Accumulator<N> operator *(Accumulator<N> a, Accumulator<N> b) {
            return Mul(a, b);
        }

        public static Accumulator<N> operator /(Accumulator<N> a, Accumulator<N> b) {
            return Div(a, b).div;
        }

        public static Accumulator<N> operator %(Accumulator<N> a, Accumulator<N> b) {
            return Div(a, b).rem;
        }

        public static Accumulator<N> Add(Accumulator<N> v1, Accumulator<N> v2) {
            return new Accumulator<N>(BigUInt<Double<N>>.Add(v1.value, v2.value));
        }

        public static Accumulator<N> Sub(Accumulator<N> v1, Accumulator<N> v2) {
            return new Accumulator<N>(BigUInt<Double<N>>.Sub(v1.value, v2.value));
        }

        public static Accumulator<N> Add(Accumulator<N> v1, UInt32 v2) {
            return new Accumulator<N>(BigUInt<Double<N>>.Add(v1.value, v2));
        }

        public static Accumulator<N> Sub(Accumulator<N> v1, UInt32 v2) {
            return new Accumulator<N>(BigUInt<Double<N>>.Sub(v1.value, v2));
        }


        public static Accumulator<N> Mul(Accumulator<N> v1, Accumulator<N> v2) {
            return new Accumulator<N>(BigUInt<Double<N>>.Mul(v1.value, v2.value));
        }

        public static (Accumulator<N> div, Accumulator<N> rem) Div(Accumulator<N> v1, Accumulator<N> v2) {
            (BigUInt<Double<N>> div, BigUInt<Double<N>> rem) = BigUInt<Double<N>>.DivRem(v1.value, v2.value);

            return (new Accumulator<N>(div), new Accumulator<N>(rem));
        }

        public static Accumulator<N> MulShift(Accumulator<N> v1, Accumulator<N> v2) {
            Accumulator<N> v = new(BigUInt<Double<N>>.Mul(v1.value, v2.value));

            return RightRoundShift(v, Mantissa<N>.Bits - 1);
        }

        public static Accumulator<N> RoundDiv(Accumulator<N> v1, Accumulator<N> v2) {
            return new Accumulator<N>(BigUInt<Double<N>>.RoundDiv(v1.value, v2.value));
        }
    }
}
