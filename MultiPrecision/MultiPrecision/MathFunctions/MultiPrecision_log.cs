﻿namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log(MultiPrecision<N> x) {
            MultiPrecision<Plus1<N>> y = MultiPrecision<Plus1<N>>.Log2(x.Convert<Plus1<N>>()) * MultiPrecision<Plus1<N>>.Ln2;

            return y.Convert<N>();
        }

        public static MultiPrecision<N> Log1p(MultiPrecision<N> x) {
            const int exp_threshold = 2;

            if (x.Exponent >= -exp_threshold) {
                return MultiPrecision<Plus1<N>>.Log(1 + x.Convert<Plus1<N>>()).Convert<N>();
            }

            MultiPrecision<Plus1<N>> x_expand = x.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> w = x_expand * x_expand;
            MultiPrecision<Plus1<N>> y = x_expand;
            MultiPrecision<Plus1<N>> z = w;
            MultiPrecision<Plus1<N>> s = x_expand - 1;

            for (long i = 2, f = 2; i < Bits; i += exp_threshold * 2, f += 2) {
                MultiPrecision<Plus1<N>> dy = z * (s * f - 1) / checked(f * (f + 1));
                y += dy;

                if (MultiPrecision<Plus1<N>>.IsZero(dy) || y.Exponent - dy.Exponent > Bits) {
                    break;
                }

                z *= w;
            }

            return y.Convert<N>();
        }
    }
}
