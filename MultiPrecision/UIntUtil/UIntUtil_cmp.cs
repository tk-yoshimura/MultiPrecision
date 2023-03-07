using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        /// <summary>Comparate uint32 array a == b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool Equal(uint length, UInt32[] arr_a, UInt32[] arr_b) {
#if DEBUG
            Debug<ArgumentException>.Assert(length <= arr_a.Length);
            Debug<ArgumentException>.Assert(length <= arr_b.Length);
#endif
            if (length <= 0u) {
                return true;
            }

            if (arr_a[0] != arr_b[0] || arr_a[length - 1u] != arr_b[length - 1u]) {
                return false;
            }

            fixed (UInt32* va0 = arr_a, vb0 = arr_b) {
                UInt32* va = va0, vb = vb0;

                Vector256<UInt32> a0, a1, a2, a3, b0, b1, b2, b3;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    (a0, a1, a2, a3) = LoadX4(va, va0, arr_a.Length);
                    (b0, b1, b2, b3) = LoadX4(vb, vb0, arr_b.Length);

                    uint flag =
                        (MaskEqual(a0, b0)) |
                        (MaskEqual(a1, b1) << ShiftIDX1) |
                        (MaskEqual(a2, b2) << ShiftIDX2) |
                        (MaskEqual(a3, b3) << ShiftIDX3);

                    if (flag != ~0u) {
                        return false;
                    }

                    va += MM256UInt32s * 4;
                    vb += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (a0, a1) = LoadX2(va, va0, arr_a.Length);
                    (b0, b1) = LoadX2(vb, vb0, arr_b.Length);

                    uint flag =
                        (MaskEqual(a0, b0)) |
                        (MaskEqual(a1, b1) << ShiftIDX1);

                    if (flag != 65535u) {
                        return false;
                    }

                    va += MM256UInt32s * 2;
                    vb += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    a0 = Load(va, va0, arr_a.Length);
                    b0 = Load(vb, vb0, arr_b.Length);

                    uint flag = MaskEqual(a0, b0);

                    if (flag != 255u) {
                        return false;
                    }

                    va += MM256UInt32s;
                    vb += MM256UInt32s;
                    r -= MM256UInt32s;
                }
                if (r > 0u) {
                    Vector256<UInt32> mask = Mask256.Lower(r);

                    a0 = MaskLoad(va, mask, va0, arr_a.Length);
                    b0 = MaskLoad(vb, mask, vb0, arr_b.Length);

                    uint flag = MaskEqual(a0, b0);

                    if (flag != 255u) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>Comparate uint32 array a &lt;= b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool LessThanOrEqual(uint length, UInt32[] arr_a, UInt32[] arr_b) {
#if DEBUG
            Debug<ArgumentException>.Assert(length <= arr_a.Length);
            Debug<ArgumentException>.Assert(length <= arr_b.Length);
#endif
            if (length <= 0u) {
                return true;
            }

            if (arr_a[length - 1u] < arr_b[length - 1u]) {
                return true;
            }
            if (arr_a[length - 1u] > arr_b[length - 1u]) {
                return false;
            }

            fixed (UInt32* va0 = arr_a, vb0 = arr_b) {
                UInt32* va = va0 + length, vb = vb0 + length;

                Vector256<UInt32> a0, a1, a2, a3, b0, b1, b2, b3;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    (a0, a1, a2, a3) = LoadX4(va - MM256UInt32s * 4, va0, arr_a.Length);
                    (b0, b1, b2, b3) = LoadX4(vb - MM256UInt32s * 4, vb0, arr_b.Length);

                    uint gt_flag =
                        (MaskGreaterThan(a0, b0)) |
                        (MaskGreaterThan(a1, b1) << ShiftIDX1) |
                        (MaskGreaterThan(a2, b2) << ShiftIDX2) |
                        (MaskGreaterThan(a3, b3) << ShiftIDX3);

                    uint lt_flag =
                        (MaskLessThan(a0, b0)) |
                        (MaskLessThan(a1, b1) << ShiftIDX1) |
                        (MaskLessThan(a2, b2) << ShiftIDX2) |
                        (MaskLessThan(a3, b3) << ShiftIDX3);

                    if ((gt_flag | lt_flag) != 0u) {
                        return gt_flag < lt_flag;
                    }

                    va -= MM256UInt32s * 4;
                    vb -= MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (a0, a1) = LoadX2(va - MM256UInt32s * 2, va0, arr_a.Length);
                    (b0, b1) = LoadX2(vb - MM256UInt32s * 2, vb0, arr_b.Length);

                    uint gt_flag =
                        (MaskGreaterThan(a0, b0)) |
                        (MaskGreaterThan(a1, b1) << ShiftIDX1);

                    uint lt_flag =
                        (MaskLessThan(a0, b0)) |
                        (MaskLessThan(a1, b1) << ShiftIDX1);

                    if ((gt_flag | lt_flag) != 0u) {
                        return gt_flag < lt_flag;
                    }

                    va -= MM256UInt32s * 2;
                    vb -= MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    a0 = Load(va - MM256UInt32s, va0, arr_a.Length);
                    b0 = Load(vb - MM256UInt32s, vb0, arr_b.Length);

                    uint gt_flag = MaskGreaterThan(a0, b0);

                    uint lt_flag = MaskLessThan(a0, b0);

                    if ((gt_flag | lt_flag) != 0u) {
                        return gt_flag < lt_flag;
                    }

                    r -= MM256UInt32s;
                }
                if (r > 0u) {
                    Vector256<UInt32> mask = Mask256.Lower(r);

                    a0 = MaskLoad(va0, mask, va0, arr_a.Length);
                    b0 = MaskLoad(vb0, mask, vb0, arr_b.Length);

                    uint gt_flag = MaskGreaterThan(a0, b0);

                    uint lt_flag = MaskLessThan(a0, b0);

                    return gt_flag <= lt_flag;
                }
            }

            return true;
        }

        /// <summary>Comparate uint32 array a &gt;= b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool GreaterThanOrEqual(uint length, UInt32[] arr_a, UInt32[] arr_b) {
