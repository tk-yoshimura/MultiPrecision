using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal sealed partial class MantissaBuffer<N> {

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
