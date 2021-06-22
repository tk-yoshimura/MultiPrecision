using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision {

    public static partial class MultiPrecisionUtil {
        public class RichardsonExtrapolation<N> where N : struct, IConstant {
            private static readonly List<MultiPrecision<N>> rs = new();

            private MultiPrecision<N>[] values = Enumerable.Empty<MultiPrecision<N>>().ToArray();

            public MultiPrecision<N> Epsilon { private set; get; } = MultiPrecision<N>.NaN;
            public MultiPrecision<N> Value => values.Length > 0 ? values.Last() : MultiPrecision<N>.NaN;
            public int Iterations => values.Length;

            public void Append(MultiPrecision<N> new_value) {
                if (values.Length <= 0) {
                    values = new MultiPrecision<N>[] { new_value };
                    return;
                }

                MultiPrecision<N>[] values_next = new MultiPrecision<N>[values.Length + 1];

                values_next[0] = new_value;

                for (int i = 1; i <= values.Length; i++) {
                    values_next[i] = values_next[i - 1] + (values_next[i - 1] - values[i - 1]) * R(i);
                }

                Epsilon = MultiPrecision<N>.Abs(values_next.Last() - values.Last());
                values = values_next;
            }

            internal static MultiPrecision<N> R(int i) {
                for (int k = rs.Count + 1; k <= i; k++) {
                    MultiPrecision<N> r = 1 / (MultiPrecision<N>.Ldexp(MultiPrecision<N>.One, checked(k * 2)) - 1);
                    rs.Add(r);
                }

                return rs[i - 1];
            }
        }
    }
}
