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
                    Consts.inv_pi = MultiPrecisionUtil.Convert<N, Plus1<N>>(MultiPrecision<Plus1<N>>.One / MultiPrecision<Plus1<N>>.PI);
                }

                return Consts.inv_pi;
            }
        }

        private static MultiPrecision<N> GeneratePI() {
            MultiPrecision<Plus1<N>> a = MultiPrecision<Plus1<N>>.One;
            MultiPrecision<Plus1<N>> b = MultiPrecision<Plus1<N>>.Ldexp(MultiPrecision<Plus1<N>>.Sqrt2, -1);
            MultiPrecision<Plus1<N>> t = MultiPrecision<Plus1<N>>.Ldexp(MultiPrecision<Plus1<N>>.One, -2);
            MultiPrecision<Plus1<N>> p = MultiPrecision<Plus1<N>>.One;

            for (long i = 1; i < Bits; i *= 2) {
                MultiPrecision<Plus1<N>> a_next = MultiPrecision<Plus1<N>>.Ldexp(a + b, -1);
                MultiPrecision<Plus1<N>> b_next = MultiPrecision<Plus1<N>>.Sqrt(a * b);
                MultiPrecision<Plus1<N>> t_next = t - p * (a - a_next) * (a - a_next);
                MultiPrecision<Plus1<N>> p_next = MultiPrecision<Plus1<N>>.Ldexp(p, 1);

                a = a_next;
                b = b_next;
                t = t_next;
                p = p_next;
            }

            MultiPrecision<Plus1<N>> c = a + b;
            MultiPrecision<Plus1<N>> y = c * c / MultiPrecision<Plus1<N>>.Ldexp(t, 2);

            return MultiPrecisionUtil.Convert<N, Plus1<N>>(y);
        }
    }
}
