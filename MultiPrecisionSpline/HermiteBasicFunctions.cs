using MultiPrecision;

namespace MultiPrecisionSpline {
    public static class HermiteBasicFunctions<N> where N : struct, IConstant {
        public static class Cubic {
            public static (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1) Value(MultiPrecision<N> t) {
                MultiPrecision<N> t2 = t * t;

                MultiPrecision<N> hy0 = 1 + t2 * (-3 + t * 2);
                MultiPrecision<N> hg0 = t + t2 * (-2 + t);
                MultiPrecision<N> hy1 = t2 * (3 - t * 2);
                MultiPrecision<N> hg1 = t2 * (-1 + t);

                return (hy0, hg0, hy1, hg1);
            }

            public static (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1) Grad(MultiPrecision<N> t) {
                MultiPrecision<N> hy0 = t * (-6 + t * 6);
                MultiPrecision<N> hg0 = 1 + t * (-4 + t * 3);
                MultiPrecision<N> hy1 = -hy0;
                MultiPrecision<N> hg1 = t * (-2 + t * 3);

                return (hy0, hg0, hy1, hg1);
            }
        }

        public static class Quintic {
            public static (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hgg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1, MultiPrecision<N> hgg1) Value(MultiPrecision<N> t) {
                MultiPrecision<N> t3 = t * t * t;

                MultiPrecision<N> hy0 = 1 + t3 * (-10 + t * (15 - t * 6));
                MultiPrecision<N> hg0 = t + t3 * (-6 + t * (8 - t * 3));
                MultiPrecision<N> hgg0 = (t * t + t3 * (-3 + t * (3 - t))) / 2;
                MultiPrecision<N> hy1 = t3 * (10 + t * (-15 + t * 6));
                MultiPrecision<N> hg1 = t3 * (-4 + t * (7 - t * 3));
                MultiPrecision<N> hgg1 = t3 * (1 + t * (-2 + t)) / 2;

                return (hy0, hg0, hgg0, hy1, hg1, hgg1);
            }

            public static (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hgg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1, MultiPrecision<N> hgg1) Grad(MultiPrecision<N> t) {
                MultiPrecision<N> t2 = t * t;

                MultiPrecision<N> hy0 = t2 * (-30 + t * (60 - t * 30));
                MultiPrecision<N> hg0 = 1 + t2 * (-18 + t * (32 - t * 15));
                MultiPrecision<N> hgg0 = t + t2 * (-9 + t * (12 - t * 5)) / 2;
                MultiPrecision<N> hy1 = -hy0;
                MultiPrecision<N> hg1 = t2 * (-12 + t * (28 - t * 15));
                MultiPrecision<N> hgg1 = t2 * (3 + t * (-8 + t * 5)) / 2;

                return (hy0, hg0, hgg0, hy1, hg1, hgg1);
            }

            public static (MultiPrecision<N> hy0, MultiPrecision<N> hg0, MultiPrecision<N> hgg0, MultiPrecision<N> hy1, MultiPrecision<N> hg1, MultiPrecision<N> hgg1) SecondGrad(MultiPrecision<N> t) {
                MultiPrecision<N> hy0 = t * (-60 + t * (180 - t * 120));
                MultiPrecision<N> hg0 = t * (-36 + t * (96 - t * 60));
                MultiPrecision<N> hgg0 = 1 + t * (-9 + t * (18 - t * 10));
                MultiPrecision<N> hy1 = -hy0;
                MultiPrecision<N> hg1 = t * (-24 + t * (84 - t * 60));
                MultiPrecision<N> hgg1 = t * (3 + t * (-12 + t * 10));

                return (hy0, hg0, hgg0, hy1, hg1, hgg1);
            }
        }
    }
}
