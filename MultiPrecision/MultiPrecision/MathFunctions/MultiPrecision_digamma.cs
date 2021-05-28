namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Digamma(MultiPrecision<N> z) {
            if (z.IsNaN || (z.Sign == Sign.Minus && !z.IsFinite)) {
                return NaN;
            }

            if (z.IsZero || (z.Sign == Sign.Plus && !z.IsFinite)) {
                return PositiveInfinity;
            }

            if (z.Sign == Sign.Minus || z.Exponent < -1) {
                MultiPrecision<N> tanpi = TanPI(z);

                if (tanpi.IsZero) {
                    return NaN;
                }

                MultiPrecision<N> y = Digamma(1 - z) - PI / tanpi;

                return y;
            }
            else {
                if (z < Consts.Gamma.Threshold) {
                    MultiPrecision<LanczosExpand<N>> x = DiffLogLanczosAg(z);
                    MultiPrecision<LanczosExpand<N>> s = z.Convert<LanczosExpand<N>>() + Consts.Gamma.Lanczos.G - MultiPrecision<LanczosExpand<N>>.Point5;
                    MultiPrecision<LanczosExpand<N>> t = MultiPrecision<LanczosExpand<N>>.Log(s);

                    MultiPrecision<LanczosExpand<N>> y_ex = t - Consts.Gamma.Lanczos.G / s + x;

                    MultiPrecision<N> y = y_ex.Convert<N>();

                    return y;
                }
                else {
                    MultiPrecision<SterlingExpand<N>> z_ex = z.Convert<SterlingExpand<N>>();

                    MultiPrecision<SterlingExpand<N>> p = MultiPrecision<SterlingExpand<N>>.Log(z_ex);
                    MultiPrecision<SterlingExpand<N>> s = DiffLogSterlingTerm(z_ex);

                    MultiPrecision<SterlingExpand<N>> y = p - 1 / (2 * z_ex) - s;

                    return y.Convert<N>();
                }
            }
        }

        private static MultiPrecision<LanczosExpand<N>> DiffLogLanczosAg(MultiPrecision<N> z) {
            MultiPrecision<Double<LanczosExpand<N>>> x_ex = Consts.Gamma.Lanczos.Coef[0];
            MultiPrecision<Double<LanczosExpand<N>>> y_ex = 0;
            MultiPrecision<Double<LanczosExpand<N>>> z_ex = (z - 1).Convert<Double<LanczosExpand<N>>>();

            for (int i = 1; i < Consts.Gamma.Lanczos.N; i++) {
                MultiPrecision<Double<LanczosExpand<N>>> w = Consts.Gamma.Lanczos.Coef[i];
                MultiPrecision<Double<LanczosExpand<N>>> r = 1 / (z_ex + i);
                MultiPrecision<Double<LanczosExpand<N>>> c = w * r;

                x_ex += c;
                y_ex -= c * r; 
            }

            MultiPrecision<LanczosExpand<N>> x = (y_ex / x_ex).Convert<LanczosExpand<N>>();
            return x;
        }

        private static MultiPrecision<SterlingExpand<N>> DiffLogSterlingTerm(MultiPrecision<SterlingExpand<N>> z) {
            MultiPrecision<SterlingExpand<N>> v = 1 / z;
            MultiPrecision<SterlingExpand<N>> w = v * v;

            MultiPrecision<SterlingExpand<N>> x = 0, u = w;
            long k = 1;

            foreach (MultiPrecision<SterlingExpand<N>> s in Consts.Gamma.Sterling.Coef) {
                MultiPrecision<SterlingExpand<N>> c = k * (u * s);

                x += c;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<SterlingExpand<N>>.Bits) {
                    break;
                }

                k += 2;
                u *= w;
            }

            return x;
        }
    }
}
