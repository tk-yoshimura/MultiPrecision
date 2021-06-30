using MultiPrecision;

namespace MultiPrecisionSpline {
    public class CatmullRomSpline<N> : CubicHermiteN3Spline<N> where N : struct, IConstant {
        public CatmullRomSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys)
            : base(xs, ys) { }

        protected override sealed MultiPrecision<N> ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y) pm1, (MultiPrecision<N> x, MultiPrecision<N> y) p0, (MultiPrecision<N> x, MultiPrecision<N> y) pp1) {
            (MultiPrecision<N> xm1, MultiPrecision<N> ym1) = pm1;
            (MultiPrecision<N> x0, MultiPrecision<N> y0) = p0;
            (MultiPrecision<N> xp1, MultiPrecision<N> yp1) = pp1;

            MultiPrecision<N> g = ((yp1 - y0) / (xp1 - x0) + (y0 - ym1) / (x0 - xm1)) / 2;

            return g;
        }
    }
}
