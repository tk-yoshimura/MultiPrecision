using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.Intrinsics.X86.Avx;
using static System.Runtime.Intrinsics.X86.Avx2;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        /// <summary>Comparate uint32 array a &gt; b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> CompareGreaterThan(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> gt = Xor(CompareEqual(Max(x, y), y), Vector256.Create(~0u));

            return gt;
        }

        /// <summary>Comparate uint32 array a &lt; b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> CompareLessThan(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> lt = Xor(CompareEqual(Max(y, x), x), Vector256.Create(~0u));

            return lt;
        }

        /// <summary>Comparate uint32 array a != b</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt32> CompareNotEqual(Vector256<UInt32> x, Vector256<UInt32> y) {
            Vector256<UInt32> neq = Xor(CompareEqual(x, y), Vector256.Create(~0u));

            return neq;
        }

        /// <summary>Judge all zero vector</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAllZero(Vector256<UInt32> x) => TestZ(x, x);

        /// <summary>Comparate vector a == b bits</summary>
        private static uint MaskEqual(Vector256<uint> a0, Vector256<uint> b0) {
            return (uint)MoveMask(CompareEqual(a0, b0).AsSingle());
        }

        /// <summary>Comparate vector a != b bits</summary>
        private static uint MaskNotEqual(Vector256<uint> a0, Vector256<uint> b0) {
            return (uint)MoveMask(CompareNotEqual(a0, b0).AsSingle());
        }

        /// <summary>Comparate vector a &lt;= b bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint MaskLessThan(Vector256<uint> a, Vector256<uint> b) {
            return (uint)MoveMask(CompareLessThan(a, b).AsSingle());
        }

        /// <summary>Comparate vector a &gt;= b bits</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint MaskGreaterThan(Vector256<uint> a, Vector256<uint> b) {
            return (uint)MoveMask(CompareGreaterThan(a, b).AsSingle());
        }
    }
}
