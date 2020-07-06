namespace MultiPrecision {
    public static partial class MultiPrecisionUtil {

        public static (MultiPrecision<N> hi, MultiPrecision<N> lo) KahanSum<N>(MultiPrecision<N> hi, MultiPrecision<N> lo, MultiPrecision<N> v) where N : struct, IConstant { 
            MultiPrecision<N> y = v - lo;
            MultiPrecision<N> t = hi + y;
            lo = (t - hi) - y;
            hi = t;

            return (hi, lo);
        }
    }
}
