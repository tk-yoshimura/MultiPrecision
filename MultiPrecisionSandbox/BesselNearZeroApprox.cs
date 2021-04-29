using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionSandbox {
    public static class BesselNearZeroApprox<N, M> where N : struct, IConstant where M : struct, IConstant {
        readonly static Dictionary<MultiPrecision<N>, BesselNearZeroCoef<M>> coef_table = new();

        static BesselNearZeroApprox() {
            if (MultiPrecision<N>.Bits > MultiPrecision<M>.Bits) {
                throw new ArgumentException();
            }
        }

        public static int BesselNearZeroConvergence(MultiPrecision<N> nu, MultiPrecision<N> z) {
            BesselNearZeroCoef<M> table;
            if (coef_table.ContainsKey(nu)) {
                table = coef_table[nu];
            }
            else {
                table = new BesselNearZeroCoef<M>(nu.Convert<M>());
                coef_table.Add(nu, table);
            }

            MultiPrecision<M> z_ex = z.Convert<M>();
            MultiPrecision<M> u = nu.IsZero ? z_ex : MultiPrecision<M>.Pow(z_ex, nu.Convert<M>());
            MultiPrecision<M> w = z_ex * z_ex;

            MultiPrecision<M> x = 0;

            for (int terms = 0; terms < int.MaxValue; terms++) {
                MultiPrecision<M> c = u * table.Value(terms);

                x += c;
                u *= w;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    return terms;
                }
            }

            return int.MaxValue;
        }

        public static MultiPrecision<N> Value(MultiPrecision<N> nu, MultiPrecision<N> z, int terms) {
            BesselNearZeroCoef<M> table;
            if (coef_table.ContainsKey(nu)) {
                table = coef_table[nu];
            }
            else {
                table = new BesselNearZeroCoef<M>(nu.Convert<M>());
                coef_table.Add(nu, table);
            }

            MultiPrecision<M> z_ex = z.Convert<M>();
            MultiPrecision<M> u = nu.IsZero ? z_ex : MultiPrecision<M>.Pow(z_ex, nu.Convert<M>());
            MultiPrecision<M> w = z_ex * z_ex;

            MultiPrecision<M> x = 0;

            for (int k = 0; k < terms; k++) {
                MultiPrecision<M> c = u * table.Value(k);

                x += c;
                u *= w;

                if (c.IsZero || x.Exponent - c.Exponent > MultiPrecision<Plus1<N>>.Bits) {
                    break;
                }
            }

            return x.Convert<N>();
        }
    }
}
