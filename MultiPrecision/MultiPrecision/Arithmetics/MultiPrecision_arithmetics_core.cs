namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        internal static (Mantissa<N> n, Int64 exponent) Add((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            if (a.exponent > b.exponent) {
                Int64 d = a.exponent - b.exponent;

                if (d <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Add(a.n, b.n, checked((int)d));
                    Int64 exponent = checked(a.exponent + n_exponent);

                    return (n, exponent);
                }
                else {
                    return (a.n, a.exponent);
                }
            }
            else {
                Int64 d = b.exponent - a.exponent;

                if (d <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Add(b.n, a.n, checked((int)d));
                    Int64 exponent = checked(b.exponent + n_exponent);

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
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Sub(a.n, b.n, checked((int)d));
                    Int64 exponent = checked(a.exponent + n_exponent);

                    return (n, exponent, Sign.Plus);
                }
                else {
                    return (a.n, a.exponent, Sign.Plus);
                }
            }
            else if (a.exponent < b.exponent) {
                Int64 d = b.exponent - a.exponent;

                if (d <= Mantissa<N>.Bits) {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Sub(b.n, a.n, checked((int)d));
                    Int64 exponent = checked(b.exponent + n_exponent);

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
                    Int64 exponent = checked(a.exponent + n_exponent);

                    return (n, exponent, sign);
                }
            }
        }

        internal static (Mantissa<N> n, Int64 exponent) Mul((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            (Mantissa<N> n, int n_exponent) = Mantissa<N>.Mul(a.n, b.n);

            Int64 exponent = checked(a.exponent + b.exponent + n_exponent);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, Int64 exponent) Mul((Mantissa<N> n, Int64 exponent) a, UInt64 b) {
            (Mantissa<N> n, int n_exponent) = Mantissa<N>.Mul(a.n, b);

            Int64 exponent = checked(a.exponent + n_exponent);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, Int64 exponent) Div((Mantissa<N> n, Int64 exponent) a, (Mantissa<N> n, Int64 exponent) b) {
            (Mantissa<N> n, int n_exponent) = Mantissa<N>.Div(a.n, b.n);

            Int64 exponent = checked(a.exponent - b.exponent + n_exponent);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, Int64 exponent) Div((Mantissa<N> n, Int64 exponent) a, UInt64 b) {
            (Mantissa<N> n, int n_exponent) = Mantissa<N>.Div(a.n, b);

            Int64 exponent = checked(a.exponent + n_exponent);

            return (n, exponent);
        }

        internal static (Mantissa<N> n, Int64 exponent) Add((Mantissa<N> n, Int64 exponent) a, UInt64 b) {
            Int64 d = (b > 0uL) ? a.exponent : Int64.MaxValue;

            if (d < -Mantissa<N>.Bits) {
                uint lzc = UIntUtil.LeadingZeroCount(b);
                Mantissa<N> n = new(
                    BigUInt<N>.LeftShift(
                        b,
                        BigUInt<N>.Bits - UIntUtil.UInt64Bits + (int)lzc,
                        check_overflow: false, enable_clone: false)
                    );
                int exponent = checked(UIntUtil.UInt64Bits - 1 - (int)lzc);

                return (n, exponent);
            }
            else if (d <= Mantissa<N>.Bits + UIntUtil.UInt64Bits) {
                (Mantissa<N> n, int n_exponent) = Mantissa<N>.Add(a.n, b, (int)d);
                Int64 exponent = checked(a.exponent + n_exponent);

                return (n, exponent);
            }
            else {
                return (a.n, a.exponent);
            }
        }

        internal static (Mantissa<N> n, Int64 exponent, Sign sign) Diff((Mantissa<N> n, Int64 exponent) a, UInt64 b) {
            Int64 d = (b > 0uL) ? a.exponent : Int64.MaxValue;

            if (d < -Mantissa<N>.Bits) {
                uint lzc = UIntUtil.LeadingZeroCount(b);
                Mantissa<N> n = new(
                    BigUInt<N>.LeftShift(
                        b,
                        BigUInt<N>.Bits - UIntUtil.UInt64Bits + (int)lzc,
                        check_overflow: false, enable_clone: false)
                    );
                int exponent = checked(UIntUtil.UInt64Bits - 1 - (int)lzc);

                return (n, exponent, Sign.Minus);
            }
            else if (d <= Mantissa<N>.Bits + UIntUtil.UInt64Bits) {
                uint lzc = UIntUtil.LeadingZeroCount(b);

                if (a.exponent <= checked(UIntUtil.UInt64Bits - 1 - (int)lzc)) {
                    Mantissa<N> b_n = new(
                        BigUInt<N>.LeftShift(
                            b,
                            BigUInt<N>.Bits - UIntUtil.UInt64Bits + (int)lzc,
                            check_overflow: false, enable_clone: false)
                        );
                    long b_exponent = checked(UIntUtil.UInt64Bits - 1 - (int)lzc);

                    return Diff(a, (b_n, b_exponent));
                }
                else {
                    (Mantissa<N> n, int n_exponent) = Mantissa<N>.Sub(a.n, b, (int)d);
                    Int64 exponent = checked(a.exponent + n_exponent);

                    return (n, exponent, Sign.Plus);
                }
            }
            else {
                return (a.n, a.exponent, Sign.Plus);
            }
        }
    }
}
