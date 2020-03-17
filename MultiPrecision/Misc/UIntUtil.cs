using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static class UIntUtil {
        public const int UInt32Bits = sizeof(UInt32) * 8;
        public const int UInt64Bits = sizeof(UInt64) * 8;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Zeroset([DisallowNull] UInt32[] v) {
            for (int i = 0; i < v.Length; i++) {
                v[i] = 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero([DisallowNull] UInt32[] v) {
            for (int i = 0; i < v.Length; i++) {
                if (v[i] != 0) {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFull([DisallowNull] UInt32[] v) {
            for (int i = 0; i < v.Length; i++) {
                if (v[i] != UInt32.MaxValue) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>Comparate uint32 array a == b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equal(int length, [DisallowNull] UInt32[] a, [DisallowNull] UInt32[] b) {
            for (int i = 0; i < length; i++) {
                if (a[i] != b[i]) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>Comparate uint32 array a &lt;= b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqual(int length, [DisallowNull] UInt32[] a, [DisallowNull] UInt32[] b) {
            for (int i = length - 1; i >= 0; i--) {
                if (a[i] > b[i]) {
                    return false;
                }
                else if (a[i] < b[i]) {
                    return true;
                }
            }

            return true;
        }

        /// <summary>Comparate uint32 array a &gt;= b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqual(int length, [DisallowNull] UInt32[] a, [DisallowNull] UInt32[] b) {
            for (int i = length - 1; i >= 0; i--) {
                if (a[i] < b[i]) {
                    return false;
                }
                else if (a[i] > b[i]) {
                    return true;
                }
            }

            return true;
        }

        /// <summary>Count leading zero uint32 array</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeadingZeroCount([DisallowNull] UInt32[] v) {
            uint cnt = 0;

            for (int i = v.Length - 1; i >= 0; i--) {
                if (v[i] == 0) {
                    cnt += UInt32Bits;
                }
                else {
                    cnt += Lzcnt.LeadingZeroCount(v[i]);
                    break;
                }
            }

            return checked((int)cnt);
        }

        /// <summary>Count digits uint32 elements</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Digits([DisallowNull] UInt32[] v) {
            for (int i = v.Length - 1; i >= 0; i--) {
                if (v[i] != 0) {
                    return i + 1;
                }
            }
        
            return 1;
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LeftBlockShift([DisallowNull] UInt32[] v, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            int length = v.Length;

            for (int i = Math.Min(length, length - sft) - 1; i >= 0; i--) {
                v[i + sft] = v[i];
            }
            for (int i = 0; i < Math.Min(sft, length); i++) {
                v[i] = 0;
            }
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RightBlockShift([DisallowNull] UInt32[] v, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            int length = v.Length;

            for (int i = sft; i < length; i++) {
                v[i - sft] = v[i];
            }
            for (int i = Math.Max(0, length - sft); i < length; i++) {
                v[i] = 0;
            }
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft</summary>
        public static void LeftShift([DisallowNull] UInt32[] v, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            int length = v.Length;

            if (sft >= length * UInt32Bits) {
                Zeroset(v);
                return;
            }

            int sftdev = sft / UInt32Bits;
            int sftrem = sft % UInt32Bits;

            if (sftrem == 0) {
                LeftBlockShift(v, sftdev);
                return;
            }

            UInt32[] v_sft = new UInt32[length];

            v_sft[sftdev] = v[0] << sftrem;
            for (int i = sftdev + 1; i < length; i++) {
                v_sft[i] = (v[i - sftdev] << sftrem) | (v[i - sftdev - 1] >> (UInt32Bits - sftrem));
            }

            Array.Copy(v_sft, 0, v, 0, length);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft</summary>
        public static void RightShift([DisallowNull] UInt32[] v, int sft) {
            if (sft < 0) {
                throw new ArgumentException(nameof(sft));
            }

            int length = v.Length;

            if (sft >= length * UInt32Bits) {
                Zeroset(v);
                return;
            }

            int sftdev = sft / UInt32Bits;
            int sftrem = sft % UInt32Bits;

            if (sftrem == 0) {
                RightBlockShift(v, sftdev);
                return;
            }

            UInt32[] v_sft = new UInt32[length];
    
            int i = sftdev;
            for (; i < length - 1; i++) {
                v_sft[i - sftdev] = (v[i] >> sftrem) | (v[i + 1] << (UInt32Bits - sftrem));
            }
            if (i - sftdev >= 0) {
                v_sft[i - sftdev] = v[i] >> sftrem;
            }

            Array.Copy(v_sft, 0, v, 0, length);
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
