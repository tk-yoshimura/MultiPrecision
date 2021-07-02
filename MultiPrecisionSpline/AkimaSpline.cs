using MultiPrecision;

namespace MultiPrecisionSpline {
    public class AkimaSpline<N> : CubicHermiteN5Spline<N> where N : struct, IConstant {
        public AkimaSpline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys)
            : base(xs, ys) { }

        protected override sealed MultiPrecision<N> ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y) pm1, (MultiPrecision<N> x, MultiPrecision<N> y) p0, (MultiPrecision<N> x, MultiPrecision<N> y) pp1) {
            (MultiPrecision<N> xm1, MultiPrecision<N> ym1) = pm1;
            (MultiPrecision<N> x0, MultiPrecision<N> y0) = p0;
            (MultiPrecision<N> xp1, MultiPrecision<N> yp1) = pp1;
            
            MultiPrecision<N> mm1 = (y0 - ym1) / (x0 - xm1), mp1 = (yp1 - y0) / (xp1 - x0);

            return (mm1 + mp1) / 2;
        }

        protected override MultiPrecision<N> ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y) pm1, (MultiPrecision<N> x, MultiPrecision<N> y) p0, (MultiPrecision<N> x, MultiPrecision<N> y) pp1, (MultiPrecision<N> x, MultiPrecision<N> y) pp2) {
            (MultiPrecision<N> xm1, MultiPrecision<N> ym1) = pm1;
            (MultiPrecision<N> x0, MultiPrecision<N> y0) = p0;
            (MultiPrecision<N> xp1, MultiPrecision<N> yp1) = pp1;
            (MultiPrecision<N> xp2, MultiPrecision<N> yp2) = pp2;

            MultiPrecision<N> mm1 = (y0 - ym1) / (x0 - xm1), mp1 = (yp1 - y0) / (xp1 - x0), mp2 = (yp2 - yp1) / (xp2 - xp1);

            if (mm1 == mp1) {
                return mm1;
            }

            if (mp1 == mp2) {
                return (mm1 + mp1) / 2;
            }

            return mm1;
        }

        protected override MultiPrecision<N> ComputeGrads((MultiPrecision<N> x, MultiPrecision<N> y) pm2, (MultiPrecision<N> x, MultiPrecision<N> y) pm1, (MultiPrecision<N> x, MultiPrecision<N> y) p0, (MultiPrecision<N> x, MultiPrecision<N> y) pp1, (MultiPrecision<N> x, MultiPrecision<N> y) pp2) {
            (MultiPrecision<N> xm2, MultiPrecision<N> ym2) = pm2;
            (MultiPrecision<N> xm1, MultiPrecision<N> ym1) = pm1;
            (MultiPrecision<N> x0, MultiPrecision<N> y0) = p0;
            (MultiPrecision<N> xp1, MultiPrecision<N> yp1) = pp1;
            (MultiPrecision<N> xp2, MultiPrecision<N> yp2) = pp2;

            MultiPrecision<N> mm2 = (ym1 - ym2) / (xm1 - xm2), mm1 = (y0 - ym1) / (x0 - xm1);
            MultiPrecision<N> mp1 = (yp1 - y0) / (xp1 - x0), mp2 = (yp2 - yp1) / (xp2 - xp1);

            if (mm1 == mp1) {
                return mm1;
            }

            if (mm2 == mm1 && mp1 == mp2) {
                return (mm1 + mp1) / 2;
            }

            if (mm1 == mm2) {
                return mm1;
            }

            if (mp1 == mp2) {
                return mp1;
            }

            MultiPrecision<N> mm = MultiPrecision<N>.Abs(mm2 - mm1);
            MultiPrecision<N> mp = MultiPrecision<N>.Abs(mp1 - mp2);

            return (mp1 * mm + mm1 * mp) / (mm + mp);
        }
    }
}
