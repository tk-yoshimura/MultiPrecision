using MultiPrecision.Properties;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Gamma(MultiPrecision<N> x) {
            if (IsNaN(x) || (x.Sign == Sign.Minus && !IsFinite(x))) {
                return NaN;
            }

            if (IsZero(x) || (x.Sign == Sign.Plus && !IsFinite(x))) {
                return PositiveInfinity;
            }

            if (x.Sign == Sign.Minus || x.Exponent < -1) {
                MultiPrecision<N> sinpi = SinPI(x);

                if (IsZero(sinpi)) {
                    return NaN;
                }

                MultiPrecision<N> y = PI / (sinpi * Gamma(1 - x));

                return y;
            }
            else {
                if (x < Consts.Gamma.Threshold) {
                    MultiPrecision<LanczosExpand<N>> a = LanczosAg(x);
                    MultiPrecision<LanczosExpand<N>> s = x.Convert<LanczosExpand<N>>() - MultiPrecision<LanczosExpand<N>>.Point5;
                    MultiPrecision<LanczosExpand<N>> t = (s + Consts.Gamma.Lanczos.G) / MultiPrecision<LanczosExpand<N>>.E;

                    MultiPrecision<LanczosExpand<N>> y_ex = MultiPrecision<LanczosExpand<N>>.Pow(t, s) * a;

                    MultiPrecision<N> y = y_ex.Convert<N>();

                    return y;
                }
                else {
                    MultiPrecision<SterlingExpand<N>> z_ex = x.Convert<SterlingExpand<N>>();

                    MultiPrecision<SterlingExpand<N>> r = MultiPrecision<SterlingExpand<N>>.Sqrt(2 * MultiPrecision<SterlingExpand<N>>.PI / z_ex);
                    MultiPrecision<SterlingExpand<N>> p = MultiPrecision<SterlingExpand<N>>.Pow(z_ex / MultiPrecision<SterlingExpand<N>>.E, z_ex);
                    MultiPrecision<SterlingExpand<N>> s = MultiPrecision<SterlingExpand<N>>.Exp(SterlingTerm(z_ex));

                    MultiPrecision<SterlingExpand<N>> y = r * p * s;

                    return y.Convert<N>();
                }
            }
        }

        public static MultiPrecision<N> LogGamma(MultiPrecision<N> x) {
            if (IsNaN(x) || IsZero(x) || x.Sign == Sign.Minus) {
                return NaN;
            }

            if (!IsFinite(x)) {
                return PositiveInfinity;
            }

            if (x.Exponent < -1) {
                return Log(Gamma(x));
            }

            if ((x - 1).Exponent <= -Bits / 8) {
                x -= 1;

                return x * (-226800 * EulerGamma
                        + x * (18900 * (PI * PI)
                        + x * (-75600 * Zeta3
                        + x * (630 * Pow(PI, 4)
                        + x * (-45360 * Zeta5
                        + x * (40 * Pow(PI, 6)
                        + x * (-32400 * Zeta7
                        + x * (3 * Pow(PI, 8))))))))) / 226800;
            }

            if ((x - 2).Exponent <= -Bits / 8) {
                x -= 2;

                return x * (226800 * (1 - EulerGamma)
                        + x * (18900 * ((PI * PI) - 6)
                        + x * (75600 * (1 - Zeta3)
                        + x * (630 * (Pow(PI, 4) - 90)
                        + x * (45360 * (1 - Zeta5)
                        + x * (40 * (Pow(PI, 6) - 945)
                        + x * (32400 * (1 - Zeta7)
                        + x * (3 * (Pow(PI, 8) - 9450))))))))) / 226800;
            }

            if (x < Consts.Gamma.Threshold) {
                MultiPrecision<LanczosExpand<N>> a = MultiPrecision<LanczosExpand<N>>.Log(LanczosAg(x));
                MultiPrecision<LanczosExpand<N>> s = x.Convert<LanczosExpand<N>>() - MultiPrecision<LanczosExpand<N>>.Point5;
                MultiPrecision<LanczosExpand<N>> t = MultiPrecision<LanczosExpand<N>>.Log(s + Consts.Gamma.Lanczos.G);

                MultiPrecision<LanczosExpand<N>> y_ex = a + s * (t - 1);

                MultiPrecision<N> y = y_ex.Convert<N>();

                return y;
            }
            else {
                MultiPrecision<SterlingExpand<N>> z_ex = x.Convert<SterlingExpand<N>>();

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
                MultiPrecision<SterlingExpand<N>> c = u * s;

                x += c;

                if (MultiPrecision<SterlingExpand<N>>.IsZero(c) || x.Exponent - c.Exponent > MultiPrecision<SterlingExpand<N>>.Bits) {
                    break;
                }

                u *= w;
            }

            MultiPrecision<SterlingExpand<N>> y = x * v;

            return y;
        }

        private static partial class Consts {
            public static class Gamma {
                public static MultiPrecision<N> Threshold { private set; get; }

                static Gamma() {
                    Threshold = Length switch {
                        <= 4 => (MultiPrecision<N>)16,
                        <= 8 => (MultiPrecision<N>)32,
                        <= 16 => (MultiPrecision<N>)60,
                        <= 32 => (MultiPrecision<N>)116,
                        <= 64 => (MultiPrecision<N>)228,
                        <= 128 => (MultiPrecision<N>)456,
                        <= 256 => (MultiPrecision<N>)908,
                        _ => throw new ArgumentOutOfRangeException(nameof(Length)),
                    };

#if DEBUG
                    Trace.WriteLine($"Gamma<{Length}> initialized.");
#endif
                }

                public static class Lanczos {
                    private static readonly MultiPrecision<LanczosExpand<N>> g = null;
                    private static readonly MultiPrecision<Double<LanczosExpand<N>>>[] coef = null;

                    public static int N => coef.Length;
                    public static MultiPrecision<LanczosExpand<N>> G => g;
                    public static ReadOnlyCollection<MultiPrecision<Double<LanczosExpand<N>>>> Coef => Array.AsReadOnly(coef);

                    static Lanczos() {
                        byte[] state = null;

                        state = Length switch {
                            <= 4 => Resources.lanczos_mp4,
                            <= 8 => Resources.lanczos_mp8,
                            <= 16 => Resources.lanczos_mp16,
                            <= 32 => Resources.lanczos_mp32,
                            <= 64 => Resources.lanczos_mp64,
                            <= 128 => Resources.lanczos_mp128,
                            <= 256 => Resources.lanczos_mp256,
                            _ => throw new ArgumentOutOfRangeException(nameof(Length)),
                        };
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
                    public static ReadOnlyCollection<MultiPrecision<SterlingExpand<N>>> Coef => Array.AsReadOnly(coef);

                    static Sterling() {
                        var terms = Length switch {
                            <= 4 => 32,
                            <= 8 => 60,
                            <= 16 => 134,
                            <= 32 => 294,
                            <= 64 => 640,
                            <= 128 => 1262,
                            <= 256 => 2608,
                            _ => throw new ArgumentOutOfRangeException(nameof(Length)),
                        };
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
        public readonly int Value {
            get {
                int length = default(N).Value;
                return length switch {
                    <= 4 => 5,
                    <= 8 => 10,
                    <= 16 => 20,
                    <= 32 => 40,
                    <= 64 => 80,
                    <= 128 => 160,
                    <= 256 => 320,
                    _ => throw new ArgumentOutOfRangeException(
                                                "In the gamma function, the calculation is invalid for precision greater than 256 in length.",
                                                nameof(N)
                                            ),
                };
            }
        }
    }

    internal struct SterlingExpand<N> : IConstant where N : struct, IConstant {
        public readonly int Value {
            get {
                int length = default(N).Value;
                return length switch {
                    <= 4 => 5,
                    <= 8 => 9,
                    <= 16 => 17,
                    <= 32 => 33,
                    <= 64 => 65,
                    <= 128 => 129,
                    <= 256 => 257,
                    _ => throw new ArgumentOutOfRangeException(
                                                "In the gamma function, the calculation is invalid for precision greater than 256 in length.",
                                                nameof(N)
                                            ),
                };
            }
        }
    }
}