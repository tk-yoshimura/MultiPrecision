using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void CarryAdd(int dig, UInt32 v) {

#if DEBUG
            Debug.Assert(dig >= 0);
#endif

            fixed (UInt32* arr = value) {
                for (int i = dig; i < Length && v > 0; i++) {
                    (v, arr[i]) = UIntUtil.Unpack(unchecked((UInt64)arr[i] + (UInt64)v));
                }

                if (v > 0) {
                    throw new OverflowException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void CarrySub(int dig, UInt32 v) {

#if DEBUG
            Debug.Assert(dig >= 0);
#endif

            fixed (UInt32* arr = value) {
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
