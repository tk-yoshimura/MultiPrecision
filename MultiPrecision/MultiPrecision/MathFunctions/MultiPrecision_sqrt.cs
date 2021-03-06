﻿using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Sqrt(MultiPrecision<N> x) {
            if (x.Sign == Sign.Minus || x.IsNaN) {
                return NaN;
            }
            if (!x.IsFinite) {
                return PositiveInfinity;
            }
            if (x.IsZero) {
                return Zero;
            }

            MultiPrecision<Plus1<N>> x_expand = x.Convert<Plus1<N>>();

            Int64 exponent = x_expand.Exponent;
            MultiPrecision<Plus1<N>> v = new(Sign.Plus, exponent % 2, x_expand.mantissa, round: false);

            MultiPrecision<Plus1<N>> a = 1d / Math.Sqrt((double)v);
            MultiPrecision<Plus1<N>> h = 1 - v * a * a;
            UInt32 h_exponent_prev = ExponentMax, h_exponent_post = h.exponent;

            while (h_exponent_prev > h_exponent_post && !h.IsZero) {
                a *= 1 + h * (4 + h * 3) / 8;
                h = 1 - v * a * a;

                h_exponent_prev = h_exponent_post;
                h_exponent_post = h.exponent;
            }

            MultiPrecision<Plus1<N>> y_expand = MultiPrecision<Plus1<N>>.Ldexp(v * a, (int)((exponent - exponent % 2) >> 1));

            MultiPrecision<N> y = y_expand.Convert<N>();

            return y;
        }
    }
}
