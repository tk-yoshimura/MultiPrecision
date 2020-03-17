using System;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public (Mantissa<N> n, int sft) Mantissa {
            get {
                const UInt32 round = UInt32.MaxValue / 2;

                if (IsZero) {
                    return (Mantissa<N>.Zero, 0);
                }

                int lzc = LeadingZeroCount;

                Accumulator<N> n_sft = LeftShift(this, lzc);

                if (n_sft.arr[Mantissa<N>.Length - 1] > round) {
                    int i = Mantissa<N>.Length;
                    for (; i < Accumulator<N>.Length; i++) {
                        if (n_sft.arr[i] < UInt32.MaxValue) {
                            n_sft.CarryAdd(Mantissa<N>.Length, 1);
                            break;
                        }
                    }
                    if (i == Accumulator<N>.Length) {
                        return (Mantissa<N>.One, lzc - 1);
                    }
                }

                Mantissa<N> n = new Mantissa<N>(n_sft.arr, Mantissa<N>.Length);

                return (n, lzc);
            }
        }
    }
}
