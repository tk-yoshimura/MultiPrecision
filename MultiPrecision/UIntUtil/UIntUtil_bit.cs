using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

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

            Zeroset(v, 0, (uint)mask_index);
        }
    }
}
