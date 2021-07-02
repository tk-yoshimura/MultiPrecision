using MultiPrecision;
using System.Collections.Generic;

namespace MultiPrecisionSpline {
    public class CubicHermiteSpline<N> : Spline<N> where N : struct, IConstant {

        public IReadOnlyList<MultiPrecision<N>> Grads { private set; get; }

        public CubicHermiteSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys) :
            base(xs, ys) {

            this.Grads = CheckArray(ComputeGrads());
        }

        protected virtual MultiPrecision<N>[] ComputeGrads() {
            if (Length <= 1) {
                return new MultiPrecision<N>[] { 0 };
            }
            else if (Length <= 2) {
                MultiPrecision<N> g = (Points[1].y - Points[0].y) / (Points[1].x - Points[0].x);

                return new MultiPrecision<N>[] { g, g };
            }

            MultiPrecision<N>[] ms = new MultiPrecision<N>[Length - 1];

            for (int i = 0; i < Length - 1; i++) {
                (MultiPrecision<N> x0, MultiPrecision<N> y0) = Points[i];
                (MultiPrecision<N> xp1, MultiPrecision<N> yp1) = Points[i + 1];

                ms[i] = (yp1 - y0) / (xp1 - x0);
            }

            MultiPrecision<N>[] gs = new MultiPrecision<N>[Length];

            for (int i = 1; i < Length - 1; i++) {
                gs[i] = (ms[i] + ms[i - 1]) / 2;
            }

            gs[0] = 2 * ms[0] - gs[1];
            gs[^1] = 2 * ms[^1] - gs[^2];

            return gs;
        }

        public override sealed MultiPrecision<N> Value(MultiPrecision<N> x) {
            int index = SegmentIndex(x);

            if (index < 0) {
                return Points[0].y + (x - Points[0].x) * Grads[0];
            }
            if (index >= Length - 1) {
                return Points[^1].y + (x - Points[^1].x) * Grads[^1];
            }

            (MultiPrecision<N> x0, MultiPrecision<N> y0) = Points[index];
            (MultiPrecision<N> x1, MultiPrecision<N> y1) = Points[index + 1];
            MultiPrecision<N> g0 = Grads[index], g1 = Grads[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1) = HermiteBasic(t);

            MultiPrecision<N> y = hy0 * y0 + hy1 * y1 + dx * (hg0 * g0 + hg1 * g1);

            return y;
        }

        public MultiPrecision<N> Grad(MultiPrecision<N> x) {
            int index = SegmentIndex(x);

            if (index < 0) {
                return Grads[0];
            }
            if (index >= Length - 1) {
                return Grads[^1];
            }

            (MultiPrecision<N> x0, MultiPrecision<N> y0) = Points[index];
            (MultiPrecision<N> x1, MultiPrecision<N> y1) = Points[index + 1];
            MultiPrecision<N> g0 = Grads[index], g1 = Grads[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1) = HermiteBasicGrad(t);

            MultiPrecision<N> y = (hy0 * y0 + hy1 * y1) / dx + (hg0 * g0 + hg1 * g1);

            return y;
        }

        public static (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1) HermiteBasic(MultiPrecision<N> t) {
            MultiPrecision<N> t2 = t * t;

            MultiPrecision<N> hy0 = 1 + t2 * (-3 + 2 * t);
            MultiPrecision<N> hg0 = t + t2 * (-2 + t);
            MultiPrecision<N> hy1 = t2 * (3 - 2 * t);
            MultiPrecision<N> hg1 = t2 * (-1 + t);

            return (hy0, hg0, hy1, hg1);
        }

        public static (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1) HermiteBasicGrad(MultiPrecision<N> t) {
            MultiPrecision<N> hy0 = t * (-6 + 6 * t);
            MultiPrecision<N> hg0 = 1 + t * (-4 + 3 * t);
            MultiPrecision<N> hy1 = -hy0;
            MultiPrecision<N> hg1 = t * (-2 + 3 * t);

            return (hy0, hg0, hy1, hg1);
        }
    }
}
