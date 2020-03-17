using System;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static Accumulator<N> operator<<(Accumulator<N> a, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return LeftShift(a, sft);
        }

        public static Accumulator<N> operator>>(Accumulator<N> a, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            return RightShift(a, sft);
        }

        public static Accumulator<N> LeftShift(Accumulator<N> n, int sft) {
            Accumulator<N> ret = n.Copy();

            ret.LeftShift(sft);

            return ret;
        }

        public static Accumulator<N> RightShift(Accumulator<N> n, int sft) {
            Accumulator<N> ret = n.Copy();

            ret.RightShift(sft);

            return ret;
        }

        public static Accumulator<N> LeftBlockShift(Accumulator<N> n, int sft) {
            Accumulator<N> ret = n.Copy();

            ret.LeftBlockShift(sft);

            return ret;
        }        

        public static Accumulator<N> RightBlockShift(Accumulator<N> n, int sft) {
            Accumulator<N> ret = n.Copy();

            ret.RightBlockShift(sft);

            return ret;
        }

        private void LeftShift(int sft) {
            UIntUtil.LeftShift(arr, sft);
        }

        private void RightShift(int sft) {
            UIntUtil.RightShift(arr, sft);
        }

        private void LeftBlockShift(int sft) {
            UIntUtil.LeftBlockShift(arr, sft);
        }

        private void RightBlockShift(int sft) {
            UIntUtil.RightBlockShift(arr, sft);
        }
    }
}
