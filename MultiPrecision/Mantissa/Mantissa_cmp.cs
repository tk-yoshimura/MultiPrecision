namespace MultiPrecision {
    public sealed partial class Mantissa<N> where N : struct, IConstant {

        public static bool operator ==(Mantissa<N> a, Mantissa<N> b) {
            for (int i = 0; i < Length; i++) {
                if (a.arr[i] != b.arr[i]) {
                    return false;
                }

            }

            return true;
        }

        public static bool operator !=(Mantissa<N> a, Mantissa<N> b) {
            return !(a == b);
        }

        public static bool operator <=(Mantissa<N> a, Mantissa<N> b) {
            for (int i = Length - 1; i >= 0; i--) {
                if (a.arr[i] > b.arr[i]) {
                    return false;
                }
                else if (a.arr[i] < b.arr[i]) {
                    return true;
                }
            }

            return true;
        }

        public static bool operator >=(Mantissa<N> a, Mantissa<N> b) {
            for (int i = Length - 1; i >= 0; i--) {
                if (a.arr[i] < b.arr[i]) {
                    return false;
                }
                else if (a.arr[i] > b.arr[i]) {
                    return true;
                }
            }

            return true;
        }

        public static bool operator <(Mantissa<N> a, Mantissa<N> b) {
            return !(a >= b);
        }

        public static bool operator >(Mantissa<N> a, Mantissa<N> b) {
            return !(a <= b);
        }
    }
}
