using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MultiPrecision;

namespace MultiPrecisionSandbox {
    static class SterlingApprox<N> where N : struct, IConstant {
        static readonly Dictionary<int, MultiPrecision<Double<N>>> table = new();

        public static MultiPrecision<N> Gamma(MultiPrecision<N> z, int terms) {
            if (z < 0.5) {
                throw new ArgumentException(nameof(z));
            }

            MultiPrecision<Double<N>> z_ex = z.Convert<Double<N>>();

            MultiPrecision<Double<N>> r = MultiPrecision<Double<N>>.Sqrt(2 * MultiPrecision<Double<N>>.PI / z_ex);
            MultiPrecision<Double<N>> p = MultiPrecision<Double<N>>.Pow(z_ex / MultiPrecision<Double<N>>.E, z_ex);
            MultiPrecision<Double<N>> s = MultiPrecision<Double<N>>.Exp(SterlingTerm(z_ex, terms));

            MultiPrecision<Double<N>> y = r * p * s;

            return y.Convert<N>();
        }

        private static MultiPrecision<Double<N>> SterlingTerm(MultiPrecision<Double<N>> z, int terms) {
            MultiPrecision<Double<N>> v = 1 / z;
            MultiPrecision<Double<N>> w = v * v;

            MultiPrecision<Double<N>> x = 0, u = 1;

            for (int k = 1; k <= terms; k++) {
                MultiPrecision<Double<N>> c = u * SterlingCoef(k);

                x += c;
                u *= w;
            }

            MultiPrecision<Double<N>> y = x * v;

            return y;
        }

        private static MultiPrecision<Double<N>> SterlingCoef(int k) {
            if (!table.ContainsKey(k)) {
                MultiPrecision<Double<N>> c = MultiPrecision<Double<N>>.BernoulliSequence(k) / checked((2 * k) * (2 * k - 1));

                table.Add(k, c);
            }

            return table[k];
        }
    }
}
