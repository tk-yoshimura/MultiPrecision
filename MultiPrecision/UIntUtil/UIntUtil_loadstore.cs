using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        /// <summary>Mask Load uint32 vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Vector256<UInt32> MaskLoad(UInt32* ptr, Vector256<UInt32> mask, UInt32* ptr0, int arr_length) {
#if DEBUG
            uint mask_bits = (uint)MoveMask(mask.AsSingle());
            uint mask_length = (mask_bits > 0) ? MM256UInt32s - Lzcnt.LeadingZeroCount(mask_bits << ShiftIDX3) : 0;

            Debug<AccessViolationException>.Assert(ptr >= ptr0 && ptr - ptr0 + mask_length <= arr_length);
#endif

            Vector256<UInt32> v0 = Avx2.MaskLoad(ptr, mask);

            return v0;
        }

        /// <summary>Load uint32 vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Vector256<UInt32> Load(UInt32* ptr, UInt32* ptr0, int arr_length) {
#if DEBUG
            Debug<AccessViolationException>.Assert(ptr >= ptr0 && ptr - ptr0 + MM256UInt32s <= arr_length);
#endif
            Vector256<UInt32> v0 = Avx2.LoadVector256(ptr);

            return v0;
        }

        /// <summary>Load uint32 vector x2</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe (Vector256<UInt32> v0, Vector256<UInt32> v1) LoadX2(UInt32* ptr, UInt32* ptr0, int arr_length) {
#if DEBUG
            Debug<AccessViolationException>.Assert(ptr >= ptr0 && ptr - ptr0 + MM256UInt32s * 2 <= arr_length);
#endif
            Vector256<UInt32> v0 = Avx2.LoadVector256(ptr);
            Vector256<UInt32> v1 = Avx2.LoadVector256(ptr + MM256UInt32s);

            return (v0, v1);
        }

        /// <summary>Load uint32 vector x4</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe (Vector256<UInt32> v0, Vector256<UInt32> v1, Vector256<UInt32> v2, Vector256<UInt32> v3) LoadX4(UInt32* ptr, UInt32* ptr0, int arr_length) {
#if DEBUG
            Debug<AccessViolationException>.Assert(ptr >= ptr0 && ptr - ptr0 + MM256UInt32s * 4 <= arr_length);
#endif      
            Vector256<UInt32> v0 = Avx2.LoadVector256(ptr);
            Vector256<UInt32> v1 = Avx2.LoadVector256(ptr + MM256UInt32s);
            Vector256<UInt32> v2 = Avx2.LoadVector256(ptr + MM256UInt32s * 2);
            Vector256<UInt32> v3 = Avx2.LoadVector256(ptr + MM256UInt32s * 3);

            return (v0, v1, v2, v3);
        }

        /// <summary>Mask Store uint32 vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void MaskStore(UInt32* ptr, Vector256<UInt32> v0, Vector256<UInt32> mask, UInt32* ptr0, int arr_length) {
#if DEBUG
            uint mask_bits = (uint)MoveMask(mask.AsSingle());
            uint mask_length = (mask_bits > 0) ? MM256UInt32s - Lzcnt.LeadingZeroCount(mask_bits << ShiftIDX3) : 0;

            Debug<AccessViolationException>.Assert(ptr >= ptr0 && ptr - ptr0 + mask_length <= arr_length);
#endif
            Avx2.MaskStore(ptr, mask, v0);
        }

        /// <summary>Store uint32 vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Store(UInt32* ptr, Vector256<UInt32> v0, UInt32* ptr0, int arr_length) {
#if DEBUG
            Debug<AccessViolationException>.Assert(ptr >= ptr0 && ptr - ptr0 + MM256UInt32s <= arr_length);
#endif
            Avx2.Store(ptr, v0);
        }

        /// <summary>Store uint32 vector x2</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void StoreX2(UInt32* ptr, Vector256<UInt32> v0, Vector256<UInt32> v1, UInt32* ptr0, int arr_length) {
#if DEBUG
            Debug<AccessViolationException>.Assert(ptr >= ptr0 && ptr - ptr0 + MM256UInt32s * 2 <= arr_length);
#endif
            Avx2.Store(ptr, v0);
            Avx2.Store(ptr + MM256UInt32s, v1);
        }

        /// <summary>Store uint32 vector x4</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void StoreX4(UInt32* ptr, Vector256<UInt32> v0, Vector256<UInt32> v1, Vector256<UInt32> v2, Vector256<UInt32> v3, UInt32* ptr0, int arr_length) {
#if DEBUG
            Debug<AccessViolationException>.Assert(ptr >= ptr0 && ptr - ptr0 + MM256UInt32s * 4 <= arr_length);
#endif
            Avx2.Store(ptr, v0);
            Avx2.Store(ptr + MM256UInt32s, v1);
            Avx2.Store(ptr + MM256UInt32s * 2, v2);
            Avx2.Store(ptr + MM256UInt32s * 3, v3);
        }
    }
}
