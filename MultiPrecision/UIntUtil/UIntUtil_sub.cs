using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        /// <summary>Operate uint32 array a -= b</summary>
        public static void Sub(UInt32[] arr_a, UInt32[] arr_b) {
            uint digits_b = Digits(arr_b);
            if (digits_b == 0u) {
                return;
            }
            if (digits_b == 1u) {
                Sub(0u, arr_a, arr_b[0]);
                return;
            }
            if (digits_b == 2u) {
                Sub(0u, arr_a, Pack(arr_b[1], arr_b[0]));
                return;
            }
            if (digits_b > arr_a.Length) {
                throw new OverflowException();
            }

            Sub(0u, digits_b, arr_a, arr_b);
        }

        /// <summary>Operate uint32 array a -= b</summary>
        public static void Sub(UInt32[] arr_a, UInt32 b) {
            Sub(0u, arr_a, b);
        }

        /// <summary>Operate uint32 array a -= b</summary>
        public static void Sub(UInt32[] arr_a, UInt64 b) {
            Sub(0u, arr_a, b);
        }

        /// <summary>Operate uint32 array a-= b &lt;&lt; offset</summary>
        private static unsafe void Sub(uint offset, uint digits_b, UInt32[] arr_a, UInt32[] arr_b) {
#if DEBUG
            if (digits_b != Digits(arr_b)) {
                throw new ArgumentException($"mismatch digits of {nameof(arr_b)}", nameof(digits_b));
            }
#endif

            if (digits_b + offset > arr_a.Length) {
                throw new OverflowException();
            }

            fixed (UInt32* va0 = arr_a, vb0 = arr_b) {
                UInt32* va = va0 + offset, vb = vb0;
                Vector256<UInt32> a0, a1, a2, a3, b0, b1, b2, b3;

                uint digit = 0;
                UInt32 carry_prev = 0u;

                while (digit + MM256UInt32s * 4 <= digits_b) {
                    (a0, a1, a2, a3) = LoadX4(va, va0, arr_a.Length);
                    (b0, b1, b2, b3) = LoadX4(vb, vb0, arr_b.Length);

                    (a0, b0) = Sub(a0, b0);
                    (a1, b1) = Sub(a1, b1);
                    (a2, b2) = Sub(a2, b2);
                    (a3, b3) = Sub(a3, b3);

                    (b0, b1, b2, b3, UInt32 carry) = CarryShiftX4(b0, b1, b2, b3, 0u);
                    b0 = b0.WithElement(0, carry_prev);
                    (a0, a1, a2, a3, carry) = FlushCarrySubX4(a0, a1, a2, a3, b0, b1, b2, b3, carry);

                    StoreX4(va, a0, a1, a2, a3, va0, arr_a.Length);
                    carry_prev = carry;

                    va += MM256UInt32s * 4;
                    vb += MM256UInt32s * 4;
                    digit += MM256UInt32s * 4;
                }
                if (digit + MM256UInt32s * 2 <= digits_b) {
                    (a0, a1) = LoadX2(va, va0, arr_a.Length);
                    (b0, b1) = LoadX2(vb, vb0, arr_b.Length);

                    (a0, b0) = Sub(a0, b0);
                    (a1, b1) = Sub(a1, b1);

                    (b0, b1, UInt32 carry) = CarryShiftX2(b0, b1, 0u);
                    b0 = b0.WithElement(0, carry_prev);
                    (a0, a1, carry) = FlushCarrySubX2(a0, a1, b0, b1, carry);

                    StoreX2(va, a0, a1, va0, arr_a.Length);
                    carry_prev = carry;

                    va += MM256UInt32s * 2;
                    vb += MM256UInt32s * 2;
                    digit += MM256UInt32s * 2;
                }
                if (digit + MM256UInt32s <= digits_b) {
                    a0 = Load(va, va0, arr_a.Length);
                    b0 = Load(vb, vb0, arr_b.Length);

                    (a0, b0) = Sub(a0, b0);

                    (b0, UInt32 carry) = CarryShift(b0, 0u);
                    b0 = b0.WithElement(0, carry_prev);
                    (a0, carry) = FlushCarrySub(a0, b0, carry);

                    Store(va, a0, va0, arr_a.Length);
                    carry_prev = carry;

                    va += MM256UInt32s;
                    vb += MM256UInt32s;
                    digit += MM256UInt32s;
                }
                if (digit < digits_b) {
                    uint rem_a = (uint)arr_a.Length - digit - offset;
                    uint rem_b = digits_b - digit;
                    Vector256<UInt32> mask_a = Mask256.Lower(rem_a);
                    Vector256<UInt32> mask_b = Mask256.Lower(rem_b);

                    a0 = MaskLoad(va, mask_a, va0, arr_a.Length);
                    b0 = MaskLoad(vb, mask_b, vb0, arr_b.Length);

                    (a0, b0) = Sub(a0, b0);

                    (b0, UInt32 carry) = CarryShift(b0, 0u);
                    b0 = b0.WithElement(0, carry_prev);
                    (a0, carry) = FlushCarrySub(a0, b0, carry);

                    if (rem_a < MM256UInt32s) {
                        if (a0.GetElement((int)rem_a) > 0u) {
                            throw new OverflowException();
                        }
                        MaskStore(va, a0, mask_a, va0, arr_a.Length);
                    }
                    else {
                        Store(va, a0, va0, arr_a.Length);
                        Sub(digit + MM256UInt32s, arr_a, carry);
                    }
                }
                else {
                    Sub(digit, arr_a, carry_prev);
                }
            }
        }

        /// <summary>Operate uint32 array a -= b &lt;&lt; offset</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void Sub(uint offset, UInt32[] arr_a, UInt32 b) {
            fixed (UInt32* va0 = arr_a) {
                for (uint i = offset, length = (uint)arr_a.Length; i < length && b > 0u; i++) {
                    UInt32 a = va0[i];

                    va0[i] = unchecked(a - b);
                    b = (va0[i] <= a) ? 0u : 1u;
                }
            }

            if (b > 0u) {
                throw new OverflowException();
            }
        }

        /// <summary>Operate uint32 array a -= b &lt;&lt; offset</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void Sub(uint offset, UInt32[] arr_a, UInt64 b) {
            if (b == 0uL) {
                return;
            }

            (UInt32 b_hi, UInt32 b_lo) = Unpack(b);

            fixed (UInt32* va0 = arr_a) {
                for (uint i = offset, length = (uint)arr_a.Length; i < length && b_lo > 0u; i++) {
                    UInt32 a = va0[i];

                    va0[i] = unchecked(a - b_lo);
                    b_lo = (va0[i] <= a) ? 0u : 1u;
                }
                for (uint i = offset + 1u, length = (uint)arr_a.Length; i < length && b_hi > 0u; i++) {
                    UInt32 a = va0[i];

                    va0[i] = unchecked(a - b_hi);
                    b_hi = (va0[i] <= a) ? 0u : 1u;
                }
            }

            if ((b_lo | b_hi) > 0u) {
                throw new OverflowException();
            }
        }
    }
}
