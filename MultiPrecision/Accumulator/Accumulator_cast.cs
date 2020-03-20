using System;
using System.Numerics;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static implicit operator BigInteger(Accumulator<N> n) {
            return n.value;
        }

        public static implicit operator Accumulator<N>(UInt32 v) {
            return new Accumulator<N>(v);
        }

        public static implicit operator Accumulator<N>(UInt64 v) {
            return new Accumulator<N>(v);
        }
    }
}
