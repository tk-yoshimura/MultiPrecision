using System;
using System.Linq;

namespace MultiPrecision.ParameterSearchUtil {
    public class MaxSearch<N> : ParameterSearch<N> where N : struct, IConstant {

        public MaxSearch((long min, long max) likely_range, (long min, long max) search_range) :
            base(likely_range, search_range) { }

        public long MaxPoint => MaxLikelihoodPoint;

        public override long MaxLikelihoodPoint {
            get {
                if (Samples.Count <= 0) {
                    throw new InvalidOperationException();
                }

                return Samples.Keys.ToArray()[Samples.Values.MaxIndex()];
            }
        }
    }

    public class MinSearch<N> : ParameterSearch<N> where N : struct, IConstant {

        public MinSearch((long min, long max) likely_range, (long min, long max) search_range) :
            base(likely_range, search_range) { }

        public long MinPoint => MaxLikelihoodPoint;

        public override long MaxLikelihoodPoint {
            get {
                if (Samples.Count <= 0) {
                    throw new InvalidOperationException();
                }

                return Samples.Keys.ToArray()[Samples.Values.MinIndex()];
            }
        }
    }
}
