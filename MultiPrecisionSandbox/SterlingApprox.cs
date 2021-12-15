using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionSandbox {
    static class SterlingApprox<N> where N : struct, IConstant {
        static readonly Dictionary<int, MultiPrecision<Plus1<N>>> table = new();

        public static MultiPrecision<N> Gamma(MultiPrecision<N> z, int terms) {
            if (z < 0.5) {
                throw new ArgumentException(nameof(z));
            }

            MultiPrecision<Plus1<N>> z_ex = z.Convert<Plus1<N>>();

            MultiPrecision<Plus1<N>> r = MultiPrecision<Plus1<N>>.Sqrt(2 * MultiPrecision<Plus1<N>>.PI / z_ex);
            MultiPrecision<Plus1<N>> p = MultiPrecision<Plus1<N>>.Pow(z_ex / MultiPrecision<Plus1<N>>.E, z_ex);
            MultiPrecision<Plus1<N>> s = MultiPrecision<Plus1<N>>.Exp(SterlingTerm(z_ex, terms));

            MultiPrecision<Plus1<N>> y = r * p * s;

            return y.Convert<N>();
        }

        private static MultiPrecision<Plus1<N>> SterlingTerm(MultiPrecision<Plus1<N>> z, int terms) {
            MultiPrecision<Plus1<N>> v = 1 / z;
            MultiPrecision<Plus1<N>> w = v * v;

            MultiPrecision<Plus1<N>> x = 0, u = 1;

            for (int k = 1; k <= terms; k++) {
                MultiPrecision<Plus1<N>> c = u * SterlingCoef(k);

                x += c;
                u *= w;
            }

            MultiPrecision<Plus1<N>> y = x * v;

            return y;
        }

        public static int SterlingTermConvergence(MultiPrecision<Plus1<N>> z) {
            MultiPrecision<Plus1<N>> v = 1 / z;
            MultiPrecision<Plus1<N>> w = v * v;

            MultiPrecision<Plus1<N>> x = 0, u = 1, prev_c = null;

            for (int terms = 1; terms < int.MaxValue; terms++) {
                MultiPrecision<Plus1<N>> c = u * SterlingCoef(terms);

                x += c;
                u *= w;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<N>.Bits) {
                    return terms;
                }
                if (prev_c is not null && prev_c.Exponent < c.Exponent) {
                    return int.MaxValue;
                }

                prev_c = c;
            }

            return int.MaxValue;
        }

        private static MultiPrecision<Plus1<N>> SterlingCoef(int k) {
            if (!table.ContainsKey(k)) {
                MultiPrecision<Plus1<N>> c = MultiPrecision<Plus1<N>>.BernoulliSequence(k) / checked((2 * k) * (2 * k - 1));

                table.Add(k, c);
            }

            return table[k];
        }
    }
}