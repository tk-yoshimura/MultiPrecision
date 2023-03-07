namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public Mantissa(string s) : this(new BigUInt<N>(s)) { }

        public static Mantissa<N> Parse(string s) {
            return new Mantissa<N>(s);
        }

        public static bool TryParse(string s, out Mantissa<N> result) {
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
