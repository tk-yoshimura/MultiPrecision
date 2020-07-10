using System;
using System.Collections.Generic;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {
        public static MultiPrecision<N> Zeta(int n) {
            throw new NotImplementedException();
        }

        private static partial class Consts {
            public static class Zeta {
                public static bool Initialized { private set; get; } = false;
                public static IReadOnlyDictionary<int, MultiPrecision<N>> Table { private set; get; } = null;
                public static MultiPrecision<Plus1<N>> ApproxB { private set; get; } = null;

                public static void Initialize() {
                    Table = new Dictionary<int, MultiPrecision<N>>() { 
                        { -13, Div(-1, 12) },
                        { -11, Div(691, 32760) },
                        { -9, Div(-1, 132) },
                        { -7, Div(1, 240) },
                        { -5, Div(-1, 252) },
                        { -3, Div(1, 120) },
                        { -1, Div(-1, 12) },
                        { 0, Ldexp(-1, -1) },
                        { 1, PositiveInfinity },
                        { 2, Pow(PI, 2) / 6 },
                        { 4, Pow(PI, 4) / 90 },
                        { 6, Pow(PI, 6) / 945 },
                        { 8, Pow(PI, 8) / 9450 },
                        { 10, Pow(PI, 10) / 93555 },
                        { 12, Pow(PI, 12) * 691 / 638512875 },
                        { 14, Pow(PI, 14) * 2 / 18243225 },
                    };

                    Initialized = true;
                }
            }
        }
    }
}
