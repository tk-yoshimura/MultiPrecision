namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Rcp(MultiPrecision<N> x) {
            if (IsNaN(x)) {
                return NaN;
            }
            if (!IsFinite(x)) {
                return Zero;
            }
            if (IsZero(x)) {
                return x.Sign == Sign.Plus ? PositiveInfinity : NegativeInfinity;
            }

            MultiPrecision<Plus1<N>> x_expand = x.Convert<Plus1<N>>();

            MultiPrecision<Plus1<N>> v = new(Sign.Plus, 0, x_expand.mantissa, round: false);

            MultiPrecision<Plus1<N>> a = 1d / (double)v;
            MultiPrecision<Plus1<N>> h = 1 - v * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !MultiPrecision<Plus1<N>>.IsZero(h)) {
                MultiPrecision<Plus1<N>> squa_h = h * h;

                a *= 1 + (1 + squa_h) * (h + squa_h);
                h = 1 - v * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<Plus1<N>> y_expand = new(x.Sign, -x.Exponent + a.Exponent, a.mantissa, round: false);

            MultiPrecision<N> y = y_expand.Convert<N>();

            return y;
        }
    }
}
