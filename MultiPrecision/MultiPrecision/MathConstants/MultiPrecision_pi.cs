namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> pi = null, inv_pi = null;
        }

        public static MultiPrecision<N> PI {
            get {
                if (Consts.pi is null) {
                    Consts.pi = GeneratePI();
                }

                return Consts.pi;
            }
        }

        public static MultiPrecision<N> InvertPI {
            get {
                if (Consts.inv_pi is null) {
                    Consts.inv_pi = MultiPrecisionUtil.Convert<N, Next<N>>(MultiPrecision<Next<N>>.One / MultiPrecision<Next<N>>.PI);
                }

                return Consts.inv_pi;
            }
        }

        private static MultiPrecision<N> GeneratePI() {
            MultiPrecision<Next<N>> a = MultiPrecision<Next<N>>.One;
            MultiPrecision<Next<N>> b = MultiPrecision<Next<N>>.Ldexp(MultiPrecision<Next<N>>.Sqrt2, -1);
            MultiPrecision<Next<N>> t = MultiPrecision<Next<N>>.Ldexp(MultiPrecision<Next<N>>.One, -2);
            MultiPrecision<Next<N>> p = MultiPrecision<Next<N>>.One;

            for (long i = 1; i < Bits; i *= 2) {
                MultiPrecision<Next<N>> a_next = MultiPrecision<Next<N>>.Ldexp(a + b, -1);
                MultiPrecision<Next<N>> b_next = MultiPrecision<Next<N>>.Sqrt(a * b);
                MultiPrecision<Next<N>> t_next = t - p * (a - a_next) * (a - a_next);
                MultiPrecision<Next<N>> p_next = MultiPrecision<Next<N>>.Ldexp(p, 1);

                a = a_next;
                b = b_next;
                t = t_next;
                p = p_next;
            }

            MultiPrecision<Next<N>> c = a + b;
            MultiPrecision<Next<N>> y = c * c / MultiPrecision<Next<N>>.Ldexp(t, 2);

            return MultiPrecisionUtil.Convert<N, Next<N>>(y);
        }
    }
}
