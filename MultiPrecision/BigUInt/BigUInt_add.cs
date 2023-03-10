namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {

        public static BigUInt<N> operator +(BigUInt<N> a, BigUInt<N> b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(BigUInt<N> a, UInt32 b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(UInt32 a, BigUInt<N> b) {
            return Add(b, a);
        }

        public static BigUInt<N> operator +(BigUInt<N> a, UInt64 b) {
            return Add(a, b);
        }

        public static BigUInt<N> operator +(UInt64 a, BigUInt<N> b) {
            return Add(b, a);
        }

        public static BigUInt<N> Add(BigUInt<N> a, BigUInt<N> b) {
            uint v1_digits = a.Digits, v2_digits = b.Digits;

            if (v1_digits >= v2_digits) {
                BigUInt<N> ret = a.Copy();

                UIntUtil.Add(ret.value, b.value);

                return ret;
            }
            else {
                BigUInt<N> ret = b.Copy();

                UIntUtil.Add(ret.value, a.value);

                return ret;
            }
        }

        public static BigUInt<M> Add<M>(BigUInt<N> a, BigUInt<N> b) where M : struct, IConstant {
            uint v1_digits = a.Digits, v2_digits = b.Digits;

            if (v1_digits >= v2_digits) {
                BigUInt<M> ret = a.Convert<M>();

                UIntUtil.Add(ret.value, b.value);

                return ret;
            }
            else {
                BigUInt<M> ret = b.Convert<M>();

                UIntUtil.Add(ret.value, a.value);

                return ret;
            }
        }

        public static BigUInt<N> Add(BigUInt<N> a, UInt32 b) {
            BigUInt<N> ret = a.Copy();

            UIntUtil.Add(ret.value, b);

            return ret;
        }

        public static BigUInt<N> Add(BigUInt<N> a, UInt64 b) {
            BigUInt<N> ret = a.Copy();

            UIntUtil.Add(ret.value, b);

            return ret;
        }

        public static BigUInt<N> Add(BigUInt<N> a, UInt64 b, int sft) {
            BigUInt<N> ret = a.Copy();

            UIntUtil.Add(ret.value, b, sft);

            return ret;
        }
    }
}
