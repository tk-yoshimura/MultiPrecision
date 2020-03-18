using System;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> pi = null;
        }

        public static MultiPrecision<N> PI {
            get {
                if (Consts.pi is null) {
                    Consts.pi = GeneratePI();
                }

                return Consts.pi;
            }
        }

        private static MultiPrecision<N> GeneratePI() {
            MultiPrecision<N> a = One, b = Sqrt2 / 2, t = Ldexp(One, -2), p = One;

            for(long i = 1; i < Bits; i *= 2) {
                MultiPrecision<N> a_next = Ldexp(a + b, -1);
                MultiPrecision<N> b_next = Sqrt(a * b);
                MultiPrecision<N> t_next = t - p * (a - a_next) * (a - a_next);
                MultiPrecision<N> p_next = Ldexp(p, 1);

                a = a_next;
                b = b_next;
                t = t_next;
                p = p_next;
            }

            MultiPrecision<N> c = a + b;
            MultiPrecision<N> y = c * c / Ldexp(t, 2);

            return y;
        }
    }
}
