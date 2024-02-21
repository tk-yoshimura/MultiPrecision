using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Regex parse_regex = ParserRegex();

        public BigUInt(string s)
            : this(ParseCore(s), enable_clone: false) { }

        public static implicit operator BigUInt<N>(string s) {
            return new BigUInt<N>(s);
        }

        public static BigUInt<N> Parse(string s) {
            return new BigUInt<N>(ParseCore(s), enable_clone: false);
        }

        private static UInt32[] ParseCore(string s) {
            if (!parse_regex.IsMatch(s)) {
                throw new FormatException();
            }

            s = s.TrimStart('0');

            if (s == string.Empty) {
                return new UInt32[Length];
            }

            const int decimals = 9;

            UInt32[] dec = new UInt32[(s.Length + decimals - 1) / decimals];
            for (int i = 0, idx = s.Length - decimals; i < dec.Length; i++, idx -= decimals) {
                if (idx >= 0) {
                    dec[i] = UInt32.Parse(s[idx..(decimals + idx)], NumberStyles.Integer, CultureInfo.InvariantCulture);
                }
                else {
                    dec[i] = UInt32.Parse(s[..(decimals + idx)], NumberStyles.Integer, CultureInfo.InvariantCulture);
                }
            }

            int bin_digits = 0;
            UInt32[] bin = new UInt32[Length];

            for (int j = dec.Length - 1; j >= 0; j--) {
                if (bin_digits > Length) {
                    throw new OverflowException();
                }

                UInt32 carry = dec[j];

                for (int i = 0; i < bin_digits; i++) {
                    UInt64 res = UIntUtil.DecimalPack(bin[i], carry);

                    (carry, bin[i]) = UIntUtil.Unpack(res);
                }

                if (carry > 0) {
                    if (bin_digits >= Length) {
                        throw new OverflowException();
                    }

                    (carry, bin[bin_digits]) = UIntUtil.Unpack(carry);
                    bin_digits++;

                    if (carry > 0) {
                        if (bin_digits >= Length) {
                            throw new OverflowException();
                        }

                        bin[bin_digits] = carry;
                        bin_digits++;
                    }
                }
            }

            return bin;
        }

        public static bool TryParse(string s, out BigUInt<N> result) {
            try {
                result = Parse(s);
                return true;
            }
            catch (Exception e) when (e is FormatException || e is OverflowException) {
                result = Zero;
                return false;
            }
        }

        [GeneratedRegex(@"^\d+$")]
        private static partial Regex ParserRegex();
    }
}
