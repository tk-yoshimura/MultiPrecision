using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace MultiPrecision {
    internal static partial class UIntUtil {

#pragma warning disable CA1857

        /// <summary>Shift uint32 array v &lt;&lt;= sft</summary>
        public static unsafe void LeftShift(UInt32[] value, int sft, bool check_overflow = false) {
            ArgumentOutOfRangeException.ThrowIfNegative(sft);

            int sft_block = sft / UInt32Bits, sft_rem = sft % UInt32Bits;

            if (sft_rem == 0) {
                LeftBlockShift(value, sft_block, check_overflow);
                return;
            }

            if (check_overflow && sft > LeadingZeroCount(value)) {
                throw new OverflowException();
            }
            if (!check_overflow && sft_block >= value.Length) {
                Zeroset(value);
                return;
            }

            byte lsft = (byte)sft_rem, rsft = (byte)(UInt32Bits - sft_rem);

            uint r = (uint)(value.Length - sft_block - 1), rem = r % MM256UInt32s;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r;

                Vector256<UInt32> x0, x1, x2, x3, y0, y1, y2, y3, z0, z1, z2, z3;

                if (rem > 0) {
                    Vector256<UInt32> mask = Mask256.Lower(rem);
                    x0 = MaskLoad(v - rem, mask, v0, value.Length);
                    y0 = MaskLoad(v - rem + 1, mask, v0, value.Length);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));

                    MaskStore(v - rem + sft_block + 1, z0, mask, v0, value.Length);
                    r -= rem;
                    v -= rem;
                }
                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadX4(v - MM256UInt32s * 4, v0, value.Length);
                    (y0, y1, y2, y3) = LoadX4(v - MM256UInt32s * 4 + 1, v0, value.Length);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));
                    z1 = Or(ShiftRightLogical(x1, rsft), ShiftLeftLogical(y1, lsft));
                    z2 = Or(ShiftRightLogical(x2, rsft), ShiftLeftLogical(y2, lsft));
                    z3 = Or(ShiftRightLogical(x3, rsft), ShiftLeftLogical(y3, lsft));

                    StoreX4(v - MM256UInt32s * 4 + sft_block + 1, z0, z1, z2, z3, v0, value.Length);
                    r -= MM256UInt32s * 4;
                    v -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadX2(v - MM256UInt32s * 2, v0, value.Length);
                    (y0, y1) = LoadX2(v - MM256UInt32s * 2 + 1, v0, value.Length);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));
                    z1 = Or(ShiftRightLogical(x1, rsft), ShiftLeftLogical(y1, lsft));

                    StoreX2(v - MM256UInt32s * 2 + sft_block + 1, z0, z1, v0, value.Length);
                    r -= MM256UInt32s * 2;
                    v -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = Load(v - MM256UInt32s, v0, value.Length);
                    y0 = Load(v - MM256UInt32s + 1, v0, value.Length);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));

                    Store(v - MM256UInt32s + sft_block + 1, z0, v0, value.Length);

                    v -= MM256UInt32s;
                }

                v[sft_block] = v[0] << sft_rem;
            }

            Zeroset(value, 0, (uint)sft_block);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft</summary>
        public static unsafe void RightShift(UInt32[] value, int sft) {
            ArgumentOutOfRangeException.ThrowIfNegative(sft);

            int sft_block = sft / UInt32Bits, sft_rem = sft % UInt32Bits;

            if (sft_rem == 0) {
                RightBlockShift(value, sft_block);
                return;
            }
            if (sft_block >= value.Length) {
                Zeroset(value);
                return;
            }

            byte rsft = (byte)sft_rem, lsft = (byte)(UInt32Bits - sft_rem);

            uint count = (uint)(value.Length - sft_block - 1), r = count;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0;

                Vector256<UInt32> x0, x1, x2, x3, y0, y1, y2, y3, z0, z1, z2, z3;

                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadX4(v + sft_block, v0, value.Length);
                    (y0, y1, y2, y3) = LoadX4(v + sft_block + 1, v0, value.Length);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));
                    z1 = Or(ShiftRightLogical(x1, rsft), ShiftLeftLogical(y1, lsft));
                    z2 = Or(ShiftRightLogical(x2, rsft), ShiftLeftLogical(y2, lsft));
                    z3 = Or(ShiftRightLogical(x3, rsft), ShiftLeftLogical(y3, lsft));

                    StoreX4(v, z0, z1, z2, z3, v0, value.Length);
                    r -= MM256UInt32s * 4;
                    v += MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadX2(v + sft_block, v0, value.Length);
                    (y0, y1) = LoadX2(v + sft_block + 1, v0, value.Length);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));
                    z1 = Or(ShiftRightLogical(x1, rsft), ShiftLeftLogical(y1, lsft));

                    StoreX2(v, z0, z1, v0, value.Length);
                    r -= MM256UInt32s * 2;
                    v += MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = Load(v + sft_block, v0, value.Length);
                    y0 = Load(v + sft_block + 1, v0, value.Length);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));

                    Store(v, z0, v0, value.Length);
                    r -= MM256UInt32s;
                    v += MM256UInt32s;
                }
                if (r > 0u) {
                    Vector256<UInt32> mask = Mask256.Lower(r);

                    x0 = MaskLoad(v + sft_block, mask, v0, value.Length);
                    y0 = MaskLoad(v + sft_block + 1, mask, v0, value.Length);

                    z0 = Or(ShiftRightLogical(x0, rsft), ShiftLeftLogical(y0, lsft));

                    MaskStore(v, z0, mask, v0, value.Length);

                    v += r;
                }

                v[0] = v[sft_block] >> sft_rem;
            }

            Zeroset(value, count + 1, (uint)sft_block);
        }
