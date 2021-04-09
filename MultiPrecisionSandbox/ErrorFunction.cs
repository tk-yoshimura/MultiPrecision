using static System.Math;

namespace MultiPrecisionSandbox {
    public static class ErrorFunction {
        static readonly double c = 1 / Sqrt(PI);

        static readonly double[] ts_nz = new double[] {
            -1d / 3d,
            +1d / 10d,
            -1d / 42d,
            +1d / 216d,
            -1d / 1320d,
            +1d / 9360d,
            -1d / 75600d,
            +1d / 685440d,
            -1d / 6894720d,
            +1d / 76204800d,
            -1d / 918086400d,
            +1d / 11975040000d,
            -1d / 168129561600d,
            +1d / 2528170444800d,
            -1d / 40537905408000d,
            +1d / 690452066304000d,
        };

        static readonly double[] rs_1p5 = new double[] {
            +3.10959374658724304747,
            +1.58216379147219475644,
            +6.86581462686392581682e-2,
            -2.12933615281174988772e-2,
            +5.57089639520217477139e-3,
            -1.14835497518737868660e-3,
            +1.39417507329783315423e-4,
            +1.87655791394342838676e-5,
            -1.89098763825169480367e-5,
            +7.25603370377527754519e-6,
            -1.87765133260417411004e-6,
            +2.96494347867468959004e-7,
            +1.24853727986010693082e-8,
            -2.98636521903145840244e-8,
            +1.29951459936111140962e-8,
            -3.66462961828327543879e-9,
            +6.58452450693565385952e-10,
        };

        static readonly double[] rs_2p5 = new double[] {
            +4.74368980487682914473,
            +1.67300808283044683760,
            +2.88681492582322304165e-2,
            -7.67641573732775733597e-3,
            +1.86520884894688752364e-3,
            -4.09600642713004542233e-4,
            +7.91319763740753329281e-5,
            -1.25417512428482236160e-5,
            +1.24401927494939114762e-6,
            +1.11729622704882375499e-7,
            -1.06577836515170629194e-7,
            +3.73672347994704160224e-8,
            -9.68791793029374656766e-9,
            +2.00705758562891713156e-9,
            -3.13517047235050787604e-10,
        };

        static readonly double[] rs_3p5 = new double[] {
            +6.43941309823043372807,
            +1.71352517475253703158,
            +1.38933570584877952850e-2,
            -3.09714092255103471508e-3,
            +6.52696270909484307285e-4,
            -1.29671165048597283445e-4,
            +2.41292118133577937105e-5,
            -4.15189762523462461597e-6,
            +6.43611326450812548159e-7,
            -8.44720936946712699581e-8,
            +7.55373556561019923784e-9,
            +2.63663467366442802600e-10,
            -3.52909361611086312255e-10,
            +1.13947834533010736477e-10,
            -2.74281792359942252744e-11,
        };

        public static double Erf(double z) {
            if (double.IsNaN(z)) {
                return double.NaN;
            }
            if (z < 0) {
                return -Erf(Abs(z));
            }

            if (z <= 1) {
                return ErfNearZero(z);
            }
            if (z <= 2) {
                return 1 - ErfcRange12(z);
            }
            if (z <= 3) {
                return 1 - ErfcRange23(z);
            }
            if (z <= 4) {
                return 1 - ErfcRange34(z);
            }
            if (z <= 6) {
                return 1 - ErfcLarger4(z);
            }

            return 1;
        }

        public static double Erfc(double z) {
            if (double.IsNaN(z)) {
                return double.NaN;
            }

            if (z <= 1) {
                return 1 - Erf(z);
            }
            if (z <= 2) {
                return ErfcRange12(z);
            }
            if (z <= 3) {
                return ErfcRange23(z);
            }
            if (z <= 4) {
                return ErfcRange34(z);
            }
            if (z <= 27.25) {
                return ErfcLarger4(z);
            }

            return 0;
        }

        public static double InverseErf(double z) {
            if (double.IsNaN(z) || z < -1 || z > 1) {
                return double.NaN;
            }

            if (z == -1) {
                return double.NegativeInfinity;
            }
            if (z == 1) {
                return double.PositiveInfinity;
            }
            if (z < 0) { 
                return -InverseErf(Abs(z));
            }

            if (z < 1.220703125e-4) { 
                double w = PI * z * z;
                double t = Sqrt(PI) * ((40320 + w * (3360 + w * (588 + w * 127))) / 80640);

                return z * t;
            }

            if (z < 0.5) {
                return InverseErfRootFinding(z);
            }
            else {
                return InverseErfcRootFinding(1 - z);
            }
        }

