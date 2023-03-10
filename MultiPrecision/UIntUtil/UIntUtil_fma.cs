using System.Runtime.Intrinsics;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        /// <summary>Operate uint32 array arr_dst += a * b</summary>
        public static void Fma(UInt32[] arr_dst, UInt32[] arr_a, UInt32[] arr_b) {
            uint digits_a = Digits(arr_a), digits_b = Digits(arr_b);

            if (digits_a >= digits_b) {
                for (uint i = 0; i < (digits_b & (~1u)); i += 2) {
                    Fma(i, digits_a, arr_dst, arr_a, Pack(arr_b[i + 1u], arr_b[i]));
                }
                for (uint i = (digits_b & (~1u)); i < digits_b; i++) {
                    Fma(i, digits_a, arr_dst, arr_a, arr_b[i]);
                }
            }
            else {
                for (uint i = 0; i < (digits_a & (~1u)); i += 2) {
                    Fma(i, digits_b, arr_dst, arr_b, Pack(arr_a[i + 1u], arr_a[i]));
                }
                for (uint i = (digits_a & (~1u)); i < digits_a; i++) {
                    Fma(i, digits_b, arr_dst, arr_b, arr_a[i]);
                }
            }
        }

        /// <summary>Operate uint32 array arr_dst += a * b</summary>
        public static void Fma(UInt32[] arr_dst, UInt32[] arr_a, UInt32 b) {
            Fma(0u, Digits(arr_a), arr_dst, arr_a, b);
        }

        /// <summary>Operate uint32 array arr_dst += a * b</summary>
        public static void Fma(UInt32[] arr_dst, UInt32[] arr_a, UInt64 b) {
            Fma(0u, Digits(arr_a), arr_dst, arr_a, b);
        }

        /// <summary>Operate uint32 array arr_dst += a * b &lt;&lt; offset</summary>
        private static unsafe void Fma(uint offset, uint digits_a, UInt32[] arr_dst, UInt32[] arr_a, UInt32 b) {
            if (b == 0u) {
                return;
            }

#if DEBUG
            if (digits_a != Digits(arr_a)) {
                throw new ArgumentException($"mismatch digits of {nameof(arr_a)}", nameof(digits_a));
            }
#endif

            if (digits_a + offset > arr_dst.Length) {
                throw new OverflowException();
            }

            fixed (UInt32* va0 = arr_a, vd0 = arr_dst) {
                UInt32* va = va0, vd = vd0 + offset;

                Vector256<UInt32> b0 = Vector256.Create((UInt64)b).AsUInt32();
                uint r = digits_a;
                UInt32 carry_prev = 0u;

                Vector256<UInt32> a0, a1, a2, a3, d0, d1, d2, d3, r0, r1, r2, r3, c0, c1, c2, c3;

                while (r >= MM256UInt32s * 4) {
                    (a0, a1, a2, a3) = LoadX4(va, va0, arr_a.Length);
                    (d0, d1, d2, d3) = LoadX4(vd, vd0, arr_dst.Length);

                    (r0, c0) = Mul(a0, b0);
                    (r1, c1) = Mul(a1, b0);
                    (r2, c2) = Mul(a2, b0);
                    (r3, c3) = Mul(a3, b0);

                    (c0, c1, c2, c3, UInt32 carry) = CarryShiftX4(c0, c1, c2, c3, 0u);
                    (r0, r1, r2, r3, carry) = FlushCarryAddX4(r0, r1, r2, r3, c0, c1, c2, c3, carry);

                    (d0, c0) = Add(d0, r0);
                    (d1, c1) = Add(d1, r1);
                    (d2, c2) = Add(d2, r2);
                    (d3, c3) = Add(d3, r3);

                    (c0, c1, c2, c3, carry) = CarryShiftX4(c0, c1, c2, c3, carry);
                    c0 = c0.WithElement(0, carry_prev);
                    (d0, d1, d2, d3, carry) = FlushCarryAddX4(d0, d1, d2, d3, c0, c1, c2, c3, carry);

                    StoreX4(vd, d0, d1, d2, d3, vd0, arr_dst.Length);
                    carry_prev = carry;

                    va += MM256UInt32s * 4;
                    vd += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                    offset += MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (a0, a1) = LoadX2(va, va0, arr_a.Length);
                    (d0, d1) = LoadX2(vd, vd0, arr_dst.Length);

                    (r0, c0) = Mul(a0, b0);
                    (r1, c1) = Mul(a1, b0);

                    (c0, c1, UInt32 carry) = CarryShiftX2(c0, c1, 0u);
                    (r0, r1, carry) = FlushCarryAddX2(r0, r1, c0, c1, carry);

                    (d0, c0) = Add(d0, r0);
                    (d1, c1) = Add(d1, r1);

                    (c0, c1, carry) = CarryShiftX2(c0, c1, carry);
                    c0 = c0.WithElement(0, carry_prev);
                    (d0, d1, carry) = FlushCarryAddX2(d0, d1, c0, c1, carry);

                    StoreX2(vd, d0, d1, vd0, arr_dst.Length);
                    carry_prev = carry;

                    va += MM256UInt32s * 2;
                    vd += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                    offset += MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    a0 = Load(va, va0, arr_a.Length);
                    d0 = Load(vd, vd0, arr_dst.Length);

                    (r0, c0) = Mul(a0, b0);

                    (c0, UInt32 carry) = CarryShift(c0, 0u);
                    (r0, carry) = FlushCarryAdd(r0, c0, carry);

                    (d0, c0) = Add(d0, r0);

                    (c0, carry) = CarryShift(c0, carry);
                    c0 = c0.WithElement(0, carry_prev);
                    (d0, carry) = FlushCarryAdd(d0, c0, carry);

                    Store(vd, d0, vd0, arr_dst.Length);
                    carry_prev = carry;

                    va += MM256UInt32s;
                    vd += MM256UInt32s;
                    r -= MM256UInt32s;
                    offset += MM256UInt32s;
                }
                if (r > 0u) {
                    Vector256<UInt32> mask_a = Mask256.Lower(r);
                    a0 = MaskLoad(va, mask_a, va0, arr_a.Length);

                    uint rem_d = (uint)arr_dst.Length - offset;
                    Vector256<UInt32> mask_d = Mask256.Lower(rem_d);
                    d0 = MaskLoad(vd, mask_d, vd0, arr_dst.Length);

                    (r0, c0) = Mul(a0, b0);

                    (c0, UInt32 carry) = CarryShift(c0, 0u);
                    (r0, carry) = FlushCarryAdd(r0, c0, carry);

                    (d0, c0) = Add(d0, r0);

                    (c0, carry) = CarryShift(c0, carry);
                    c0 = c0.WithElement(0, carry_prev);
                    (d0, carry) = FlushCarryAdd(d0, c0, carry);

                    if (rem_d < MM256UInt32s) {
                        if (d0.GetElement((int)rem_d) > 0u) {
                            throw new OverflowException();
                        }
                        MaskStore(vd, d0, mask_d, vd0, arr_dst.Length);
                    }
                    else {
                        Store(vd, d0, vd0, arr_dst.Length);
                        Add(offset + MM256UInt32s, arr_dst, carry);
                    }
                }
                else {
                    Add(offset, arr_dst, carry_prev);
                }
            }
        }

        /// <summary>Operate uint32 array arr_dst += a * b &lt;&lt; offset</summary>
        private static unsafe void Fma(uint offset, uint digits_a, UInt32[] arr_dst, UInt32[] arr_a, UInt64 b) {
            if (b == 0uL) {
                return;
            }

#if DEBUG
            if (digits_a != Digits(arr_a)) {
                throw new ArgumentException($"mismatch digits of {nameof(arr_a)}", nameof(digits_a));
            }
#endif
            (UInt32 b_hi, UInt32 b_lo) = Unpack(b);
            if (b_hi == 0u) {
                Fma(offset, digits_a, arr_dst, arr_a, b_lo);
                return;
            }

            if (digits_a + offset + 1u > arr_dst.Length) {
                throw new OverflowException();
            }

            fixed (UInt32* va0 = arr_a, vd0 = arr_dst) {
                UInt32* va = va0, vd = vd0 + offset;

                Vector256<UInt32> b0 = Vector256.Create((UInt64)b_lo).AsUInt32();
                Vector256<UInt32> b1 = Vector256.Create((UInt64)b_hi).AsUInt32();
                uint r = digits_a;
                UInt32 carry_lo_prev = 0u, carry_hi_prev = 0u;

                Vector256<UInt32> a0, a1, a2, a3, d0, d1, d2, d3, r0, r1, r2, r3, c0, c1, c2, c3;

                while (r >= MM256UInt32s * 4) {
                    (a0, a1, a2, a3) = LoadX4(va, va0, arr_a.Length);

                    /*mul lo*/
                    {
                        (d0, d1, d2, d3) = LoadX4(vd, vd0, arr_dst.Length);

                        (r0, c0) = Mul(a0, b0);
                        (r1, c1) = Mul(a1, b0);
                        (r2, c2) = Mul(a2, b0);
                        (r3, c3) = Mul(a3, b0);

                        (c0, c1, c2, c3, UInt32 carry) = CarryShiftX4(c0, c1, c2, c3, 0u);
                        (r0, r1, r2, r3, carry) = FlushCarryAddX4(r0, r1, r2, r3, c0, c1, c2, c3, carry);

                        (d0, c0) = Add(d0, r0);
                        (d1, c1) = Add(d1, r1);
                        (d2, c2) = Add(d2, r2);
                        (d3, c3) = Add(d3, r3);

                        (c0, c1, c2, c3, carry) = CarryShiftX4(c0, c1, c2, c3, carry);
                        c0 = c0.WithElement(0, carry_lo_prev);
                        (d0, d1, d2, d3, carry) = FlushCarryAddX4(d0, d1, d2, d3, c0, c1, c2, c3, carry);

                        StoreX4(vd, d0, d1, d2, d3, vd0, arr_dst.Length);
                        carry_lo_prev = carry;
                    }

                    /*mul hi*/
                    {
                        (d0, d1, d2, d3) = LoadX4(vd + 1u, vd0, arr_dst.Length);

                        (r0, c0) = Mul(a0, b1);
                        (r1, c1) = Mul(a1, b1);
                        (r2, c2) = Mul(a2, b1);
                        (r3, c3) = Mul(a3, b1);

                        (c0, c1, c2, c3, UInt32 carry) = CarryShiftX4(c0, c1, c2, c3, 0u);
                        (r0, r1, r2, r3, carry) = FlushCarryAddX4(r0, r1, r2, r3, c0, c1, c2, c3, carry);

                        (d0, c0) = Add(d0, r0);
                        (d1, c1) = Add(d1, r1);
                        (d2, c2) = Add(d2, r2);
                        (d3, c3) = Add(d3, r3);

                        (c0, c1, c2, c3, carry) = CarryShiftX4(c0, c1, c2, c3, carry);
                        c0 = c0.WithElement(0, carry_hi_prev);
                        (d0, d1, d2, d3, carry) = FlushCarryAddX4(d0, d1, d2, d3, c0, c1, c2, c3, carry);

                        StoreX4(vd + 1u, d0, d1, d2, d3, vd0, arr_dst.Length);
                        carry_hi_prev = carry;
                    }

                    va += MM256UInt32s * 4;
                    vd += MM256UInt32s * 4;
                    r -= MM256UInt32s * 4;
                    offset += MM256UInt32s * 4;
                }
                if (r >= MM256UInt32s * 2) {
                    (a0, a1) = LoadX2(va, va0, arr_a.Length);

                    /*mul lo*/
                    {
                        (d0, d1) = LoadX2(vd, vd0, arr_dst.Length);

                        (r0, c0) = Mul(a0, b0);
                        (r1, c1) = Mul(a1, b0);

                        (c0, c1, UInt32 carry) = CarryShiftX2(c0, c1, 0u);
                        (r0, r1, carry) = FlushCarryAddX2(r0, r1, c0, c1, carry);

                        (d0, c0) = Add(d0, r0);
                        (d1, c1) = Add(d1, r1);

                        (c0, c1, carry) = CarryShiftX2(c0, c1, carry);
                        c0 = c0.WithElement(0, carry_lo_prev);
                        (d0, d1, carry) = FlushCarryAddX2(d0, d1, c0, c1, carry);

                        StoreX2(vd, d0, d1, vd0, arr_dst.Length);
                        carry_lo_prev = carry;
                    }

                    /*mul hi*/
                    {
                        (d0, d1) = LoadX2(vd + 1u, vd0, arr_dst.Length);

                        (r0, c0) = Mul(a0, b1);
                        (r1, c1) = Mul(a1, b1);

                        (c0, c1, UInt32 carry) = CarryShiftX2(c0, c1, 0u);
                        (r0, r1, carry) = FlushCarryAddX2(r0, r1, c0, c1, carry);

                        (d0, c0) = Add(d0, r0);
                        (d1, c1) = Add(d1, r1);

                        (c0, c1, carry) = CarryShiftX2(c0, c1, carry);
                        c0 = c0.WithElement(0, carry_hi_prev);
                        (d0, d1, carry) = FlushCarryAddX2(d0, d1, c0, c1, carry);

                        StoreX2(vd + 1u, d0, d1, vd0, arr_dst.Length);
                        carry_hi_prev = carry;
                    }

                    va += MM256UInt32s * 2;
                    vd += MM256UInt32s * 2;
                    r -= MM256UInt32s * 2;
                    offset += MM256UInt32s * 2;
                }
                if (r >= MM256UInt32s) {
                    a0 = Load(va, va0, arr_a.Length);

                    /*mul lo*/
                    {
                        d0 = Load(vd, vd0, arr_dst.Length);

                        (r0, c0) = Mul(a0, b0);

                        (c0, UInt32 carry) = CarryShift(c0, 0u);
                        (r0, carry) = FlushCarryAdd(r0, c0, carry);

                        (d0, c0) = Add(d0, r0);

                        (c0, carry) = CarryShift(c0, carry);
                        c0 = c0.WithElement(0, carry_lo_prev);
                        (d0, carry) = FlushCarryAdd(d0, c0, carry);

                        Store(vd, d0, vd0, arr_dst.Length);
                        carry_lo_prev = carry;
                    }

                    /*mul hi*/
                    {
                        d0 = Load(vd + 1u, vd0, arr_dst.Length);

                        (r0, c0) = Mul(a0, b1);

                        (c0, UInt32 carry) = CarryShift(c0, 0u);
                        (r0, carry) = FlushCarryAdd(r0, c0, carry);

                        (d0, c0) = Add(d0, r0);

                        (c0, carry) = CarryShift(c0, carry);
                        c0 = c0.WithElement(0, carry_hi_prev);
                        (d0, carry) = FlushCarryAdd(d0, c0, carry);

                        Store(vd + 1u, d0, vd0, arr_dst.Length);
                        carry_hi_prev = carry;
                    }

                    va += MM256UInt32s;
                    vd += MM256UInt32s;
                    r -= MM256UInt32s;
                    offset += MM256UInt32s;
                }
                if (r > 0u) {
                    Vector256<UInt32> mask_a = Mask256.Lower(r);
                    a0 = MaskLoad(va, mask_a, va0, arr_a.Length);

                    /*mul lo*/
                    {
                        uint rem_d = (uint)arr_dst.Length - offset;
                        Vector256<UInt32> mask_d = Mask256.Lower(rem_d);
                        d0 = MaskLoad(vd, mask_d, vd0, arr_dst.Length);

                        (r0, c0) = Mul(a0, b0);

                        (c0, UInt32 carry) = CarryShift(c0, 0u);
                        (r0, carry) = FlushCarryAdd(r0, c0, carry);

                        (d0, c0) = Add(d0, r0);

                        (c0, carry) = CarryShift(c0, carry);
                        c0 = c0.WithElement(0, carry_lo_prev);
                        (d0, carry) = FlushCarryAdd(d0, c0, carry);

                        if (rem_d < MM256UInt32s) {
                            if (d0.GetElement((int)rem_d) > 0u) {
                                throw new OverflowException();
                            }
                            MaskStore(vd, d0, mask_d, vd0, arr_dst.Length);
                        }
                        else {
                            Store(vd, d0, vd0, arr_dst.Length);
                            Add(offset + MM256UInt32s, arr_dst, carry);
                        }
                    }

                    /*mul hi*/
                    {
                        uint rem_d = (uint)arr_dst.Length - offset - 1u;
                        Vector256<UInt32> mask_d = Mask256.Lower(rem_d);
                        d0 = MaskLoad(vd + 1u, mask_d, vd0, arr_dst.Length);

                        (r0, c0) = Mul(a0, b1);

                        (c0, UInt32 carry) = CarryShift(c0, 0u);
                        (r0, carry) = FlushCarryAdd(r0, c0, carry);

                        (d0, c0) = Add(d0, r0);

                        (c0, carry) = CarryShift(c0, carry);
                        c0 = c0.WithElement(0, carry_hi_prev);
                        (d0, carry) = FlushCarryAdd(d0, c0, carry);

                        if (rem_d < MM256UInt32s) {
                            if (d0.GetElement((int)rem_d) > 0u) {
                                throw new OverflowException();
                            }
                            MaskStore(vd + 1u, d0, mask_d, vd0, arr_dst.Length);
                        }
                        else {
                            Store(vd + 1u, d0, vd0, arr_dst.Length);
                            Add(offset + MM256UInt32s + 1u, arr_dst, carry);
                        }
                    }
                }
                else {
                    Add(offset, arr_dst, carry_lo_prev);
                    Add(offset + 1u, arr_dst, carry_hi_prev);
                }
            }
        }
    }
}
