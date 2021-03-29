using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision {

    public static class MultiPrecisionEnumerableExpand {
        public static MultiPrecision<N> Sum<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            MultiPrecision<N> sum = MultiPrecision<N>.Zero, c = MultiPrecision<N>.Zero;

            foreach (var v in source) {
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

        public static MultiPrecision<N> Min<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            MultiPrecision<N> min = MultiPrecision<N>.NaN;

            foreach (var v in source) {
                min = !(min <= v) ? v : min;
            }

            return min;
        }

        public static MultiPrecision<N> Max<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            MultiPrecision<N> max = MultiPrecision<N>.NaN;

            foreach (var v in source) {
                max = !(max >= v) ? v : max;
            }

            return max;
        }

        public static int MinIndex<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            if (source.Count() <= 0) {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            MultiPrecision<N> min = MultiPrecision<N>.NaN;

            int index = 0, min_index = 0;
            foreach (var v in source) {
                if (!(min <= v)) {
                    min = v;
                    min_index = index;
                }
                index++;
            }

            return min_index;
        }

        public static int MaxIndex<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            if (source.Count() <= 0) {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            MultiPrecision<N> max = MultiPrecision<N>.NaN;

            int index = 0, max_index = 0;
            foreach (var v in source) {
                if (!(max >= v)) {
                    max = v;
                    max_index = index;
                }
                index++;
            }

            return max_index;
        }
    }
}
