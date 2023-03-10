namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        public static MultiPrecision<N> Random(Random random) {
            BigUInt<Plus1<N>> n = new(UIntUtil.Random(random, BigUInt<Plus1<N>>.Length, BigUInt<Plus1<N>>.Bits), enable_clone: false);

            uint lzc = n.LeadingZeroCount;
            int sft = UIntUtil.UInt32Bits - (int)lzc;
            BigUInt<N> n_sft = ((sft >= 0)
                ? BigUInt<Plus1<N>>.RightShift(n, sft, enable_clone: false)
                : BigUInt<Plus1<N>>.LeftShift(n, -sft, enable_clone: false)
            ).Convert<N>(check_overflow: false);

            return new MultiPrecision<N>(Sign.Plus, -(int)lzc - 1, new Mantissa<N>(n_sft), round: false);
        }
    }
}
