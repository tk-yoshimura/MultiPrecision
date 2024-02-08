namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> Zero { get; } = new Mantissa<N>(BigUInt<N>.Zero);

        public static Mantissa<N> One { get; } = new Mantissa<N>(Enumerable.Repeat(0u, Length - 1).Concat(new UInt32[] { 0x80000000u }).ToArray(), enable_clone: false);

        public static Mantissa<N> Full { get; } = new Mantissa<N>(BigUInt<N>.Full);

        private static partial class Consts {
            public static Dictionary<int, Mantissa<N>> decimals = [];
        }

        public static Mantissa<N> Decimal(int digits) {
            if (!Consts.decimals.TryGetValue(digits, out Mantissa<N> value)) {
                value = new Mantissa<N>(BigUInt<N>.Decimal(digits));
                Consts.decimals.Add(digits, value);
            }

            return value;
        }
    }
}
