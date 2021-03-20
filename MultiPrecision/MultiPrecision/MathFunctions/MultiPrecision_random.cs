using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        public static MultiPrecision<N> Random(Random random) {
            Accumulator<N> acc = new(new Mantissa<N>(UIntUtil.Random(random, Length, Bits)));

            (Mantissa<N> n, int sft) = acc.Mantissa;

            return new MultiPrecision<N>(Sign.Plus, Mantissa<N>.Bits - sft - 1, n, round: false);
        }
    }
}
