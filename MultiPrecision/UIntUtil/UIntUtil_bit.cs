﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMSB([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            UInt32 mask = 1u << (UInt32Bits - posrem - 1);

            v[v.Length - posdev - 1] |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetMSB([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            UInt32 mask = 1u << (UInt32Bits - posrem - 1);

            v[v.Length - posdev - 1] &= ~mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 GetMSB([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            return (v[v.Length - posdev - 1] >> (UInt32Bits - posrem - 1)) & 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FlushMSB([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            int mask_index = posdev;

            v[mask_index] = (v[mask_index] << (UInt32Bits - posrem - 1)) >> (UInt32Bits - posrem - 1);

            Zeroset(v, (uint)(mask_index + 1), (uint)(v.Length - mask_index - 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLSB([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            UInt32 mask = 1u << posrem;

            v[posdev] |= mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetLSB([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            UInt32 mask = 1u << posrem;

            v[posdev] &= ~mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 GetLSB([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            return (v[posdev] >> (posrem)) & 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FlushLSB([DisallowNull] UInt32[] v, int pos) {
            int posdev = pos / UInt32Bits;
            int posrem = pos % UInt32Bits;

            int mask_index = v.Length - posdev - 1;

            v[mask_index] = (v[mask_index] >> (UInt32Bits - posrem - 1)) << (UInt32Bits - posrem - 1);

            Zeroset(v, 0, (uint)mask_index);
        }

        public static UInt32[] Random(Random random, int length, int bits) { 
            UInt32[] value = (new UInt32[length]).Select((_, idx) => idx < bits / UIntUtil.UInt32Bits ? (UInt32)random.NextUInt32() : 0u).ToArray();
            
            if(bits % UIntUtil.UInt32Bits > 0) { 
                value[bits / UIntUtil.UInt32Bits] = (UInt32)random.NextUInt32() >> (UIntUtil.UInt32Bits - bits % UIntUtil.UInt32Bits);
            }

            return value;
        }

        public static UInt32 NextUInt32(this Random random) { 
            byte[] vs = new byte[sizeof(UInt32)];

            random.NextBytes(vs);

            return (UInt32)vs[0] | ((UInt32)vs[1] << 8) | ((UInt32)vs[2] << 16) | ((UInt32)vs[3] << 24);
        }
    }
}
