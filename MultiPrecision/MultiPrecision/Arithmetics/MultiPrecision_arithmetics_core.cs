using System;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        internal static (Mantissa<N> n, Int64 exponent) Add((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            if (a.exponent > b.exponent) {
                Int64 d = a.exponent - b.exponent;

                if (d < Accumulator<N>.Bits) {
                    Accumulator<N> a_acc = new(a.n, Mantissa<N>.Bits - 1);
                    Accumulator<N> b_acc = new(b.n, Mantissa<N>.Bits - 1 - (int)d);

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
                    Accumulator<N> a_acc = new(a.n, Mantissa<N>.Bits - 1 - (int)d);
                    Accumulator<N> b_acc = new(b.n, Mantissa<N>.Bits - 1);

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
                    Accumulator<N> a_acc = new(a.n, Mantissa<N>.Bits - 1);
                    Accumulator<N> b_acc = new(b.n, Mantissa<N>.Bits - 1 - (int)d);

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
                    Accumulator<N> a_acc = new(a.n, Mantissa<N>.Bits - 1 - (int)d);
                    Accumulator<N> b_acc = new(b.n, Mantissa<N>.Bits - 1);

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
                Accumulator<N> a_acc = new(a.n, Mantissa<N>.Bits - 1);
                Accumulator<N> b_acc = new(b.n, Mantissa<N>.Bits - 1);

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
            Accumulator<N> a_acc = new(a.n, Mantissa<N>.Bits);
            Accumulator<N> b_acc = new(b.n);

            Accumulator<N> c_acc = Accumulator<N>.RoundDiv(a_acc, b_acc);

            (Mantissa<N> n, int sft) = c_acc.Mantissa;

            Int64 exponent = checked(a.exponent - b.exponent - sft + Mantissa<N>.Bits - 1);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, Int64 exponent, bool round) Add(Mantissa<N> a, UInt64 b, Int64 relative_exponent) {
            if (b == 0 || -relative_exponent > Bits) {
                return (a, 0, round: false);
            }
            if (relative_exponent > Bits) {
                return (b, relative_exponent, round: false);
            }

            int expands = BigUInt<Plus4<N>>.Length - BigUInt<N>.Length;

            (UInt32 b_hi, UInt32 b_lo) = UIntUtil.Unpack(b);

            BigUInt<Plus4<N>> a_acc = new BigUInt<Plus4<N>>(a.Value.ToArray(), 1);
            BigUInt<Plus4<N>> b_acc = new BigUInt<Plus4<N>>(new UInt32[] { b_lo, b_hi }, Length + 1);
            
            if (relative_exponent < 1) {
                b_acc = BigUInt<Plus4<N>>.RightShift(b_acc, checked((int)-relative_exponent) + 1);
            }
            else if(relative_exponent > 1){ 
                a_acc = BigUInt<Plus4<N>>.RightShift(a_acc, checked((int)relative_exponent) - 1);
            }

            BigUInt<Plus4<N>> c_acc = a_acc + b_acc;

            int lzc = c_acc.LeadingZeroCount;
            c_acc <<= lzc;

            Int64 exponent = UIntUtil.UInt32Bits * (expands - 1) - lzc + ((relative_exponent > 1) ? relative_exponent - 1 : 0);
            bool round = c_acc[expands - 1] > UIntUtil.UInt32Round;
            Mantissa<N> mantissa = new Mantissa<N>(c_acc.Value.Skip(expands).ToArray(), enable_clone: false);

            return (mantissa, exponent, round);
        }

        internal static (Mantissa<N> n, Int64 exponent, bool round, Sign sign) Diff(Mantissa<N> a, UInt64 b, Int64 relative_exponent) {
            if (b == 0 || -relative_exponent > Bits) {
                return (a, 0, round: false, Sign.Plus);
            }
            if (relative_exponent > Bits) {
                return (b, relative_exponent, round: false, Sign.Minus);
            }

            int expands = BigUInt<Plus4<N>>.Length - BigUInt<N>.Length;

            (UInt32 b_hi, UInt32 b_lo) = UIntUtil.Unpack(b);

            BigUInt<Plus4<N>> a_acc = new BigUInt<Plus4<N>>(a.Value.ToArray(), 1);
            BigUInt<Plus4<N>> b_acc = new BigUInt<Plus4<N>>(new UInt32[] { b_lo, b_hi }, Length + 1);
            
            if (relative_exponent < 1) {
                b_acc = BigUInt<Plus4<N>>.RightShift(b_acc, checked((int)-relative_exponent) + 1);
            }
            else if(relative_exponent > 1){ 
                a_acc = BigUInt<Plus4<N>>.RightShift(a_acc, checked((int)relative_exponent) - 1);
            }

            BigUInt<Plus4<N>> c_acc;
            Sign sign;

            if (a_acc > b_acc) {
                c_acc = a_acc - b_acc;
                sign = Sign.Plus;
            }
            else if (a_acc < b_acc) {
                c_acc = b_acc - a_acc;
                sign = Sign.Minus;
            }
            else {
                return (Mantissa<N>.Zero, 0, round: false, Sign.Plus);
            }

            int lzc = c_acc.LeadingZeroCount;
            c_acc <<= lzc;

            Int64 exponent = UIntUtil.UInt32Bits * (expands - 1) - lzc + ((relative_exponent > 1) ? relative_exponent - 1 : 0);
            bool round = c_acc[expands - 1] > UIntUtil.UInt32Round;
            Mantissa<N> mantissa = new Mantissa<N>(c_acc.Value.Skip(expands).ToArray(), enable_clone: false);

            return (mantissa, exponent, round, sign);
        }
    }
}
