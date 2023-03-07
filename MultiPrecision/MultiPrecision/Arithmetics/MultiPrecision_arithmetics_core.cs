namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        internal static (Mantissa<N> n, Int64 exponent) Add((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            if (a.exponent > b.exponent) {
                Int64 d = a.exponent - b.exponent;

                if (d <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int sft) = Mantissa<N>.Add(a.n, b.n, checked((int)d));
                    Int64 exponent = checked(a.exponent + sft);

                    return (n, exponent);
                }
                else {
                    return (a.n, a.exponent);
                }
            }
            else {
                Int64 d = b.exponent - a.exponent;

                if (d <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int sft) = Mantissa<N>.Add(b.n, a.n, checked((int)d));
                    Int64 exponent = checked(b.exponent + sft);

                    return (n, exponent);
                }
                else {
                    return (b.n, b.exponent);
                }
            }
        }

        internal static (Mantissa<N> n, Int64 exponent, Sign sign) Diff((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            if (a.exponent > b.exponent) {
                Int64 d = a.exponent - b.exponent;

                if (d <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int sft) = Mantissa<N>.Sub(a.n, b.n, checked((int)d));
                    Int64 exponent = checked(a.exponent + sft);

                    return (n, exponent, Sign.Plus);
                }
                else {
                    return (a.n, a.exponent, Sign.Plus);
                }
            }
            else if (a.exponent < b.exponent) {
                Int64 d = b.exponent - a.exponent;

                if (d <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int sft) = Mantissa<N>.Sub(b.n, a.n, checked((int)d));
                    Int64 exponent = checked(b.exponent + sft);

                    return (n, exponent, Sign.Minus);
                }
                else {
                    return (b.n, b.exponent, Sign.Minus);
                }
            }
            else {
                ((Mantissa<N> n, int sft), Sign sign) = (a.n >= b.n)
                    ? (Mantissa<N>.Sub(a.n, b.n, 0), Sign.Plus)
                    : (Mantissa<N>.Sub(b.n, a.n, 0), Sign.Minus);

                if (n.IsZero) {
                    return (Mantissa<N>.Zero, (Int64)ExponentMin - (Int64)ExponentMax, sign);
                }
                else {
                    Int64 exponent = checked(a.exponent + sft);

                    return (n, exponent, sign);
                }
            }
        }

        internal static (Mantissa<N> n, Int64 exponent) Mul((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            (Mantissa<N> n, int sft) = Mantissa<N>.Mul(a.n, b.n);

            Int64 exponent = checked(a.exponent + b.exponent + sft);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, Int64 exponent) Div((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            (Mantissa<N> n, int sft) = Mantissa<N>.Div(a.n, b.n);

            Int64 exponent = checked(a.exponent - b.exponent + sft);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, Int64 exponent, bool round) Add(Mantissa<N> a, UInt64 b, Int64 relative_exponent) {
            int expands = BigUInt<Plus4<N>>.Length - BigUInt<N>.Length;

            (UInt32 b_hi, UInt32 b_lo) = UIntUtil.Unpack(b);

            BigUInt<Plus4<N>> a_acc = new(a.Value.ToArray(), 1);
            BigUInt<Plus4<N>> b_acc = new(new UInt32[] { b_lo, b_hi }, Length + 1);

            if (relative_exponent < 1) {
                b_acc = BigUInt<Plus4<N>>.RightRoundShift(b_acc, checked((int)-relative_exponent) + 1);
            }
            else if (relative_exponent > 1) {
                a_acc = BigUInt<Plus4<N>>.RightRoundShift(a_acc, checked((int)relative_exponent) - 1);
            }

            BigUInt<Plus4<N>> c_acc = a_acc + b_acc;

            uint lzc = c_acc.LeadingZeroCount;
            BigUInt<Plus4<N>>.LeftShift(c_acc, (int)lzc, check_overflow: false, enable_clone: false);

            Int64 exponent = UIntUtil.UInt32Bits * (expands - 1) - (int)lzc + ((relative_exponent > 1) ? relative_exponent - 1 : 0);
            bool round = c_acc[expands - 1] > UIntUtil.UInt32Round;
            Mantissa<N> mantissa = new(c_acc.Value.Skip(expands).ToArray(), enable_clone: false);

            return (mantissa, exponent, round);
        }

        internal static (Mantissa<N> n, Int64 exponent, bool round, Sign sign) Diff(Mantissa<N> a, UInt64 b, Int64 relative_exponent) {
            int expands = BigUInt<Plus4<N>>.Length - BigUInt<N>.Length;

            (UInt32 b_hi, UInt32 b_lo) = UIntUtil.Unpack(b);

            BigUInt<Plus4<N>> a_acc = new(a.Value.ToArray(), 1);
            BigUInt<Plus4<N>> b_acc = new(new UInt32[] { b_lo, b_hi }, Length + 1);

            if (relative_exponent < 1) {
                b_acc = BigUInt<Plus4<N>>.RightRoundShift(b_acc, checked((int)-relative_exponent) + 1);
            }
            else if (relative_exponent > 1) {
                a_acc = BigUInt<Plus4<N>>.RightRoundShift(a_acc, checked((int)relative_exponent) - 1);
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

            uint lzc = c_acc.LeadingZeroCount;
            BigUInt<Plus4<N>>.LeftShift(c_acc, (int)lzc, check_overflow: false, enable_clone: false);

            Int64 exponent = UIntUtil.UInt32Bits * (expands - 1) - (int)lzc + ((relative_exponent > 1) ? relative_exponent - 1 : 0);
            bool round = c_acc[expands - 1] > UIntUtil.UInt32Round;
            Mantissa<N> mantissa = new(c_acc.Value.Skip(expands).ToArray(), enable_clone: false);

            return (mantissa, exponent, round, sign);
        }
    }
}
