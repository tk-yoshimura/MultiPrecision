using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void CarryAdd(uint dig, UInt32 v) {
                
            for (uint i = dig; i < Length && v > 0; i++) {
                (v, arr[i]) = UIntUtil.Unpack((UInt64)arr[i] + (UInt64)v);
            }

            if (v > 0) {
                throw new OverflowException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void CarrySub(uint dig, UInt32 v) {
                
            for (uint i = dig; i < Length && v > 0; i++) {
                if (arr[i] >= v) {
                    arr[i] -= v;
                    v = 0;
                }
                else {
                    arr[i] = unchecked((~v) + arr[i] + 1);
                    v = 1;
                }
            }

            if (v > 0) {
                throw new OverflowException();
            }
        }
    }
}
