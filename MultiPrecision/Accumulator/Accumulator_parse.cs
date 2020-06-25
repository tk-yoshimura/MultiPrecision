using System;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public Accumulator(string s) : this(new BigUInt<Double<N>>(s)) { }

        public static Accumulator<N> Parse(string s) {
            return new Accumulator<N>(s);
        }

        public static bool TryParse(string s, out Accumulator<N> result) {
            try {
                result = Parse(s);
                return true;
            }
            catch (Exception e) when (e is FormatException || e is OverflowException) {
                result = null;
                return false;
            }
        }
    }
}
