using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> InverseErf(MultiPrecision<N> x) {
            if (x.IsNaN || x < MinusOne || x > One) {
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
            if (x.IsNaN || x < Zero || x > 2) {
                return NaN;
            }
            if (x == Zero) {
                return PositiveInfinity;
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

                double xd = (double)x, lg = Math.Log(1 - xd * xd), lga = 2 / (Math.PI * a) + lg / 2;
                z0 = Math.Sqrt(Math.Sqrt(lga * lga - lg / a) - lga);
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

            MultiPrecision<N> y = MultiPrecisionUtil.HalleyRootFinding(z0, erf, derf);

            return y;
        }

        private static MultiPrecision<N> InverseErfcRootFinding(MultiPrecision<N> x) {
            MultiPrecision<N> s = 2 / Sqrt(PI);

            MultiPrecision<N> z0;
            if (Length <= 4) {
                const double a = 0.147;

                double xd = (double)x, lg = Math.Log((2 - xd) * xd), lga = 2 / (Math.PI * a) + lg / 2;
                z0 = Math.Sqrt(Math.Sqrt(lga * lga - lg / a) - lga);
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

            MultiPrecision<N> y = MultiPrecisionUtil.HalleyRootFinding(z0, erfc, derfc);

            return y;
        }
    }
}
