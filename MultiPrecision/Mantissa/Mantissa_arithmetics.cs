﻿using System;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public static Mantissa<N> operator +(Mantissa<N> a, Mantissa<N> b) {
            return Add(a, b);
        }

        public static Mantissa<N> operator -(Mantissa<N> a, Mantissa<N> b) {
            return Sub(a, b);
        }

        public static Mantissa<N> operator *(Mantissa<N> a, Mantissa<N> b) {
            return Mul(a, b);
        }

        public static Mantissa<N> operator /(Mantissa<N> a, Mantissa<N> b) {
            return Div(a, b).div;
        }

        public static Mantissa<N> operator %(Mantissa<N> a, Mantissa<N> b) {
            return Div(a, b).rem;
        }

        private void Add(Mantissa<N> v) {
            for (uint dig = 0; dig < Length; dig++) {
                CarryAdd(dig, v.arr[dig]);
            }
        }

        private void Sub(Mantissa<N> v) {
            for (uint dig = 0; dig < Length; dig++) {
                CarrySub(dig, v.arr[dig]);
            }
        }

        public static Mantissa<N> Add(Mantissa<N> v1, Mantissa<N> v2) {
            Mantissa<N> ret = v1.Copy();
            
            ret.Add(v2);

            return ret;
        }

        public static Mantissa<N> Sub(Mantissa<N> v1, Mantissa<N> v2) {
            Mantissa<N> ret = v1.Copy();

            ret.Sub(v2);

            return ret;
        }

        public static Mantissa<N> Mul(Mantissa<N> v1, Mantissa<N> v2) {
            Mantissa<N> ret = Zero;

            uint v1_digits = v1.Digits, v2_digits = v2.Digits;

            for (uint dig1 = 0; dig1 < v1_digits; dig1++) {
                if (v1.arr[dig1] == 0) {
                    continue;
                }

                for (uint dig2 = 0; dig2 < v2_digits; dig2++) {
                    if (v2.arr[dig2] == 0) {
                        continue;
                    }

                    (UInt32 h, UInt32 l) = UIntUtil.Unpack((UInt64)v1.arr[dig1] * (UInt64)v2.arr[dig2]);

                    ret.CarryAdd(dig1 + dig2, l);
                    ret.CarryAdd(dig1 + dig2 + 1, h);
                }
            }

            return ret;
        }

        public static (Mantissa<N> div, Mantissa<N> rem) Div(Mantissa<N> v1, Mantissa<N> v2) {
            if (v2.IsZero) {
                throw new DivideByZeroException();
            }

            uint lzc_v1 = v1.LeadingZeroCount, lzc_v2 = v2.LeadingZeroCount;
            uint sft = Math.Min(lzc_v1, lzc_v2 - lzc_v2 / UIntUtil.UInt32Bits * UIntUtil.UInt32Bits);

            v1 = LeftShift(v1, sft);
            v2 = LeftShift(v2, sft);

            Mantissa<N> div = Zero, rem = v1;

            UInt64 denom = 0;
            uint denom_digits = v2.Digits;

            for (int i = Length - 1; i >= 0; i--) {
                if (denom == 0 && v2.arr[i] != 0) {
                    denom = v2.arr[i];
                    continue;
                }
                if (denom > 0 && v2.arr[i] != 0) {
                    denom += 1;
                    break;
                }
            }

            for (uint i = (uint)Length - 1; i >= denom_digits;) {
                UInt64 numer = UIntUtil.Pack(rem.arr[i], rem.arr[i - 1]);

                UInt64 n = numer / denom;
                (UInt32 nh, UInt32 nl) = UIntUtil.Unpack(n);
                div.CarryAdd(i - denom_digits + 1, nh);
                div.CarryAdd(i - denom_digits, nl);

                Mantissa<N> sub = Mul(new Mantissa<N>(n), v2);
                sub.LeftShiftArray(i - denom_digits);

                rem.Sub(sub);

                if (rem.arr[i] == 0) {
                    i--;
                }
            }

            while(true) {
                UInt64 numer = rem.arr[denom_digits - 1];

                UInt64 n = numer / denom;
                (UInt32 nh, UInt32 nl) = UIntUtil.Unpack(n);
                div.CarryAdd(1, nh);
                div.CarryAdd(0, nl);

                Mantissa<N> sub = Mul(new Mantissa<N>(n), v2);
                rem.Sub(sub);

                if (n == 0) {
                    break;
                }
            }

            if (v2 <= rem) {
                div.CarryAdd(0, 1);
                rem.Sub(v2);
            }

            rem.RightShift(sft);

            return (div, rem);
        }
    }
}