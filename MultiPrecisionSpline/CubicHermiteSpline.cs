using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MultiPrecisionSpline {
    public class CubicHermiteSpline<N> : Spline<N> where N : struct, IConstant {

        public ReadOnlyCollection<MultiPrecision<N>> Grads { private set; get; }

        public CubicHermiteSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys)
            : base(xs, ys) {

            this.Grads = Array.AsReadOnly(CheckArray(ComputeGrads()));
        }

        public CubicHermiteSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys, MultiPrecision<N>[] gs)
            : base(xs, ys) {

            this.Grads = Array.AsReadOnly(CheckArray(gs));
        }

        protected virtual MultiPrecision<N>[] ComputeGrads() {
            if (Length <= 1) {
                return new MultiPrecision<N>[] { 0 };
            }
            else if (Length <= 2) {
                MultiPrecision<N> g = (Ys[1] - Ys[0]) / (Xs[1] - Xs[0]);

                return new MultiPrecision<N>[] { g, g };
            }

            MultiPrecision<N>[] ms = new MultiPrecision<N>[Length - 1];

            for (int i = 0; i < Length - 1; i++) {
                MultiPrecision<N> x0 = Xs[i], y0 = Ys[i];
                MultiPrecision<N> x1 = Xs[i + 1], y1 = Ys[i + 1];

                ms[i] = (y1 - y0) / (x1 - x0);
            }

            MultiPrecision<N>[] gs = new MultiPrecision<N>[Length];

            for (int i = 1; i < Length - 1; i++) {
                gs[i] = (ms[i] + ms[i - 1]) / 2;
            }

            gs[0] = 2 * ms[0] - gs[1];
            gs[^1] = 2 * ms[^1] - gs[^2];

            return gs;
        }

        public override MultiPrecision<N> Value(MultiPrecision<N> x) {
            int index = SegmentIndex(x);

            if (index < 0) {
                return Ys[0] + (x - Xs[0]) * Grads[0];
            }
            if (index >= Length - 1) {
                return Ys[^1] + (x - Xs[^1]) * Grads[^1];
            }

            MultiPrecision<N> x0 = Xs[index], y0 = Ys[index], g0 = Grads[index];
            MultiPrecision<N> x1 = Xs[index + 1], y1 = Ys[index + 1], g1 = Grads[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> hy0, MultiPrecision<N> hg0,
                MultiPrecision<N> hy1, MultiPrecision<N> hg1) = HermiteBasicFunctions<N>.Cubic.Value(t);

            MultiPrecision<N> y = hy0 * y0 + hy1 * y1 + dx * (hg0 * g0 + hg1 * g1);

            return y;
        }

        public virtual MultiPrecision<N> Grad(MultiPrecision<N> x) {
            int index = SegmentIndex(x);

            if (index < 0) {
                return Grads[0];
            }
            if (index >= Length - 1) {
                return Grads[^1];
            }

            MultiPrecision<N> x0 = Xs[index], y0 = Ys[index], g0 = Grads[index];
            MultiPrecision<N> x1 = Xs[index + 1], y1 = Ys[index + 1], g1 = Grads[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> hy0, MultiPrecision<N> hg0,
                MultiPrecision<N> hy1, MultiPrecision<N> hg1) = HermiteBasicFunctions<N>.Cubic.Grad(t);

            MultiPrecision<N> y = (hy0 * y0 + hy1 * y1) / dx + (hg0 * g0 + hg1 * g1);

            return y;
        }
    }
}
