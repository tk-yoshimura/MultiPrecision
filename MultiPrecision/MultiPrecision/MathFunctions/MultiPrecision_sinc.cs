namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {
        public static MultiPrecision<N> Sinc(MultiPrecision<N> x, bool normalized = true) {
            if (normalized) {
                if (Abs(x).Exponent < -Bits / 2) {
                    return 1 - Square(x * Pi) / 6;
                }

                MultiPrecision<N> c = Pi * x;

                if (IsInfinity(c)) {
                    return Zero;
                }

                return SinPi(x) / c;
            }
            else {
                if (Abs(x).Exponent < -Bits / 2) {
                    return 1 - x * x / 6;
                }

                if (IsInfinity(x)) {
                    return Zero;
                }

                return Sin(x) / x;
            }
        }

        public static MultiPrecision<N> Sinhc(MultiPrecision<N> x) {
            if (Abs(x).Exponent < -Bits / 2) {
                return 1 + x * x / 6;
            }

            MultiPrecision<N> sinh = Sinh(x);

            if (IsInfinity(x)) {
                return PositiveInfinity;
            }

            return sinh / x;
        }

        public static MultiPrecision<N> Jinc(MultiPrecision<N> x) {
            MultiPrecision<N> x_abs = Abs(x);

            if (x_abs.Exponent < -Bits / 2) {
                return Point5 - Ldexp(x * x, -4);
            }

            MultiPrecision<N> j1 = BesselJ(1, x_abs);

            return j1 / x_abs;
        }
    }
}