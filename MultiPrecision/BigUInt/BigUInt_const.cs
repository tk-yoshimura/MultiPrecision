using System;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        public static BigUInt<N, K> Zero { get; } = new BigUInt<N, K>();

        public static BigUInt<N, K> Full { get; } = new BigUInt<N, K>(Enumerable.Repeat(0xFFFFFFFFu, Length).ToArray());

        public static BigUInt<N, K> MaxDecimal { private set; get; }

        static BigUInt() {
            UInt64 v = 1;
            for (int i = 0; i < MaxDecimalDigits % UIntUtil.UInt64MaxDecimalDigits; i++) {
                v *= 10;
            }

            BigUInt<N, K> uint64_maxdec = UIntUtil.UInt64MaxDecimal;
            BigUInt<N, K> biguint_maxdec = v;

            for (int i = 0; i < MaxDecimalDigits / UIntUtil.UInt64MaxDecimalDigits; i++) {
                biguint_maxdec *= uint64_maxdec;
            }

            MaxDecimal = biguint_maxdec;
        }
    }
}
