using System;
using System.Linq;

namespace MultiPrecision {
    public sealed partial class Mantissa<N> where N : struct, IConstant {

        public override string ToString() {
            const UInt64 dec_base = 1000000000u;

            int bin_digits = (int)Digits, dec_digits = 0;

            // dec_digits <= bin_digits * digits(2^32 - 1) / digits(10^9 - 1) + 2
            UInt32[] dec = new UInt32[checked(bin_digits * 10 / 9 + 2)]; 

            for (int j = bin_digits - 1; j >= 0; j--) {
                UInt32 carry = arr[j];
                for (int i = 0; i < dec_digits; i++) {
                    UInt64 res = UIntUtil.Pack(dec[i], carry);

                    dec[i] = (UInt32)(res % dec_base);
                    carry = (UInt32)(res / dec_base);
                }
                if (carry > 0) {
                    dec[dec_digits] = (UInt32)(carry % dec_base);
                    dec_digits++;

                    carry = (UInt32)(carry / dec_base);

                    if (carry > 0) {
                        dec[dec_digits] = carry;
                        dec_digits++;
                    }

#if DEBUG
                    if (carry / dec_base > 0) {
                        throw new OverflowException();
                    }
#endif
                }
            }

            string str = string.Join(string.Empty, dec.Select((d) => $"{d:D9}").Reverse()).TrimStart('0');

            return (str != string.Empty) ? str : "0";
        }
    }
}
