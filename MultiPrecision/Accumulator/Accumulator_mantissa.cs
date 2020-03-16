using System;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public (Mantissa<N> n, int sft) MantissaShift{
            get {
                const UInt32 round = UInt32.MaxValue / 2;

                if (IsZero) {
                    return (Mantissa<N>.Zero, 0);
                }

                Mantissa<N> n = new Mantissa<N>();
                Accumulator<N> n_sft = Copy();

                uint lzc = LeadingZeroCount;
                n_sft.LeftShift(lzc);
                
                int sft = (int)lzc - Mantissa<N>.Bits;

                if(n_sft.arr[Mantissa<N>.Length - 1] > round) { 
                    int i = Mantissa<N>.Length;
                    for(; i < Accumulator<N>.Length; i++) { 
                        if(n_sft.arr[i] < UInt32.MaxValue) {
                            n_sft.CarryAdd((uint)Mantissa<N>.Length, 1);
                            break;
                        }
                    }
                    if(i == Accumulator<N>.Length) { 
                       return (Mantissa<N>.One, sft - 1); 
                    }
                }

                Array.Copy(n_sft.arr, Mantissa<N>.Length, n.arr, 0, Mantissa<N>.Length);

                return (n, sft);
            }
        }
    }
}
