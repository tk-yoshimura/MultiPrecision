namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {
        public static BigUInt<N> Fma(BigUInt<N> c, BigUInt<N> a, BigUInt<N> b, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fma(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<N> Fma(BigUInt<N> c, BigUInt<N> a, UInt32 b, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<N> Fma(BigUInt<N> c, BigUInt<N> a, UInt64 b, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<M> Fma<M>(BigUInt<M> c, BigUInt<N> a, BigUInt<N> b, bool enable_clone = true) where M : struct, IConstant {
            BigUInt<M> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fma(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<M> Fma<M>(BigUInt<M> c, BigUInt<N> a, UInt32 b, bool enable_clone = true) where M : struct, IConstant {
            BigUInt<M> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<M> Fma<M>(BigUInt<M> c, BigUInt<N> a, UInt64 b, bool enable_clone = true) where M : struct, IConstant {
            BigUInt<M> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fma(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<N> Fms(BigUInt<N> c, BigUInt<N> a, BigUInt<N> b, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fms(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<N> Fms(BigUInt<N> c, BigUInt<N> a, UInt32 b, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<N> Fms(BigUInt<N> c, BigUInt<N> a, UInt64 b, bool enable_clone = true) {
            BigUInt<N> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<M> Fms<M>(BigUInt<M> c, BigUInt<N> a, BigUInt<N> b, bool enable_clone = true) where M : struct, IConstant {
            BigUInt<M> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fms(ret.value, a.value, b.value);

            return ret;
        }

        public static BigUInt<M> Fms<M>(BigUInt<M> c, BigUInt<N> a, UInt32 b, bool enable_clone = true) where M : struct, IConstant {
            BigUInt<M> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }

        public static BigUInt<M> Fms<M>(BigUInt<M> c, BigUInt<N> a, UInt64 b, bool enable_clone = true) where M : struct, IConstant {
            BigUInt<M> ret = enable_clone ? c.Copy() : c;

            UIntUtil.Fms(ret.value, a.value, b);

            return ret;
        }
    }
}
