using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    public sealed partial class Mantissa<N> where N : struct, IConstant {

        public uint LeadingZeroCount {
            get {
                uint cnt = 0;

                for (int i = Length - 1; i >= 0; i--) {
                    if (arr[i] == 0) {
                        cnt += UIntUtil.UInt32Bits;
                    }
                    else {
                        cnt += Lzcnt.LeadingZeroCount(arr[i]);
                        break;
                    }
                }

                return cnt;
            }
        }
    }
}
