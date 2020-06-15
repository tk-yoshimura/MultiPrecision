using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MultiPrecision;

namespace MultiPrecisionSandbox {
    public static class GammaCoef<N> where N : struct, IConstant {

        public static MultiPrecision<N>[] PSeries(MultiPrecision<N> g, int length) {
            MultiPrecision<N> p5 = MultiPrecision<N>.Ldexp(1, -1);
            MultiPrecision<N> g_p5 = g + p5;
            MultiPrecision<N> d32 = MultiPrecision<N>.Ldexp(3, -1);

            List<MultiPrecision<N>> fact = new List<MultiPrecision<N>>() {
                MultiPrecision<N>.Sqrt(MultiPrecision<N>.PI)
            };

            List<MultiPrecision<N>> pow = new List<MultiPrecision<N>>() {
                1 / MultiPrecision<N>.Sqrt(g_p5)
            };

            List<MultiPrecision<N>> exp = new List<MultiPrecision<N>>() {
                MultiPrecision<N>.Exp(g_p5)
            };

            List<MultiPrecision<N>> ps = new List<MultiPrecision<N>>();

            for (int k = 0; k < length; k++) { 
                MultiPrecision<N> p = 0;

                for(int j = 0; j <= k; j++) { 
                    p += Chebyshev<N>.Table(2 * k + 1, 2 * j + 1) * fact[j] * pow[j] * exp[j];
                }

                ps.Add(p);

                fact.Add(MultiPrecision<N>.Ldexp(fact.Last() * (2 * k + 1), -1));
                pow.Add(MultiPrecision<N>.Pow(k + g_p5 + 1, -k - d32));
                exp.Add(exp[0] + (k + 1) * MultiPrecision<N>.E);
            }

            return ps.ToArray();
        }
    }
}
