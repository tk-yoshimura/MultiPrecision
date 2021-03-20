using System;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public (Mantissa<N> n, int sft) Mantissa {
            get {
                if (IsZero) {
                    return (Mantissa<N>.Zero, 0);
                }

                int lzc = LeadingZeroCount;

                Accumulator<N> n_sft = LeftShift(this, lzc);

                if (n_sft.value[Mantissa<N>.Length - 1] <= UIntUtil.UInt32Round) {
                    Mantissa<N> n = new(n_sft);
                    return (n, lzc);
                }
                for (int i = Mantissa<N>.Length; i < Accumulator<N>.Length; i++) {
                    if (n_sft.value[i] < UInt32.MaxValue) {
                        Mantissa<N> n = new(n_sft);
                        return (n, lzc);
                    }
                }
                return (Mantissa<N>.One, lzc - 1);
            }
        }
    }
}
