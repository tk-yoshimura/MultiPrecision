﻿namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Truncate(MultiPrecision<N> x) {
            if (!IsFinite(x)) {
                return NaN;
            }

            if (x.Exponent >= Mantissa<N>.Bits) {
                throw new ArgumentException(
                    "The Truncate function was given an input value with no decimal precision.",
                    nameof(x)
                );
            }
            if (x.Exponent < 0) {
                return Zero;
            }

            UInt32[] vs = [.. x.mantissa.Value];

            UIntUtil.ZerosetLowerBit(vs, checked((uint)(Mantissa<N>.Bits - (int)x.Exponent - 1)));
            MultiPrecision<N> y = new(x.Sign, x.exponent, new Mantissa<N>(vs, enable_clone: false));

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
            if (x.Sign == Sign.Minus) {
                return Floor(x + Point5);
            }
            else {
                return Floor(TruncateMantissa(x, 1) + Point5);
            }
        }

        public static MultiPrecision<N> RoundMantissa(MultiPrecision<N> x, int round_bits) {
            if (round_bits < 0 || round_bits >= Bits) {
                throw new ArgumentOutOfRangeException(nameof(round_bits));
            }
            if (round_bits == 0 || x.mantissa.IsZero || IsNaN(x)) {
                return x;
            }

            Mantissa<N> n = Mantissa<N>.RightRoundShift(x.mantissa, round_bits);
            int lzc = n.LeadingZeroCount;
            n <<= lzc;

            return new MultiPrecision<N>(x.Sign, x.Exponent + round_bits - lzc, n, round: false);
        }

        public static MultiPrecision<N> TruncateMantissa(MultiPrecision<N> x, int truncate_bits) {
            if (truncate_bits < 0 || truncate_bits >= Bits) {
                throw new ArgumentOutOfRangeException(nameof(truncate_bits));
            }
            if (truncate_bits == 0 || x.mantissa.IsZero || IsNaN(x)) {
                return x;
            }

            Mantissa<N> n = Mantissa<N>.ZerosetLowerBit(x.mantissa, (uint)truncate_bits);

            return new MultiPrecision<N>(x.Sign, x.Exponent, n, round: false);
        }
    }
}
