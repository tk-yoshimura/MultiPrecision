namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Min(MultiPrecision<N> a, MultiPrecision<N> b) {
            if(a.IsNaN || b.IsNaN) { 
                return NaN;
            }

            return (a < b) ? a : b;
        }

        public static MultiPrecision<N> Max(MultiPrecision<N> a, MultiPrecision<N> b) {
            if(a.IsNaN || b.IsNaN) { 
                return NaN;
            }

            return (a > b) ? a : b;
        }
    }
}
