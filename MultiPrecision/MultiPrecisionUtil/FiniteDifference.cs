using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {

    public static partial class MultiPrecisionUtil {
        public static (MultiPrecision<N> value, MultiPrecision<N> error) FiniteDifference<N>(Func<MultiPrecision<N>, MultiPrecision<N>> f,
                                                                                             MultiPrecision<N> x,
                                                                                             [AllowNull] MultiPrecision<N> h = null) where N : struct, IConstant {
            if (f is null) {
                throw new ArgumentNullException(nameof(f));
            }
            if (h is not null) {
                if (h.IsZero || !h.IsFinite || h.Sign == Sign.Minus) {
                    throw new ArgumentOutOfRangeException(nameof(h));
                }
            }

            if (h is null) {
                long exponent = Math.Max(0, x.Exponent) - MultiPrecision<N>.Bits / 16;

                h = MultiPrecision<N>.Ldexp(1, exponent);
            }

            MultiPrecision<Plus1<N>> h_ex = h.Convert<Plus1<N>>();

            MultiPrecision<N> h1 = h, h2 = 2 * h, h3 = 3 * h, h4 = 4 * h, h5 = 5 * h, h6 = 6 * h, h7 = 7 * h, h8 = 8 * h;
            MultiPrecision<N> h9 = 9 * h, h10 = 10 * h, h11 = 11 * h, h12 = 12 * h, h13 = 13 * h, h14 = 14 * h, h15 = 15 * h, h16 = 16 * h;

            MultiPrecision<Plus1<N>> dy1 = f(x + h1).Convert<Plus1<N>>() - f(x - h1).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy2 = f(x + h2).Convert<Plus1<N>>() - f(x - h2).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy3 = f(x + h3).Convert<Plus1<N>>() - f(x - h3).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy4 = f(x + h4).Convert<Plus1<N>>() - f(x - h4).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy5 = f(x + h5).Convert<Plus1<N>>() - f(x - h5).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy6 = f(x + h6).Convert<Plus1<N>>() - f(x - h6).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy7 = f(x + h7).Convert<Plus1<N>>() - f(x - h7).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy8 = f(x + h8).Convert<Plus1<N>>() - f(x - h8).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy9 = f(x + h9).Convert<Plus1<N>>() - f(x - h9).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy10 = f(x + h10).Convert<Plus1<N>>() - f(x - h10).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy11 = f(x + h11).Convert<Plus1<N>>() - f(x - h11).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy12 = f(x + h12).Convert<Plus1<N>>() - f(x - h12).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy13 = f(x + h13).Convert<Plus1<N>>() - f(x - h13).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy14 = f(x + h14).Convert<Plus1<N>>() - f(x - h14).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy15 = f(x + h15).Convert<Plus1<N>>() - f(x - h15).Convert<Plus1<N>>();
            MultiPrecision<Plus1<N>> dy16 = f(x + h16).Convert<Plus1<N>>() - f(x - h16).Convert<Plus1<N>>();

            MultiPrecision<Plus1<N>> dy_s = 
                (74959204320L * dy1 - 30452176755L * dy2 + 14330436120L * dy3 - 6568116555L * dy4
                + 2765522760L * dy5 - 1037071035L * dy6 + 338635440L * dy7 - 94279185L * dy8
                + 21861840L * dy9 - 4099095L * dy10 + 596232L * dy11 - 63063L * dy12
                + 4312L * dy13 - 143L * dy14) / (80313433200L * h_ex);

            MultiPrecision<Plus1<N>> dy_t = 
                (135909226252800L * dy1 - 56628844272000L * dy2 + 27817677888000L * dy3 - 13561117970400L * dy4
                + 6199368215040L * dy5 - 2583070089600L * dy6 + 962634816000L * dy7 - 315864549000L * dy8
                + 89845916160L * dy9 - 21770356608L * dy10 + 4398051840L * dy11 - 719919200L * dy12
                + 91660800L * dy13 - 8511360L * dy14 + 512512L * dy15 - 15015L * dy16) / (144403552893600L * h_ex);

            MultiPrecision<Plus1<N>> error = MultiPrecision<Plus1<N>>.Abs(dy_s - dy_t);

            return (dy_t.Convert<N>(), error.Convert<N>());
        }
    }
}
