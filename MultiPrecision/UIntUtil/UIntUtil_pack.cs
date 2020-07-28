using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (UInt32 high, UInt32 low) DecimalUnpack(UInt64 v) {

            UInt32 high, low;

#if DEBUG
            checked
#else
            unchecked  
#endif
            {
                high = (UInt32)(v / UInt32MaxDecimal);
                low = (UInt32)(v - (UInt64)high * (UInt64)UInt32MaxDecimal);
            }

            return (high, low);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (UInt32 high, UInt32 low) DecimalUnpack(UInt32 v) {
            UInt32 high = v / UInt32MaxDecimal;
            UInt32 low = v - high * UInt32MaxDecimal;

            return (high, low);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 DecimalPack(UInt32 high, UInt32 low) {
            return (UInt64)high * (UInt64)UInt32MaxDecimal + (UInt64)low;
        }
    }
}
