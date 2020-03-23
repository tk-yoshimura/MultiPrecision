using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        public static BigUInt<N, K> operator <<(BigUInt<N, K> n, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return LeftShift(n, sft);
        }

        public static BigUInt<N, K> operator >>(BigUInt<N, K> n, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return RightShift(n, sft);
        }

        public static BigUInt<N, K> LeftShift(BigUInt<N, K> n, int sft) {
            BigUInt<N, K> ret = n.Copy();

            ret.LeftShift(sft);

            return ret;
        }

        public static BigUInt<N, K> RightShift(BigUInt<N, K> n, int sft) {
            BigUInt<N, K> ret = n.Copy();

            ret.RightShift(sft);

            return ret;
        }

        public static BigUInt<N, K> LeftBlockShift(BigUInt<N, K> n, int sft) {
            BigUInt<N, K> ret = n.Copy();

            ret.LeftBlockShift(sft);

            return ret;
        }

        public static BigUInt<N, K> RightBlockShift(BigUInt<N, K> n, int sft) {
            BigUInt<N, K> ret = n.Copy();

            ret.RightBlockShift(sft);

            return ret;
        }

        public static BigUInt<N, K> RightRoundShift(BigUInt<N, K> n, int sft) {
            BigUInt<N, K> ret = n.Copy();

            ret.RightShift(sft);

            if (sft >= 1 && UIntUtil.GetBit(n.value, sft - 1) != 0) {
                ret.CarryAdd(0, 1);
            }

            return ret;
        }

        public static BigUInt<N, K> RightRoundBlockShift(BigUInt<N, K> n, int sft) {
            BigUInt<N, K> ret = n.Copy();

            ret.RightBlockShift(sft);

            if (sft >= 1 && n.value[sft - 1] > UIntUtil.UInt32Round) {
                ret.CarryAdd(0, 1);
            }

            return ret;
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft</summary>
        private unsafe void LeftShift(int sft) {
            UIntUtil.LeftShift(value, sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft</summary>
        private unsafe void RightShift(int sft) {
            UIntUtil.RightShift(value, sft);
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void LeftBlockShift(int sft) {
            UIntUtil.LeftBlockShift(value, sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft * UInt32Bits</summary>
        private void RightBlockShift(int sft) {
            UIntUtil.RightBlockShift(value, sft);
        }
    }
}
