﻿using System;
using System.Diagnostics;
using System.Linq;

namespace MultiPrecision {

    [DebuggerDisplay("{ToHexcode()}")]
    internal sealed partial class BigUInt<N> {

        public override string ToString() {
            int bin_digits = (int)Digits, dec_digits = 0;

            // dec_digits <= bin_digits * digits(2^32 - 1) / digits(10^9 - 1) + 2
            UInt32[] dec = new UInt32[checked(bin_digits * 10 / 9 + 2)];

            for (int j = bin_digits - 1; j >= 0; j--) {
                UInt32 carry = value[j];
                for (int i = 0; i < dec_digits; i++) {
                    UInt64 res = UIntUtil.Pack(dec[i], carry);

                    dec[i] = (UInt32)(res % UIntUtil.UInt32MaxDecimal);
                    carry = (UInt32)(res / UIntUtil.UInt32MaxDecimal);
                }
                if (carry > 0) {
                    dec[dec_digits] = (UInt32)(carry % UIntUtil.UInt32MaxDecimal);
                    dec_digits++;

                    carry = (UInt32)(carry / UIntUtil.UInt32MaxDecimal);

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

        public string ToHexcode() => UIntUtil.ToHexcode(value);
    }
}
