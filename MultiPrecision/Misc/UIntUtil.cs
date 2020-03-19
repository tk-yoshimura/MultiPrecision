﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static class UIntUtil {
        public const int UInt32Bits = sizeof(UInt32) * 8;
        public const int UInt64Bits = sizeof(UInt64) * 8;
        public const int UInt32MaxDecimalDigits = UInt32Bits * 30103 / 100000;
        public const int UInt64MaxDecimalDigits = UInt64Bits * 30103 / 100000;
        public const UInt32 UInt32MaxDecimal = 1000000000u;
        public const UInt64 UInt64MaxDecimal = 10000000000000000000ul;

        public const UInt32 UInt32Round = UInt32.MaxValue >> 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (UInt32 high, UInt32 low) Unpack(UInt64 v) {
            UInt32 low = unchecked((UInt32)v);
            UInt32 high = unchecked((UInt32)(v >> UInt32Bits));

            return (high, low);
        } 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Pack(UInt32 high, UInt32 low) {            
            return (((UInt64)high) << UInt32Bits) | ((UInt64)low);
        }

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
