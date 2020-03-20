using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> : IComparable<Accumulator<N>> {

        public static bool operator ==(Accumulator<N> a, Accumulator<N> b) {
            return a.value == b.value;
        }

        public static bool operator !=(Accumulator<N> a, Accumulator<N> b) {
            return a.value != b.value;
        }

        public static bool operator <=(Accumulator<N> a, Accumulator<N> b) {
            return a.value <= b.value;
        }

        public static bool operator >=(Accumulator<N> a, Accumulator<N> b) {
            return a.value >= b.value;
        }

        public static bool operator <(Accumulator<N> a, Accumulator<N> b) {
            return a.value < b.value;
        }

        public static bool operator >(Accumulator<N> a, Accumulator<N> b) {
            return a.value > b.value;
        }

        public int CompareTo([AllowNull] Accumulator<N> other) {
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
