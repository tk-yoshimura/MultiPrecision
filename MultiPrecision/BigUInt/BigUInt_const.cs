using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        public static BigUInt<N, K> Zero { get; } = new BigUInt<N, K>();

        public static BigUInt<N, K> Full { get; } = new BigUInt<N, K>(Enumerable.Repeat(0xFFFFFFFFu, Length).ToArray());

        private static partial class Consts {
            public static Dictionary<UInt64, BigUInt<N, K>> integers = new Dictionary<UInt64, BigUInt<N, K>>();
            public static Dictionary<int, BigUInt<N, K>> decimals = new Dictionary<int, BigUInt<N, K>>();
        }

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

        public static BigUInt<N, K> Integer(UInt64 n) {
            if (!Consts.integers.ContainsKey(n)) {
                Consts.integers.Add(n, n);
            }

            return Consts.integers[n];
        }

        public static BigUInt<N, K> Decimal(int digits) {
            if (!Consts.decimals.ContainsKey(digits)) {
                Consts.decimals.Add(digits, GenerateDecimal(digits));
            }

            return Consts.decimals[digits];
        }

        private static BigUInt<N, K> GenerateDecimal(int digits) {
            if (digits < 1) {
                throw new ArgumentException(nameof(digits));
            }

            UInt64 v = 1;
            for (int i = 0; i < digits % UIntUtil.UInt64MaxDecimalDigits; i++) {
                v *= 10;
            }

            BigUInt<N, K> uint64_maxdec = UIntUtil.UInt64MaxDecimal;
            BigUInt<N, K> biguint_dec = v;

            for (int i = 0; i < digits / UIntUtil.UInt64MaxDecimalDigits; i++) {
                biguint_dec *= uint64_maxdec;
            }

            return biguint_dec;
        }
    }
}
