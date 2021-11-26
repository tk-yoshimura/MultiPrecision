using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiPrecisionSpline {
    public abstract class Spline<N> where N : struct, IConstant {

        public ReadOnlyCollection<MultiPrecision<N>> Xs { private set; get; }

        public ReadOnlyCollection<MultiPrecision<N>> Ys { private set; get; }

        public int Length => Xs.Count;

        public Spline(MultiPrecision<N>[] xs, MultiPrecision<N>[] ys) {
            if (xs.Length != ys.Length) {
                throw new ArgumentException("Array lengths don't match.", $"{xs}, {ys}");
            }
            if (xs.Length <= 0) {
                throw new ArgumentException("Array contains no elements.", $"{xs}, {ys}");
            }
            if (xs.Any((x) => x is null || !x.IsFinite)) {
                throw new ArgumentException("Array contains invalid element.", $"{xs}");
            }
            if (ys.Any((y) => y is null || !y.IsFinite)) {
                throw new ArgumentException("Array contains invalid element.", $"{ys}");
            }

            for (int i = 1; i < xs.Length; i++) {
                if (!(xs[i - 1] < xs[i])) {
                    throw new ArgumentException("X-coordinate order is invalid", $"{xs}");
                }
            }

            this.Xs = Array.AsReadOnly(xs);
            this.Ys = Array.AsReadOnly(ys);
        }

        public abstract MultiPrecision<N> Value(MultiPrecision<N> x);

        protected int SegmentIndex(MultiPrecision<N> x) {
            if (Xs[0] >= x) {
                return -1;
            }
            if (Xs[^1] <= x) {
                return Length - 1;
            }

            int index = 0;

            for (int h = Math.Max(1, Length / 2); h >= 1; h /= 2) {
                for (int i = index; i < Length - h; i += h) {
                    if (Xs[i + h] > x) {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }

        protected MultiPrecision<N>[] CheckArray(MultiPrecision<N>[] arr) {
            if (arr.Length != Length) {
                throw new ArgumentException("Array length don't match.");
            }
            if (arr.Any((a) => a is null)) {
                throw new ArgumentException("Array contains null element.");
            }

            return arr;
        }
    }
}
