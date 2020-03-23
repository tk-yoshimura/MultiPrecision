using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision {

    public static class MultiPrecisionEnumerableExpand {
        public static MultiPrecision<N> Sum<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant { 
            MultiPrecision<N> sum = MultiPrecision<N>.Zero, c = MultiPrecision<N>.Zero;

            foreach(var v in source) { 
                MultiPrecision<N> y = v - c;
                MultiPrecision<N> t = sum + y;
                c = (t - sum) - y;
                sum = t;
            }
            
            return sum;
        }

        public static MultiPrecision<N> Average<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant { 
            return source.Sum() / source.Count();
        }
    }
}
