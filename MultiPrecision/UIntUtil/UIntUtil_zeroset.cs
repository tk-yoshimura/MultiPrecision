using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Zeroset(UInt32[] value, uint index, uint length) {
            if (length <= 0) {
                return;
            }

#if DEBUG
            Debug<IndexOutOfRangeException>.Assert(checked(index + length) <= value.Length);
#endif

            uint length_sets = Mask256.UInt32Sets(length), length_rems = Mask256.UInt32Rems(length);

            fixed (UInt32* v = value) {
                for (uint i = 0; i < length_sets; i += Mask256.MM256UInt32s) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(i + index + Mask256.MM256UInt32s) <= value.Length);
#endif

                    Avx.Store(v + i + index, Vector256<UInt32>.Zero);
                }
                if (length_rems > 0) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(length_sets + index + length_rems) <= value.Length);
#endif

                    Avx2.MaskStore(v + length_sets + index, Mask256.LSV(length_rems), Vector256<UInt32>.Zero);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Zeroset(UInt32[] value) {
            uint length = (uint)value.Length;

            uint length_sets = Mask256.UInt32Sets(length), length_rems = Mask256.UInt32Rems(length);

            fixed (UInt32* v = value) {
                for (uint i = 0; i < length_sets; i += Mask256.MM256UInt32s) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(i + Mask256.MM256UInt32s) <= value.Length);
#endif

                    Avx.Store(v + i, Vector256<UInt32>.Zero);
                }
                if (length_rems > 0) {
#if DEBUG
                    Debug<IndexOutOfRangeException>.Assert(checked(length_sets + length_rems) <= value.Length);
#endif

                    Avx2.MaskStore(v + length_sets, Mask256.LSV(length_rems), Vector256<UInt32>.Zero);
                }
            }
        }
    }
}
