using System;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        /// <summary>Comparate uint32 array a == b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool Equal(int length, UInt32[] a, UInt32[] b) {

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
        public static unsafe bool LessThanOrEqual(int length, UInt32[] a, UInt32[] b) {

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
        public static unsafe bool GreaterThanOrEqual(int length, UInt32[] a, UInt32[] b) {

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
        public static unsafe bool IsZero(UInt32[] value) {
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
        public static unsafe bool IsFull(UInt32[] value) {
            fixed (UInt32* v = value) {
                for (int i = 0; i < value.Length; i++) {
                    if ((~v[i]) != 0) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>Count leading match bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int MatchBits(int length, UInt32[] a, UInt32[] b) {

#if DEBUG
            Debug<ArgumentException>.Assert(length == a.Length);
            Debug<ArgumentException>.Assert(length == b.Length);
#endif
            int matches = 0;

            fixed (UInt32* va = a, vb = b) {
                for (int i = length - 1; i >= 0; i--) {
                    if (a[i] == b[i]) {
                        matches += UInt32Bits;
                    }
                    else {
                        if (a[i] >= b[i]) {
                            matches += LeadingZeroCount(a[i] - b[i]);
                        }
                        else {
                            matches += LeadingZeroCount(b[i] - a[i]);
                        }

                        break;
                    }
                }
            }

            return matches;
        }
    }
}
