using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionSandbox {

    static class GammaExpects<N> where N : struct, IConstant {
        private static readonly MultiPrecision<N> sqrt_pi;
        private static readonly Dictionary<int, MultiPrecision<Double<N>>> table;

        static GammaExpects() {
            sqrt_pi = MultiPrecision<N>.Sqrt(MultiPrecision<N>.PI);

            table = new Dictionary<int, MultiPrecision<Double<N>>>();

            table.Add(1, 1);
            table.Add(2, 1);
        }

        public static MultiPrecision<N> Gamma(int z2) {
            if (z2 < 1) {
                throw new ArgumentException(nameof(z2));
            }

            static MultiPrecision<Double<N>> gamma(int i) {
                if (table.ContainsKey(i)) {
                    return table[i];
                }

                MultiPrecision<Double<N>> y = MultiPrecision<Double<N>>.Ldexp(i - 2, -1) * gamma(i - 2);

                table.Add(i, y);
                return y;
            }

            MultiPrecision<N> g = gamma(z2).Convert<N>();

            return (z2 % 2 == 0) ? g : (g * sqrt_pi);
        }
    }
}
