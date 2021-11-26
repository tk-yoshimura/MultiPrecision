using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision.ParameterSearchUtil {
    public class MaxSearch<N> : ParameterSearch<N> where N : struct, IConstant {

        public MaxSearch((long min, long max) likely_range, (long min, long max) search_range) :
            base(likely_range, search_range) { }

        public long MaxPoint => MaxLikelihoodPoint;

        protected override long MaxLikelihoodPoint {
            get {
                if (Samples.Count <= 0) {
                    throw new InvalidOperationException();
                }

                IReadOnlyList<(long param, MultiPrecision<N> value)> samples = Samples;

                return Samples[Samples.Select((sample) => sample.value).ToList().MaxIndex()].param;
            }
        }
    }

    public class MinSearch<N> : ParameterSearch<N> where N : struct, IConstant {

        public MinSearch((long min, long max) likely_range, (long min, long max) search_range) :
            base(likely_range, search_range) { }

        public long MinPoint => MaxLikelihoodPoint;

        protected override long MaxLikelihoodPoint {
            get {
                if (Samples.Count <= 0) {
                    throw new InvalidOperationException();
                }

                IReadOnlyList<(long param, MultiPrecision<N> value)> samples = Samples;

                return Samples[Samples.Select((sample) => sample.value).ToList().MinIndex()].param;
            }
        }
    }
}
