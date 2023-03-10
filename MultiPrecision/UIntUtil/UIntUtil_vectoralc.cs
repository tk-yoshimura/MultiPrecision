using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        /// <summary>Vector add with carry</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<UInt32> ret, Vector256<UInt32> carry) Add(Vector256<UInt32> a, Vector256<UInt32> b) {
            Vector256<UInt32> ret = Avx2.Add(a, b);
            Vector256<UInt32> carry = ShiftRightLogical(CompareGreaterThan(a, ret), UInt32Bits - 1);

            return (ret, carry);
        }

        /// <summary>Vector sub with carry</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<UInt32> ret, Vector256<UInt32> carry) Sub(Vector256<UInt32> a, Vector256<UInt32> b) {
            Vector256<UInt32> ret = Avx2.Subtract(a, b);
            Vector256<UInt32> carry = ShiftRightLogical(CompareLessThan(a, ret), UInt32Bits - 1);

            return (ret, carry);
        }

        /// <summary>Vector mul with carry</summary>
        /// <remarks>Parameter 'b' must have the same value every other one. e.g.&lt;v,0,v,0,v,0,v,0&gt;</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<UInt32> ret, Vector256<UInt32> carry) Mul(Vector256<UInt32> a, Vector256<UInt32> b) {
            Vector256<UInt32> zero = Vector256<UInt32>.Zero;

            Vector256<UInt32> al = UnpackLow(a, zero), ah = UnpackHigh(a, zero);

            Vector256<UInt32> a0 = Permute2x128(al, ah, 0b00100000), a1 = Permute2x128(al, ah, 0b00110001);

            Vector256<UInt32> x0 = Avx2.Multiply(a0, b).AsUInt32();
            Vector256<UInt32> x1 = Avx2.Multiply(a1, b).AsUInt32();

            Vector256<UInt32> r = Permute4x64(Shuffle(x0.AsSingle(), x1.AsSingle(), 0b10001000).AsDouble(), 0b11011000).AsUInt32();
            Vector256<UInt32> c = Permute4x64(Shuffle(x0.AsSingle(), x1.AsSingle(), 0b11011101).AsDouble(), 0b11011000).AsUInt32();

            return (r, c);
        }

        /// <summary>Left Shift carry vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<UInt32> r0, UInt32 carry)
            CarryShift(Vector256<UInt32> v0, UInt32 carry) {

            Vector256<UInt32> perm = Vector256.Create(7u, 0u, 1u, 2u, 3u, 4u, 5u, 6u);
            Vector256<UInt32> u0 = PermuteVar8x32(v0, perm);

            Vector256<UInt32> r0 = Blend(Vector256<UInt32>.Zero, u0, 0b11111110);

#if DEBUG
            checked {
#endif
                carry += u0.GetElement(0);
#if DEBUG
            }
#endif

            return (r0, carry);
        }

        /// <summary>Left Shift carry vector x2</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<UInt32> r0, Vector256<UInt32> r1, UInt32 carry)
            CarryShiftX2(Vector256<UInt32> v0, Vector256<UInt32> v1, UInt32 carry) {

            Vector256<UInt32> perm = Vector256.Create(7u, 0u, 1u, 2u, 3u, 4u, 5u, 6u);
            Vector256<UInt32> u0 = PermuteVar8x32(v0, perm);
            Vector256<UInt32> u1 = PermuteVar8x32(v1, perm);

            Vector256<UInt32> r0 = Blend(Vector256<UInt32>.Zero, u0, 0b11111110);
            Vector256<UInt32> r1 = Blend(u0, u1, 0b11111110);

#if DEBUG
            checked {
#endif
                carry += u1.GetElement(0);
#if DEBUG
            }
#endif

            return (r0, r1, carry);
        }

        /// <summary>Left Shift carry vector x3</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<UInt32> r0, Vector256<UInt32> r1, Vector256<UInt32> r2, UInt32 carry)
            CarryShiftX3(Vector256<UInt32> v0, Vector256<UInt32> v1, Vector256<UInt32> v2, UInt32 carry) {

            Vector256<UInt32> perm = Vector256.Create(7u, 0u, 1u, 2u, 3u, 4u, 5u, 6u);
            Vector256<UInt32> u0 = PermuteVar8x32(v0, perm);
            Vector256<UInt32> u1 = PermuteVar8x32(v1, perm);
            Vector256<UInt32> u2 = PermuteVar8x32(v2, perm);

            Vector256<UInt32> r0 = Blend(Vector256<UInt32>.Zero, u0, 0b11111110);
            Vector256<UInt32> r1 = Blend(u0, u1, 0b11111110);
            Vector256<UInt32> r2 = Blend(u1, u2, 0b11111110);

#if DEBUG
            checked {
#endif
                carry += u2.GetElement(0);
#if DEBUG
            }
#endif

            return (r0, r1, r2, carry);
        }

        /// <summary>Left Shift carry vector x4</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<UInt32> r0, Vector256<UInt32> r1, Vector256<UInt32> r2, Vector256<UInt32> r3, UInt32 carry)
            CarryShiftX4(Vector256<UInt32> v0, Vector256<UInt32> v1, Vector256<UInt32> v2, Vector256<UInt32> v3, UInt32 carry) {

            Vector256<UInt32> perm = Vector256.Create(7u, 0u, 1u, 2u, 3u, 4u, 5u, 6u);
            Vector256<UInt32> u0 = PermuteVar8x32(v0, perm);
            Vector256<UInt32> u1 = PermuteVar8x32(v1, perm);
            Vector256<UInt32> u2 = PermuteVar8x32(v2, perm);
            Vector256<UInt32> u3 = PermuteVar8x32(v3, perm);

            Vector256<UInt32> r0 = Blend(Vector256<UInt32>.Zero, u0, 0b11111110);
            Vector256<UInt32> r1 = Blend(u0, u1, 0b11111110);
            Vector256<UInt32> r2 = Blend(u1, u2, 0b11111110);
            Vector256<UInt32> r3 = Blend(u2, u3, 0b11111110);

#if DEBUG
            checked {
#endif
                carry += u3.GetElement(0);
#if DEBUG
            }
#endif

            return (r0, r1, r2, r3, carry);
        }

        /// <summary>Flush add carry vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<uint> r0, UInt32 carry)
            FlushCarryAdd(Vector256<uint> r0, Vector256<uint> c0, UInt32 carry) {

            while (!IsAllZero(c0)) {
                (r0, c0) = Add(r0, c0);
                (c0, carry) = CarryShift(c0, carry);
            }

            return (r0, carry);
        }

        /// <summary>Flush add carry vector x2</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<uint> r0, Vector256<uint> r1, UInt32 carry)
            FlushCarryAddX2(Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> c0, Vector256<uint> c1, UInt32 carry) {

            while (!IsAllZero(c0)) {
                (r0, c0) = Add(r0, c0);
                (r1, c1) = Add(r1, c1);
                (c0, c1, carry) = CarryShiftX2(c0, c1, carry);
            }

            (r1, carry) = FlushCarryAdd(r1, c1, carry);

            return (r0, r1, carry);
        }

        /// <summary>Flush add carry vector x3</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> r2, UInt32 carry)
            FlushCarryAddX3(Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> r2, Vector256<uint> c0, Vector256<uint> c1, Vector256<uint> c2, UInt32 carry) {

            while (!IsAllZero(c0)) {
                (r0, c0) = Add(r0, c0);
                (r1, c1) = Add(r1, c1);
                (r2, c2) = Add(r2, c2);
                (c0, c1, c2, carry) = CarryShiftX3(c0, c1, c2, carry);
            }

            (r1, r2, carry) = FlushCarryAddX2(r1, r2, c1, c2, carry);

            return (r0, r1, r2, carry);
        }

        /// <summary>Flush add carry vector x4</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> r2, Vector256<uint> r3, UInt32 carry)
            FlushCarryAddX4(Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> r2, Vector256<uint> r3, Vector256<uint> c0, Vector256<uint> c1, Vector256<uint> c2, Vector256<uint> c3, UInt32 carry) {

            while (!IsAllZero(c0)) {
                (r0, c0) = Add(r0, c0);
                (r1, c1) = Add(r1, c1);
                (r2, c2) = Add(r2, c2);
                (r3, c3) = Add(r3, c3);
                (c0, c1, c2, c3, carry) = CarryShiftX4(c0, c1, c2, c3, carry);
            }

            (r1, r2, r3, carry) = FlushCarryAddX3(r1, r2, r3, c1, c2, c3, carry);

            return (r0, r1, r2, r3, carry);
        }

        /// <summary>Flush sub carry vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<uint> r0, UInt32 carry)
            FlushCarrySub(Vector256<uint> r0, Vector256<uint> c0, UInt32 carry) {

            while (!IsAllZero(c0)) {
                (r0, c0) = Sub(r0, c0);
                (c0, carry) = CarryShift(c0, carry);
            }

            return (r0, carry);
        }

        /// <summary>Flush sub carry vector x2</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<uint> r0, Vector256<uint> r1, UInt32 carry)
            FlushCarrySubX2(Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> c0, Vector256<uint> c1, UInt32 carry) {

            while (!IsAllZero(c0)) {
                (r0, c0) = Sub(r0, c0);
                (r1, c1) = Sub(r1, c1);
                (c0, c1, carry) = CarryShiftX2(c0, c1, carry);
            }

            (r1, carry) = FlushCarrySub(r1, c1, carry);

            return (r0, r1, carry);
        }

        /// <summary>Flush sub carry vector x3</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> r2, UInt32 carry)
            FlushCarrySubX3(Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> r2, Vector256<uint> c0, Vector256<uint> c1, Vector256<uint> c2, UInt32 carry) {

            while (!IsAllZero(c0)) {
                (r0, c0) = Sub(r0, c0);
                (r1, c1) = Sub(r1, c1);
                (r2, c2) = Sub(r2, c2);
                (c0, c1, c2, carry) = CarryShiftX3(c0, c1, c2, carry);
            }

            (r1, r2, carry) = FlushCarrySubX2(r1, r2, c1, c2, carry);

            return (r0, r1, r2, carry);
        }

        /// <summary>Flush sub carry vector x4</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> r2, Vector256<uint> r3, UInt32 carry)
            FlushCarrySubX4(Vector256<uint> r0, Vector256<uint> r1, Vector256<uint> r2, Vector256<uint> r3, Vector256<uint> c0, Vector256<uint> c1, Vector256<uint> c2, Vector256<uint> c3, UInt32 carry) {

            while (!IsAllZero(c0)) {
                (r0, c0) = Sub(r0, c0);
                (r1, c1) = Sub(r1, c1);
                (r2, c2) = Sub(r2, c2);
                (r3, c3) = Sub(r3, c3);
                (c0, c1, c2, c3, carry) = CarryShiftX4(c0, c1, c2, c3, carry);
            }

            (r1, r2, r3, carry) = FlushCarrySubX3(r1, r2, r3, c1, c2, c3, carry);

            return (r0, r1, r2, r3, carry);
        }
    }
}
