using System;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {

    public static partial class MultiPrecisionUtil {
        public static (MultiPrecision<N> value, MultiPrecision<N> error) RombergIntegrate<N>(Func<MultiPrecision<N>, MultiPrecision<N>> f, 
                                                                                             MultiPrecision<N> a, MultiPrecision<N> b, 
                                                                                             int min_iterations = 4, int max_iterations = 16, 
                                                                                             [AllowNull] MultiPrecision<N> epsilon = null)  where N : struct, IConstant {

            if (f is null) {
                throw new ArgumentNullException(nameof(f));
            }
            if (!(a <= b)) {
                throw new ArgumentException(
                    $"{nameof(a)} < {nameof(b)}", 
                    $"{nameof(a)},{nameof(b)}"
                );
            }
            if (min_iterations >= 0 && max_iterations >= 0 && min_iterations >= max_iterations) { 
                throw new ArgumentException(
                    $"{nameof(min_iterations)} < {nameof(max_iterations)}", 
                    $"{nameof(min_iterations)},{nameof(max_iterations)}"
                );
            }

            RichardsonExtrapolation<Plus1<N>> conv = new();
            MultiPrecision<N> h = b - a;
            MultiPrecision<N> s = 0, c = 0;

            void kahan_sum(MultiPrecision<N> v) {
                MultiPrecision<N> y = v - c;
                MultiPrecision<N> t = s + y;
                c = (t - s) - y;
                s = t;
            }

            kahan_sum(f(a));
            kahan_sum(f(b));

            s /= 2;
            c /= 2;

            while (true) {
                for (MultiPrecision<N> x = a + h / 2; x < b; x += h) {
                    kahan_sum(f(x));
                }

                h /= 2;

                conv.Append((s * h).Convert<Plus1<N>>());

                if (min_iterations >= 0 && conv.Iterations < min_iterations) {
                    continue;
                }

                if (max_iterations >= 0 && conv.Iterations >= max_iterations) {
                    break;
                }
                if (epsilon is not null && conv.Epsilon.Exponent < epsilon.Exponent) {
                    break;
                }
                if (!conv.Value.IsFinite || h.IsZero) {
                    break;
                }
                if (conv.Epsilon.IsFinite && conv.Value.Exponent - conv.Epsilon.Exponent >= MultiPrecision<N>.Bits) {
                    break;
                }
            }

            return (conv.Value.Convert<N>(), conv.Epsilon.Convert<N>());
        }
    }
}
