using MultiPrecision.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Gamma(MultiPrecision<N> z) {
            if (!Consts.Gamma.Initialized) {
                Consts.Gamma.Initialize();
            }

            if (z.IsNaN || (z.Sign == Sign.Minus && !z.IsFinite)) {
                return NaN;
            }

            if (z.IsZero || (z.Sign == Sign.Plus && !z.IsFinite)) {
                return PositiveInfinity;
            }

            if (z.Sign == Sign.Minus || z.Exponent < -1) {
                MultiPrecision<N> sinpi = SinPI(z);

                if (sinpi.IsZero) {
                    return NaN;
                }

                MultiPrecision<N> y = PI / (sinpi * Gamma(1 - z));

                return y;
            }
            else {
                if (z < Consts.Gamma.SterlingThreshold) {
                    MultiPrecision<LanczosExpand<N>> x = LanczosAg(z);
                    MultiPrecision<LanczosExpand<N>> s = z.Convert<LanczosExpand<N>>() - MultiPrecision<LanczosExpand<N>>.Point5;
                    MultiPrecision<LanczosExpand<N>> t = (s + Consts.Gamma.LanczosG) / MultiPrecision<LanczosExpand<N>>.E;

                    MultiPrecision<LanczosExpand<N>> y_ex = MultiPrecision<LanczosExpand<N>>.Pow(t, s) * x;

                    MultiPrecision<N> y = y_ex.Convert<N>();

                    return y;
                }
                else {
                    MultiPrecision<SterlingExpand<N>> z_ex = z.Convert<SterlingExpand<N>>();

                    MultiPrecision<SterlingExpand<N>> r = MultiPrecision<SterlingExpand<N>>.Sqrt(2 * MultiPrecision<SterlingExpand<N>>.PI / z_ex);
                    MultiPrecision<SterlingExpand<N>> p = MultiPrecision<SterlingExpand<N>>.Pow(z_ex / MultiPrecision<SterlingExpand<N>>.E, z_ex);
                    MultiPrecision<SterlingExpand<N>> s = MultiPrecision<SterlingExpand<N>>.Exp(SterlingTerm(z_ex));

                    MultiPrecision<SterlingExpand<N>> y = r * p * s;

                    return y.Convert<N>();
                }
            }
        }

        public static MultiPrecision<N> LogGamma(MultiPrecision<N> z) {
            if (!Consts.Gamma.Initialized) {
                Consts.Gamma.Initialize();
            }

            if (z.IsNaN || z.IsZero || z.Sign == Sign.Minus) {
                return NaN;
            }

            if (!z.IsFinite) {
                return PositiveInfinity;
            }

            if (z.Exponent < -2) {
                return Log(Gamma(z));
            }

            if ((z - 1).Exponent <= -Bits / 8) {
                z -= 1;

                return z * (-EulerGamma
                        + z * ((PI * PI / 12)
                        + z * (-(Zeta3 / 3)
                        + z * ((Pow(PI, 4) / 360)
                        + z * (-(Zeta5 / 5)
                        + z * ((Pow(PI, 6) / 5670)
                        + z * (-(Zeta7 / 7)
                        + z * (Pow(PI, 8) / 75600))))))));
            }

            if ((z - 2).Exponent <= -Bits / 8) {
                z -= 2;

                return z * ((1 - EulerGamma)
                        + z * (((PI * PI - 6) / 12)
                        + z * (((1 - Zeta3) / 3)
                        + z * (((Pow(PI, 4) - 90) / 360)
                        + z * (((1 - Zeta5) / 5)
                        + z * (((Pow(PI, 6) - 945) / 5670)
                        + z * (((1 - Zeta7) / 7)
                        + z * ((Pow(PI, 8) - 9450) / 75600))))))));
            }

            if (z < Consts.Gamma.SterlingThreshold) {
                MultiPrecision<LanczosExpand<N>> x = MultiPrecision<LanczosExpand<N>>.Log(LanczosAg(z));
                MultiPrecision<LanczosExpand<N>> s = z.Convert<LanczosExpand<N>>() - MultiPrecision<LanczosExpand<N>>.Point5;
                MultiPrecision<LanczosExpand<N>> t = MultiPrecision<LanczosExpand<N>>.Log(s + Consts.Gamma.LanczosG);

                MultiPrecision<LanczosExpand<N>> y_ex = x + s * (t - 1);

                MultiPrecision<N> y = y_ex.Convert<N>();

                return y;
            }
            else {
                MultiPrecision<SterlingExpand<N>> z_ex = z.Convert<SterlingExpand<N>>();

                MultiPrecision<SterlingExpand<N>> p = (z_ex - MultiPrecision<SterlingExpand<N>>.Point5) * MultiPrecision<SterlingExpand<N>>.Log(z_ex);
                MultiPrecision<SterlingExpand<N>> s = SterlingTerm(z_ex);

                MultiPrecision<SterlingExpand<N>> y = Consts.Gamma.SterlingLogBias - z_ex + p + s;

                return y.Convert<N>();
            }
        }

        private static MultiPrecision<LanczosExpand<N>> LanczosAg(MultiPrecision<N> z) {
            MultiPrecision<Double<LanczosExpand<N>>> x_ex = Consts.Gamma.LanczosCoef[0];
            MultiPrecision<Double<LanczosExpand<N>>> z_ex = (z - 1).Convert<Double<LanczosExpand<N>>>();

            for (int i = 1; i < Consts.Gamma.LanczosN; i++) {
                MultiPrecision<Double<LanczosExpand<N>>> w = Consts.Gamma.LanczosCoef[i];

                x_ex += w / (z_ex + i);
            }

            MultiPrecision<LanczosExpand<N>> x = x_ex.Convert<LanczosExpand<N>>();
            return x;
        }

        private static MultiPrecision<SterlingExpand<N>> SterlingTerm(MultiPrecision<SterlingExpand<N>> z) {
            MultiPrecision<SterlingExpand<N>> v = 1 / z;
            MultiPrecision<SterlingExpand<N>> w = v * v;

            MultiPrecision<SterlingExpand<N>> x = 0, u = 1;

            foreach (MultiPrecision<SterlingExpand<N>> s in Consts.Gamma.SterlingCoef) {
                x += u * s;
                u *= w;
            }

            MultiPrecision<SterlingExpand<N>> y = x * v;

            return y;
        }

        private static partial class Consts {
            public static class Gamma {
                private static MultiPrecision<LanczosExpand<N>> lanczos_g = null;
                private static MultiPrecision<Double<LanczosExpand<N>>>[] lanczos_coef = null;

                private static MultiPrecision<N> sterling_threshold = null;
                private static MultiPrecision<SterlingExpand<N>> sterling_logbias = null;
                private static MultiPrecision<SterlingExpand<N>>[] sterling_coef = null;

                public static MultiPrecision<LanczosExpand<N>> LanczosG => lanczos_g;
                public static int LanczosN => lanczos_coef.Length;
                public static IReadOnlyList<MultiPrecision<Double<LanczosExpand<N>>>> LanczosCoef => lanczos_coef;

                public static int SterlingN => sterling_coef.Length;

                public static MultiPrecision<N> SterlingThreshold => sterling_threshold;
                public static MultiPrecision<SterlingExpand<N>> SterlingLogBias => sterling_logbias;
                public static IReadOnlyList<MultiPrecision<SterlingExpand<N>>> SterlingCoef => sterling_coef;

                public static bool Initialized { private set; get; } = false;

                public static void Initialize() {
                    InitializeLanczos();
                    InitializeSterling();

                    Initialized = true;
                }

                private static void InitializeLanczos() {
                    byte[] state = null;

                    if (Length <= 4) {
                        state = Resources.lanczos_mp4;
                    }
                    else if (Length <= 8) {
                        state = Resources.lanczos_mp8;
                    }
                    else if (Length <= 16) {
                        state = Resources.lanczos_mp16;
                    }
                    else if (Length <= 32) {
                        state = Resources.lanczos_mp32;
                    }
                    else if (Length <= 64) {
                        state = Resources.lanczos_mp64;
                    }
                    else if (Length <= 128) {
                        state = Resources.lanczos_mp128;
                    }
                    else {
                        throw new ArgumentOutOfRangeException(nameof(Length));
                    }

                    (MultiPrecision<LanczosExpand<N>> g, MultiPrecision<LanczosExpand<N>>[] table) = ReadLanczosState(state);

                    lanczos_g = g;

                    lanczos_coef = table
                        .Select(
                            (v) => v.Convert<Double<LanczosExpand<N>>>()
                        ).ToArray();
                }

                private static (MultiPrecision<LanczosExpand<N>> g, MultiPrecision<LanczosExpand<N>>[] table) ReadLanczosState(byte[] state) {
                    MultiPrecision<LanczosExpand<N>> g;
                    MultiPrecision<LanczosExpand<N>>[] table;

                    using (MemoryStream stream = new(state)) {
                        using (BinaryReader reader = new(stream)) {
                            int length = reader.ReadInt32();

                            g = reader.ReadMultiPrecision<LanczosExpand<N>>();

                            table = new MultiPrecision<LanczosExpand<N>>[length];

                            for (int i = 0; i < table.Length; i++) {
                                table[i] = reader.ReadMultiPrecision<LanczosExpand<N>>();
                            }
                        }
                    }

                    return (g, table);
                }

                private static void InitializeSterling() {
                    int terms;

                    if (Length <= 4) {
                        terms = 10;
                        sterling_threshold = 150;
                    }
                    else if (Length <= 8) {
                        terms = 26;
                        sterling_threshold = 100;
                    }
                    else if (Length <= 16) {
                        terms = 62;
                        sterling_threshold = 128;
                    }
                    else if (Length <= 32) {
                        terms = 124;
                        sterling_threshold = 250;
                    }
                    else if (Length <= 64) {
                        terms = 258;
                        sterling_threshold = 472;
                    }
                    else if (Length <= 128) {
                        terms = 518;
                        sterling_threshold = 936;
                    }
                    else {
                        throw new ArgumentOutOfRangeException(nameof(Length));
                    }

                    sterling_logbias = MultiPrecision<SterlingExpand<N>>.Log(
                        MultiPrecision<SterlingExpand<N>>.Sqrt(MultiPrecision<SterlingExpand<N>>.PI * 2)
                    );

                    sterling_coef = new MultiPrecision<SterlingExpand<N>>[terms];

                    for (int i = 0, k = 1; i < sterling_coef.Length; i++, k++) {
                        sterling_coef[i] = MultiPrecision<SterlingExpand<N>>.BernoulliSequence(k) / checked((2 * k) * (2 * k - 1));
                    }
                }
            }
        }
    }

    internal struct LanczosExpand<N> : IConstant where N : struct, IConstant {
        public int Value {
            get {
                int length = default(N).Value;
                if (length <= 4) {
                    return 6;
                }
                else if (length <= 8) {
                    return 12;
                }
                else if (length <= 16) {
                    return 20;
                }
                else if (length <= 32) {
                    return 40;
                }
                else if (length <= 64) {
                    return 80;
                }
                else if (length <= 128) {
                    return 160;
                }
                else {
                    throw new ArgumentOutOfRangeException(nameof(N));
                }
            }
        }
    }

    internal struct SterlingExpand<N> : IConstant where N : struct, IConstant {
        public int Value {
            get {
                int length = default(N).Value;
                if (length <= 4) {
                    return 8;
                }
                else if (length <= 8) {
                    return 16;
                }
                else if (length <= 16) {
                    return 32;
                }
                else if (length <= 32) {
                    return 64;
                }
                else if (length <= 64) {
                    return 128;
                }
                else if (length <= 128) {
                    return 256;
                }
                else {
                    throw new ArgumentOutOfRangeException(nameof(N));
                }
            }
        }
    }
}