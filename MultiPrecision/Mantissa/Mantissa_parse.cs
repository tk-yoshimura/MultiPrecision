using System;
using System.Text.RegularExpressions;

namespace MultiPrecision {
    public sealed partial class Mantissa<N> where N : struct, IConstant {

        public Mantissa(string s) : this() {
            if (!(new Regex("[0-9]+").IsMatch(s))) {
                throw new FormatException();
            }

            s = s.TrimStart('0');

            if (s == string.Empty) {
                return;
            }

            const int decimals = 9;
            Mantissa<N> decbase = new Mantissa<N>(1000000000ul), decpow = new Mantissa<N>(1);
                        
            for (int i = 0; ; i += decimals) {
                if (i + decimals < s.Length) {
                    UInt32 dec = UInt32.Parse(s.Substring(s.Length - decimals - i, decimals));
                    Add(Mul(new Mantissa<N>(dec), decpow));
                    decpow *= decbase;
                }
                else {
                    UInt32 dec = UInt32.Parse(s.Substring(0, s.Length - i));
                    Add(Mul(new Mantissa<N>(dec), decpow));
                    break;
                }
            }
        }

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
