using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> : IComparable<MultiPrecision<N>> {

        public static bool operator ==(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) return false;
            
            return ((a.sign == b.sign) && (a.exponent == b.exponent) && (a.mantissa == b.mantissa)) || (a.IsZero && b.IsZero);
        }

        public static bool operator !=(MultiPrecision<N> a, MultiPrecision<N> b) {
            return !(a == b);
        }

        public static bool operator <=(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) return false;

            if (a.sign != b.sign) {
                return a.sign == Sign.Minus || (a.IsZero && b.IsZero);
            }

            if (a.sign == Sign.Plus) {
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
            if (a.IsNaN || b.IsNaN) return false;

            if (a.sign != b.sign) {
                return a.sign == Sign.Plus || (a.IsZero && b.IsZero);
            }

            if (a.sign == Sign.Plus) {
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
            if (a.IsNaN || b.IsNaN) return false;
            
            return !(a >= b);
        }

        public static bool operator >(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) return false;

            return !(a <= b);
        }

        public int CompareTo([AllowNull] MultiPrecision<N> other) {
            if(other is null) { 
                return 1;
            }

            if(this < other) { 
                return -1;
            }

            if(this == other) { 
                return 0;
            }

            return 1;
        }
    }
}
