namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {

        public static BigUInt<N> operator <<(BigUInt<N> n, int sft) {
            if (sft < 0) {
                throw new ArgumentOutOfRangeException(nameof(sft));
            }

            return LeftShift(n, sft, check_overflow: false, enable_clone: true);
        }

        public static BigUInt<N> operator >>(BigUInt<N> n, int sft) {
            if (sft < 0) {
                throw new ArgumentOutOfRangeException(nameof(sft));
            }

            return RightShift(n, sft, enable_clone: true);
        }

        public static BigUInt<N> LeftShift(BigUInt<N> n, int sft, bool check_overflow = false, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? n.Copy() : n;

            UIntUtil.LeftShift(ret.value, sft, check_overflow);

            return ret;
        }

        public static BigUInt<N> RightShift(BigUInt<N> n, int sft, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? n.Copy() : n;

            UIntUtil.RightShift(ret.value, sft);

            return ret;
        }

        public static BigUInt<N> LeftBlockShift(BigUInt<N> n, int sft, bool check_overflow = false, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? n.Copy() : n;

            UIntUtil.LeftBlockShift(ret.value, sft, check_overflow);

            return ret;
        }

        public static BigUInt<N> RightBlockShift(BigUInt<N> n, int sft, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? n.Copy() : n;

            UIntUtil.RightBlockShift(ret.value, sft);

            return ret;
        }

        public static BigUInt<N> RightRoundShift(BigUInt<N> n, int sft, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? n.Copy() : n;

            UIntUtil.RightRoundShift(ret.value, sft);

            return ret;
        }

        public static BigUInt<N> RightRoundBlockShift(BigUInt<N> n, int sft, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? n.Copy() : n;

            UIntUtil.RightRoundBlockShift(ret.value, sft);

            return ret;
        }
    }
}
