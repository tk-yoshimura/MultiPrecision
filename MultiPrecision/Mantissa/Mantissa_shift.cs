namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> operator<<(Mantissa<N> n, int sft) {
            return new Mantissa<N>(n.value << sft);
        }

        public static Mantissa<N> operator>>(Mantissa<N> n, int sft) {
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

        public static Mantissa<N> CarryRightShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N, Pow2.N1>.CarryRightShift(n.value, sft));
        }

        public static Mantissa<N> CarryRightBlockShift(Mantissa<N> n, int sft) {
            return new Mantissa<N>(BigUInt<N, Pow2.N1>.CarryRightBlockShift(n.value, sft));
        }
    }
}
