using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPower2(UInt32 value) {
            return (value >= 1) && ((value & (value - 1)) == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPower2(UInt64 value) {
            return (value >= 1) && ((value & (value - 1)) == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Power2(UInt32 value) {
            return UIntUtil.UInt32Bits - LeadingZeroCount(value) - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Power2(UInt64 value) {
            return UIntUtil.UInt64Bits - LeadingZeroCount(value) - 1;
        }
    }
}
