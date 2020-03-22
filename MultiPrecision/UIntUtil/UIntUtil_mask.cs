using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        private static class Mask {
            private static readonly Vector256<UInt32>[] lstable, mstable;

            public const uint MM256UInt32s = 8;

            static unsafe Mask() {
                lstable = new Vector256<UInt32>[MM256UInt32s];
                mstable = new Vector256<UInt32>[MM256UInt32s];

                UInt32[] value = new UInt32[15] { ~0u, ~0u, ~0u, ~0u, ~0u, ~0u, ~0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u };

                fixed (UInt32* v = value) {
                    for (int i = 0; i < lstable.Length; i++) {
                        lstable[i] = Avx.LoadVector256(v + (MM256UInt32s - 1 - i));
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint UInt32Sets(uint length) {
                return length / MM256UInt32s * MM256UInt32s;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint UInt32Rems(uint length) {
                return length % MM256UInt32s;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector256<UInt32> LSV(uint count) {
                return Mask.lstable[count];
            }
        }
    }
}
