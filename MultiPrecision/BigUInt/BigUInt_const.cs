using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {

        public static BigUInt<N> Zero { get; } = new BigUInt<N>();

        public static BigUInt<N> Full { get; } = new BigUInt<N>(Enumerable.Repeat(~0u, Length).ToArray(), enable_clone: false);

        private static partial class Consts {
            public static Dictionary<int, BigUInt<N>> decimals = new();
        }

        public static BigUInt<N> Decimal(int digits) {
            if (!Consts.decimals.ContainsKey(digits)) {
                Consts.decimals.Add(digits, GenerateDecimal(digits));
            }

            return Consts.decimals[digits];
        }

        private static BigUInt<N> GenerateDecimal(int digits) {
            if (digits < 1) {
                throw new ArgumentOutOfRangeException(nameof(digits));
            }

            UInt64 v = 1;
            for (int i = 0; i < digits % UIntUtil.UInt64MaxDecimalDigits; i++) {
                v *= 10;
            }

            BigUInt<N> uint64_maxdec = UIntUtil.UInt64MaxDecimal;
            BigUInt<N> biguint_dec = v;

            for (int i = 0; i < digits / UIntUtil.UInt64MaxDecimalDigits; i++) {
                biguint_dec *= uint64_maxdec;
            }

            return biguint_dec;
        }
    }
}
