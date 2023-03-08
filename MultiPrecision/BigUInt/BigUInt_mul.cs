namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {

        public static BigUInt<N> operator *(BigUInt<N> a, BigUInt<N> b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator *(BigUInt<N> a, UInt32 b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator *(UInt32 a, BigUInt<N> b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator *(BigUInt<N> a, UInt64 b) {
            return Mul(a, b);
        }

        public static BigUInt<N> operator *(UInt64 a, BigUInt<N> b) {
            return Mul(a, b);
        }

        public static BigUInt<N> Mul(BigUInt<N> a, BigUInt<N> b) {
            BigUInt<N> ret = Zero;

            UIntUtil.Fma(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<N> Mul(BigUInt<N> a, UInt32 b) {
            if (UIntUtil.IsPower2(b)) {
                return LeftShift(a, UIntUtil.Power2(b), check_overflow: true);
            }

            BigUInt<N> ret = Zero;

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<N> Mul(BigUInt<N> a, UInt64 b) {
            if (UIntUtil.IsPower2(b)) {
                return LeftShift(a, UIntUtil.Power2(b), check_overflow: true);
            }

            BigUInt<N> ret = Zero;

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<N> Mul(UInt32 a, BigUInt<N> b) {
            if (UIntUtil.IsPower2(a)) {
                return LeftShift(b, UIntUtil.Power2(a), check_overflow: true);
            }

            BigUInt<N> ret = Zero;

            UIntUtil.Fma(ret.value, b.value, a);

            return ret;
        }

        public static BigUInt<N> Mul(UInt64 a, BigUInt<N> b) {
            if (UIntUtil.IsPower2(a)) {
                return LeftShift(b, UIntUtil.Power2(a), check_overflow: true);
            }

            BigUInt<N> ret = Zero;

            UIntUtil.Fma(ret.value, b.value, a);

            return ret;
        }

        public static BigUInt<M> Mul<M>(BigUInt<N> a, BigUInt<N> b) where M : struct, IConstant {
            BigUInt<M> ret = BigUInt<M>.Zero;

            UIntUtil.Fma(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<M> Mul<M>(BigUInt<N> a, UInt64 b) where M : struct, IConstant {
            BigUInt<M> ret = BigUInt<M>.Zero;

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }
    }
}
