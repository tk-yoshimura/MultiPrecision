using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    public sealed partial class Mantissa<N> where N : struct, IConstant {

        public static Mantissa<N> operator<<(Mantissa<N> a, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return LeftShift(a, (uint)sft);
        }

        public static Mantissa<N> operator>>(Mantissa<N> a, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return RightShift(a, (uint)sft);
        }

        internal void LeftShift(uint sft) {
            if (sft >= Bits) {
                Zeroset();
                return;
            }

            int sftdev = (int)sft / UIntUtil.UInt32Bits;
            int sftrem = (int)sft % UIntUtil.UInt32Bits;

            if (sftrem == 0) {
                LeftShiftArray((uint)sftdev);
                return;
            }

            UInt32[] arr_sft = new UInt32[Length];

            arr_sft[sftdev] = arr[0] << sftrem;
            for (int i = sftdev + 1; i < Length; i++) {
                arr_sft[i] = (arr[i - sftdev] << sftrem) | (arr[i - sftdev - 1] >> (UIntUtil.UInt32Bits - sftrem));
            }

            Array.Copy(arr_sft, 0, arr, 0, Length);
        }

        internal void RightShift(uint sft) {
            if (sft >= Bits) {
                Zeroset();
                return;
            }

            int sftdev = (int)sft / UIntUtil.UInt32Bits;
            int sftrem = (int)sft % UIntUtil.UInt32Bits;

            if (sftrem == 0) {
                RightShiftArray((uint)sftdev);
                return;
            }

            UInt32[] arr_sft = new UInt32[Length];
    
            int i = sftdev;
            for (; i < Length - 1; i++) {
                arr_sft[i - sftdev] = (arr[i] >> sftrem) | (arr[i + 1] << (UIntUtil.UInt32Bits - sftrem));
            }
            if (i - sftdev >= 0) {
                arr_sft[i - sftdev] = arr[i] >> sftrem;
            }

            Array.Copy(arr_sft, 0, arr, 0, Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LeftShiftArray(uint sft) {
            for (int i = Math.Min(Length, Length - (int)sft) - 1; i >= 0; i--) {
                arr[i + sft] = arr[i];
            }
            for (uint i = 0; i < Math.Min(sft, Length); i++) {
                arr[i] = 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RightShiftArray(uint sft) {
            for (uint i = sft; i < Length; i++) {
                arr[i - sft] = arr[i];
            }
            for (int i = Math.Max(0, Length - (int)sft); i < Length; i++) {
                arr[i] = 0;
            }
        }

        public static Mantissa<N> LeftShift(Mantissa<N> n, uint sft) {
            Mantissa<N> ret = n.Copy();

            ret.LeftShift(sft);

            return ret;
        }

        public static Mantissa<N> RightShift(Mantissa<N> n, uint sft) {
            Mantissa<N> ret = n.Copy();

            ret.RightShift(sft);

            return ret;
        }

        public static Mantissa<N> LeftShiftArray(Mantissa<N> n, uint sft) {
            Mantissa<N> ret = n.Copy();

            ret.LeftShiftArray(sft);

            return ret;
        }        

        public static Mantissa<N> RightShiftArray(Mantissa<N> n, uint sft) {
            Mantissa<N> ret = n.Copy();

            ret.RightShiftArray(sft);

            return ret;
        }
    }
}
