using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static int LeadingZeroCount(UInt32[] value) {
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static int LeadingZeroCount(UInt32 value) {
            uint cnt = Lzcnt.LeadingZeroCount(value);

            return checked((int)cnt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static int LeadingZeroCount(UInt64 value) {
            (UInt32 hi, UInt32 lo) = Unpack(value);

            if (hi == 0) {
                return LeadingZeroCount(lo) + UInt32Bits;
            }

            return LeadingZeroCount(hi);
        }
    }
}
