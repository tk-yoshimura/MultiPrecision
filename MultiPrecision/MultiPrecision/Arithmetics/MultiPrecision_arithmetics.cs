using System;
using System.Linq;

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
            if (b.IsZero || a.IsNaN || b.IsNaN) {
                return NaN;
            }

            if (!b.IsFinite) {
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
                (Mantissa<N> mantissa, Int64 exponent, Sign sign) = Diff((a.mantissa, a.Exponent), (b.mantissa, b.Exponent));

                return new MultiPrecision<N>(sign == Sign.Plus ? a.Sign : b.Sign, exponent, mantissa, round: false);
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
                return (a.Sign == b.Sign) ? PositiveInfinity : NegativeInfinity;
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

        public static MultiPrecision<N> Mul(MultiPrecision<N> a, long b) {
            if (a.IsNaN) {
                return NaN;
            }
            if (a.IsZero) {
                return a;
            }
            if (!a.IsFinite) {
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

            int expands = BigUInt<Plus4<N>>.Length - BigUInt<N>.Length;

            BigUInt<Plus4<N>> acc = new(a.mantissa.Value.ToArray(), 0);

            acc *= b_abs;

            int lzc = acc.LeadingZeroCount;
            acc <<= lzc;
            
            Int64 exponent = a.Exponent - lzc + UIntUtil.UInt32Bits * expands;
            Sign sign = (a.Sign == UIntUtil.Sign(b)) ? Sign.Plus : Sign.Minus;
            bool round = acc[expands - 1] > UIntUtil.UInt32Round;
            Mantissa<N> mantissa = new Mantissa<N>(acc.Value.Skip(expands).ToArray(), enable_clone: false);

            return new MultiPrecision<N>(sign, exponent, mantissa, round);
        }

        public static MultiPrecision<N> Div(MultiPrecision<N> a, long b) {
            if (a.IsNaN) {
                return NaN;
            }
            if (a.IsZero) {
                if (b == 0) {
                    return NaN;
                }
                return (a.Sign == UIntUtil.Sign(b)) ? Zero : MinusZero;
            }
            if (!a.IsFinite) {
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

            int expands = BigUInt<Plus4<N>>.Length - BigUInt<N>.Length;

            BigUInt<Plus4<N>> acc = new(a.mantissa.Value.ToArray(), expands);

            acc /= b_abs;

            int lzc = acc.LeadingZeroCount;
            acc <<= lzc;
            
            Int64 exponent = a.Exponent - lzc;
            Sign sign = (a.Sign == UIntUtil.Sign(b)) ? Sign.Plus : Sign.Minus;
            bool round = acc[expands - 1] > UIntUtil.UInt32Round;
            Mantissa<N> mantissa = new Mantissa<N>(acc.Value.Skip(expands).ToArray(), enable_clone: false);

            return new MultiPrecision<N>(sign, exponent, mantissa, round);
        }

        public static MultiPrecision<N> Add(MultiPrecision<N> a, long b) {
            if (b == 0) {
                return a;
            }

            if (a.IsNaN) {
                return NaN;
            }
            if (a.IsZero) {
                return b;
            }
            if (!a.IsFinite) {
                return a;
            }

            UInt64 b_abs = UIntUtil.Abs(b);

            if (a.Sign == UIntUtil.Sign(b)) {
                (Mantissa<N> n, Int64 exponent, bool round) = Add(a.mantissa, b_abs, -a.Exponent);

                return new MultiPrecision<N>(a.Sign, exponent + a.Exponent, n, round);
            }
            else {
                (Mantissa<N> n, Int64 exponent, bool round, Sign sign) = Diff(a.mantissa, b_abs, -a.Exponent);

                return new MultiPrecision<N>(sign == Sign.Plus ? a.Sign : UIntUtil.Sign(b), exponent + a.Exponent, n, round);
            }
        }

        public static MultiPrecision<N> Sub(MultiPrecision<N> a, long b) {
            if (b == 0) {
                return a;
            }

            if (a.IsNaN) {
                return NaN;
            }
            if (a.IsZero) {
                return Neg(b);
            }
            if (!a.IsFinite) {
                return a;
            }

            UInt64 b_abs = UIntUtil.Abs(b);

            if (a.Sign == UIntUtil.Sign(b)) {
                (Mantissa<N> n, Int64 exponent, bool round, Sign sign) = Diff(a.mantissa, b_abs, -a.Exponent);

                return new MultiPrecision<N>(
                    (a.Sign == Sign.Plus ^ sign == Sign.Plus) ? Sign.Minus : Sign.Plus, 
                    exponent + a.Exponent, n, round);
            }
            else {
                (Mantissa<N> n, Int64 exponent, bool round) = Add(a.mantissa, b_abs, -a.Exponent);

                return new MultiPrecision<N>(a.Sign, exponent + a.Exponent, n, round);
            }
        }
    }
}
