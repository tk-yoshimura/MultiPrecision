namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> where N : struct, IConstant {

        public static bool operator ==(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) return false;

            return (a.sign == b.sign) && (a.exponent == b.exponent) && (a.mantissa == b.mantissa);
        }

        public static bool operator !=(MultiPrecision<N> a, MultiPrecision<N> b) {
            return !(a == b);
        }

        public static bool operator <=(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) return false;

            if (a.sign != b.sign) {
                return a.sign == Sign.Minus;
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
                return a.sign == Sign.Minus;
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
    }
}
