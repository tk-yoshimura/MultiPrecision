﻿namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Sin(MultiPrecision<N> x) {
            return SinPi(x * RcpPi);
        }

        public static MultiPrecision<N> Cos(MultiPrecision<N> x) {
            return CosPi(x * RcpPi);
        }

        public static MultiPrecision<N> Tan(MultiPrecision<N> x) {
            return TanPi(x * RcpPi);
        }

        public static MultiPrecision<N> SinPi(MultiPrecision<N> x) {
            return SinHalfPi(2 * x);
        }

        public static MultiPrecision<N> CosPi(MultiPrecision<N> x) {
            return CosHalfPi(2 * x);
        }

        public static MultiPrecision<N> TanPi(MultiPrecision<N> x) {
            if (x.Sign == Sign.Minus) {
                return -TanPi(Abs(x));
            }

            MultiPrecision<N> s = x - Floor(x);

            if (s <= 0.25d) {
                MultiPrecision<N> sn = SinPi(s), cn = Sqrt(1 - sn * sn);
                return sn / cn;
            }
            else if (s <= 0.75d) {
                MultiPrecision<N> cn = CosPi(s), sn = Sqrt(1 - cn * cn);
                return sn / cn;
            }
            else {
                MultiPrecision<N> sn = SinPi(s), cn = -Sqrt(1 - sn * sn);
                return sn / cn;
            }
        }

        internal static MultiPrecision<N> SinHalfPi(MultiPrecision<N> x) {
            if (!IsFinite(x)) {
                return NaN;
            }
            if (IsNegative(x)) {
                return -SinHalfPi(-x);
            }
            if (IsZero(x)) {
                return PlusZero;
            }

            if (x.Exponent < 0) {
                return MultiPrecision<Plus1<N>>.SinCurveTaylorApprox(x.Convert<Plus1<N>>()).Convert<N>();
            }

            return SinCurveTaylorApprox(x);
        }

        internal static MultiPrecision<N> CosHalfPi(MultiPrecision<N> x) {
            if (!IsFinite(x)) {
                return NaN;
            }

            if (x.Exponent < 0) {
                return MultiPrecision<Plus1<N>>.CosCurveTaylorApprox(x.Convert<Plus1<N>>()).Convert<N>();
            }

            return CosCurveTaylorApprox(x);
        }

        private static MultiPrecision<N> SinCurveTaylorApprox(MultiPrecision<N> x) {
            Debug<ArithmeticException>.Assert(x >= 0);

            MultiPrecision<N> x_int = Round(x), x_frac = x - x_int, xpi = x_frac * Pi / 2, squa_xpi = xpi * xpi;
            Int64 cycle = x_int.Exponent < UIntUtil.UInt32Bits / 2 ? ((Int64)x_int) % 4 : (Int64)(x_int % 4);

            if ((cycle == 0 || cycle == 2) && IsZero(x_frac)) {
                return Zero;
            }

            BigUInt<Double<N>> a = BigUInt<Double<N>>.Top40000000u;
            BigUInt<N> m = (squa_xpi.Exponent > -Bits - 1)
                ? BigUInt<N>.RightRoundShift(new BigUInt<N>(squa_xpi.mantissa.Value), checked(-(int)squa_xpi.Exponent), enable_clone: false)
                : 0u;
            BigUInt<N> w = m;

            Sign s = Sign.Minus;

            for (int i = (cycle == 0 || cycle == 2) ? 2 : 1; i + 1 < BigUInt<N>.TaylorTable.Count; i += 2) {
                BigUInt<N> t = BigUInt<N>.TaylorTable[i];
                BigUInt<Double<N>> d = BigUInt<N>.Mul<Double<N>>(w, t);

                if (s == Sign.Plus) {
                    a += d;
                    s = Sign.Minus;
                }
                else {
                    a -= d;
                    s = Sign.Plus;
                }

                if (d.Digits < Length) {
                    break;
                }

                w = BigUInt<Double<N>>.RightRoundShift(
                    BigUInt<N>.Mul<Double<N>>(w, m),
                    Mantissa<N>.Bits - 1, enable_clone: false)
                    .Convert<N>(check_overflow: false);
            }

            uint lzc = a.LeadingZeroCount;
            BigUInt<Double<N>>.RightRoundShift(a, Bits - (int)lzc, enable_clone: false);

            BigUInt<N> n = a.Convert<N>(check_overflow: false);

            MultiPrecision<N> y;
            if (cycle == 0 || cycle == 2) {
                y = new MultiPrecision<N>((cycle == 0 ^ x.Sign == Sign.Plus) ? Sign.Minus : Sign.Plus, -(int)lzc + 1, new Mantissa<N>(n), round: false);
                y *= xpi;
            }
            else {
                y = new MultiPrecision<N>((cycle == 1 ^ x.Sign == Sign.Plus) ? Sign.Minus : Sign.Plus, -(int)lzc + 1, new Mantissa<N>(n), round: false);
            }

            return y;
        }

        private static MultiPrecision<N> CosCurveTaylorApprox(MultiPrecision<N> x) {
            MultiPrecision<N> x_abs = Abs(x);
            MultiPrecision<N> x_int = Round(x_abs), x_frac = x_abs - x_int, xpi = x_frac * Pi / 2, squa_xpi = xpi * xpi;
            Int64 cycle = x_int.Exponent < UIntUtil.UInt32Bits / 2 ? ((Int64)x_int) % 4 : (Int64)(x_int % 4);

            if ((cycle == 1 || cycle == 3) && IsZero(x_frac)) {
                return Zero;
            }

            BigUInt<Double<N>> a = BigUInt<Double<N>>.Top40000000u;
            BigUInt<N> m = (squa_xpi.Exponent > -Bits - 1)
                ? BigUInt<N>.RightRoundShift(new BigUInt<N>(squa_xpi.mantissa.Value), checked(-(int)squa_xpi.Exponent), enable_clone: false)
                : 0u;
            BigUInt<N> w = m;

            Sign s = Sign.Minus;

            for (int i = (cycle == 0 || cycle == 2) ? 1 : 2; i + 1 < BigUInt<N>.TaylorTable.Count; i += 2) {
                BigUInt<N> t = BigUInt<N>.TaylorTable[i];
                BigUInt<Double<N>> d = BigUInt<N>.Mul<Double<N>>(w, t);

                if (s == Sign.Plus) {
                    a += d;
                    s = Sign.Minus;
                }
                else {
                    a -= d;
                    s = Sign.Plus;
                }

                if (d.Digits < Length) {
                    break;
                }

                w = BigUInt<Double<N>>.RightRoundShift(
                    BigUInt<N>.Mul<Double<N>>(w, m),
                    Mantissa<N>.Bits - 1, enable_clone: false)
                    .Convert<N>(check_overflow: false);
            }

            uint lzc = a.LeadingZeroCount;
            BigUInt<Double<N>>.RightRoundShift(a, Bits - (int)lzc, enable_clone: false);

            BigUInt<N> n = a.Convert<N>(check_overflow: false);

            MultiPrecision<N> y;
            if (cycle == 0 || cycle == 2) {
                y = new MultiPrecision<N>((cycle == 0) ? Sign.Plus : Sign.Minus, -(int)lzc + 1, new Mantissa<N>(n), round: false);
            }
            else {
                y = new MultiPrecision<N>((cycle == 1) ? Sign.Minus : Sign.Plus, -(int)lzc + 1, new Mantissa<N>(n), round: false);
                y *= xpi;
            }

            return y;
        }
    }
}
