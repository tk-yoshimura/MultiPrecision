using System.Collections.Generic;
using MultiPrecision;

namespace MultiPrecisionTest {
    public static class TestTool {

        public static IEnumerable<MultiPrecision<N>> EnumerateNeighbor<N>(MultiPrecision<N> v, int n = 10) where N : struct, IConstant {
            List<MultiPrecision<N>> vs = new List<MultiPrecision<N>>();

            MultiPrecision<N> u = v;
            for (int i = 0; i < n; i++) {
                u = MultiPrecision<N>.BitDecrement(u);
                vs.Add(u);
            }

            vs.Reverse();

            vs.Add(v);

            u = v;
            for (int i = 0; i < n; i++) {
                u = MultiPrecision<N>.BitIncrement(u);
                vs.Add(u);
            }

            return vs;
        }
    }
}
