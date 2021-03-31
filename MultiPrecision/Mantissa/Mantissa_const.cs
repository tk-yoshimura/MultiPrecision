using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> Zero { get; } = new Mantissa<N>(BigUInt<N>.Zero);

        public static Mantissa<N> One { get; } = new Mantissa<N>(Enumerable.Repeat(0u, Length - 1).Concat(new UInt32[] { 0x80000000u }).ToArray(), enable_clone: false);

        public static Mantissa<N> Full { get; } = new Mantissa<N>(BigUInt<N>.Full);

        private static partial class Consts {
            public static Dictionary<int, Mantissa<N>> decimals = new();
        }

        public static Mantissa<N> Decimal(int digits) {
            if (!Consts.decimals.ContainsKey(digits)) {
                Consts.decimals.Add(digits, new Mantissa<N>(BigUInt<N>.Decimal(digits)));
            }

            return Consts.decimals[digits];
        }
    }
}
