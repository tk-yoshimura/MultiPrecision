using System.Diagnostics;

namespace MultiPrecision {

    [DebuggerDisplay("{ToHexcode(),nq}")]
    internal sealed partial class BigUInt<N> {

        public override string ToString() {
            int bin_digits = (int)Digits, dec_digits = 0;

            // dec_digits <= bin_digits * digits(2^32 - 1) / digits(10^9 - 1) + 2
            UInt32[] dec = new UInt32[checked(bin_digits * 10 / 9 + 2)];

            for (int j = bin_digits - 1; j >= 0; j--) {

                UInt32 carry = value[j];

                for (int i = 0; i < dec_digits; i++) {
                    UInt64 res = UIntUtil.Pack(dec[i], carry);

                    (carry, dec[i]) = UIntUtil.DecimalUnpack(res);
                }

                if (carry > 0) {
                    (carry, dec[dec_digits]) = UIntUtil.DecimalUnpack(carry);
                    dec_digits++;

                    if (carry > 0) {
                        dec[dec_digits] = carry;
                        dec_digits++;
                    }

#if DEBUG
                    Debug<ArithmeticException>.Assert(carry / UIntUtil.UInt32MaxDecimal == 0);
#endif
                }
            }

            string str = string.Join(string.Empty, dec.Select((d) => $"{d:D9}").Reverse()).TrimStart('0');

            return (str != string.Empty) ? str : "0";
        }

        public string ToHexcode() => string.Join(' ', value.Reverse().Select((u) => $"{u:X8}"));
    }
}
