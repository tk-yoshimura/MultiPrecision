using System;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        public static Accumulator<N> operator +(Accumulator<N> a, Accumulator<N> b) {
            return Add(a, b);
        }

        public static Accumulator<N> operator -(Accumulator<N> a, Accumulator<N> b) {
            return Sub(a, b);
        }

        public static Accumulator<N> operator *(Accumulator<N> a, Accumulator<N> b) {
            return Mul(a, b);
        }

        public static Accumulator<N> operator /(Accumulator<N> a, Accumulator<N> b) {
            return Div(a, b).div;
        }

        public static Accumulator<N> operator %(Accumulator<N> a, Accumulator<N> b) {
            return Div(a, b).rem;
        }

        private void Add(Accumulator<N> v) {
            for (int dig = 0; dig < Length; dig++) {
                CarryAdd(dig, v.arr[dig]);
            }
        }

        private void Sub(Accumulator<N> v) {
            for (int dig = 0; dig < Length; dig++) {
                CarrySub(dig, v.arr[dig]);
            }
        }

        public static Accumulator<N> Add(Accumulator<N> v1, Accumulator<N> v2) {
            Accumulator<N> ret = v1.Copy();
            
            ret.Add(v2);

            return ret;
        }

        public static Accumulator<N> Sub(Accumulator<N> v1, Accumulator<N> v2) {
            Accumulator<N> ret = v1.Copy();

            ret.Sub(v2);

            return ret;
        }

        public static Accumulator<N> Mul(Accumulator<N> v1, Accumulator<N> v2) {
            Accumulator<N> ret = Zero.Copy();

            int v1_digits = v1.Digits, v2_digits = v2.Digits;

            for (int dig1 = 0; dig1 < v1_digits; dig1++) {
                if (v1.arr[dig1] == 0) {
                    continue;
                }

                for (int dig2 = 0; dig2 < v2_digits; dig2++) {
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

        public static (Accumulator<N> div, Accumulator<N> rem) Div(Accumulator<N> v1, Accumulator<N> v2) {
            if (v2.IsZero) {
                throw new DivideByZeroException();
            }

            int lzc_v1 = v1.LeadingZeroCount, lzc_v2 = v2.LeadingZeroCount;
            int sft = Math.Min(lzc_v1, lzc_v2 - lzc_v2 / UIntUtil.UInt32Bits * UIntUtil.UInt32Bits);

            v1 = LeftShift(v1, sft);
            v2 = LeftShift(v2, sft);

            Accumulator<N> div = Zero.Copy(), rem = v1;

            UInt64 denom = 0;
            int denom_digits = v2.Digits;

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

            for (int i = Length - 1; i >= denom_digits;) {
                UInt64 numer = UIntUtil.Pack(rem.arr[i], rem.arr[i - 1]);

                UInt64 n = numer / denom;
                (UInt32 nh, UInt32 nl) = UIntUtil.Unpack(n);
                div.CarryAdd(i - denom_digits + 1, nh);
                div.CarryAdd(i - denom_digits, nl);

                Accumulator<N> sub = Mul(new Accumulator<N>(n), v2);
                sub.LeftBlockShift(i - denom_digits);

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

                Accumulator<N> sub = Mul(new Accumulator<N>(n), v2);
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
