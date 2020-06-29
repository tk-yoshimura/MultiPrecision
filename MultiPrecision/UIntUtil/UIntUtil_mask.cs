using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        internal static class Mask256 {
            private static readonly Vector256<UInt32>[] lstable, mstable;

            public static readonly uint MM256UInt32s = (uint)Vector256<UInt32>.Count;

            static unsafe Mask256() {
                lstable = new Vector256<UInt32>[MM256UInt32s];
                mstable = new Vector256<UInt32>[MM256UInt32s];

                UInt32[] value = new UInt32[15] { ~0u, ~0u, ~0u, ~0u, ~0u, ~0u, ~0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u };

                fixed (UInt32* v = value) {
                    for (int i = 0; i < lstable.Length; i++) {
                        lstable[i] = Avx.LoadVector256(v + (MM256UInt32s - 1 - i));
                        mstable[i] = Avx2.Xor(Vector256.Create(~0u), lstable[i]);
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
                return lstable[count];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector256<UInt32> MSV(uint count) {
                return mstable[count];
            }
        }

        internal static class Mask128 {
            private static readonly Vector128<UInt32>[] lstable, mstable;

            public static readonly uint MM128UInt32s = (uint)Vector128<UInt32>.Count;

            static unsafe Mask128() {
                lstable = new Vector128<UInt32>[MM128UInt32s];
                mstable = new Vector128<UInt32>[MM128UInt32s];

                UInt32[] value = new UInt32[7] { ~0u, ~0u, ~0u, 0u, 0u, 0u, 0u };

                fixed (UInt32* v = value) {
                    for (int i = 0; i < lstable.Length; i++) {
                        lstable[i] = Avx.LoadVector128(v + (MM128UInt32s - 1 - i));
                        mstable[i] = Avx2.Xor(Vector128.Create(~0u), lstable[i]);
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint UInt32Sets(uint length) {
                return length / MM128UInt32s * MM128UInt32s;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint UInt32Rems(uint length) {
                return length % MM128UInt32s;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector128<UInt32> LSV(uint count) {
                return lstable[count];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector128<UInt32> MSV(uint count) {
                return mstable[count];
            }
        }
    }
}
