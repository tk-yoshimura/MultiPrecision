using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        internal static (Mantissa<N> n, Int64 exponent) Add((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            if (a.exponent > b.exponent) {
                Int64 d = a.exponent - b.exponent;

                if (d < Accumulator<N>.Bits) {
                    Accumulator<N> a_acc = new Accumulator<N>(a.n, Mantissa<N>.Bits - 1);
                    Accumulator<N> b_acc = new Accumulator<N>(b.n, Mantissa<N>.Bits - 1 - (int)d);

                    Accumulator<N> c_acc = Accumulator<N>.Add(a_acc, b_acc);

                    (Mantissa<N> n, int sft) = c_acc.Mantissa;

                    Int64 exponent = checked(a.exponent - sft + 1);

                    return (n, exponent);
                }
                else {
                    return (a.n, a.exponent);
                }
            }
            else {
                Int64 d = b.exponent - a.exponent;

                if (d < Accumulator<N>.Bits) {
                    Accumulator<N> a_acc = new Accumulator<N>(a.n, Mantissa<N>.Bits - 1 - (int)d);
                    Accumulator<N> b_acc = new Accumulator<N>(b.n, Mantissa<N>.Bits - 1);

                    Accumulator<N> c_acc = Accumulator<N>.Add(a_acc, b_acc);

                    (Mantissa<N> n, int sft) = c_acc.Mantissa;

                    Int64 exponent = checked(b.exponent - sft + 1);

                    return (n, exponent);
                }
                else {
                    return (b.n, b.exponent);
                }
            }
        }

        internal static (Mantissa<N> n, Int64 exponent) Diff((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            if (a.exponent > b.exponent) {
                Int64 d = a.exponent - b.exponent;

                if (d < Accumulator<N>.Bits) {
                    Accumulator<N> a_acc = new Accumulator<N>(a.n, Mantissa<N>.Bits - 1);
                    Accumulator<N> b_acc = new Accumulator<N>(b.n, Mantissa<N>.Bits - 1 - (int)d);

                    Accumulator<N> c_acc = Accumulator<N>.Sub(a_acc, b_acc);

                    (Mantissa<N> n, int sft) = c_acc.Mantissa;

                    Int64 exponent = checked(a.exponent - sft + 1);

                    return (n, exponent);
                }
                else {
                    return (a.n, a.exponent);
                }
            }
            else if (a.exponent < b.exponent) {
                Int64 d = b.exponent - a.exponent;

                if (d < Accumulator<N>.Bits) {
                    Accumulator<N> a_acc = new Accumulator<N>(a.n, Mantissa<N>.Bits - 1 - (int)d);
                    Accumulator<N> b_acc = new Accumulator<N>(b.n, Mantissa<N>.Bits - 1);

                    Accumulator<N> c_acc = Accumulator<N>.Sub(b_acc, a_acc);

                    (Mantissa<N> n, int sft) = c_acc.Mantissa;

                    Int64 exponent = checked(b.exponent - sft + 1);

                    return (n, exponent);
                }
                else {
                    return (b.n, b.exponent);
                }
            }
            else {
                Accumulator<N> a_acc = new Accumulator<N>(a.n, Mantissa<N>.Bits - 1);
                Accumulator<N> b_acc = new Accumulator<N>(b.n, Mantissa<N>.Bits - 1);

                Accumulator<N> c_acc = (a.n > b.n) ? Accumulator<N>.Sub(a_acc, b_acc) : Accumulator<N>.Sub(b_acc, a_acc);

                if (c_acc.IsZero) {
                    return (Mantissa<N>.Zero, (Int64)ExponentMin - (Int64)ExponentMax);
                }
                else {
                    (Mantissa<N> n, int sft) = c_acc.Mantissa;

                    Int64 exponent = checked(a.exponent - sft + 1);

                    return (n, exponent);
                }
            }
        }

        internal static (Mantissa<N> n, Int64 exponent) Mul((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            Accumulator<N> c_acc = Mantissa<N>.Mul(a.n, b.n);

            (Mantissa<N> n, int sft) = c_acc.Mantissa;

            Int64 exponent = checked(a.exponent + b.exponent - sft + 1);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, Int64 exponent) Div((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            Accumulator<N> a_acc = new Accumulator<N>(a.n, Mantissa<N>.Bits);
            Accumulator<N> b_acc = new Accumulator<N>(b.n);

            Accumulator<N> c_acc = Accumulator<N>.RoundDiv(a_acc, b_acc);

            (Mantissa<N> n, int sft) = c_acc.Mantissa;

            Int64 exponent = checked(a.exponent - b.exponent - sft + Mantissa<N>.Bits - 1);

            return (n, exponent);
        }
    }
}
