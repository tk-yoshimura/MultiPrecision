using System;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> Zero { get; } = new Mantissa<N>(BigUInt<N, Pow2.N1>.Zero);

        public static Mantissa<N> One { get; } = new Mantissa<N>(Enumerable.Repeat(0x00000000u, Length - 1).Concat(new UInt32[] { 0x80000000u }).ToArray());

        public static Mantissa<N> Full { get; } = new Mantissa<N>(BigUInt<N, Pow2.N1>.Full);
    }
}
