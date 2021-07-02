using MultiPrecision;

namespace MultiPrecisionSpline {
    public abstract class CubicHermiteN5Spline<N> : CubicHermiteSpline<N> where N : struct, IConstant {
        public CubicHermiteN5Spline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys)
            : base(xs, ys) {
        }

        protected override sealed MultiPrecision<N>[] ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y)[] points) {
            MultiPrecision<N>[] gs = new MultiPrecision<N>[Length];

            for (int i = 2; i < Length - 2; i++) {
                gs[i] = ComputeGrads(points[i - 2], points[i - 1], points[i], points[i + 1], points[i + 2]);
            }

            if (Length == 3) {
                gs[1] = ComputeGrads(points[0], points[1], points[2]);
            }
            else { 
                gs[1]  = ComputeGrads(points[0], points[1], points[2], points[3]);
                gs[^2] = -ComputeGrads(points[^1], points[^2], points[^3], points[^4]);
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

        protected abstract MultiPrecision<N> ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y) pm1, (MultiPrecision<N> x, MultiPrecision<N> y) p0, (MultiPrecision<N> x, MultiPrecision<N> y) pp1, (MultiPrecision<N> x, MultiPrecision<N> y) pp2);

        protected abstract MultiPrecision<N> ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y) pm2, (MultiPrecision<N> x, MultiPrecision<N> y) pm1, (MultiPrecision<N> x, MultiPrecision<N> y) p0, (MultiPrecision<N> x, MultiPrecision<N> y) pp1, (MultiPrecision<N> x, MultiPrecision<N> y) pp2);
    }
}
