using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

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
    }
}
