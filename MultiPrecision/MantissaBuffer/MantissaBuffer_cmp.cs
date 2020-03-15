namespace MultiPrecision {
    internal sealed partial class MantissaBuffer<N> where N : struct, IConstant {

        public static bool operator ==(MantissaBuffer<N> a, MantissaBuffer<N> b) {
            for (int i = 0; i < Length; i++) {
                if (a.arr[i] != b.arr[i]) {
                    return false;
                }

            }

            return true;
        }

        public static bool operator !=(MantissaBuffer<N> a, MantissaBuffer<N> b) {
            return !(a == b);
        }

        public static bool operator <=(MantissaBuffer<N> a, MantissaBuffer<N> b) {
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

        public static bool operator >=(MantissaBuffer<N> a, MantissaBuffer<N> b) {
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

        public static bool operator <(MantissaBuffer<N> a, MantissaBuffer<N> b) {
            return !(a >= b);
        }

        public static bool operator >(MantissaBuffer<N> a, MantissaBuffer<N> b) {
            return !(a <= b);
        }
    }
}
