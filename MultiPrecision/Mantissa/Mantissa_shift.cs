using System;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> operator<<(Mantissa<N> a, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return LeftShift(a, sft);
        }

        public static Mantissa<N> operator>>(Mantissa<N> a, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return RightShift(a, sft);
        }

        internal void LeftShift(int sft) {
            UIntUtil.LeftShift(arr, sft);
        }

        internal void RightShift(int sft) {
            UIntUtil.RightShift(arr, sft);
        }

        internal void LeftBlockShift(int sft) {
            UIntUtil.LeftBlockShift(arr, sft);
        }

        internal void RightBlockShift(int sft) {
            UIntUtil.RightBlockShift(arr, sft);
        }

        public static Mantissa<N> LeftShift(Mantissa<N> n, int sft) {
            Mantissa<N> ret = n.Copy();

            ret.LeftShift(sft);

            return ret;
        }

        public static Mantissa<N> RightShift(Mantissa<N> n, int sft) {
            Mantissa<N> ret = n.Copy();

            ret.RightShift(sft);

            return ret;
        }

        public static Mantissa<N> LeftBlockShift(Mantissa<N> n, int sft) {
            Mantissa<N> ret = n.Copy();

            ret.LeftBlockShift(sft);

            return ret;
        }        

        public static Mantissa<N> RightBlockShift(Mantissa<N> n, int sft) {
            Mantissa<N> ret = n.Copy();

            ret.RightBlockShift(sft);

            return ret;
        }
    }
}