#if DEBUG
            Debug<ArgumentException>.Assert(length <= arr_a.Length);
            Debug<ArgumentException>.Assert(length <= arr_b.Length);
#endif
            if (length <= 0u) {
                return true;
            }

            if (arr_a[length - 1u] > arr_b[length - 1u]) {
                return true;
            }
            if (arr_a[length - 1u] < arr_b[length - 1u]) {
                return false;
            }

            fixed (UInt32* va0 = arr_a, vb0 = arr_b) {
                UInt32* va = va0 + length, vb = vb0 + length;

                Vector256<UInt32> a0, a1, a2, a3, b0, b1, b2, b3;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    (a0, a1, a2, a3) = LoadX4(va - MM256UInt32s * 4, va0, arr_a.Length);
                    (b0, b1, b2, b3) = LoadX4(vb - MM256UInt32s * 4, vb0, arr_b.Length);

                    uint gt_flag =
                        (MaskGreaterThan(a0, b0)) |
                        (MaskGreaterThan(a1, b1) << ShiftIDX1) |
                        (MaskGreaterThan(a2, b2) << ShiftIDX2) |
                        (MaskGreaterThan(a3, b3) << ShiftIDX3);

                    uint lt_flag =
                        (MaskLessThan(a0, b0)) |
                        (MaskLessThan(a1, b1) << ShiftIDX1) |
                        (MaskLessThan(a2, b2) << ShiftIDX2) |
                        (MaskLessThan(a3, b3) << ShiftIDX3);

                    if ((gt_flag | lt_flag) != 0u) {
                        return gt_flag > lt_flag;
                    }

                    va -= MM256UInt32s * 4;
                    vb -= MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (a0, a1) = LoadX2(va - MM256UInt32s * 2, va0, arr_a.Length);
                    (b0, b1) = LoadX2(vb - MM256UInt32s * 2, vb0, arr_b.Length);

                    uint gt_flag =
                        (MaskGreaterThan(a0, b0)) |
                        (MaskGreaterThan(a1, b1) << ShiftIDX1);

                    uint lt_flag =
                        (MaskLessThan(a0, b0)) |
                        (MaskLessThan(a1, b1) << ShiftIDX1);

                    if ((gt_flag | lt_flag) != 0u) {
                        return gt_flag > lt_flag;
                    }

                    va -= MM256UInt32s * 2;
                    vb -= MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    a0 = Load(va - MM256UInt32s, va0, arr_a.Length);
                    b0 = Load(vb - MM256UInt32s, vb0, arr_b.Length);

                    uint gt_flag = MaskGreaterThan(a0, b0);

                    uint lt_flag = MaskLessThan(a0, b0);

                    if ((gt_flag | lt_flag) != 0u) {
                        return gt_flag > lt_flag;
                    }

                    r -= MM256UInt32s;
                }
                if (r > 0u) {
                    Vector256<UInt32> mask = Mask256.Lower(r);

                    a0 = MaskLoad(va0, mask, va0, arr_a.Length);
                    b0 = MaskLoad(vb0, mask, vb0, arr_b.Length);

                    uint gt_flag = MaskGreaterThan(a0, b0);

                    uint lt_flag = MaskLessThan(a0, b0);

                    return gt_flag >= lt_flag;
                }
            }

            return true;
        }

        /// <summary>Judge uint32 array is zero</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsZero(uint length, UInt32[] arr) {
            if (length <= 0u) {
                return true;
            }

            if (arr[0] != 0u || arr[length - 1u] != 0u) {
                return false;
            }

            fixed (UInt32* v0 = arr) {
                UInt32* v = v0;

                Vector256<UInt32> x0, x1, x2, x3;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadX4(v, v0, arr.Length);

                    if (!(IsAllZero(x0) & IsAllZero(x1) & IsAllZero(x2) & IsAllZero(x3))) {
                        return false;
                    }

                    v += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadX2(v, v0, arr.Length);

                    if (!(IsAllZero(x0) & IsAllZero(x1))) {
                        return false;
                    }

                    v += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = Load(v, v0, arr.Length);

                    if (!IsAllZero(x0)) {
                        return false;
                    }

                    v += MM256UInt32s;
                    r -= MM256UInt32s;
                }
                if (r > 0u) {
                    x0 = MaskLoad(v, Mask256.Lower(r), v0, arr.Length);

                    if (!IsAllZero(x0)) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>Judge uint32 array is full</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsFull(uint length, UInt32[] arr) {
            if (length <= 0u) {
                return false;
            }

            if (arr[0] != ~0u || arr[length - 1u] != ~0u) {
                return false;
            }

            fixed (UInt32* v0 = arr) {
                UInt32* v = v0;
                Vector256<UInt32> fulls = Vector256.Create(~0u);

                Vector256<UInt32> x0, x1, x2, x3;

                uint r = length;
                while (r >= MM256UInt32s * 4) {
                    (x0, x1, x2, x3) = LoadX4(v, v0, arr.Length);

                    if (!(TestC(x0, fulls) & TestC(x1, fulls) & TestC(x2, fulls) & TestC(x3, fulls))) {
                        return false;
                    }

                    v += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (x0, x1) = LoadX2(v, v0, arr.Length);

                    if (!(TestC(x0, fulls) & TestC(x1, fulls))) {
                        return false;
                    }

                    v += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    x0 = Load(v, v0, arr.Length);

                    if (!TestC(x0, fulls)) {
                        return false;
                    }

                    v += MM256UInt32s;
                    r -= MM256UInt32s;
                }
                if (r > 0u) {
                    Vector256<uint> mask = Mask256.Lower(r);

                    x0 = MaskLoad(v, mask, v0, arr.Length);

                    if (!IsAllZero(Xor(x0, mask))) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>Count leading match bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint MatchBits(uint length, UInt32[] arr_a, UInt32[] arr_b) {
#if DEBUG
            Debug<ArgumentException>.Assert(length <= arr_a.Length);
            Debug<ArgumentException>.Assert(length <= arr_b.Length);
#endif

            if (length <= 0u) {
                return 0;
            }

            uint matches = 0;

            fixed (UInt32* va0 = arr_a, vb0 = arr_b) {
                for (int i = (int)length - 1; i >= 0; i--) {
                    UInt32 xor = va0[i] ^ vb0[i];

                    if (xor == 0u) {
                        matches += UInt32Bits;
                    }
                    else {
                        matches += LeadingZeroCount(xor);
                        break;
                    }
                }
            }

            return matches;
        }
    }
}
