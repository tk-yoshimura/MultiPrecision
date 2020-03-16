using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> : IComparable<Accumulator<N>> {

        public static bool operator ==(Accumulator<N> a, Accumulator<N> b) {
            return UIntUtil.Equal(Length, a.arr, b.arr);
        }

        public static bool operator !=(Accumulator<N> a, Accumulator<N> b) {
            return !(a == b);
        }

        public static bool operator <=(Accumulator<N> a, Accumulator<N> b) {
            return UIntUtil.LessThanOrEqual(Length, a.arr, b.arr);
        }

        public static bool operator >=(Accumulator<N> a, Accumulator<N> b) {
            return UIntUtil.GreaterThanOrEqual(Length, a.arr, b.arr);
        }

        public static bool operator <(Accumulator<N> a, Accumulator<N> b) {
            return !(a >= b);
        }

        public static bool operator >(Accumulator<N> a, Accumulator<N> b) {
            return !(a <= b);
        }

        public int CompareTo([AllowNull] Accumulator<N> other) {
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
