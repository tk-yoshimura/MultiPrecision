using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecisionSpline {
    public static class SplineUtil<N> where N : struct, IConstant {
        public static MultiPrecision<N>[] FiniteDifference(IReadOnlyList<MultiPrecision<N>> xs, IReadOnlyList<MultiPrecision<N>> vs) {
            if (xs.Count != vs.Count) {
                throw new ArgumentException("Array lengths don't match.", $"{xs}, {vs}");
            }

            int n = xs.Count;

            if (n <= 2) {
                return Enumerable.Repeat<MultiPrecision<N>>(0, n).ToArray();
            }

            MultiPrecision<N>[] ms = new MultiPrecision<N>[n - 1];

            for (int i = 0; i < n - 1; i++) {
                MultiPrecision<N> x0 = xs[i], v0 = vs[i];
                MultiPrecision<N> x1 = xs[i + 1], v1 = vs[i + 1];

                ms[i] = (v1 - v0) / (x1 - x0);
            }

            MultiPrecision<N>[] rs = new MultiPrecision<N>[n];

            for (int i = 1; i < n - 1; i++) {
                rs[i] = (ms[i] + ms[i - 1]) / 2;
            }

            rs[0] = rs[^1] = 0;

            return rs;
        }
    }
}
