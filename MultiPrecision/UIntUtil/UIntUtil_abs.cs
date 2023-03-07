using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Abs(Int64 x) {
            return (x >= 0) ? unchecked((UInt64)x) : ~unchecked((UInt64)x) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sign Sign(Int64 x) {
            return (x >= 0) ? MultiPrecision.Sign.Plus : MultiPrecision.Sign.Minus;
        }
    }
}
