namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Exp(MultiPrecision<N> x) {
            MultiPrecision<Plus1<N>> y = MultiPrecision<Plus1<N>>.Pow2(x.Convert<Plus1<N>>() * MultiPrecision<Plus1<N>>.LbE);

            return y.Convert<N>();
        }

        public static MultiPrecision<N> Expm1(MultiPrecision<N> x) {
            if (x.Exponent >= 0) {
                return Exp(x) - 1;
            }

            MultiPrecision<Plus1<N>> x_expand = x.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> z = x_expand;
            MultiPrecision<Plus1<N>> y = MultiPrecision<Plus1<N>>.Zero;

            foreach (MultiPrecision<Plus1<N>> t in MultiPrecision<Plus1<N>>.TaylorSequence.Skip(1)) {
                MultiPrecision<Plus1<N>> dy = t * z;
                y += dy;

                if (dy.IsZero || y.Exponent - dy.Exponent > Bits) {
                    break;
                }

                z *= x_expand;
            }

            return y.Convert<N>();
        }
    }
}
