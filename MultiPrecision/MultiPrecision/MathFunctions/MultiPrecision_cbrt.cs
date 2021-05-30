using System;
using System.Diagnostics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Cbrt(MultiPrecision<N> x) {
            if (x.IsNaN) {
                return NaN;
            }
            if (!x.IsFinite) {
                return (x.Sign == Sign.Plus) ? PositiveInfinity : NegativeInfinity;
            }
            if (x.IsZero) {
                return Zero;
            }

            MultiPrecision<Plus1<N>> x_expand = x.Convert<Plus1<N>>();

            Int64 exponent = x_expand.Exponent;
            MultiPrecision<Plus1<N>> v = new(Sign.Plus, exponent % 3, x_expand.mantissa, round: false);

            MultiPrecision<Plus1<N>> a = Math.Pow((double)v, -1d / 3d);
            MultiPrecision<Plus1<N>> h = 1 - v * a * a * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= 1 + h * (27 + h * (18 + h * 14)) / 81;
                h = 1 - v * a * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<Plus1<N>> y_expand = MultiPrecision<Plus1<N>>.Ldexp(v * a * a, (int)((exponent - exponent % 3) / 3));

            MultiPrecision<N> y = y_expand.Convert<N>();

            return (x.Sign == Sign.Plus) ? y : -y;
        }
    }
}
