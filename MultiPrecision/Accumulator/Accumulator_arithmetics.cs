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
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.Add(v1.value, v2.value));
        }

        public static Accumulator<N> Sub(Accumulator<N> v1, Accumulator<N> v2) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.Sub(v1.value, v2.value));
        }

        public static Accumulator<N> Add(Accumulator<N> v1, UInt32 v2) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.Add(v1.value, v2));
        }

        public static Accumulator<N> Sub(Accumulator<N> v1, UInt32 v2) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.Sub(v1.value, v2));
        }


        public static Accumulator<N> Mul(Accumulator<N> v1, Accumulator<N> v2) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.Mul(v1.value, v2.value));
        }

        public static (Accumulator<N> div, Accumulator<N> rem) Div(Accumulator<N> v1, Accumulator<N> v2) {
            (BigUInt<N, Pow2.N2> div, BigUInt<N, Pow2.N2> rem) = BigUInt<N, Pow2.N2>.Div(v1.value, v2.value);

            return (new Accumulator<N>(div), new Accumulator<N>(rem));
        }

        public static Accumulator<N> MulShift(Accumulator<N> v1, Accumulator<N> v2) {
            Accumulator<N> v = new Accumulator<N>(BigUInt<N, Pow2.N2>.Mul(v1.value, v2.value));

            bool round = v.Value[Mantissa<N>.Length - 1] > UIntUtil.UInt32Round;

            v >>= Mantissa<N>.Bits;

            if (round) {
                v.value.CarryAdd(0, 1);
            }

            return v;
        }
    }
}
