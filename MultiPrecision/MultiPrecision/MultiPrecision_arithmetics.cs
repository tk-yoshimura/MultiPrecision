using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> operator +(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Add(a, b);
        }

        public static MultiPrecision<N> operator -(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Sub(a, b);
        }

        public static MultiPrecision<N> operator *(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Mul(a, b);
        }

        public static MultiPrecision<N> operator /(MultiPrecision<N> a, MultiPrecision<N> b) {
            return Div(a, b);
        }

        public static MultiPrecision<N> operator %(MultiPrecision<N> a, MultiPrecision<N> b) {
            return a - Truncate(a / b) * b;
        }

        public static MultiPrecision<N> operator +(MultiPrecision<N> x) {
            return x;
        }

        public static MultiPrecision<N> operator -(MultiPrecision<N> x) {
            return Neg(x);
        }

        public static MultiPrecision<N> Add(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) {
                return NaN;
            }

            if (a.IsZero) {
                return b;
            }
            if (b.IsZero) {
                return a;
            }

            if (!a.IsFinite) {
                if (b.IsFinite) {
                    return a;
                }
                if (a.Sign != b.Sign) {
                    return NaN;
                }

                return new MultiPrecision<N>(a.Sign, ExponentMax, Mantissa<N>.Zero);
            }
            if (!b.IsFinite) {
                return b;
            }

            if (a.Sign == b.Sign) {
                (Mantissa<N> mantissa, Int64 exponent) = Add((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(a.Sign, exponent, mantissa, round: false);
            }
            else {
                (Mantissa<N> mantissa, Int64 exponent) = Diff((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>((Abs(a) > Abs(b)) ? a.Sign : b.Sign, exponent, mantissa, round: false);
            }
        }

        public static MultiPrecision<N> Sub(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) {
                return NaN;
            }

            if (a.IsZero) {
                return -b;
            }
            if (b.IsZero) {
                return a;
            }

            if (!a.IsFinite) {
                if (b.IsFinite) {
                    return a;
                }
                if (a.Sign == b.Sign) {
                    return NaN;
                }

                return new MultiPrecision<N>(a.Sign, ExponentMax, Mantissa<N>.Zero);
            }
            if (!b.IsFinite) {
                return -b;
            }

            if (a.Sign == b.Sign) {
                (Mantissa<N> mantissa, Int64 exponent) = Diff((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>((a > b) ? Sign.Plus : Sign.Minus, exponent, mantissa, round: false);
            }
            else {
                (Mantissa<N> mantissa, Int64 exponent) = Add((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(a.Sign, exponent, mantissa, round: false);
            }
        }

        public static MultiPrecision<N> Mul(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) {
                return NaN;
            }

            if (a.IsZero || b.IsZero) {
                if (!a.IsFinite || !b.IsFinite) {
                    return NaN;
                }

                return a.Sign == b.Sign ? Zero : MinusZero;
            }

            if (!a.IsFinite || !b.IsFinite) {
                return (a.Sign == b.Sign) ? PositiveInfinity : NegativeInfinity;
            }

            (Mantissa<N> mantissa, Int64 exponent) = Mul((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

            return new MultiPrecision<N>((a.Sign == b.Sign) ? Sign.Plus : Sign.Minus, exponent, mantissa, round: false);
        }

        public static MultiPrecision<N> Div(MultiPrecision<N> a, MultiPrecision<N> b) {
            if (a.IsNaN || b.IsNaN) {
                return NaN;
            }

            if (b.IsZero) {
                if (a.IsZero) {
                    return NaN;
                }
                else {
                    return (a.Sign == b.Sign) ? PositiveInfinity : NegativeInfinity;
                }
            }

            if (!a.IsFinite) {
                if (b.IsFinite) {
                    return (a.Sign == b.Sign) ? PositiveInfinity : NegativeInfinity;
                }
                return NaN;
            }
            if (!b.IsFinite) {
                return (a.Sign == b.Sign) ? Zero : MinusZero;
            }

            (Mantissa<N> mantissa, Int64 exponent) = Div((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

            return new MultiPrecision<N>((a.Sign == b.Sign) ? Sign.Plus : Sign.Minus, exponent, mantissa, round: false);
        }

        public static MultiPrecision<N> Neg(MultiPrecision<N> v) {
            return new MultiPrecision<N>((v.Sign == Sign.Plus) ? Sign.Minus : Sign.Plus, v.exponent, v.mantissa);
        }
    }
}
