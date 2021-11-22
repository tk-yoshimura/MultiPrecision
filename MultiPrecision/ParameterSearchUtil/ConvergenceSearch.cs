using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision.ParameterSearchUtil {
    public class ConvergenceSearch<N> : ParameterSearch<N> where N : struct, IConstant {

        private readonly bool larger_convergence;

        public ConvergenceSearch((long min, long max) likely_range, (long min, long max) search_range, bool larger_convergence = true)
            : base(likely_range, search_range) {

            this.larger_convergence = larger_convergence;
        }

        public long ConvergencePoint => MaxLikelihoodPoint;

        protected override long MaxLikelihoodPoint {
            get {
                IReadOnlyList<(long param, MultiPrecision<N> value)> samples = Samples;

                if (larger_convergence) {
                    for (int i = samples.Count - 1; i >= 1; i--) {
                        if (samples[i].value != samples[i - 1].value) {
                            return samples[i].param;
                        }
                    }

                    return samples.First().param;
                }
                else {
                    for (int i = 0; i < samples.Count - 1; i++) {
                        if (samples[i].value != samples[i + 1].value) {
                            return samples[i].param;
                        }
                    }

                    return samples.Last().param;
                }
            }
        }
    }
}
