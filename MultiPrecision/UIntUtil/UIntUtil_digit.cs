using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint Digits(UInt32[] value) {
            uint cnt = 0, r = (uint)value.Length;

            fixed (UInt32* v0 = value) {
                UInt32* v = v0 + r - MM256UInt32s;

                while (r >= MM256UInt32s) {
                    Vector256<UInt32> x = Load(v, v0, value.Length);
                    if (IsAllZero(x)) {
                        cnt += MM256UInt32s;
                        v -= MM256UInt32s;
                        r -= MM256UInt32s;
                    }
                    else {
                        uint flag = MaskNotEqual(x, Vector256<UInt32>.Zero) << ShiftIDX3;
                        cnt += LeadingZeroCount(flag);
                        r = 0;
                        break;
                    }
                }
                if (r > 0u) {
                    Vector256<UInt32> mask = Mask256.Lower(r);

                    Vector256<UInt32> x = MaskLoad(v0, mask, v0, value.Length);
                    if (IsAllZero(x)) {
                        cnt += r;
                    }
                    else {
                        uint flag = MaskNotEqual(x, Vector256<UInt32>.Zero) << (int)(ShiftIDX4 - r);
                        cnt += LeadingZeroCount(flag);
                    }
                }
            }

            return (uint)value.Length - cnt;
        }
    }
}
