using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {

    public static partial class MultiPrecisionUtil {

        public static MultiPrecision<N> NewtonRaphsonRootFinding<N>(MultiPrecision<N> x0,
                                                                    Func<MultiPrecision<N>, MultiPrecision<N>> f,
                                                                    Func<MultiPrecision<N>, MultiPrecision<N>> df,
                                                                    [AllowNull] MultiPrecision<N> x_min = null,
                                                                    [AllowNull] MultiPrecision<N> x_max = null,
                                                                    int max_iterations = -1, bool break_overshoot = false) where N : struct, IConstant {

            if ((x_min != null && x0 < x_min) || (x_max != null && x0 > x_max)) {
                throw new ArgumentOutOfRangeException(nameof(x0));
            }
            if (!x0.IsFinite) {
                return x0;
            }

            MultiPrecision<N> dx = f(x0) / df(x0);
            MultiPrecision<N> x = x0 - dx;

            Sign prev_sign = dx.Sign;
            int overshoots = 0;

            int iter = 0;
            while ((max_iterations < 0 || iter < max_iterations) && !dx.IsZero && dx.IsFinite && x.Exponent - dx.Exponent <= MultiPrecision<N>.Bits) {
                dx = f(x) / df(x);

                x -= dx;

                if ((x_min != null && x < x_min) || (x_max != null && x > x_max)) {
                    return MultiPrecision<N>.NaN;
                }

                iter++;

                if (break_overshoot) {
                    if (prev_sign != dx.Sign) {
                        overshoots++;
                        if (overshoots >= 2) {
                            break;
                        }
                    }
                    else {
                        overshoots = 0;
                    }

                    prev_sign = dx.Sign;
                }
            }

            return x;
        }

        public static MultiPrecision<N> HalleyRootFinding<N>(MultiPrecision<N> x0,
                                                             Func<MultiPrecision<N>, MultiPrecision<N>> f,
                                                             Func<MultiPrecision<N>, (MultiPrecision<N> d1, MultiPrecision<N> d2)> df,
                                                             [AllowNull] MultiPrecision<N> x_min = null,
                                                             [AllowNull] MultiPrecision<N> x_max = null,
                                                             int max_iterations = -1, bool break_overshoot = false) where N : struct, IConstant {

            if ((x_min != null && x0 < x_min) || (x_max != null && x0 > x_max)) {
                throw new ArgumentOutOfRangeException(nameof(x0));
            }
            if (!x0.IsFinite) {
                return x0;
            }

            MultiPrecision<N> y = f(x0);
            (MultiPrecision<N> df1, MultiPrecision<N> df2) = df(x0);
            MultiPrecision<N> dx = (2 * y * df1) / (2 * df1 * df1 - y * df2);
            MultiPrecision<N> x = x0 - dx;

            Sign prev_sign = dx.Sign;
            int overshoots = 0;

            int iter = 0;
            while ((max_iterations < 0 || iter < max_iterations) && !dx.IsZero && dx.IsFinite && x.Exponent - dx.Exponent <= MultiPrecision<N>.Bits) {
                y = f(x);
                (df1, df2) = df(x);
                dx = (2 * y * df1) / (2 * df1 * df1 - y * df2);

                x -= dx;

                if ((x_min != null && x < x_min) || (x_max != null && x > x_max)) {
                    return MultiPrecision<N>.NaN;
                }

                iter++;

                if (break_overshoot) {
                    if (prev_sign != dx.Sign) {
                        overshoots++;
                        if (overshoots >= 2) {
                            break;
                        }
                    }
                    else {
                        overshoots = 0;
                    }

                    prev_sign = dx.Sign;
                }
            }

            return x;
        }
    }
}
