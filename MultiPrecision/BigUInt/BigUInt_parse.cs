using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {

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

            UInt32[] dec = new UInt32[(s.Length + decimals - 1) / decimals];
            for (int i = 0, idx = s.Length - decimals; i < dec.Length; i++, idx -= decimals) {
                if (idx >= 0) {
                    dec[i] = UInt32.Parse(s.Substring(idx, decimals), NumberStyles.Integer, CultureInfo.InvariantCulture);
                }
                else {
                    dec[i] = UInt32.Parse(s.Substring(0, decimals + idx), NumberStyles.Integer, CultureInfo.InvariantCulture);
                }
            }

            int bin_digits = 0;

            for (int j = dec.Length - 1; j >= 0; j--) {
                if (bin_digits > Length) {
                    throw new OverflowException();
                }

                UInt32 carry = dec[j];
                for (int i = 0; i < bin_digits; i++) {
                    UInt64 res = (UInt64)value[i] * UIntUtil.UInt32MaxDecimal + (UInt64)carry;
                    (carry, value[i]) = UIntUtil.Unpack(res);
                }
                if (carry > 0) {
                    if (bin_digits >= Length) {
                        throw new OverflowException();
                    }

                    (carry, value[bin_digits]) = UIntUtil.Unpack(carry);
                    bin_digits++;

                    if (carry > 0) {
                        if (bin_digits >= Length) {
                            throw new OverflowException();
                        }

                        value[bin_digits] = carry;
                        bin_digits++;
                    }
                }
            }
        }

        public static BigUInt<N> Parse(string s) {
            return new BigUInt<N>(s);
        }

        public static bool TryParse(string s, out BigUInt<N> result) {
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
