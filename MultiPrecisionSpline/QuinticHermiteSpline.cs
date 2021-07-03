using MultiPrecision;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecisionSpline {
    public class QuinticHermiteSpline<N> : CubicHermiteSpline<N> where N : struct, IConstant {

        public IReadOnlyList<MultiPrecision<N>> SecondGrads { private set; get; }

        public QuinticHermiteSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys)
            : base(xs, ys) {

            this.SecondGrads = CheckArray(ComputeSecondGrads());
        }

        public QuinticHermiteSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys, MultiPrecision<N>[] gs)
            : base(xs, ys, gs) {

            this.SecondGrads = CheckArray(ComputeSecondGrads());
        }

        public QuinticHermiteSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys, MultiPrecision<N>[] gs, MultiPrecision<N>[] ggs)
            : base(xs, ys, gs) {

            this.SecondGrads = CheckArray(ggs);
        }

        protected virtual MultiPrecision<N>[] ComputeSecondGrads() {
            if (Length <= 2) {
                return Enumerable.Repeat<MultiPrecision<N>>(0, Length).ToArray();
            }

            MultiPrecision<N>[] ggs = new MultiPrecision<N>[Length];

            for (int i = 1; i < Length - 1; i++) {
                (MultiPrecision<N> x0, MultiPrecision<N> y0) = Points[i - 1];
                (MultiPrecision<N> x1, MultiPrecision<N> y1) = Points[i];
                (MultiPrecision<N> x2, MultiPrecision<N> y2) = Points[i + 1];

                MultiPrecision<N> dx01 = x1 - x0, dx12 = x2 - x1, dx20 = x0 - x2;

                ggs[i] = -2 * (dx01 * y2 + dx12 * y0 + dx20 * y1) / (dx01 * dx12 * dx20);
            }

            ggs[0] = ggs[^1] = 0;

            return ggs;
        }

        public override MultiPrecision<N> Value(MultiPrecision<N> x) {
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
            MultiPrecision<N> gg0 = SecondGrads[index], gg1 = SecondGrads[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hgg0,
                MultiPrecision<N> hy1, MultiPrecision<N> hg1, MultiPrecision<N> hgg1) = HermiteBasicFunctions<N>.Quintic.Value(t);

            MultiPrecision<N> y = hy0 * y0 + hy1 * y1 + dx * (hg0 * g0 + hg1 * g1 + dx * (hgg0 * gg0 + hgg1 * gg1));

            return y;
        }

        public override MultiPrecision<N> Grad(MultiPrecision<N> x) {
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
            MultiPrecision<N> gg0 = SecondGrads[index], gg1 = SecondGrads[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hgg0,
                MultiPrecision<N> hy1, MultiPrecision<N> hg1, MultiPrecision<N> hgg1) = HermiteBasicFunctions<N>.Quintic.Grad(t);

            MultiPrecision<N> y = (hy0 * y0 + hy1 * y1) / dx + (hg0 * g0 + hg1 * g1) + dx * (hgg0 * gg0 + hgg1 * gg1);

            return y;
        }

        public virtual MultiPrecision<N> SecondGrad(MultiPrecision<N> x) {
            int index = SegmentIndex(x);

            if (index < 0 || index >= Length - 1) {
                return 0;
            }

            (MultiPrecision<N> x0, MultiPrecision<N> y0) = Points[index];
            (MultiPrecision<N> x1, MultiPrecision<N> y1) = Points[index + 1];
            MultiPrecision<N> g0 = Grads[index], g1 = Grads[index + 1];
            MultiPrecision<N> gg0 = SecondGrads[index], gg1 = SecondGrads[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hgg0,
                MultiPrecision<N> hy1, MultiPrecision<N> hg1, MultiPrecision<N> hgg1) = HermiteBasicFunctions<N>.Quintic.SecondGrad(t);

            MultiPrecision<N> y = ((hy0 * y0 + hy1 * y1) / dx + (hg0 * g0 + hg1 * g1)) / dx + (hgg0 * gg0 + hgg1 * gg1);

            return y;
        }
    }
}
