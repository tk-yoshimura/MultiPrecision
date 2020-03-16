using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Add(MultiPrecision<N> a, MultiPrecision<N> b) {
            Accumulator<N> ret = v1.Copy();
            
            ret.Add(v2);

            return ret;
        }

        private static (Int64 exponent, MultiPrecision<N> n) Add((Int64 exponent, MultiPrecision<N> n) a, (Int64 exponent, MultiPrecision<N> n) b) {
            
        }
    }
}
