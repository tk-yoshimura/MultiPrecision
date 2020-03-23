using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        /// <summary>Comparate uint32 array a == b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static bool Equal(int length, [DisallowNull] UInt32[] a, [DisallowNull] UInt32[] b) {

#if DEBUG
            Debug<ArgumentException>.Assert(length == a.Length);
            Debug<ArgumentException>.Assert(length == b.Length);
#endif

            fixed (UInt32* va = a, vb = b) {
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

#if DEBUG
            Debug<ArgumentException>.Assert(length == a.Length);
            Debug<ArgumentException>.Assert(length == b.Length);
#endif

            fixed (UInt32* va = a, vb = b) {
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

#if DEBUG
            Debug<ArgumentException>.Assert(length == a.Length);
            Debug<ArgumentException>.Assert(length == b.Length);
#endif

            fixed (UInt32* va = a, vb = b) {
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
        public static unsafe bool IsZero([DisallowNull] UInt32[] value) {
            fixed (UInt32* v = value) {
                for (int i = 0; i < value.Length; i++) {
                    if (v[i] != 0) {
                        return false;
                    }
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsFull([DisallowNull] UInt32[] value) {
            fixed (UInt32* v = value) {
                for (int i = 0; i < value.Length; i++) {
                    if ((~v[i]) != 0) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
