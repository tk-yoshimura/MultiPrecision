namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        public static MultiPrecision<N> Random(Random random) {
            BigUInt<Plus1<N>> n = new(UIntUtil.Random(random, BigUInt<Plus1<N>>.Length, BigUInt<Plus1<N>>.Bits), enable_clone: false);

            int sft = UIntUtil.UInt32Bits - (int)n.LeadingZeroCount;
            BigUInt<N> n_sft = ((sft >= 0)
                ? BigUInt<Plus1<N>>.RightShift(n, sft, enable_clone: false)
                : BigUInt<Plus1<N>>.LeftShift(n, -sft, enable_clone: false)
            ).Convert<N>();

            return new MultiPrecision<N>(Sign.Plus, Mantissa<N>.Bits - (int)sft - 1, new Mantissa<N>(n_sft), round: false);
        }
    }
}
