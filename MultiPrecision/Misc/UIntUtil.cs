using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static class UIntUtil {
        public const int UInt32Bits = sizeof(UInt32) * 8;
        public const int UInt64Bits = sizeof(UInt64) * 8;

        /// <summary>Comparate uint32 array a == b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static bool Equal(int length, [DisallowNull] UInt32[] a, [DisallowNull] UInt32[] b) {

            fixed(UInt32* va = a, vb = b) { 
                for (int i = 0; i < length; i++) {
                    if (va[i] != vb[i]) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>Comparate uint32 array a &lt;= b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static bool LessThanOrEqual(int length, [DisallowNull] UInt32[] a, [DisallowNull] UInt32[] b) {

            fixed(UInt32* va = a, vb = b) { 
                for (int i = length - 1; i >= 0; i--) {
                    if (va[i] > vb[i]) {
                        return false;
                    }
                    else if (va[i] < vb[i]) {
                        return true;
                    }
                }
            }

            return true;
        }

        /// <summary>Comparate uint32 array a &gt;= b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static bool GreaterThanOrEqual(int length, [DisallowNull] UInt32[] a, [DisallowNull] UInt32[] b) {

            fixed(UInt32* va = a, vb = b) { 
                for (int i = length - 1; i >= 0; i--) {
                    if (va[i] < vb[i]) {
                        return false;
                    }
                    else if (va[i] > vb[i]) {
                        return true;
                    }
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBit([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            UInt32 mask = 1u << (UInt32Bits - posrem - 1);

            v[v.Length - posdev - 1] |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetBit([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            UInt32 mask = 1u << (UInt32Bits - posrem - 1);

            v[v.Length - posdev - 1] &= ~mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 GetBit([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            return (v[v.Length - posdev - 1] >> (UInt32Bits - posrem - 1)) & 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FlushBit([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            int mask_index = v.Length - posdev - 1;

            v[mask_index] = (v[mask_index] >> (UInt32Bits - posrem - 1)) << (UInt32Bits - posrem - 1);

            for(int i = 0; i < mask_index; i++) { 
                v[i] = 0u;
            }
        }
    }
}
