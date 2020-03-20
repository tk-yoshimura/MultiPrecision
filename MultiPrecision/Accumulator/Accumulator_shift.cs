namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static Accumulator<N> operator<<(Accumulator<N> n, int sft) {
            return new Accumulator<N>(n.value << sft);
        }

        public static Accumulator<N> operator>>(Accumulator<N> n, int sft) {
            return new Accumulator<N>(n.value >> sft);
        }

        public static Accumulator<N> LeftShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.LeftShift(n.value, sft));
        }

        public static Accumulator<N> RightShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.RightShift(n.value, sft));
        }

        public static Accumulator<N> LeftBlockShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.LeftBlockShift(n.value, sft));
        }        

        public static Accumulator<N> RightBlockShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.RightBlockShift(n.value, sft));
        }

        public static Accumulator<N> CarryRightShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.CarryRightShift(n.value, sft));
        }

        public static Accumulator<N> CarryRightBlockShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<N, Pow2.N2>.CarryRightBlockShift(n.value, sft));
        }
    }
}
