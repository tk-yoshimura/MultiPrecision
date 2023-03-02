using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.Intrinsics.X86.Avx;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe (UInt64 n, uint lzc) TopUInt64(UInt32[] value) {
            uint cnt = 0, lzc = 0, length = (uint)value.Length, r = length;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r - MM256UInt32s;

                while (r >= MM256UInt32s) {
                    Vector256<UInt32> x = Load(v, v0, value.Length);
                    if (TestZ(x, x)) {
                        cnt += MM256UInt32s;
                        v -= MM256UInt32s;
                        r -= MM256UInt32s;
                    }
                    else {
                        uint flag = MaskNotEqual(x, Vector256<UInt32>.Zero) << ShiftIDX3;
                        uint idx = LeadingZeroCount(flag);
                        cnt += idx;
                        lzc = LeadingZeroCount(v[MM256UInt32s - 1 - idx]);
                        r = 0;
                        break;
                    }
                }
                if (r > 0u) {
                    Vector256<UInt32> mask = Mask256.Lower(r);

                    Vector256<UInt32> x = MaskLoad(v0, mask, v0, value.Length);
                    if (TestZ(x, x)) {
                        cnt += r;
                    }
                    else {
                        uint flag = MaskNotEqual(x, Vector256<UInt32>.Zero) << (int)(ShiftIDX4 - r);
                        uint idx = LeadingZeroCount(flag);
                        cnt += idx;
                        lzc = LeadingZeroCount(v0[r - 1 - idx]);
                    }
                }
            }

            uint lzc_all = cnt * UInt32Bits + lzc;
            uint digits = length - cnt;

            if (digits <= 0u) {
                return (0uL, lzc_all);
            }
            if (digits <= 1u) {
                return (Pack(value[0] << (int)lzc, 0u), lzc_all);
            }
            if (digits <= 2u) {
                return (Pack(value[1], value[0]) << (int)lzc, lzc_all);
            }

            if (lzc == 0) {
                UInt64 n = Pack(value[digits - 1u], value[digits - 2u]);

                return (n, lzc_all);
            }

            unchecked {
                UInt64 n = (Pack(value[digits - 1u], value[digits - 2u]) << (int)lzc) | (value[digits - 3u] >> (int)(UInt32Bits - lzc));

                return (n, lzc_all);
            }
        }
    }
}
