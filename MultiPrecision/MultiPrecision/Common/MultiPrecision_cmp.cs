﻿using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> : IComparable<MultiPrecision<N>> {

        public static bool operator ==([AllowNull] MultiPrecision<N> a, [AllowNull] MultiPrecision<N> b) {
            if (a is null && b is null) return true;
            if (a is null || b is null || IsNaN(a) || IsNaN(b)) return false;

            return ((a.Sign == b.Sign) && (a.exponent == b.exponent) && (a.mantissa == b.mantissa)) || (IsZero(a) && IsZero(b));
        }

        public static bool operator !=([AllowNull] MultiPrecision<N> a, [AllowNull] MultiPrecision<N> b) {
            return !(a == b);
        }

        public static bool operator <=(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsNaN(a) || IsNaN(b)) return false;

            if (a.Sign != b.Sign) {
                return a.Sign == Sign.Minus || (IsZero(a) && IsZero(b));
            }

            if (a.Sign == Sign.Plus) {
                if (a.exponent != b.exponent) {
                    return a.exponent < b.exponent;
                }

                return a.mantissa <= b.mantissa;
            }
            else {
                if (a.exponent != b.exponent) {
                    return a.exponent > b.exponent;
                }

                return a.mantissa >= b.mantissa;
            }
        }

        public static bool operator >=(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsNaN(a) || IsNaN(b)) return false;

            if (a.Sign != b.Sign) {
                return a.Sign == Sign.Plus || (IsZero(a) && IsZero(b));
            }

            if (a.Sign == Sign.Plus) {
                if (a.exponent != b.exponent) {
                    return a.exponent > b.exponent;
                }

                return a.mantissa >= b.mantissa;
            }
            else {
                if (a.exponent != b.exponent) {
                    return a.exponent < b.exponent;
                }

                return a.mantissa <= b.mantissa;
            }
        }

        public static bool operator <(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsNaN(a) || IsNaN(b)) return false;

            return !(a >= b);
        }

        public static bool operator >(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsNaN(a) || IsNaN(b)) return false;

            return !(a <= b);
        }

        public static bool NearlyEquals(MultiPrecision<N> a, MultiPrecision<N> b, MultiPrecision<N> eps) {
            return a - eps <= b && b <= a + eps;
        }

        public static bool NearlyEqualBits(MultiPrecision<N> a, MultiPrecision<N> b, int ignore_bits) {
            if (IsZero(a) && IsZero(b)) {
                return true;
            }

            if (IsNaN(a) || IsNaN(b) || (a.Sign != b.Sign)) {
                return false;
            }

            if (a.exponent == b.exponent) {
                int matchbits = Mantissa<N>.MatchBits(a.mantissa, b.mantissa);

                if ((Bits - matchbits) <= ignore_bits) {
                    return true;
                }

                BigUInt<N> diff = (a.mantissa > b.mantissa)
                    ? new BigUInt<N>(a.Mantissa) - new BigUInt<N>(b.Mantissa)
                    : new BigUInt<N>(b.Mantissa) - new BigUInt<N>(a.Mantissa);

                matchbits = (int)diff.LeadingZeroCount;

                return (Bits - matchbits) <= ignore_bits;
            }
            if (a.Exponent + 1 == b.Exponent || b.Exponent + 1 == a.Exponent) {
                MultiPrecision<N> diff = a - b;

                return (diff.Exponent + Bits - Math.Min(a.Exponent, b.Exponent)) <= ignore_bits;
            }

            return false;
        }

        public int CompareTo([AllowNull] MultiPrecision<N> other) {
            if (other is null) {
                return 1;
            }

            if (this < other) {
                return -1;
            }

            if (this == other) {
                return 0;
            }

            return 1;
        }
    }
}
