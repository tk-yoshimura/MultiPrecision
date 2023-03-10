using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> : IComparable<BigUInt<N>> {

        public static bool operator ==(BigUInt<N> a, BigUInt<N> b) {
            return UIntUtil.Equal((uint)Length, a.value, b.value);
        }

        public static bool operator !=(BigUInt<N> a, BigUInt<N> b) {
            return !(a == b);
        }

        public static bool operator <=(BigUInt<N> a, BigUInt<N> b) {
            return UIntUtil.LessThanOrEqual((uint)Length, a.value, b.value);
        }

        public static bool operator >=(BigUInt<N> a, BigUInt<N> b) {
            return UIntUtil.GreaterThanOrEqual((uint)Length, a.value, b.value);
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

        public static uint MatchBits(BigUInt<N> a, BigUInt<N> b) {
            return UIntUtil.MatchBits((uint)Length, a.value, b.value);
        }
    }
}
