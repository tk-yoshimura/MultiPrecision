using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        /// <summary>Shift uint32 array v &lt;&lt;= sft</summary>
        public static unsafe void LeftShift(UInt32[] value, int sft) {

#if DEBUG
            Debug<ArgumentOutOfRangeException>.Assert(sft >= 0);
#endif

            if (sft > LeadingZeroCount(value)) {
                throw new OverflowException();
            }

            int sftdev = sft / UInt32Bits;
            int sftrem = sft % UInt32Bits;

            if (sftrem == 0) {
                LeftBlockShift(value, sftdev);
                return;
            }

            uint count = (uint)(value.Length - sftdev - 1);
            uint count_sets = Mask256.UInt32Sets(count), count_rems = Mask256.UInt32Rems(count);

            fixed (UInt32* v = value) {
                if (count_rems > 0) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(count_sets + sftdev + 1 + count_rems) <= value.Length);
#endif

                    Vector256<UInt32> mask = Mask256.LSV(count_rems);
                    Vector256<UInt32> ls = Avx2.ShiftRightLogical(Avx2.MaskLoad(v + count_sets, mask), (byte)(UInt32Bits - sftrem));
                    Vector256<UInt32> ms = Avx2.ShiftLeftLogical(Avx2.MaskLoad(v + count_sets + 1, mask), (byte)sftrem);

                    Avx2.MaskStore(v + count_sets + sftdev + 1, mask, Avx2.Or(ls, ms));
                }
                for (uint i = count_sets; i >= Mask256.MM256UInt32s; i -= Mask256.MM256UInt32s) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(i + sftdev + 1) <= value.Length);
#endif

                    Vector256<UInt32> ls = Avx2.ShiftRightLogical(Avx.LoadVector256(v + i - Mask256.MM256UInt32s), (byte)(UInt32Bits - sftrem));
                    Vector256<UInt32> ms = Avx2.ShiftLeftLogical(Avx.LoadVector256(v + i + 1 - Mask256.MM256UInt32s), (byte)sftrem);

                    Avx.Store(v + i + sftdev + 1 - Mask256.MM256UInt32s, Avx2.Or(ls, ms));
                }

#if DEBUG
                Debug<IndexOutOfRangeException>.Assert(sftdev < value.Length);
#endif

                v[sftdev] = v[0] << sftrem;
            }

            Zeroset(value, 0, (uint)sftdev);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft</summary>
        public static unsafe void RightShift(UInt32[] value, int sft) {

#if DEBUG
            Debug<ArgumentOutOfRangeException>.Assert(sft >= 0);
#endif

            int sftdev = sft / UInt32Bits;
            int sftrem = sft % UInt32Bits;

            if (sftrem == 0 || sftdev >= value.Length) {
                RightBlockShift(value, sftdev);
                return;
            }

            uint count = (uint)(value.Length - sftdev - 1);
            uint count_sets = Mask256.UInt32Sets(count), count_rems = Mask256.UInt32Rems(count);

            fixed (UInt32* v = value) {
                for (uint i = 0; i < count_sets; i += Mask256.MM256UInt32s) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(i + sftdev + 1 + Mask256.MM256UInt32s) <= value.Length);
#endif

                    Vector256<UInt32> ls = Avx2.ShiftRightLogical(Avx.LoadVector256(v + i + sftdev), (byte)sftrem);
                    Vector256<UInt32> ms = Avx2.ShiftLeftLogical(Avx.LoadVector256(v + i + sftdev + 1), (byte)(UInt32Bits - sftrem));

                    Avx.Store(v + i, Avx2.Or(ls, ms));

                }
                if (count_rems > 0) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(count_sets + sftdev + 1 + count_rems) <= value.Length);
#endif

                    Vector256<UInt32> mask = Mask256.LSV(count_rems);
                    Vector256<UInt32> ls = Avx2.ShiftRightLogical(Avx2.MaskLoad(v + count_sets + sftdev, mask), (byte)sftrem);
                    Vector256<UInt32> ms = Avx2.ShiftLeftLogical(Avx2.MaskLoad(v + count_sets + sftdev + 1, mask), (byte)(UInt32Bits - sftrem));

                    Avx2.MaskStore(v + count_sets, mask, Avx2.Or(ls, ms));
                }

#if DEBUG
                Debug<IndexOutOfRangeException>.Assert(count < value.Length);
#endif

                v[count] = v[value.Length - 1] >> sftrem;
            }

            Zeroset(value, count + 1, (uint)sftdev);
        }

        /// <summary>Shift uint32 array v &lt;&lt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void LeftBlockShift(UInt32[] value, int sft) {

#if DEBUG
            Debug<ArgumentOutOfRangeException>.Assert(sft >= 0);
#endif

            if (checked(sft + Digits(value)) > value.Length) {
                throw new OverflowException();
            }

            uint count = (uint)(value.Length - sft);
            uint count_sets = Mask256.UInt32Sets(count), count_rems = Mask256.UInt32Rems(count);

            fixed (UInt32* v = value) {
                if (count_rems > 0) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(count_sets + sft + count_rems) <= value.Length);
#endif

                    Vector256<UInt32> mask = Mask256.LSV(count_rems);
                    Avx2.MaskStore(v + count_sets + sft, mask, Avx2.MaskLoad(v + count_sets, mask));

                }
                for (uint i = count_sets; i >= Mask256.MM256UInt32s; i -= Mask256.MM256UInt32s) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(i + sft) <= value.Length);
#endif

                    Avx.Store(v + i + sft - Mask256.MM256UInt32s, Avx.LoadVector256(v + i - Mask256.MM256UInt32s));
                }
            }

            Zeroset(value, 0, (uint)sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft * UInt32Bits</summary>
        public static unsafe void RightBlockShift(UInt32[] value, int sft) {

#if DEBUG
            Debug<ArgumentOutOfRangeException>.Assert(sft >= 0);
#endif

            if (sft >= value.Length) {
                Zeroset(value);
                return;
            }

            uint count = (uint)(value.Length - sft);
            uint count_sets = Mask256.UInt32Sets(count), count_rems = Mask256.UInt32Rems(count);

            fixed (UInt32* v = value) {
                for (uint i = 0; i < count_sets; i += Mask256.MM256UInt32s) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(i + sft + Mask256.MM256UInt32s) <= value.Length);
#endif

                    Avx.Store(v + i, Avx.LoadVector256(v + i + sft));
                }
                if (count_rems > 0) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(count_sets + sft + count_rems) <= value.Length);
#endif

                    Vector256<UInt32> mask = Mask256.LSV(count_rems);
                    Avx2.MaskStore(v + count_sets, mask, Avx2.MaskLoad(v + count_sets + sft, mask));
                }
            }

            Zeroset(value, count, (uint)sft);
        }
    }
}
