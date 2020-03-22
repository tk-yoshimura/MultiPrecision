using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static int LeadingZeroCount([DisallowNull] UInt32[] value) {
            uint cnt = 0;

            fixed (UInt32* v = value) {
                for (int i = value.Length - 1; i >= 0; i--) {
                    if (v[i] == 0) {
                        cnt += UIntUtil.UInt32Bits;
                    }
                    else {
                        cnt += Lzcnt.LeadingZeroCount(v[i]);
                        break;
                    }
                }
            }

            return checked((int)cnt);
        }
    }
}