#pragma warning restore CA1857

        /// <summary>Shift uint32 array v &lt;&lt;= sft * UInt32Bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void LeftBlockShift(UInt32[] value, int sft, bool check_overflow = false) {
            ArgumentOutOfRangeException.ThrowIfNegative(sft);

            if (check_overflow && checked(sft + Digits(value)) > value.Length) {
                throw new OverflowException();
            }
            if (!check_overflow && sft >= value.Length) {
                Zeroset(value);
                return;
            }

            uint r = (uint)(value.Length - sft), rem = r % MM256UInt32s;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r;

                Vector256<UInt32> x0, x1, x2, x3;

                if (rem > 0) {
                    Vector256<UInt32> mask = Mask256.Lower(rem);
                    MaskStore(v + sft - rem, MaskLoad(v - rem, mask, v0, value.Length), mask, v0, value.Length);
                    r -= rem;
                    v -= rem;
                }
                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadX4(v - MM256UInt32s * 4, v0, value.Length);
                    StoreX4(v + sft - MM256UInt32s * 4, x0, x1, x2, x3, v0, value.Length);
                    r -= MM256UInt32s * 4;
                    v -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadX2(v - MM256UInt32s * 2, v0, value.Length);
                    StoreX2(v + sft - MM256UInt32s * 2, x0, x1, v0, value.Length);
                    r -= MM256UInt32s * 2;
                    v -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = Load(v - MM256UInt32s, v0, value.Length);
                    Store(v + sft - MM256UInt32s, x0, v0, value.Length);
                }
            }

            Zeroset(value, 0, (uint)sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft * UInt32Bits</summary>
        public static unsafe void RightBlockShift(UInt32[] value, int sft) {
            ArgumentOutOfRangeException.ThrowIfNegative(sft);

            if (sft >= value.Length) {
                Zeroset(value);
                return;
            }

            uint count = (uint)(value.Length - sft), r = count;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0;

                Vector256<UInt32> x0, x1, x2, x3;

                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadX4(v + sft, v0, value.Length);
                    StoreX4(v, x0, x1, x2, x3, v0, value.Length);
                    r -= MM256UInt32s * 4;
                    v += MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadX2(v + sft, v0, value.Length);
                    StoreX2(v, x0, x1, v0, value.Length);
                    r -= MM256UInt32s * 2;
                    v += MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = Load(v + sft, v0, value.Length);
                    Store(v, x0, v0, value.Length);
                    r -= MM256UInt32s;
                    v += MM256UInt32s;
                }
                if (r > 0u) {
                    Vector256<UInt32> mask = Mask256.Lower(r);
                    MaskStore(v, MaskLoad(v + sft, mask, v0, value.Length), mask, v0, value.Length);
                }
            }

            Zeroset(value, count, (uint)sft);
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft with round</summary>
        public static unsafe void RightRoundShift(UInt32[] value, int sft) {
            ArgumentOutOfRangeException.ThrowIfNegative(sft);
            if (sft == 0) {
                return;
            }

            int roundbit = sft - 1;

            int roundbit_block = roundbit / UInt32Bits, roundbit_rem = roundbit % UInt32Bits;

            if (roundbit_block >= value.Length) {
                Zeroset(value);
                return;
            }

            UInt32 round = (value[roundbit_block] >> roundbit_rem) & 1u;

            RightShift(value, sft);

            if (round > 0u) {
                Add(value, 1u);
            }
        }

        /// <summary>Shift uint32 array v &gt;&gt;= sft with round</summary>
        public static unsafe void RightRoundBlockShift(UInt32[] value, int sft) {
            ArgumentOutOfRangeException.ThrowIfNegative(sft);
            if (sft == 0) {
                return;
            }

            if (sft > value.Length) {
                Zeroset(value);
                return;
            }

            bool round = value[sft - 1] > UInt32Round;

            RightBlockShift(value, sft);

            if (round) {
                Add(value, 1u);
            }
        }
    }
}
