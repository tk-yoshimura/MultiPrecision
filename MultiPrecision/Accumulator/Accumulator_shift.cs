namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static Accumulator<N> operator <<(Accumulator<N> n, int sft) {
            return new Accumulator<N>(n.value << sft);
        }

        public static Accumulator<N> operator >>(Accumulator<N> n, int sft) {
            return new Accumulator<N>(n.value >> sft);
        }

        public static Accumulator<N> LeftShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<Double<N>>.LeftShift(n.value, sft, check_overflow: true));
        }

        public static Accumulator<N> RightShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<Double<N>>.RightShift(n.value, sft));
        }

        public static Accumulator<N> LeftBlockShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<Double<N>>.LeftBlockShift(n.value, sft, check_overflow: true));
        }

        public static Accumulator<N> RightBlockShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<Double<N>>.RightBlockShift(n.value, sft));
        }

        public static Accumulator<N> RightRoundShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<Double<N>>.RightRoundShift(n.value, sft));
        }

        public static Accumulator<N> RightRoundBlockShift(Accumulator<N> n, int sft) {
            return new Accumulator<N>(BigUInt<Double<N>>.RightRoundBlockShift(n.value, sft));
        }
    }
}
