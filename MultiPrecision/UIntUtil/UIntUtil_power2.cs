using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPower2(UInt32 value) {
            return (value >= 1u) && ((value & (value - 1u)) == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPower2(UInt64 value) {
            return (value >= 1uL) && ((value & (value - 1uL)) == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Power2(UInt32 value) {
            return UInt32Bits - 1 - (int)LeadingZeroCount(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Power2(UInt64 value) {
            return UInt64Bits - 1 - (int)LeadingZeroCount(value);
        }
    }
}
