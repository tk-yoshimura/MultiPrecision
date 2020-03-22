using System;
using System.Collections.Generic;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static Accumulator<N> Zero { get; } = new Accumulator<N>(BigUInt<N, Pow2.N2>.Zero);

        public static Accumulator<N> One { get; } = new Accumulator<N>(Mantissa<N>.One) * new Accumulator<N>(Mantissa<N>.One);

        public static Accumulator<N> Full { get; } = new Accumulator<N>(BigUInt<N, Pow2.N2>.Full);

        private static partial class Consts {
            public static Dictionary<UInt64, Accumulator<N>> integers = new Dictionary<UInt64, Accumulator<N>>();
            public static Dictionary<int, Accumulator<N>> decimals = new Dictionary<int, Accumulator<N>>();
        }

        public static Accumulator<N> Integer(UInt64 n) {
            if (!Consts.integers.ContainsKey(n)) {
                Consts.integers.Add(n, new Accumulator<N>(BigUInt<N, Pow2.N2>.Integer(n)));
            }

            return Consts.integers[n];
        }

        public static Accumulator<N> Decimal(int digits) {
            if (!Consts.decimals.ContainsKey(digits)) {
                Consts.decimals.Add(digits, new Accumulator<N>(BigUInt<N, Pow2.N2>.Decimal(digits)));
            }

            return Consts.decimals[digits];
        }
    }
}
