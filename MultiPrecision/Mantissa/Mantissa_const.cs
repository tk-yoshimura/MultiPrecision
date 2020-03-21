using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> Zero { get; } = new Mantissa<N>(BigUInt<N, Pow2.N1>.Zero);

        public static Mantissa<N> One { get; } = new Mantissa<N>(Enumerable.Repeat(0x00000000u, Length - 1).Concat(new UInt32[] { 0x80000000u }).ToArray());

        public static Mantissa<N> Full { get; } = new Mantissa<N>(BigUInt<N, Pow2.N1>.Full);

        private static partial class Consts {
            public static Dictionary<UInt64, Mantissa<N>> integers = new Dictionary<UInt64, Mantissa<N>>();
            public static Dictionary<int, Mantissa<N>> decimals = new Dictionary<int, Mantissa<N>>();
        }

        public static Mantissa<N> Integer(UInt64 n) {
            if (!Consts.integers.ContainsKey(n)) {
                Consts.integers.Add(n, new Mantissa<N>(BigUInt<N, Pow2.N1>.Integer(n)));
            }

            return Consts.integers[n];
        }

        public static Mantissa<N> Decimal(int digits) {
            if (!Consts.decimals.ContainsKey(digits)) {
                Consts.decimals.Add(digits, new Mantissa<N>(BigUInt<N, Pow2.N1>.Decimal(digits)));
            }

            return Consts.decimals[digits];
        }
    }
}
