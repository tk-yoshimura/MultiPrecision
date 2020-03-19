using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void CarryAdd(int dig, UInt32 v) {

#if DEBUG
            if (dig < 0) {
                throw new ArgumentException();
            }
#endif
            
            fixed(UInt32 *arr = Value) { 
                for (int i = dig; i < Length && v > 0; i++) {
                    (v, arr[i]) = Unpack((UInt64)arr[i] + (UInt64)v);
                }

                if (v > 0) {
                    throw new OverflowException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void CarrySub(int dig, UInt32 v) {

#if DEBUG
            if (dig < 0) {
                throw new ArgumentException();
            }
#endif
                
            fixed(UInt32 *arr = Value) { 
                for (int i = dig; i < Length && v > 0; i++) {
                    if (arr[i] >= v) {
                        arr[i] -= v;
                        v = 0;
                    }
                    else {
                        arr[i] = unchecked((~v) + arr[i] + 1);
                        v = 1;
                    }
                }
            }

            if (v > 0) {
                throw new OverflowException();
            }
        }
    }
}
