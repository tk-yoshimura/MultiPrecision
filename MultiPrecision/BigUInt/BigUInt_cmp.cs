using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> : IComparable<BigUInt<N, K>> {

        public static bool operator ==(BigUInt<N, K> a, BigUInt<N, K> b) {
            return UIntUtil.Equal(Length, a.value, b.value);
        }

        public static bool operator !=(BigUInt<N, K> a, BigUInt<N, K> b) {
            return !(a == b);
        }

        public static bool operator <=(BigUInt<N, K> a, BigUInt<N, K> b) {
            return UIntUtil.LessThanOrEqual(Length, a.value, b.value);
        }

        public static bool operator >=(BigUInt<N, K> a, BigUInt<N, K> b) {
            return UIntUtil.GreaterThanOrEqual(Length, a.value, b.value);
        }

        public static bool operator <(BigUInt<N, K> a, BigUInt<N, K> b) {
            return !(a >= b);
        }

        public static bool operator >(BigUInt<N, K> a, BigUInt<N, K> b) {
            return !(a <= b);
        }

        public int CompareTo([AllowNull] BigUInt<N, K> other) {
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
