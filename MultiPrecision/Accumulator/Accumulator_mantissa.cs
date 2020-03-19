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

                if (n_sft.value[Mantissa<N>.Length - 1] > UIntUtil.UInt32Round) {
                    int i = Mantissa<N>.Length;
                    for (; i < Accumulator<N>.Length; i++) {
                        if (n_sft.value[i] < UInt32.MaxValue) {
                            n_sft.value.CarryAdd(Mantissa<N>.Length, 1);
                            break;
                        }
                    }
                    if (i == Accumulator<N>.Length) {
                        return (Mantissa<N>.One, lzc - 1);
                    }
                }

                Mantissa<N> n = new Mantissa<N>(n_sft.value.Value, Mantissa<N>.Length);

                return (n, lzc);
            }
        }
    }
}
