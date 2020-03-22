using System;
using System.Text.RegularExpressions;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        private static readonly Regex parse_regex = new Regex(@"^\d+$");

        public BigUInt(string s) : this() {
            if (!parse_regex.IsMatch(s)) {
                throw new FormatException();
            }

            s = s.TrimStart('0');

            if (s == string.Empty) {
                return;
            }

            const int decimals = 9;
            BigUInt<N, K> decbase = Decimal(decimals), decpow = 1;

            for (int i = 0; ; i += decimals) {
                if (i + decimals < s.Length) {
                    UInt32 dec = UInt32.Parse(s.Substring(s.Length - decimals - i, decimals));
                    Add(Mul(new BigUInt<N, K>(dec), decpow));
                    decpow *= decbase;
                }
                else {
                    UInt32 dec = UInt32.Parse(s.Substring(0, s.Length - i));
                    Add(Mul(new BigUInt<N, K>(dec), decpow));
                    break;
                }
            }
        }

        public static BigUInt<N, K> Parse(string s) {
            return new BigUInt<N, K>(s);
        }

        public static bool TryParse(string s, out BigUInt<N, K> result) {
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
