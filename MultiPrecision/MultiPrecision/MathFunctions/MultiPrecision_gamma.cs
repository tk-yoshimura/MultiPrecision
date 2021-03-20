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
                MultiPrecision<LanczosExpand<N>> x = Ag(z);
                MultiPrecision<LanczosExpand<N>> s = z.Convert<LanczosExpand<N>>() - Consts.Gamma.Point5;
                MultiPrecision<LanczosExpand<N>> t = (s + Consts.Gamma.G) / MultiPrecision<LanczosExpand<N>>.E;

                MultiPrecision<LanczosExpand<N>> y_ex = MultiPrecision<LanczosExpand<N>>.Pow(t, s) * x;

                MultiPrecision<N> y = y_ex.Convert<N>();

                return y;
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

            if (z.Exponent < 2) {
                return Log(Gamma(z));
            }

            MultiPrecision<LanczosExpand<N>> x = MultiPrecision<LanczosExpand<N>>.Log(Ag(z));
            MultiPrecision<LanczosExpand<N>> s = z.Convert<LanczosExpand<N>>() - Consts.Gamma.Point5;
            MultiPrecision<LanczosExpand<N>> t = MultiPrecision<LanczosExpand<N>>.Log(s + Consts.Gamma.G);

            MultiPrecision<LanczosExpand<N>> y_ex = x + s * (t - MultiPrecision<LanczosExpand<N>>.One);

            MultiPrecision<N> y = y_ex.Convert<N>();

            return y;
        }

        private static MultiPrecision<LanczosExpand<N>> Ag(MultiPrecision<N> z) {
            MultiPrecision<Double<LanczosExpand<N>>> x_ex = Consts.Gamma.Coef[0];
            MultiPrecision<Double<LanczosExpand<N>>> z_ex = (z - 1).Convert<Double<LanczosExpand<N>>>();

            for (int i = 1; i < Consts.Gamma.N; i++) {
                MultiPrecision<Double<LanczosExpand<N>>> w = Consts.Gamma.Coef[i];

                x_ex += w / (z_ex + i);
            }

            MultiPrecision<LanczosExpand<N>> x = x_ex.Convert<LanczosExpand<N>>();
            return x;
        }

        private static partial class Consts {
            public static class Gamma {
                private static MultiPrecision<LanczosExpand<N>> p5 = null, lanczos_g = null;
                private static MultiPrecision<Double<LanczosExpand<N>>>[] lanczos_coef = null;

                public static MultiPrecision<LanczosExpand<N>> Point5 => p5;
                public static MultiPrecision<LanczosExpand<N>> G => lanczos_g;
                public static int N => lanczos_coef.Length;
                public static IReadOnlyList<MultiPrecision<Double<LanczosExpand<N>>>> Coef => lanczos_coef;

                public static bool Initialized { private set; get; } = false;

                public static void Initialize() {
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

                    (MultiPrecision<LanczosExpand<N>> g, MultiPrecision<LanczosExpand<N>>[] table) = ReadState(state);

                    p5 = MultiPrecision<LanczosExpand<N>>.Ldexp(MultiPrecision<LanczosExpand<N>>.One, -1);
                    lanczos_g = g;

                    lanczos_coef = table
                        .Select(
                            (v) => v.Convert<Double<LanczosExpand<N>>>()
                        ).ToArray();

                    Initialized = true;
                }

                private static (MultiPrecision<LanczosExpand<N>> g, MultiPrecision<LanczosExpand<N>>[] table) ReadState(byte[] state) {
                    MultiPrecision<LanczosExpand<N>> g;
                    MultiPrecision<LanczosExpand<N>>[] table;

                    using (MemoryStream stream = new MemoryStream(state)) {
                        using (BinaryReader reader = new BinaryReader(stream)) {
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
}