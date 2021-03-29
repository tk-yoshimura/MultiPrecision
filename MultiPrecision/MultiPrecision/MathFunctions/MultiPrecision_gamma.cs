using MultiPrecision.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Gamma(MultiPrecision<N> z) {
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
                if (z < Consts.Gamma.Threshold) {
                    MultiPrecision<LanczosExpand<N>> x = LanczosAg(z);
                    MultiPrecision<LanczosExpand<N>> s = z.Convert<LanczosExpand<N>>() - MultiPrecision<LanczosExpand<N>>.Point5;
                    MultiPrecision<LanczosExpand<N>> t = (s + Consts.Gamma.Lanczos.G) / MultiPrecision<LanczosExpand<N>>.E;

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

                return z * (-226800 * EulerGamma
                        + z * (18900 * (PI * PI)
                        + z * (-75600 * Zeta3
                        + z * (630 * Pow(PI, 4)
                        + z * (-45360 * Zeta5
                        + z * (40 * Pow(PI, 6)
                        + z * (-32400 * Zeta7
                        + z * (3 * Pow(PI, 8))))))))) / 226800;
            }

            if ((z - 2).Exponent <= -Bits / 8) {
                z -= 2;

                return z * (226800 * (1 - EulerGamma)
                        + z * (18900 * ((PI * PI) - 6)
                        + z * (75600 * (1 - Zeta3)
                        + z * (630 * (Pow(PI, 4) - 90)
                        + z * (45360 * (1 - Zeta5)
                        + z * (40 * (Pow(PI, 6) - 945)
                        + z * (32400 * (1 - Zeta7)
                        + z * (3 * (Pow(PI, 8) - 9450))))))))) / 226800;
            }

            if (z < Consts.Gamma.Threshold) {
                MultiPrecision<LanczosExpand<N>> x = MultiPrecision<LanczosExpand<N>>.Log(LanczosAg(z));
                MultiPrecision<LanczosExpand<N>> s = z.Convert<LanczosExpand<N>>() - MultiPrecision<LanczosExpand<N>>.Point5;
                MultiPrecision<LanczosExpand<N>> t = MultiPrecision<LanczosExpand<N>>.Log(s + Consts.Gamma.Lanczos.G);

                MultiPrecision<LanczosExpand<N>> y_ex = x + s * (t - 1);

                MultiPrecision<N> y = y_ex.Convert<N>();

                return y;
            }
            else {
                MultiPrecision<SterlingExpand<N>> z_ex = z.Convert<SterlingExpand<N>>();

                MultiPrecision<SterlingExpand<N>> p = (z_ex - MultiPrecision<SterlingExpand<N>>.Point5) * MultiPrecision<SterlingExpand<N>>.Log(z_ex);
                MultiPrecision<SterlingExpand<N>> s = SterlingTerm(z_ex);

                MultiPrecision<SterlingExpand<N>> y = Consts.Gamma.Sterling.LogBias - z_ex + p + s;

                return y.Convert<N>();
            }
        }

        private static MultiPrecision<LanczosExpand<N>> LanczosAg(MultiPrecision<N> z) {
            MultiPrecision<Double<LanczosExpand<N>>> x_ex = Consts.Gamma.Lanczos.Coef[0];
            MultiPrecision<Double<LanczosExpand<N>>> z_ex = (z - 1).Convert<Double<LanczosExpand<N>>>();

            for (int i = 1; i < Consts.Gamma.Lanczos.N; i++) {
                MultiPrecision<Double<LanczosExpand<N>>> w = Consts.Gamma.Lanczos.Coef[i];

                x_ex += w / (z_ex + i);
            }

            MultiPrecision<LanczosExpand<N>> x = x_ex.Convert<LanczosExpand<N>>();
            return x;
        }

        private static MultiPrecision<SterlingExpand<N>> SterlingTerm(MultiPrecision<SterlingExpand<N>> z) {
            MultiPrecision<SterlingExpand<N>> v = 1 / z;
            MultiPrecision<SterlingExpand<N>> w = v * v;

            MultiPrecision<SterlingExpand<N>> x = 0, u = 1;

            foreach (MultiPrecision<SterlingExpand<N>> s in Consts.Gamma.Sterling.Coef) {
                x += u * s;
                u *= w;
            }

            MultiPrecision<SterlingExpand<N>> y = x * v;

            return y;
        }

        private static partial class Consts {
            public static class Gamma {
                public static MultiPrecision<N> Threshold { private set; get; }

                static Gamma() {
                    switch (Length) {
                        case <= 4:
                            Threshold = 150;
                            break;
                        case <= 8:
                            Threshold = 100;
                            break;
                        case <= 16:
                            Threshold = 128;
                            break;
                        case <= 32:
                            Threshold = 250;
                            break;
                        case <= 64:
                            Threshold = 472;
                            break;
                        case <= 128:
                            Threshold = 936;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(Length));
                    }

#if DEBUG
                    Trace.WriteLine($"Gamma<{Length}> initialized.");
#endif
                }

                public static class Lanczos {
                    private static readonly MultiPrecision<LanczosExpand<N>> g = null;
                    private static readonly MultiPrecision<Double<LanczosExpand<N>>>[] coef = null;

                    public static int N => coef.Length;
                    public static MultiPrecision<LanczosExpand<N>> G => g;
                    public static IReadOnlyList<MultiPrecision<Double<LanczosExpand<N>>>> Coef => coef;

                    static Lanczos() {
                        byte[] state = null;

                        switch (Length) {
                            case <= 4:
                                state = Resources.lanczos_mp4;
                                break;
                            case <= 8:
                                state = Resources.lanczos_mp8;
                                break;
                            case <= 16:
                                state = Resources.lanczos_mp16;
                                break;
                            case <= 32:
                                state = Resources.lanczos_mp32;
                                break;
                            case <= 64:
                                state = Resources.lanczos_mp64;
                                break;
                            case <= 128:
                                state = Resources.lanczos_mp128;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(Length));
                        }

                        (MultiPrecision<LanczosExpand<N>> g, MultiPrecision<LanczosExpand<N>>[] table) = ReadLanczosState(state);

                        Lanczos.g = g;

                        coef = table
                            .Select(
                                (v) => v.Convert<Double<LanczosExpand<N>>>()
                            ).ToArray();

#if DEBUG
                        Trace.WriteLine($"Gamma.Lanczos<{Length}> initialized.");
#endif
                    }

                    private static (MultiPrecision<LanczosExpand<N>> g, MultiPrecision<LanczosExpand<N>>[] table) ReadLanczosState(byte[] state) {
                        MultiPrecision<LanczosExpand<N>> g;
                        MultiPrecision<LanczosExpand<N>>[] table;

                        using (MemoryStream stream = new(state)) {
                            using BinaryReader reader = new(stream);

                            int length = reader.ReadInt32();

                            g = reader.ReadMultiPrecision<LanczosExpand<N>>();

                            table = new MultiPrecision<LanczosExpand<N>>[length];

                            for (int i = 0; i < table.Length; i++) {
                                table[i] = reader.ReadMultiPrecision<LanczosExpand<N>>();
                            }
                        }

                        return (g, table);
                    }
                }

                public static class Sterling {
                    private static readonly MultiPrecision<SterlingExpand<N>> logbias = null;
                    private static readonly MultiPrecision<SterlingExpand<N>>[] coef = null;

                    public static int N => coef.Length;
                    public static MultiPrecision<SterlingExpand<N>> LogBias => logbias;
                    public static IReadOnlyList<MultiPrecision<SterlingExpand<N>>> Coef => coef;

                    static Sterling() {
                        int terms;

                        switch (Length) {
                            case <= 4:
                                terms = 10;
                                break;
                            case <= 8:
                                terms = 26;
                                break;
                            case <= 16:
                                terms = 62;
                                break;
                            case <= 32:
                                terms = 124;
                                break;
                            case <= 64:
                                terms = 258;
                                break;
                            case <= 128:
                                terms = 518;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(Length));
                        }

                        logbias = MultiPrecision<SterlingExpand<N>>.Log(
                            MultiPrecision<SterlingExpand<N>>.Sqrt(MultiPrecision<SterlingExpand<N>>.PI * 2)
                        );

                        coef = new MultiPrecision<SterlingExpand<N>>[terms];

                        for (int i = 0, k = 1; i < coef.Length; i++, k++) {
                            coef[i] = MultiPrecision<SterlingExpand<N>>.BernoulliSequence(k) / checked((2 * k) * (2 * k - 1));
                        }

#if DEBUG
                        Trace.WriteLine($"Gamma.Sterling<{Length}> initialized.");
#endif
                    }
                }
            }
        }
    }

    internal struct LanczosExpand<N> : IConstant where N : struct, IConstant {
        public int Value {
            get {
                int length = default(N).Value;
                switch (length) {
                    case <= 4:
                        return 6;
                    case <= 8:
                        return 12;
                    case <= 16:
                        return 20;
                    case <= 32:
                        return 40;
                    case <= 64:
                        return 80;
                    case <= 128:
                        return 160;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(N));
                }
            }
        }
    }

    internal struct SterlingExpand<N> : IConstant where N : struct, IConstant {
        public int Value {
            get {
                int length = default(N).Value;
                switch (length) {
                    case <= 4:
                        return 8;
                    case <= 8:
                        return 16;
                    case <= 16:
                        return 32;
                    case <= 32:
                        return 64;
                    case <= 64:
                        return 128;
                    case <= 128:
                        return 256;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(N));
                }
            }
        }
    }
}