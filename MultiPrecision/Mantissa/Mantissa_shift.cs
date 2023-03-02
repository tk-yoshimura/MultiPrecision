namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> operator <<(Mantissa<N> n, int sft) {
            return new Mantissa<N>(n.value << sft);
        }

        public static Mantissa<N> operator >>(Mantissa<N> n, int sft) {
            return new Mantissa<N>(n.value >> sft);
        }

        public static Mantissa<N> LeftShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N>.LeftShift(n.value, sft, check_overflow: true));
        }

        public static Mantissa<N> RightShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N>.RightShift(n.value, sft));
        }

        public static Mantissa<N> LeftBlockShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N>.LeftBlockShift(n.value, sft, check_overflow: true));
        }

        public static Mantissa<N> RightBlockShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N>.RightBlockShift(n.value, sft));
        }

        public static Mantissa<N> RightRoundShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N>.RightRoundShift(n.value, sft));
        }

        public static Mantissa<N> RightRoundBlockShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N>.RightRoundBlockShift(n.value, sft));
        }
    }
}
