using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision.ParameterSearchUtil {
    public abstract class ParameterSearch<N> where N : struct, IConstant {
        private readonly SortedList<long, MultiPrecision<N>> samples = new();
        private readonly List<long> samples_standby = new();

        public IReadOnlyDictionary<long, MultiPrecision<N>> Samples => samples;

        public bool IsSearched => Step <= 1 && samples_standby.Count <= 0;

        public virtual long MaxLikelihoodPoint { get; }

        public (long min, long max) SearchRange { private set; get; } 

        public long Step { private set; get; }

        public IEnumerable<long> SampleRequests => samples_standby;

        public bool IsConvergenced {
            get {
                if (!IsSearched) {
                    return false;
                }

                long max_likelihood = MaxLikelihoodPoint;

                return max_likelihood > SearchRange.min && max_likelihood < SearchRange.max;
            }
        }

        public ParameterSearch((long min, long max) likely_range, (long min, long max) search_range) {
            if (likely_range.min >= likely_range.max) {
                throw new ArgumentException(nameof(likely_range));
            }
            if (search_range.min >= search_range.max) {
                throw new ArgumentException(nameof(search_range));
            }
            if (likely_range.min < search_range.min || likely_range.max > search_range.max) { 
                throw new ArgumentException($"{nameof(likely_range)},{nameof(search_range)}");
            }

            this.SearchRange = search_range;

            checked {
                long range = likely_range.max - likely_range.min;
                long range_pow2 = 1;
                while (range_pow2 < range) {
                    range_pow2 *= 2;
                }

                likely_range.min -= (range_pow2 - range) / 2;
                likely_range.max = likely_range.min + range_pow2 + 1;

                Step = range_pow2 / 2;
            }

            PushSampleRequest(likely_range.min);
            PushSampleRequest(likely_range.min + Step);
            PushSampleRequest(likely_range.max);
        }

        protected void PushSampleRequest(long sample_point) {
            if (sample_point >= SearchRange.min && sample_point <= SearchRange.max
                && !samples.Keys.Contains(sample_point)) {

                samples_standby.Add(sample_point);
            }
        }

        public void PushSampleResult(long sample_point, MultiPrecision<N> sample) {
            if (samples.ContainsKey(sample_point)) {
                samples[sample_point] = sample;
            }
            else {
                samples.Add(sample_point, sample);
            }
        }

        public void ReflashSampleRequests() {
            if (samples.Count < 2) {
                return;
            }

            samples_standby.Clear();

            long max_likelihood = MaxLikelihoodPoint;

            if (max_likelihood <= samples.Keys.Min() && max_likelihood > SearchRange.min) {
                while (samples_standby.Count <= 0 && Step > 1) {
                    PushSampleRequest(checked(max_likelihood - Step));
                    PushSampleRequest(checked(max_likelihood - Step / 2));
                    PushSampleRequest(checked(max_likelihood + Step / 2));

                    if (samples_standby.Count <= 0) {
                        Step /= 2;
                    }
                }
                return;
            }

            if (max_likelihood >= samples.Keys.Max() && max_likelihood < SearchRange.max) { 
                while (samples_standby.Count <= 0 && Step > 1) {
                    PushSampleRequest(checked(max_likelihood - Step / 2));
                    PushSampleRequest(checked(max_likelihood + Step / 2));
                    PushSampleRequest(checked(max_likelihood + Step));
                    
                    if (samples_standby.Count <= 0) {
                        Step /= 2;
                    }
                }
                return;
            }

            if (Step == 1) {
                return;
            }

            Step /= 2;

            PushSampleRequest(checked(max_likelihood - Step * 2));
            PushSampleRequest(checked(max_likelihood - Step));
            PushSampleRequest(checked(max_likelihood + Step));
            PushSampleRequest(checked(max_likelihood + Step * 2));
        }
    }
}
