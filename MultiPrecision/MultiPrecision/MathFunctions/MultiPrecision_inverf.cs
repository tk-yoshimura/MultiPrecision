namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> InverseErf(MultiPrecision<N> x) {
            if (IsNaN(x) || x < MinusOne || x > One) {
                return NaN;
            }
            if (x == MinusOne) {
                return NegativeInfinity;
            }
            if (x == One) {
                return PositiveInfinity;
            }
            if (x.Sign == Sign.Minus) {
                return -InverseErf(Abs(x));
            }

            if (x.Exponent <= -Bits / 4) {
                MultiPrecision<N> w = PI * x * x;
                MultiPrecision<N> t = Sqrt(PI) * ((40320 + w * (3360 + w * (588 + w * 127))) / 80640);

                return x * t;
            }
            if (x.Exponent < -1) {
                return InverseErfRootFinding(x);
            }
            else {
                return InverseErfcRootFinding(1 - x);
            }
        }

        public static MultiPrecision<N> InverseErfc(MultiPrecision<N> x) {
            if (IsNaN(x) || x < Zero || x > 2) {
                return NaN;
            }
            if (x == Zero) {
                return PositiveInfinity;
            }
            if (x == 1) {
                return Zero;
            }
            if (x.Exponent >= -1) {
                return InverseErf(1 - x);
            }

            return InverseErfcRootFinding(x);
        }

        private static MultiPrecision<N> InverseErfRootFinding(MultiPrecision<N> x) {
            MultiPrecision<N> s = 2 / Sqrt(PI);

            MultiPrecision<N> z0;
            if (Length <= 4) {
                const double a = 0.147;

                MultiPrecision<Pow2.N4> xl4 = x.Convert<Pow2.N4>();
                MultiPrecision<Pow2.N4> lg = MultiPrecision<Pow2.N4>.Log(1 - xl4 * xl4);
                MultiPrecision<Pow2.N4> lga = 2 / (MultiPrecision<Pow2.N4>.PI * a) + lg / 2;

                z0 = MultiPrecision<Pow2.N4>.Sqrt(MultiPrecision<Pow2.N4>.Sqrt(lga * lga - lg / a) - lga).Convert<N>();
            }
            else {
                z0 = MultiPrecision<Pow2.N4>.InverseErfRootFinding(x.Convert<Pow2.N4>()).Convert<N>();
            }

            MultiPrecision<N> erf(MultiPrecision<N> z) {
                return Erf(z) - x;
            };
            (MultiPrecision<N>, MultiPrecision<N>) derf(MultiPrecision<N> z) {
                MultiPrecision<N> d1 = Exp(-z * z) * s;
                MultiPrecision<N> d2 = -2 * z * d1;

                return (d1, d2);
            };

            MultiPrecision<N> y = MultiPrecisionUtil.HalleyRootFinding(z0, erf, derf, break_overshoot: true);

            return y;
        }

        private static MultiPrecision<N> InverseErfcRootFinding(MultiPrecision<N> x) {
            MultiPrecision<N> s = 2 / Sqrt(PI);

            MultiPrecision<N> z0;
            if (Length <= 4) {
                const double a = 0.147;

                MultiPrecision<Pow2.N4> xl4 = x.Convert<Pow2.N4>();
                MultiPrecision<Pow2.N4> lg = MultiPrecision<Pow2.N4>.Log((2 - xl4) * xl4);
                MultiPrecision<Pow2.N4> lga = 2 / (MultiPrecision<Pow2.N4>.PI * a) + lg / 2;

                z0 = MultiPrecision<Pow2.N4>.Sqrt(MultiPrecision<Pow2.N4>.Sqrt(lga * lga - lg / a) - lga).Convert<N>();
            }
            else {
                z0 = MultiPrecision<Pow2.N4>.InverseErfcRootFinding(x.Convert<Pow2.N4>()).Convert<N>();
            }

            MultiPrecision<N> erfc(MultiPrecision<N> z) {
                return Erfc(z) - x;
            };
            (MultiPrecision<N>, MultiPrecision<N>) derfc(MultiPrecision<N> z) {
                MultiPrecision<N> d1 = -Exp(-z * z) * s;
                MultiPrecision<N> d2 = -2 * z * d1;

                return (d1, d2);
            };

            MultiPrecision<N> y = MultiPrecisionUtil.HalleyRootFinding(z0, erfc, derfc, break_overshoot: true);

            return y;
        }
    }
}
