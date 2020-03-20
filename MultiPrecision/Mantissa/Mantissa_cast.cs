using System;
using System.Numerics;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static implicit operator BigInteger(Mantissa<N> n) {
            return n.value;
        }

        public static implicit operator Mantissa<N>(UInt32 v) {
            return new Mantissa<N>(v);
        }

        public static implicit operator Mantissa<N>(UInt64 v) {
            return new Mantissa<N>(v);
        }
    }
}
