namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> operator <<(Mantissa<N> n, int sft) {
            return new Mantissa<N>(n.value << sft);
        }

        public static Mantissa<N> operator >>(Mantissa<N> n, int sft) {
            return new Mantissa<N>(n.value >> sft);
        }

        public static Mantissa<N> LeftShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N, Pow2.N1>.LeftShift(n.value, sft));
        }

        public static Mantissa<N> RightShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N, Pow2.N1>.RightShift(n.value, sft));
        }

        public static Mantissa<N> LeftBlockShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N, Pow2.N1>.LeftBlockShift(n.value, sft));
        }

        public static Mantissa<N> RightBlockShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N, Pow2.N1>.RightBlockShift(n.value, sft));
        }

        public static Mantissa<N> RightRoundShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N, Pow2.N1>.RightRoundShift(n.value, sft));
        }

        public static Mantissa<N> RightRoundBlockShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N, Pow2.N1>.RightRoundBlockShift(n.value, sft));
        }
    }
}
