using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecisionSpline {
    public abstract class CubicHermiteSpline<N> where N : struct, IConstant {

        private readonly (MultiPrecision<N> x, MultiPrecision<N> y)[] ps;
        private readonly MultiPrecision<N>[] gs = null;

        public IReadOnlyList<(MultiPrecision<N> x, MultiPrecision<N> y)> Points => ps;

        public IReadOnlyList<MultiPrecision<N>> Grads => gs;

        public int Length => ps.Length;

        public CubicHermiteSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys) {
            if (xs.Length != ys.Length) {
                throw new ArgumentException("Array lengths don't match.", $"{xs}, {ys}");
            }
            if (xs.Length <= 0) {
                throw new ArgumentException("Array contains no elements.", $"{xs}, {ys}");
            }

            this.ps = xs.Select((v, i) => (x: v, y: ys[i])).OrderBy((v) => v.x).ToArray();

            if (xs.Length <= 1) {
                this.gs = new MultiPrecision<N>[] { 0 };
            }
            else if (xs.Length <= 2) {
                MultiPrecision<N> g = (ps[1].y - ps[0].y) / (ps[1].x - ps[0].x);

                this.gs = new MultiPrecision<N>[] { g, g };
            }
            else {
                this.gs = ComputeGrads(this.ps);
            }
        }

        protected abstract MultiPrecision<N>[] ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y)[] points);

        public MultiPrecision<N> Value(MultiPrecision<N> x) {
            int index = SegmentIndex(x);

            if (index < 0) {
                return ps[0].y + (x - ps[0].x) * gs[0];
            }
            if (index >= Length - 1) {
                return ps[^1].y + (x - ps[^1].x) * gs[^1];
            }

            (MultiPrecision<N> x0, MultiPrecision<N> y0) = ps[index];
            (MultiPrecision<N> x1, MultiPrecision<N> y1) = ps[index + 1];
            MultiPrecision<N> g0 = gs[index], g1 = gs[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> h00, MultiPrecision<N> h10, MultiPrecision<N> h01, MultiPrecision<N> h11) = HermiteBasic(t);

            MultiPrecision<N> y = h00 * y0 + h01 * y1 + dx * (h10 * g0 + h11 * g1);

            return y;
        }

        public MultiPrecision<N> Grad(MultiPrecision<N> x) {
            int index = SegmentIndex(x);

            if (index < 0) {
                return gs[0];
            }
            if (index >= Length - 1) {
                return gs[^1];
            }

            (MultiPrecision<N> x0, MultiPrecision<N> y0) = ps[index];
            (MultiPrecision<N> x1, MultiPrecision<N> y1) = ps[index + 1];
            MultiPrecision<N> g0 = gs[index], g1 = gs[index + 1];

            MultiPrecision<N> dx = x1 - x0, t = (x - x0) / dx;

            (MultiPrecision<N> h00, MultiPrecision<N> h10, MultiPrecision<N> h01, MultiPrecision<N> h11) = HermiteBasicGrad(t);

            MultiPrecision<N> y = (h00 * y0 + h01 * y1) / dx + (h10 * g0 + h11 * g1);

            return y;
        }

        public static (MultiPrecision<N> h00, MultiPrecision<N> h10, MultiPrecision<N> h01, MultiPrecision<N> h11) HermiteBasic(MultiPrecision<N> t) {
            MultiPrecision<N> a = t - 1;
            MultiPrecision<N> b = 2 * t + 1;
            MultiPrecision<N> c = 3 - 2 * t;
            MultiPrecision<N> d = MultiPrecision<N>.Square(t);
            MultiPrecision<N> e = MultiPrecision<N>.Square(a);

            return (b * e, t * e, c * d, a * d);
        }

        public static (MultiPrecision<N> h00, MultiPrecision<N> h10, MultiPrecision<N> h01, MultiPrecision<N> h11) HermiteBasicGrad(MultiPrecision<N> t) {
            MultiPrecision<N> a = t - 1;
            MultiPrecision<N> b = 6 * t * a;

            return (b, a * (3 * t - 1), -b, t * (3 * t - 2));
        }

        private int SegmentIndex(MultiPrecision<N> x) {
            if (ps[0].x >= x) {
                return -1;
            }
            if (ps[^1].x <= x) { 
                return Length - 1;
            }

            int index = 0;

            for (int h = Math.Max(1, Length / 2); h >= 1; h /= 2){
                for (int i = index; i < Length - h; i += h) {
                    if (ps[i + h].x > x) {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }
    }
}
