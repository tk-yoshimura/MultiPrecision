namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> operator +(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Add(a, b);
        }

        public static MultiPrecision<N> operator +(MultiPrecision<N> a, long b) {
            return Add(a, b);
        }

        public static MultiPrecision<N> operator +(long a, MultiPrecision<N> b) {
            return Add(b, a);
        }

        public static MultiPrecision<N> operator -(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Sub(a, b);
        }

        public static MultiPrecision<N> operator -(MultiPrecision<N> a, long b) {
            return Sub(a, b);
        }

        public static MultiPrecision<N> operator -(long a, MultiPrecision<N> b) {
            return Neg(Sub(b, a));
        }

        public static MultiPrecision<N> operator *(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Mul(a, b);
        }

        public static MultiPrecision<N> operator *(MultiPrecision<N> a, long b) {
            return Mul(a, b);
        }

        public static MultiPrecision<N> operator *(long a, MultiPrecision<N> b) {
            return Mul(b, a);
        }

        public static MultiPrecision<N> operator /(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Div(a, b);
        }

        public static MultiPrecision<N> operator /(MultiPrecision<N> a, long b) {
            return Div(a, b);
        }

        public static MultiPrecision<N> operator /(long a, MultiPrecision<N> b) {
            return a * Rcp(b);
        }

        public static MultiPrecision<N> operator %(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsZero(b) || IsNaN(a) || IsNaN(b)) {
                return NaN;
            }

            if (!IsFinite(b)) {
                return a;
            }

            MultiPrecision<N> c = Truncate(a / b) * b;

            return a - c;
        }

        public static MultiPrecision<N> operator +(MultiPrecision<N> x) {
            return x;
        }

        public static MultiPrecision<N> operator -(MultiPrecision<N> x) {
            return Neg(x);
        }

        public static MultiPrecision<N> operator ++(MultiPrecision<N> a) {
            return Add(a, 1L);
        }

        public static MultiPrecision<N> operator --(MultiPrecision<N> a) {
            return Sub(a, 1L);
        }

        public static MultiPrecision<N> Add(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsNaN(a) || IsNaN(b)) {
                return NaN;
            }

            if (IsZero(a)) {
                return !IsZero(b) ? b : (a.Sign == b.Sign) ? a : Zero;
            }
            if (IsZero(b)) {
                return a;
            }

            if (!IsFinite(a)) {
                if (IsFinite(b)) {
                    return a;
                }
                if (a.Sign != b.Sign) {
                    return NaN;
                }

                return new MultiPrecision<N>(a.Sign, ExponentMax, Mantissa<N>.Zero);
            }
            if (!IsFinite(b)) {
                return b;
            }

            if (a.Sign == b.Sign) {
                (Mantissa<N> mantissa, Int64 exponent) = Add((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(a.Sign, exponent, mantissa, round: false);
            }
            else {
                (Mantissa<N> mantissa, Int64 exponent, Sign sign) = Diff((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(sign == Sign.Plus ? a.Sign : b.Sign, exponent, mantissa, round: false);
            }
        }

        public static MultiPrecision<N> Sub(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsNaN(a) || IsNaN(b)) {
                return NaN;
            }

            if (IsZero(a)) {
                return -b;
            }
            if (IsZero(b)) {
                return a;
            }

            if (!IsFinite(a)) {
                if (IsFinite(b)) {
                    return a;
                }
                if (a.Sign == b.Sign) {
                    return NaN;
                }
                return new MultiPrecision<N>(a.Sign, ExponentMax, Mantissa<N>.Zero);
            }
            if (!IsFinite(b)) {
                return -b;
            }

            if (a.Sign == b.Sign) {
                (Mantissa<N> mantissa, Int64 exponent, Sign sign) = Diff((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(
                    (a.Sign == Sign.Plus ^ sign == Sign.Plus) ? Sign.Minus : Sign.Plus,
                    exponent, mantissa, round: false);
            }
            else {
                (Mantissa<N> mantissa, Int64 exponent) = Add((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(a.Sign, exponent, mantissa, round: false);
            }
        }

        public static MultiPrecision<N> Mul(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsNaN(a) || IsNaN(b)) {
                return NaN;
            }

            if (IsZero(a) || IsZero(b)) {
                if (!IsFinite(a) || !IsFinite(b)) {
                    return NaN;
                }
                return a.Sign == b.Sign ? Zero : MinusZero;
            }

            if (!IsFinite(a) || !IsFinite(b)) {
                return (a.Sign == b.Sign) ? PositiveInfinity : NegativeInfinity;
            }

            (Mantissa<N> mantissa, Int64 exponent) = Mul((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

            return new MultiPrecision<N>((a.Sign == b.Sign) ? Sign.Plus : Sign.Minus, exponent, mantissa, round: false);
        }

        public static MultiPrecision<N> Div(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (IsNaN(a) || IsNaN(b)) {
                return NaN;
            }

            if (IsZero(b)) {
                if (IsZero(a)) {
                    return NaN;
                }
                return (a.Sign == b.Sign) ? PositiveInfinity : NegativeInfinity;
            }

            if (!IsFinite(a)) {
                if (IsFinite(b)) {
                    return (a.Sign == b.Sign) ? PositiveInfinity : NegativeInfinity;
                }
                return NaN;
            }
            if (!IsFinite(b)) {
                return (a.Sign == b.Sign) ? Zero : MinusZero;
            }

            (Mantissa<N> mantissa, Int64 exponent) = Div((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

            return new MultiPrecision<N>((a.Sign == b.Sign) ? Sign.Plus : Sign.Minus, exponent, mantissa, round: false);
        }

        public static MultiPrecision<N> Neg(MultiPrecision<N> v) {
            return new MultiPrecision<N>((v.Sign == Sign.Plus) ? Sign.Minus : Sign.Plus, v.exponent, v.mantissa);
        }

        public static MultiPrecision<N> Add(MultiPrecision<N> a, long b) {
            if (b == 0) {
                return a;
            }

            if (IsNaN(a)) {
                return NaN;
            }
            if (IsZero(a)) {
                return b;
            }
            if (!IsFinite(a)) {
                return a;
            }

            UInt64 b_abs = UIntUtil.Abs(b);

            if (a.Sign == UIntUtil.Sign(b)) {
                (Mantissa<N> n, Int64 exponent) = Add((a.mantissa, a.Exponent), b_abs);

                return new MultiPrecision<N>(a.Sign, exponent, n, round: false);
            }
            else {
                (Mantissa<N> n, Int64 exponent, Sign sign) = Diff((a.mantissa, a.Exponent), b_abs);

                return new MultiPrecision<N>(sign == Sign.Plus ? a.Sign : UIntUtil.Sign(b), exponent, n, round: false);
            }
        }

        public static MultiPrecision<N> Sub(MultiPrecision<N> a, long b) {
            if (b == 0) {
                return a;
            }

            if (IsNaN(a)) {
                return NaN;
            }
            if (IsZero(a)) {
                return Neg(b);
            }
            if (!IsFinite(a)) {
                return a;
            }

            UInt64 b_abs = UIntUtil.Abs(b);

            if (a.Sign == UIntUtil.Sign(b)) {
                (Mantissa<N> n, Int64 exponent, Sign sign) = Diff((a.mantissa, a.Exponent), b_abs);

                return new MultiPrecision<N>(
                    (a.Sign == Sign.Plus ^ sign == Sign.Plus) ? Sign.Minus : Sign.Plus,
                    exponent, n, round: false
                );
            }
            else {
                (Mantissa<N> n, Int64 exponent) = Add((a.mantissa, a.Exponent), b_abs);

                return new MultiPrecision<N>(a.Sign, exponent, n, round: false);
            }
        }

        public static MultiPrecision<N> Mul(MultiPrecision<N> a, long b) {
            if (IsNaN(a)) {
                return NaN;
            }
            if (IsZero(a)) {
                return a;
            }
            if (!IsFinite(a)) {
                if (b == 0) {
                    return NaN;
                }
                return (a.Sign == UIntUtil.Sign(b)) ? PositiveInfinity : NegativeInfinity;
            }

            if (b == 0) {
                return a.Sign == Sign.Plus ? Zero : MinusZero;
            }
            if (b == 1) {
                return a;
            }
            if (b == -1) {
                return Neg(a);
            }

            UInt64 b_abs = UIntUtil.Abs(b);

            if (UIntUtil.IsPower2(b_abs)) {
                MultiPrecision<N> a_power2 = Ldexp(a, UIntUtil.Power2(b_abs));

                return b >= 0 ? a_power2 : Neg(a_power2);
            }

            (Mantissa<N> mantissa, Int64 exponent) = Mul((a.mantissa, a.Exponent), b_abs);

            return new MultiPrecision<N>((a.Sign == Sign.Plus ^ b >= 0) ? Sign.Minus : Sign.Plus, exponent, mantissa, round: false);
        }

        public static MultiPrecision<N> Div(MultiPrecision<N> a, long b) {
            if (IsNaN(a)) {
                return NaN;
            }
            if (IsZero(a)) {
                if (b == 0) {
                    return NaN;
                }
                return (a.Sign == UIntUtil.Sign(b)) ? Zero : MinusZero;
            }
            if (!IsFinite(a)) {
                return (a.Sign == UIntUtil.Sign(b)) ? PositiveInfinity : NegativeInfinity;
            }

            if (b == 0) {
                return (a.Sign == Sign.Plus) ? PositiveInfinity : NegativeInfinity;
            }
            if (b == 1) {
                return a;
            }
            if (b == -1) {
                return Neg(a);
            }

            UInt64 b_abs = UIntUtil.Abs(b);

            if (UIntUtil.IsPower2(b_abs)) {
                MultiPrecision<N> a_power2 = Ldexp(a, -UIntUtil.Power2(b_abs));

                return b >= 0 ? a_power2 : Neg(a_power2);
            }

            (Mantissa<N> mantissa, Int64 exponent) = Div((a.mantissa, a.Exponent), b_abs);

            return new MultiPrecision<N>((a.Sign == Sign.Plus ^ b >= 0) ? Sign.Minus : Sign.Plus, exponent, mantissa, round: false);
        }
    }
}
