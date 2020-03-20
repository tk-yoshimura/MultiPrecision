using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        public static BigUInt<N, K> operator<<(BigUInt<N, K> n, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return LeftShift(n, sft);
        }

        public static BigUInt<N, K> operator>>(BigUInt<N, K> n, int sft) {
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

        public static BigUInt<N, K> RoundRightShift(BigUInt<N, K> n, int sft) {
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

#if DEBUG
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }
#endif

            if (sft >= Bits) {
                Zeroset();
                return;
            }

            int sftdev = sft / UIntUtil.UInt32Bits;
            int sftrem = sft % UIntUtil.UInt32Bits;

            if (sftrem == 0) {
                LeftBlockShift(sftdev);
                return;
            }

            UInt32[] v_sft = new UInt32[Length];

            fixed(UInt32 *v = value) { 
                v_sft[sftdev] = v[0] << sftrem;
                for (int i = sftdev + 1; i < Length; i++) {
                    v_sft[i] = (v[i - sftdev] << sftrem) | (v[i - sftdev - 1] >> (UIntUtil.UInt32Bits - sftrem));
                }
            }

            Array.Copy(v_sft, 0, value, 0, Length);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft</summary>
        private unsafe void RightShift(int sft) {

#if DEBUG
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }
#endif

            if (sft >= Bits) {
                Zeroset();
                return;
            }

            int sftdev = sft / UIntUtil.UInt32Bits;
            int sftrem = sft % UIntUtil.UInt32Bits;

            if (sftrem == 0) {
                RightBlockShift(sftdev);
                return;
            }

            UInt32[] v_sft = new UInt32[Length];
    
            fixed(UInt32 *v = value) { 
                int i = sftdev;
                for (; i < Length - 1; i++) {
                    v_sft[i - sftdev] = (v[i] >> sftrem) | (v[i + 1] << (UIntUtil.UInt32Bits - sftrem));
                }
                if (i - sftdev >= 0) {
                    v_sft[i - sftdev] = v[i] >> sftrem;
                }
            }

            Array.Copy(v_sft, 0, value, 0, Length);
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void LeftBlockShift(int sft) {

#if DEBUG
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }
#endif

            fixed(UInt32 *v = value) { 
                for (int i = Math.Min(Length, Length - sft) - 1; i >= 0; i--) {
                    v[i + sft] = v[i];
                }
                for (int i = 0; i < Math.Min(sft, Length); i++) {
                    v[i] = 0;
                }
            }
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft * UInt32Bits</summary>
        private unsafe void RightBlockShift(int sft) {

#if DEBUG
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }
#endif

            fixed(UInt32 *v = value) { 
                for (int i = sft; i < Length; i++) {
                    v[i - sft] = v[i];
                }
                for (int i = Math.Max(0, Length - sft); i < Length; i++) {
                    v[i] = 0;
                }
            }
        }
    }
}
