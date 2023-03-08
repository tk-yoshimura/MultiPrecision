namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {

        public static BigUInt<N> operator /(BigUInt<N> a, BigUInt<N> b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt32 b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator /(UInt32 a, BigUInt<N> b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator /(BigUInt<N> a, UInt64 b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator /(UInt64 a, BigUInt<N> b) {
            return Div(a, b);
        }

        public static BigUInt<N> operator %(BigUInt<N> a, BigUInt<N> b) {
            return Rem(a, b);
        }

        public static UInt32 operator %(BigUInt<N> a, UInt32 b) {
            return Rem(a, b);
        }

        public static BigUInt<N> operator %(UInt32 a, BigUInt<N> b) {
            return Rem(a, b);
        }

        public static UInt64 operator %(BigUInt<N> a, UInt64 b) {
            return Rem(a, b);
        }

        public static BigUInt<N> operator %(UInt64 a, BigUInt<N> b) {
            return Rem(a, b);
        }

        public static (BigUInt<N> q, BigUInt<N> r) DivRem(BigUInt<N> a, BigUInt<N> b) {
            BigUInt<N> q = Zero, r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b.value);

            return (q, r);
        }

        public static BigUInt<N> Div(BigUInt<N> a, BigUInt<N> b) {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> Rem(BigUInt<N> a, BigUInt<N> b) {
            return DivRem(a, b).r;
        }

        public static (BigUInt<N> q, UInt32 r) DivRem(BigUInt<N> a, UInt32 b) {
            if (UIntUtil.IsPower2(b)) {
                return (a >> UIntUtil.Power2(b), a.value[0] & (b - 1u));
            }

            BigUInt<N> q = Zero, r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b);

            return (q, r.value[0]);
        }

        public static BigUInt<N> Div(BigUInt<N> a, UInt32 b) {
            if (UIntUtil.IsPower2(b)) {
                return a >> UIntUtil.Power2(b);
            }

            return DivRem(a, b).q;
        }

        public static UInt32 Rem(BigUInt<N> a, UInt32 b) {
            if (UIntUtil.IsPower2(b)) {
                return a.value[0] & (b - 1u);
            }

            return DivRem(a, b).r;
        }

        public static (BigUInt<N> q, UInt64 r) DivRem(BigUInt<N> a, UInt64 b) {
            if (UIntUtil.IsPower2(b)) {
                return (a >> UIntUtil.Power2(b), UIntUtil.Pack(a.value[1], a.value[0]) & (b - 1uL));
            }

            BigUInt<N> q = Zero, r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b);

            return (q, UIntUtil.Pack(r.value[1], r.value[0]));
        }

        public static BigUInt<N> Div(BigUInt<N> a, UInt64 b) {
            if (UIntUtil.IsPower2(b)) {
                return a >> UIntUtil.Power2(b);
            }

            return DivRem(a, b).q;
        }

        public static UInt64 Rem(BigUInt<N> a, UInt64 b) {
            if (UIntUtil.IsPower2(b)) {
                return UIntUtil.Pack(a.value[1], a.value[0]) & (b - 1uL);
            }

            return DivRem(a, b).r;
        }

        public static (UInt32 q, UInt32 r) DivRem(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1u) {
                return (0u, a);
            }

            UInt32 denom = b.value[0];

            return (a / denom, a % denom);
        }

        public static UInt32 Div(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1u) {
                return 0u;
            }

            UInt32 denom = b.value[0];

            return a / denom;
        }

        public static UInt32 Rem(UInt32 a, BigUInt<N> b) {
            if (b.Digits > 1u) {
                return a;
            }

            UInt32 denom = b.value[0];

            return a % denom;
        }

        public static (UInt64 q, UInt64 r) DivRem(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2u) {
                return (0uL, a);
            }

            UInt64 denom = UIntUtil.Pack(b.value[1], b.value[0]);

            return (a / denom, a % denom);
        }

        public static UInt64 Div(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2u) {
                return 0uL;
            }

            UInt64 denom = UIntUtil.Pack(b.value[1], b.value[0]);

            return a / denom;
        }

        public static UInt64 Rem(UInt64 a, BigUInt<N> b) {
            if (b.Digits > 2u) {
                return a;
            }

            UInt64 denom = UIntUtil.Pack(b.value[1], b.value[0]);

            return a % denom;
        }

        public static (BigUInt<M> q, BigUInt<N> r) DivRem<M>(BigUInt<M> a, BigUInt<N> b) where M : struct, IConstant {
            BigUInt<M> q = BigUInt<M>.Zero.Copy();
            BigUInt<M> r = a.Copy();

            UIntUtil.DivRem(q.value, r.value, b.value);

            return (q, new BigUInt<N>(r.value[..Length], enable_clone: false));
        }

        public static BigUInt<M> Div<M>(BigUInt<M> a, BigUInt<N> b) where M : struct, IConstant {
            return DivRem(a, b).q;
        }

        public static BigUInt<N> Rem<M>(BigUInt<M> a, BigUInt<N> b) where M : struct, IConstant {
            return DivRem(a, b).r;
        }

        public static BigUInt<N> RoundDiv(BigUInt<N> a, BigUInt<N> b) {
            (BigUInt<N> q, BigUInt<N> r) = DivRem(a, b);

            uint lzc_r = r.LeadingZeroCount;

            if (lzc_r == 0u) {
                UIntUtil.Add(q.value, 1u);
            }
            else {
                uint lzc_b = b.LeadingZeroCount;

                if (lzc_r == lzc_b) {
                    UIntUtil.Add(q.value, 1u);
                }
                else if ((lzc_r - lzc_b) == 1u) {
                    UIntUtil.LeftShift(r.value, 1, check_overflow: false);

                    if (UIntUtil.GreaterThanOrEqual((uint)Length, r.value, b.value)) {
                        UIntUtil.Add(q.value, 1u);
                    }
                }
            }

            return q;
        }

        public static BigUInt<N> RoundDiv(BigUInt<N> a, UInt64 b) {
            (BigUInt<N> q, UInt64 r) = DivRem(a, b);

            uint lzc_r = UIntUtil.LeadingZeroCount(r);

            if (lzc_r == 0u) {
                UIntUtil.Add(q.value, 1u);
            }
            else {
                uint lzc_b = UIntUtil.LeadingZeroCount(b);

                if (lzc_r == lzc_b) {
                    UIntUtil.Add(q.value, 1u);
                }
                else if ((lzc_r - lzc_b) == 1u) {
                    r <<= 1;

                    if (r >= b) {
                        UIntUtil.Add(q.value, 1u);
                    }
                }
            }

            return q;
        }

        public static BigUInt<M> RoundDiv<M>(BigUInt<M> a, BigUInt<N> b) where M : struct, IConstant {
            (BigUInt<M> q, BigUInt<N> r) = DivRem(a, b);

            uint lzc_r = r.LeadingZeroCount;

            if (lzc_r == 0u) {
                UIntUtil.Add(q.value, 1u);
            }
            else {
                uint lzc_b = b.LeadingZeroCount;

                if (lzc_r == lzc_b) {
                    UIntUtil.Add(q.value, 1u);
                }
                else if ((lzc_r - lzc_b) == 1u) {
                    UIntUtil.LeftShift(r.value, 1, check_overflow: false);

                    if (UIntUtil.GreaterThanOrEqual((uint)Length, r.value, b.value)) {
                        UIntUtil.Add(q.value, 1u);
                    }
                }
            }

            return q;
        }
    }
}
