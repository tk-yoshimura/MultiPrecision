namespace MultiPrecision {

    public static class MultiPrecisionEnumerableExpand {
        public static MultiPrecision<N> Sum<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            MultiPrecision<N> sum = MultiPrecision<N>.Zero, c = MultiPrecision<N>.Zero;

            foreach (MultiPrecision<N> v in source) {
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

        public static MultiPrecision<N> Variance<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            MultiPrecision<N> avg_sq = source.Select((v) => v * v).Average();
            MultiPrecision<N> sq_avg = MultiPrecision<N>.Square(source.Average());

            return avg_sq - sq_avg;
        }

        public static MultiPrecision<N> Min<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            MultiPrecision<N> min = MultiPrecision<N>.NaN;

            foreach (MultiPrecision<N> v in source) {
                min = (min <= v) ? min : v;
            }

            return min;
        }

        public static MultiPrecision<N> Max<N>(this IEnumerable<MultiPrecision<N>> source) where N : struct, IConstant {
            MultiPrecision<N> max = MultiPrecision<N>.NaN;

            foreach (MultiPrecision<N> v in source) {
                max = (max >= v) ? max : v;
            }

            return max;
        }

        public static int MinIndex<N>(this IReadOnlyList<MultiPrecision<N>> source) where N : struct, IConstant {
            if (!source.Any()) {
                return -1;
            }

            MultiPrecision<N> min = MultiPrecision<N>.NaN;

            int index = 0, min_index = 0;
            foreach (MultiPrecision<N> v in source) {
                if (!(min <= v)) {
                    min = v;
                    min_index = index;
                }
                index++;
            }

            if (MultiPrecision<N>.IsNaN(min)) {
                return -1;
            }

            return min_index;
        }

        public static int MaxIndex<N>(this IReadOnlyList<MultiPrecision<N>> source) where N : struct, IConstant {
            if (!source.Any()) {
                return -1;
            }

            MultiPrecision<N> max = MultiPrecision<N>.NaN;

            int index = 0, max_index = 0;
            foreach (MultiPrecision<N> v in source) {
                if (!(max >= v)) {
                    max = v;
                    max_index = index;
                }
                index++;
            }

            if (MultiPrecision<N>.IsNaN(max)) {
                return -1;
            }

            return max_index;
        }
    }
}
