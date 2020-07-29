using System.Collections.Generic;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static Accumulator<N> Zero { get; } = new Accumulator<N>(BigUInt<Double<N>>.Zero);

        public static Accumulator<N> One { get; } = new Accumulator<N>(Mantissa<N>.One) * new Accumulator<N>(Mantissa<N>.One);

        public static Accumulator<N> Full { get; } = new Accumulator<N>(BigUInt<Double<N>>.Full);

        private static partial class Consts {
            public static Dictionary<int, Accumulator<N>> decimals = new Dictionary<int, Accumulator<N>>();
        }

        public static Accumulator<N> Decimal(int digits) {
            if (!Consts.decimals.ContainsKey(digits)) {
                Consts.decimals.Add(digits, new Accumulator<N>(BigUInt<Double<N>>.Decimal(digits)));
            }

            return Consts.decimals[digits];
        }
    }
}
