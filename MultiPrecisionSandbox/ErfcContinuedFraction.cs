using MultiPrecision;

namespace MultiPrecisionSandbox {
    static class ErfcContinuedFraction<N> where N : struct, IConstant {

        public static MultiPrecision<N> Erfc(MultiPrecision<N> z, int frac_n) {
            MultiPrecision<N> c = z * MultiPrecision<N>.Exp(-z * z) / MultiPrecision<N>.Sqrt(MultiPrecision<N>.PI);
            MultiPrecision<N> f = Frac(z, frac_n);

            return c * f;
        }

        public static MultiPrecision<N> Frac(MultiPrecision<N> z, int n) {
            MultiPrecision<Plus1<N>> z_ex = z.Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> w = z_ex * z_ex;

            MultiPrecision<Plus1<N>> f = 
                (MultiPrecision<Plus1<N>>.Sqrt(25 + w * (440 + w * (488 + w * 16 * (10 + w))))
                 - 5 + w * 4 * (1 + w))
                / (20 + w * 8);

            for (long k = checked(4 * n - 3); k >= 1; k -= 4) {
                MultiPrecision<Plus1<N>> c0 = (k + 2) * f;
                MultiPrecision<Plus1<N>> c1 = w * ((k + 3) + MultiPrecision<Plus1<N>>.Ldexp(f, 1));
                MultiPrecision<Plus1<N>> d0 = checked((k + 1) * (k + 3)) + (4 * k + 6) * f;
                MultiPrecision<Plus1<N>> d1 = MultiPrecision<Plus1<N>>.Ldexp(c1, 1);

                f = w + k * (c0 + c1) / (d0 + d1);
            }

            MultiPrecision<Plus1<N>> y = 1 / f;

            return y.Convert<N>();
        }
    }
}
