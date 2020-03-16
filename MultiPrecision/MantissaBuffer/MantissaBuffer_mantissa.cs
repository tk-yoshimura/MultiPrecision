using System;

namespace MultiPrecision {
    internal sealed partial class MantissaBuffer<N> {

        public (Mantissa<N> n, int sft) MantissaShift{
            get {
                if (IsZero) {
                    return (Mantissa<N>.Zero, 0);
                }

                Mantissa<N> n = new Mantissa<N>();
                MantissaBuffer<N> n_sft = Copy();

                uint lzc = LeadingZeroCount;
                int sft = (int)lzc - Mantissa<N>.Bits;

                if (sft > 0) {
                    n_sft.LeftShift((uint)sft);
                }
                else if (sft < 0) {
                    n_sft.RightShift((uint)(-sft));
                }

                Array.Copy(n_sft.arr, 0, n.arr, 0, Mantissa<N>.Length);

                return (n, sft);
            }
        }
    }
}
