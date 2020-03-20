﻿using System;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Truncate(MultiPrecision<N> x) {
            if (x.IsNaN) {
                return NaN;
            }

            if (x.Exponent >= Mantissa<N>.Bits) {
                return x;
            }
            if (x.Exponent < 0) {
                return Zero;
            }

            UInt32[] vs = x.mantissa.Value.ToArray();

            UIntUtil.FlushBit(vs, (int)x.Exponent);
            MultiPrecision<N> y = new MultiPrecision<N>(x.Sign, x.exponent, new Mantissa<N>(vs));

            return y;
        }

        public static MultiPrecision<N> Floor(MultiPrecision<N> x) {
            MultiPrecision<N> x_int = Truncate(x);
            MultiPrecision<N> x_frac = x - x_int;

            if (x_frac < Zero) {
                x_int -= 1;
            }

            return x_int;
        }

        public static MultiPrecision<N> Ceiling(MultiPrecision<N> x) {
            MultiPrecision<N> x_int = Truncate(x);
            MultiPrecision<N> x_frac = x - x_int;

            if (x_frac > Zero) {
                x_int += 1;
            }

            return x_int;
        }

        public static MultiPrecision<N> Round(MultiPrecision<N> x) {
            return Floor(x + Ldexp(One, -1));
        }
    }
}