        public static double InverseErfc(double z) {
            if (double.IsNaN(z) || z < 0 || z > 2) {
                return double.NaN;
            }

            if (z == 0) {
                return double.PositiveInfinity;
            }
            if (z == 2) {
                return double.NegativeInfinity;
            }
            if (z >= 0.5) { 
                return InverseErf(1 - z);
            }

            return InverseErfcRootFinding(z);
        }

        private static double ErfNearZero(double z) {
            double w = z * z, v = w;

            double x = 1;

            foreach (double t in ts_nz) {
                x = FusedMultiplyAdd(t, v, x);
                v *= w;
            }

            double y = 2 * x * z * c;

            return y;
        }

        private static double ErfcRange12(double z) {
            double w = z - 1.5, v = 1;

            double x = 0;

            foreach (double r in rs_1p5) {
                x = FusedMultiplyAdd(r, v, x);
                v *= w;
            }

            double y = Exp(-z * z) / x;

            return y;
        }

        private static double ErfcRange23(double z) {
            double w = z - 2.5, v = 1;

            double x = 0;

            foreach (double r in rs_2p5) {
                x = FusedMultiplyAdd(r, v, x);
                v *= w;
            }

            double y = Exp(-z * z) / x;

            return y;
        }

        private static double ErfcRange34(double z) {
            double w = z - 3.5, v = 1;

            double x = 0;

            foreach (double r in rs_3p5) {
                x = FusedMultiplyAdd(r, v, x);
                v *= w;
            }

            double y = Exp(-z * z) / x;

            return y;
        }

        private static double ErfcLarger4(double z) {
            double w = z * z;

            double v = c * z * Exp(-w);

            double f = (12244648500d + w * (54875205000d + w * (68846500800d + w * (37349769600d + w * (10426416000d
                         + w * (1623847680d + w * (145259520d + w * (7346176d + w * (193536d + w * (2048d))))))))))
                        / (Sqrt(9370713555534515625d + w * (535167089943483375000d + w * (4947478840052713125000d + w * (17801196288958674900000d
                        + w * (33278674970250482850000d + w * (37290873521952388800000d + w * (27264582429908483520000d + w * (13741531501742634240000d
                        + w * (4956468805290339648000d + w * (1313137187487207936000d + w * (260148927642095923200d + w * (38989707018393600000d
                        + w * (4448461822571520000d + w * (386826204463104000d + w * (25529300926464000d + w * (1265116446720000d
                        + w * (46163815628800d + w * (1199819980800d + w * (20963655680d + w * (220200960d + w * (1048576d)))))))))))))))))))))
                        + (-3061162125d + w * (-35034079500d + w * (-58620523500d + w * (-30938846400d + w * (-4784551200d
                        + w * (747626880d + w * (339830400d + w * (45081600d + w * (2850560d + w * (87040d + w * (1024d))))))))))));

            return v * f;
        }

        private static double InverseErfRootFinding(double x) { 
            const double a = 0.147;

            double s = 2 * c;
            double lg = Log(1 - x * x), lga = 2 / (PI * a) + lg / 2;
            double z = Sqrt(Sqrt(lga * lga - lg / a) - lga);

            for (int i = 0; i < 8; i++) { 
                double y = Erf(z) - x;
                double df1 = Exp(-z * z) * s;
                double df2 = -2 * z * df1;
                double dz = (2 * y * df1) / (2 * df1 * df1 - y * df2);

                if (z == (z - dz)) {
                    break;
                }

                z -= dz;
            }

            return z;
        }

        private static double InverseErfcRootFinding(double x) { 
            const double a = 0.147;

            double s = 2 * c;
            double lg = Log((2 - x) * x), lga = 2 / (PI * a) + lg / 2;
            double z = Sqrt(Sqrt(lga * lga - lg / a) - lga);

            for (int i = 0; i < 8; i++) { 
                double y = Erfc(z) - x;
                double df1 = -Exp(-z * z) * s;
                double df2 = -2 * z * df1;
                double dz = (2 * y * df1) / (2 * df1 * df1 - y * df2);

                if (z == (z - dz)) {
                    break;
                }

                z -= dz;
            }

            return z;
        }
    }
}
