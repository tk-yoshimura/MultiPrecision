using System;
using System.Linq;

namespace MultiPrecision.ParameterSearchUtil {
    public class ConvergenceSearch<N> : ParameterSearch<N> where N : struct, IConstant {

        private readonly bool larger_convergence;

        public ConvergenceSearch((long min, long max) likely_range, (long min, long max) search_range, bool larger_convergence = true)
            : base(likely_range, search_range) {

            this.larger_convergence = larger_convergence;
        }

        public long ConvergencePoint => MaxLikelihoodPoint;

        public override long MaxLikelihoodPoint {
            get {
                MultiPrecision<N>[] samples = Samples.Values.ToArray();

                if (larger_convergence) {
                    for (int i = Samples.Count - 1; i >= 1; i--) {
                        if (samples[i] != samples[i - 1]) {
                            return Samples.Keys.ToArray()[i];
                        }
                    }

                    return Samples.Keys.ToArray().First();
                }
                else {
                    for (int i = 0; i < Samples.Count - 1; i++) {
                        if (samples[i] != samples[i + 1]) {
                            return Samples.Keys.ToArray()[i];
                        }
                    }

                    return Samples.Keys.ToArray().Last();
                }
            }
        }
    }
}
