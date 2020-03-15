using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static class UIntUtil {
        public const int UInt32Bits = sizeof(float) * 8;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (UInt32 high, UInt32 low) Unpack(UInt64 v) {
            UInt32 low = unchecked((UInt32)v);
            UInt32 high = unchecked((UInt32)(v >> UInt32Bits));

            return (high, low);
        } 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Pack(UInt32 high, UInt32 low) {            
            return (((UInt64)high) << UInt32Bits) | ((UInt64)low);
        } 
    }
}
