namespace MultiPrecision {
    internal static partial class UIntUtil {

        /// <summary>Operate uint32 array q += a / b, a = a % b</summary>
        public static void DivRem(UInt32[] arr_q, UInt32[] arr_a, UInt32[] arr_b) {
            uint digits_b = Digits(arr_b);

            if (digits_b == 0u) {
                throw new DivideByZeroException();
            }
            if (digits_b == 1u) {
                DivRem(arr_q, arr_a, arr_b[0]);
                return;
            }
            if (digits_b == 2u) {
                DivRem(arr_q, arr_a, Pack(arr_b[1], arr_b[0]));
                return;
            }

            uint digits_a = Digits(arr_a);
            if (digits_a >= digits_b) {
                DivRem(digits_b, arr_q, arr_a, arr_b[..(int)digits_b]);
            }
        }

        /// <summary>Operate uint32 array q += a / b, a = a % b</summary>
        private static void DivRem(uint digits_b, UInt32[] arr_q, UInt32[] arr_a, UInt32[] arr_b) {
            if (arr_a.Length < 1 || arr_b.Length < 1 || arr_a.Length < arr_b.Length) {
                throw new ArgumentException("invalid length", $"{nameof(arr_a)},{nameof(arr_b)}");
            }

            UInt64 numer, denom = RoundDenom(digits_b, arr_b);
            uint offset_r, offset_b = LeadingZeroCount(arr_b) + (uint)(arr_a.Length - arr_b.Length) * UInt32Bits;

            while (true) {
                (numer, offset_r) = TopUInt64(arr_a);

                if (offset_r >= offset_b) {
                    break;
                }

                UInt64 n = numer / denom;
                int sft = (int)offset_b - (int)offset_r - UInt32Bits;

                if (sft < 0) {
                    n >>= -sft;
                    sft = 0;
                }

#if DEBUG
                Debug<ArithmeticException>.Assert(n > 0uL);
#endif

                uint sft_block = (uint)sft / UInt32Bits;
                int sft_rem = sft % UInt32Bits;

                n <<= sft_rem;

                Add(sft_block, arr_q, n);
                Fms(sft_block, digits_b, arr_a, arr_b, n);
            }

            uint digits_a = Digits(arr_a);

            if (digits_a > digits_b || ((digits_a == digits_b) && GreaterThanOrEqual(digits_a, arr_a, arr_b))) {
                Add(arr_q, 1u);
                Sub(arr_a, arr_b);
            }
        }

        /// <summary>Operate uint32 array q += a / b, a = a % b</summary>
        public static void DivRem(UInt32[] arr_q, UInt32[] arr_a, UInt32 b) {
            if (arr_a.Length < 1) {
                throw new ArgumentException("invalid length", nameof(arr_a));
            }

            uint lzc_b = LeadingZeroCount(b);
            UInt64 numer, denom = b << (int)lzc_b;
            uint offset_r, offset_b = lzc_b + (uint)(arr_a.Length - 1) * UInt32Bits;

            while (true) {
                (numer, offset_r) = TopUInt64(arr_a);

                if (offset_r >= offset_b) {
                    break;
                }

                UInt64 n = numer / denom;
                int sft = (int)offset_b - (int)offset_r - UInt32Bits;

                if (sft < 0) {
                    n >>= -sft;
                    sft = 0;
                }

#if DEBUG
                Debug<ArithmeticException>.Assert(n > 0uL);
#endif

                uint sft_block = (uint)sft / UInt32Bits;
                int sft_rem = sft % UInt32Bits;

                n <<= sft_rem;

                (UInt64 n_hi, UInt64 n_lo) = Unpack(n);

                Add(sft_block, arr_q, n);
                Sub(sft_block, arr_a, b * n_lo);
                Sub(sft_block + 1, arr_a, b * n_hi);
            }

            if (arr_a[0] >= b) {
                Add(arr_q, 1u);
                Sub(arr_a, b);
            }
        }

        /// <summary>Operate uint32 array q += a / b, a = a % b</summary>
        public static void DivRem(UInt32[] arr_q, UInt32[] arr_a, UInt64 b) {
            (UInt32 b_hi, UInt32 b_lo) = Unpack(b);

            if (b_hi == 0u) {
                DivRem(arr_q, arr_a, b_lo);
                return;
            }

            if (arr_a.Length < 2) {
                throw new ArgumentException("invalid length", nameof(arr_a));
            }

            uint lzc_b = LeadingZeroCount(b);
            UInt64 b_sft = b << (int)lzc_b;

            UInt64 numer, denom = (b_sft >> UInt32Bits) + ((b_sft & UInt32.MaxValue) > 0u ? 1uL : 0uL);
            uint offset_r, offset_b = lzc_b + (uint)(arr_a.Length - 2) * UInt32Bits;

            while (true) {
                (numer, offset_r) = TopUInt64(arr_a);

                if (offset_r >= offset_b) {
                    break;
                }

                UInt64 n = numer / denom;
                int sft = (int)offset_b - (int)offset_r - UInt32Bits;

                if (sft < 0) {
                    n >>= -sft;
                    sft = 0;
                }

#if DEBUG
                Debug<ArithmeticException>.Assert(n > 0uL);
#endif

                uint sft_block = (uint)sft / UInt32Bits;
                int sft_rem = sft % UInt32Bits;

                n <<= sft_rem;

                (UInt64 n_hi, UInt64 n_lo) = Unpack(n);

                Add(sft_block, arr_q, n);
                Sub(sft_block, arr_a, b_lo * n_lo);
                Sub(sft_block + 1, arr_a, b_lo * n_hi);
                Sub(sft_block + 1, arr_a, b_hi * n_lo);
                Sub(sft_block + 2, arr_a, b_hi * n_hi);
            }

            if (Pack(arr_a[1], arr_a[0]) >= b) {
                Add(arr_q, 1u);
                Sub(arr_a, b);
            }
        }

        private static UInt64 RoundDenom(uint digits_b, UInt32[] arr_b) {
#if DEBUG
            if (digits_b != Digits(arr_b)) {
                throw new ArgumentException($"mismatch digits of {nameof(arr_b)}", nameof(digits_b));
            }
#endif

            if (digits_b == 0u) {
                return 0u;
            }

            UInt32 n0 = arr_b[digits_b - 1u];
            uint lzc = LeadingZeroCount(n0);

            if (digits_b == 1u) {
                n0 <<= (int)lzc;

                return n0;
            }
            else {
                UInt32 n1 = arr_b[digits_b - 2u];
                (UInt64 n_hi, UInt32 n_lo) = Unpack(Pack(n0, n1) << (int)lzc);

                if ((n_lo > 0u) || !IsZero(digits_b - 2u, arr_b)) {
                    return n_hi + 1uL;
                }

                return n_hi;
            }
        }
    }
}
