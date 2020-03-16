using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> : IComparable<Mantissa<N>> {

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

        public int CompareTo([AllowNull] Mantissa<N> other) {
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
