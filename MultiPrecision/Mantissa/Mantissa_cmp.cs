using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> : IComparable<Mantissa<N>> {

        public static bool operator ==(Mantissa<N> a, Mantissa<N> b) {
            return a.value == b.value;
        }

        public static bool operator !=(Mantissa<N> a, Mantissa<N> b) {
            return a.value != b.value;
        }

        public static bool operator <=(Mantissa<N> a, Mantissa<N> b) {
            return a.value <= b.value;
        }

        public static bool operator >=(Mantissa<N> a, Mantissa<N> b) {
            return a.value >= b.value;
        }

        public static bool operator <(Mantissa<N> a, Mantissa<N> b) {
            return a.value < b.value;
        }

        public static bool operator >(Mantissa<N> a, Mantissa<N> b) {
            return a.value > b.value;
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
