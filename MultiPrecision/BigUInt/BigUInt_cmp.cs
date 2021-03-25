using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> : IComparable<BigUInt<N>> {

        public static bool operator ==(BigUInt<N> a, BigUInt<N> b) {
            return UIntUtil.Equal(Length, a.value, b.value);
        }

        public static bool operator !=(BigUInt<N> a, BigUInt<N> b) {
            return !(a == b);
        }

        public static bool operator <=(BigUInt<N> a, BigUInt<N> b) {
            return UIntUtil.LessThanOrEqual(Length, a.value, b.value);
        }

        public static bool operator >=(BigUInt<N> a, BigUInt<N> b) {
            return UIntUtil.GreaterThanOrEqual(Length, a.value, b.value);
        }

        public static bool operator <(BigUInt<N> a, BigUInt<N> b) {
            return !(a >= b);
        }

        public static bool operator >(BigUInt<N> a, BigUInt<N> b) {
            return !(a <= b);
        }

        public int CompareTo([AllowNull] BigUInt<N> other) {
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

        public static int MatchBits(BigUInt<N> a, BigUInt<N> b) {
            return UIntUtil.MatchBits(Length, a.value, b.value);
        }
    }
}
