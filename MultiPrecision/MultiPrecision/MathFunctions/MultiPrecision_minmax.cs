namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Min(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) {
                return NaN;
            }

            return (a < b) ? a : b;
        }

        public static MultiPrecision<N> Max(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) {
                return NaN;
            }

            return (a > b) ? a : b;
        }

        public static MultiPrecision<N> Clamp(MultiPrecision<N> v, MultiPrecision<N> min, MultiPrecision<N> max) {
            if (!(min <= max)) {
                throw new ArgumentException($"{nameof(min)},{nameof(max)}");
            }

            return Min(Max(v, min), max);
        }
    }
}
