using System;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        public unsafe int LeadingZeroCount {
            get {
                uint cnt = 0;

                fixed (UInt32* v = value) {
                    for (int i = Length - 1; i >= 0; i--) {
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
}
