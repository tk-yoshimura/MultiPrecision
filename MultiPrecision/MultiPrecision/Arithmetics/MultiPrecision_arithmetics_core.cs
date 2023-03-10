namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        internal static (Mantissa<N> n, long exponent) Add((Mantissa<N> n, long exponent) a, (Mantissa<N> n, long exponent) b) {
            if (a.exponent > b.exponent) {
                long exponent_delta = a.exponent - b.exponent;

                if (exponent_delta <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Add(a.n, b.n, checked((int)exponent_delta));
                    long exponent = checked(a.exponent + n_exponent);

                    return (n, exponent);
                }
                else {
                    return (a.n, a.exponent);
                }
            }
            else {
                long exponent_delta = b.exponent - a.exponent;

                if (exponent_delta <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Add(b.n, a.n, checked((int)exponent_delta));
                    long exponent = checked(b.exponent + n_exponent);

                    return (n, exponent);
                }
                else {
                    return (b.n, b.exponent);
                }
            }
        }

        internal static (Mantissa<N> n, long exponent, Sign sign) Diff((Mantissa<N> n, long exponent) a, (Mantissa<N> n, long exponent) b) {
            if (a.exponent > b.exponent) {
                long exponent_delta = a.exponent - b.exponent;

                if (exponent_delta <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Sub(a.n, b.n, checked((int)exponent_delta));
                    long exponent = checked(a.exponent + n_exponent);

                    return (n, exponent, Sign.Plus);
                }
                else {
                    return (a.n, a.exponent, Sign.Plus);
                }
            }
            else if (a.exponent < b.exponent) {
                long exponent_delta = b.exponent - a.exponent;

                if (exponent_delta <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Sub(b.n, a.n, checked((int)exponent_delta));
                    long exponent = checked(b.exponent + n_exponent);

                    return (n, exponent, Sign.Minus);
                }
                else {
                    return (b.n, b.exponent, Sign.Minus);
                }
            }
            else {
                ((Mantissa<N> n, int n_exponent), Sign sign) = (a.n >= b.n)
                    ? (Mantissa<N>.Sub(a.n, b.n, 0), Sign.Plus)
                    : (Mantissa<N>.Sub(b.n, a.n, 0), Sign.Minus);

                if (n.IsZero) {
                    return (Mantissa<N>.Zero, (Int64)ExponentMin - (Int64)ExponentMax, sign);
                }
                else {
                    long exponent = checked(a.exponent + n_exponent);

                    return (n, exponent, sign);
                }
            }
        }

        internal static (Mantissa<N> n, long exponent) Mul((Mantissa<N> n, long exponent) a, (Mantissa<N> n, long exponent) b) {
            (Mantissa<N> n, int n_exponent) = Mantissa<N>.Mul(a.n, b.n);

            long exponent = checked(a.exponent + b.exponent + n_exponent);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, long exponent) Mul((Mantissa<N> n, long exponent) a, UInt64 b) {
            (Mantissa<N> n, int n_exponent) = Mantissa<N>.Mul(a.n, b);

            long exponent = checked(a.exponent + n_exponent);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, long exponent) Div((Mantissa<N> n, long exponent) a, (Mantissa<N> n, long exponent) b) {
            (Mantissa<N> n, int n_exponent) = Mantissa<N>.Div(a.n, b.n);

            long exponent = checked(a.exponent - b.exponent + n_exponent);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, long exponent) Div((Mantissa<N> n, long exponent) a, UInt64 b) {
            (Mantissa<N> n, int n_exponent) = Mantissa<N>.Div(a.n, b);

            long exponent = checked(a.exponent + n_exponent);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, long exponent) Add((Mantissa<N> n, long exponent) a, UInt64 b) {
            long exponent_delta = (b > 0uL) ? a.exponent : Int64.MaxValue;

            if (exponent_delta < -Mantissa<N>.Bits) {
                uint lzc = UIntUtil.LeadingZeroCount(b);
                Mantissa<N> n = new(
                    BigUInt<N>.LeftShift(
                        b,
                        checked(BigUInt<N>.Bits - UIntUtil.UInt64Bits + (int)lzc),
                        check_overflow: false, enable_clone: false)
                    );
                long exponent = checked(UIntUtil.UInt64Bits - 1 - (long)lzc);

                return (n, exponent);
            }
            else if (exponent_delta <= Mantissa<N>.Bits + UIntUtil.UInt64Bits) {
                (Mantissa<N> n, int n_exponent) = Mantissa<N>.Add(a.n, b, checked((int)exponent_delta));
                long exponent = checked(a.exponent + n_exponent);

                return (n, exponent);
            }
            else {
                return (a.n, a.exponent);
            }
        }

        internal static (Mantissa<N> n, long exponent, Sign sign) Diff((Mantissa<N> n, long exponent) a, UInt64 b) {
            long exponent_delta = (b > 0uL) ? a.exponent : Int64.MaxValue;

            if (exponent_delta < -Mantissa<N>.Bits) {
                uint lzc = UIntUtil.LeadingZeroCount(b);
                Mantissa<N> n = new(
                    BigUInt<N>.LeftShift(
                        b,
                        checked(BigUInt<N>.Bits - UIntUtil.UInt64Bits + (int)lzc),
                        check_overflow: false, enable_clone: false)
                    );
                long exponent = checked(UIntUtil.UInt64Bits - 1 - (long)lzc);

                return (n, exponent, Sign.Minus);
            }
            else if (exponent_delta <= Mantissa<N>.Bits + UIntUtil.UInt64Bits) {
                uint lzc = UIntUtil.LeadingZeroCount(b);

                if (a.exponent <= checked(UIntUtil.UInt64Bits - 1 - (long)lzc)) {
                    Mantissa<N> b_n = new(
                        BigUInt<N>.LeftShift(
                            b,
                            checked(BigUInt<N>.Bits - UIntUtil.UInt64Bits + (int)lzc),
                            check_overflow: false, enable_clone: false)
                        );
                    long b_exponent = checked(UIntUtil.UInt64Bits - 1 - (long)lzc);

                    return Diff(a, (b_n, b_exponent));
                }
                else {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Sub(a.n, b, checked((int)exponent_delta));
                    long exponent = checked(a.exponent + n_exponent);

                    return (n, exponent, Sign.Plus);
                }
            }
            else {
                return (a.n, a.exponent, Sign.Plus);
            }
        }
    }
}
