using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        public const uint MM256UInt32s = 8;

        internal static class Mask256 {
            static Mask256() {
                Trace.Assert(MM256UInt32s == (uint)Vector256<UInt32>.Count);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector256<UInt32> Lower(uint count) {
                return Vector256.Create(
                    (count > 0u) ? ~0u : 0u, (count > 1u) ? ~0u : 0u, (count > 2u) ? ~0u : 0u, (count > 3u) ? ~0u : 0u,
                    (count > 4u) ? ~0u : 0u, (count > 5u) ? ~0u : 0u, (count > 6u) ? ~0u : 0u, (count > 7u) ? ~0u : 0u
                );
            }
        }
    }
}
