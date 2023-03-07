namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> HarmonicNumber(int n) {
            return Consts.Harmonic.Value(n);
        }

        private static partial class Consts {
            public static class Harmonic {
                private static readonly List<MultiPrecision<N>> a_table = new();
                private static MultiPrecision<Plus1<N>> a_last;

                static Harmonic() {
                    a_table.Add(0);
                    a_table.Add(1);
                    a_last = 1;
                }

                public static MultiPrecision<N> Value(int n) {
                    if (n < 0) {
                        throw new ArgumentOutOfRangeException(nameof(n));
                    }

                    if (n < a_table.Count) {
                        return a_table[n];
                    }

                    for (int k = a_table.Count; k <= n; k++) {
                        MultiPrecision<Plus1<N>> a = a_last + (MultiPrecision<Plus1<N>>.One / k);
                        a_table.Add(a.Convert<N>());
                        a_last = a;
                    }

                    return a_table[n];
                }
            }
        }
    }
}
