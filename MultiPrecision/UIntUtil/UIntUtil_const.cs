using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        static UIntUtil() {
            if (!Avx2.IsSupported) {
                throw new PlatformNotSupportedException("This platform is not supported avx2 operations.");
            }
        }

        public const int UInt32Bits = sizeof(UInt32) * 8;
        public const int UInt64Bits = sizeof(UInt64) * 8;
        public const int UInt32MaxDecimalDigits = UInt32Bits * 30103 / 100000;
        public const int UInt64MaxDecimalDigits = UInt64Bits * 30103 / 100000;
        public const UInt32 UInt32MaxDecimal = 1000000000u;
        public const UInt64 UInt64MaxDecimal = 10000000000000000000ul;

        public const UInt32 UInt32Round = UInt32.MaxValue >> 1;

        public const int ShiftIDX1 = 8;
        public const int ShiftIDX2 = 16;
        public const int ShiftIDX3 = 24;
        public const int ShiftIDX4 = 32;
    }
}
