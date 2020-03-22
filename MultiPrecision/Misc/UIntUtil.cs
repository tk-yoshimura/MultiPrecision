using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

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

#if DEBUG
            Debug.Assert(length == a.Length);
            Debug.Assert(length == b.Length);
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
            Debug.Assert(length == a.Length);
            Debug.Assert(length == b.Length);
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
            Debug.Assert(length == a.Length);
            Debug.Assert(length == b.Length);
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

        /// <summary>Shift uint32 array v &lt;&lt;= sft</summary>
        public static unsafe void LeftShift([DisallowNull] UInt32[] value, int sft) {

#if DEBUG
            Debug.Assert(sft >= 0);
#endif

            if (sft > LeadingZeroCount(value)) {
                throw new OverflowException();
            }

            int sftdev = sft / UIntUtil.UInt32Bits;
            int sftrem = sft % UIntUtil.UInt32Bits;

            if (sftrem == 0) {
                LeftBlockShift(value, sftdev);
                return;
            }

            uint count = (uint)(value.Length - sftdev - 1);
            uint count_sets = Mask.UInt32Sets(count), count_rems = Mask.UInt32Rems(count);

            fixed (UInt32* v = value) {
                if (count_rems > 0) {
                    Vector256<UInt32> mask = Mask.LSV(count_rems);
                    Vector256<UInt32> ls = Avx2.ShiftRightLogical(Avx2.MaskLoad(v + count_sets, mask), (byte)(UInt32Bits - sftrem));
                    Vector256<UInt32> ms = Avx2.ShiftLeftLogical(Avx2.MaskLoad(v + count_sets + 1, mask), (byte)sftrem);

                    Avx2.MaskStore(v + count_sets + sftdev + 1, mask, Avx2.Or(ls, ms));

#if DEBUG
                    Debug.Assert(checked(count_sets + sftdev + 1 + count_rems) <= value.Length);
#endif
                }
                for (uint i = count_sets; i >= Mask.MM256UInt32s; i -= Mask.MM256UInt32s) {
                    Vector256<UInt32> ls = Avx2.ShiftRightLogical(Avx2.LoadVector256(v + i - Mask.MM256UInt32s), (byte)(UInt32Bits - sftrem));
                    Vector256<UInt32> ms = Avx2.ShiftLeftLogical(Avx2.LoadVector256(v + i + 1 - Mask.MM256UInt32s), (byte)sftrem);

                    Avx.Store(v + i + sftdev + 1 - Mask.MM256UInt32s, Avx2.Or(ls, ms));

#if DEBUG
                    Debug.Assert(checked(i + sftdev + 1) <= value.Length);
#endif
                }

                v[sftdev] = v[0] << sftrem;

#if DEBUG
                Debug.Assert(sftdev < value.Length);
#endif
            }

            Zeroset(value, 0, (uint)sftdev);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft</summary>
        public static unsafe void RightShift([DisallowNull] UInt32[] value, int sft) {

#if DEBUG
            Debug.Assert(sft >= 0);
#endif

            int sftdev = sft / UIntUtil.UInt32Bits;
            int sftrem = sft % UIntUtil.UInt32Bits;

            if (sftrem == 0 || sftdev >= value.Length) {
                RightBlockShift(value, sftdev);
                return;
            }

            uint count = (uint)(value.Length - sftdev - 1);
            uint count_sets = Mask.UInt32Sets(count), count_rems = Mask.UInt32Rems(count);

            fixed (UInt32* v = value) {
                for (uint i = 0; i < count_sets; i += Mask.MM256UInt32s) {
                    Vector256<UInt32> ls = Avx2.ShiftRightLogical(Avx2.LoadVector256(v + i + sftdev), (byte)sftrem);
                    Vector256<UInt32> ms = Avx2.ShiftLeftLogical(Avx2.LoadVector256(v + i + sftdev + 1), (byte)(UInt32Bits - sftrem));

                    Avx.Store(v + i, Avx2.Or(ls, ms));

#if DEBUG
                    Debug.Assert(checked(i + sftdev + 1 + Mask.MM256UInt32s) <= value.Length);
#endif
                }
                if (count_rems > 0) {
                    Vector256<UInt32> mask = Mask.LSV(count_rems);
                    Vector256<UInt32> ls = Avx2.ShiftRightLogical(Avx2.MaskLoad(v + count_sets + sftdev, mask), (byte)sftrem);
                    Vector256<UInt32> ms = Avx2.ShiftLeftLogical(Avx2.MaskLoad(v + count_sets + sftdev + 1, mask), (byte)(UInt32Bits - sftrem));

                    Avx2.MaskStore(v + count_sets, mask, Avx2.Or(ls, ms));

#if DEBUG
                    Debug.Assert(checked(count_sets + sftdev + 1 + count_rems) <= value.Length);
#endif
                }

                v[count] = v[value.Length - 1] >> sftrem;

#if DEBUG
                Debug.Assert(count < value.Length);
#endif
            }

            Zeroset(value, count + 1, (uint)sftdev);
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void LeftBlockShift([DisallowNull] UInt32[] value, int sft) {

#if DEBUG
            Debug.Assert(sft >= 0);
#endif

            if (checked(sft + Digits(value)) > value.Length) {
                throw new OverflowException();
            }

            uint count = (uint)(value.Length - sft);
            uint count_sets = Mask.UInt32Sets(count), count_rems = Mask.UInt32Rems(count);

            fixed (UInt32* v = value) {
                if (count_rems > 0) {
                    Vector256<UInt32> mask = Mask.LSV(count_rems);
                    Avx2.MaskStore(v + count_sets + sft, mask, Avx2.MaskLoad(v + count_sets, mask));

#if DEBUG
                    Debug.Assert(checked(count_sets + sft + count_rems) <= value.Length);
#endif
                }
                for (uint i = count_sets; i >= Mask.MM256UInt32s; i -= Mask.MM256UInt32s) {
                    Avx.Store(v + i + sft - Mask.MM256UInt32s, Avx2.LoadVector256(v + i - Mask.MM256UInt32s));

#if DEBUG
                    Debug.Assert(checked(i + sft) <= value.Length);
#endif
                }
            }

            Zeroset(value, 0, (uint)sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft * UInt32Bits</summary>
        public static unsafe void RightBlockShift([DisallowNull] UInt32[] value, int sft) {

#if DEBUG
            Debug.Assert(sft >= 0);
#endif

            if (sft >= value.Length) {
                Zeroset(value);
                return;
            }

            uint count = (uint)(value.Length - sft);
            uint count_sets = Mask.UInt32Sets(count), count_rems = Mask.UInt32Rems(count);

            fixed (UInt32* v = value) {
                for (uint i = 0; i < count_sets; i += Mask.MM256UInt32s) {
                    Avx.Store(v + i, Avx2.LoadVector256(v + i + sft));

#if DEBUG
                    Debug.Assert(checked(i + sft + Mask.MM256UInt32s) <= value.Length);
#endif
                }
                if (count_rems > 0) {
                    Vector256<UInt32> mask = Mask.LSV(count_rems);
                    Avx2.MaskStore(v + count_sets, mask, Avx2.MaskLoad(v + count_sets + sft, mask));

#if DEBUG
                    Debug.Assert(checked(count_sets + sft + count_rems) <= value.Length);
#endif
                }
            }

            Zeroset(value, count, (uint)sft);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static int LeadingZeroCount([DisallowNull] UInt32[] value) {
            uint cnt = 0;

            fixed (UInt32* v = value) {
                for (int i = value.Length - 1; i >= 0; i--) {
                    if (v[i] == 0) {
                        cnt += UIntUtil.UInt32Bits;
                    }
                    else {
                        cnt += Lzcnt.LeadingZeroCount(v[i]);
                        break;
                    }
                }
            }

            return checked((int)cnt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void Zeroset([DisallowNull] UInt32[] value, uint index, uint length) {
            if (length <= 0) {
                return;
            }

#if DEBUG
            Debug.Assert(checked(index + length) <= value.Length);
#endif

            uint length_sets = Mask.UInt32Sets(length), length_rems = Mask.UInt32Rems(length);

            fixed (UInt32* v = value) {
                for (uint i = 0; i < length_sets; i += Mask.MM256UInt32s) {
                    Avx.Store(v + i + index, Vector256<UInt32>.Zero);

#if DEBUG
                    Debug.Assert(checked(i + index + Mask.MM256UInt32s) <= value.Length);
#endif
                }
                if (length_rems > 0) {
                    Avx2.MaskStore(v + length_sets + index, Mask.LSV(length_rems), Vector256<UInt32>.Zero);

#if DEBUG
                    Debug.Assert(checked(length_sets + index + length_rems) <= value.Length);
#endif
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void Zeroset([DisallowNull] UInt32[] value) {
            uint length = (uint)value.Length;

            uint length_sets = Mask.UInt32Sets(length), length_rems = Mask.UInt32Rems(length);

            fixed (UInt32* v = value) {
                for (uint i = 0; i < length_sets; i += Mask.MM256UInt32s) {
                    Avx.Store(v + i, Vector256<UInt32>.Zero);

#if DEBUG
                    Debug.Assert(checked(i + Mask.MM256UInt32s) <= value.Length);
#endif
                }
                if (length_rems > 0) {
                    Avx2.MaskStore(v + length_sets, Mask.LSV(length_rems), Vector256<UInt32>.Zero);

#if DEBUG
                    Debug.Assert(checked(length_sets + length_rems) <= value.Length);
#endif
                }
            }
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

            Zeroset(v, 0, (uint)mask_index);
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int Digits([DisallowNull] UInt32[] value) {
            fixed (UInt32* v = value) {
                for (int i = value.Length - 1; i >= 0; i--) {
                    if (v[i] != 0) {
                        return i + 1;
                    }
                }
            }

            return 1;
        }

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
