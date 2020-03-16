using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> : IComparable<Mantissa<N>> {

        public static bool operator ==(Mantissa<N> a, Mantissa<N> b) {
            return UIntUtil.Equal(Length, a.arr, b.arr);
        }

        public static bool operator !=(Mantissa<N> a, Mantissa<N> b) {
            return !(a == b);
        }

        public static bool operator <=(Mantissa<N> a, Mantissa<N> b) {
            return UIntUtil.LessThanOrEqual(Length, a.arr, b.arr);
        }

        public static bool operator >=(Mantissa<N> a, Mantissa<N> b) {
            return UIntUtil.GreaterThanOrEqual(Length, a.arr, b.arr);
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
