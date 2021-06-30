using MultiPrecision;

namespace MultiPrecisionSpline {
    public abstract class CubicHermiteN3Spline<N> : CubicHermiteSpline<N> where N : struct, IConstant {
        public CubicHermiteN3Spline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys)
            : base(xs, ys) {
        }

        protected override sealed MultiPrecision<N>[] ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y)[] points) { 
            MultiPrecision<N>[] gs = new MultiPrecision<N>[Length];

            for (int i = 1; i < Length - 1; i++) {
                gs[i] = ComputeGrads(points[i - 1], points[i], points[i + 1]);
            }

            {
                (MultiPrecision<N> x0, MultiPrecision<N> y0) = points[0];
                (MultiPrecision<N> x1, MultiPrecision<N> y1) = points[1];
                MultiPrecision<N> g1 = gs[1];

                gs[0] = 2 * (y1 - y0) / (x1 - x0) - g1;
            }

            {
                (MultiPrecision<N> x0, MultiPrecision<N> y0) = points[^2];
                (MultiPrecision<N> x1, MultiPrecision<N> y1) = points[^1];
                MultiPrecision<N> g0 = gs[^2];

                gs[^1] = 2 * (y1 - y0) / (x1 - x0) - g0;
            }

            return gs;
        }

        protected abstract MultiPrecision<N> ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y) pm1, (MultiPrecision<N> x, MultiPrecision<N> y) p0, (MultiPrecision<N> x, MultiPrecision<N> y) pp1);
    }
}
