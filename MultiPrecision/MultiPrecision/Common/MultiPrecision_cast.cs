﻿using System.Numerics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static explicit operator double(MultiPrecision<N> v) {
            return v.ToDouble();
        }

        public static explicit operator float(MultiPrecision<N> v) {
            return (float)(double)v;
        }

        public static explicit operator decimal(MultiPrecision<N> v) {
            const int digits = 28;

            (Sign sign, long exponent, BigInteger num) = v.ToStringCore(digits);

            exponent -= digits;

            while (exponent < 0 && num % 10 == 0) {
                exponent++;
                num /= 10;
            }

            UInt32[] mantissa =
            [
                (UInt32)(num & (BigInteger)~0u),
                (UInt32)((num >> 32) & (BigInteger)~0u),
                (UInt32)(num >> 64),
            ];
            decimal d = new(
                unchecked((int)mantissa[0]),
                unchecked((int)mantissa[1]),
                unchecked((int)mantissa[2]),
                isNegative: sign == Sign.Minus, scale: checked((byte)(-exponent))
            );

            return d;
        }

        public static explicit operator Int64(MultiPrecision<N> v) {
            if (IsNaN(v)) {
                throw new InvalidCastException("NaN");
            }

            if (IsZero(v) || v.Exponent < 0) {
                return 0;
            }

            if (v.Exponent >= UIntUtil.UInt64Bits) {
                throw new OverflowException();
            }

            UInt32[] arr = [.. v.mantissa.Value];
            UIntUtil.RightShift(arr, Bits - (int)v.Exponent - 1);

            UInt64 u = UIntUtil.Pack(arr[1], arr[0]);

            if (v.Sign == Sign.Plus) {
                if (u > (UInt64)Int64.MaxValue) {
                    throw new OverflowException();
                }

                return unchecked((Int64)u);
            }
            else {
                if (u > (UInt64)Int64.MaxValue + 1) {
                    throw new OverflowException();
                }

                return unchecked(-(Int64)u);
            }
        }

        public static implicit operator MultiPrecision<N>(double v) {
            if (double.IsNaN(v)) {
                return NaN;
            }
            if (double.IsInfinity(v)) {
                return v > 0 ? PositiveInfinity : NegativeInfinity;
            }
            if (v == 0d) {
                return double.IsPositive(v) ? Zero : MinusZero;
            }

            const int double_bits = 52;

            int exponent = Math.ILogB(v);
            Int64 n = (Int64)Math.ScaleB(Math.Abs(v), -exponent + double_bits);

            return Math.Sign(v) * Ldexp(n, exponent - double_bits);
        }

        public static implicit operator MultiPrecision<N>(decimal v) {
            int[] arr = decimal.GetBits(v);

            Sign sign = arr[3] >= 0 ? Sign.Plus : Sign.Minus;
            int exponent = (arr[3] >> 16) & 0xFF;

            UInt32[] n = new UInt32[Length + 1];

            n[0] = (uint)arr[0];
            n[1] = (uint)arr[1];
            n[2] = (uint)arr[2];

            BigUInt<Plus1<N>> num = new(n, enable_clone: false);

            while (exponent > 0 && num % 10 == 0) {
                exponent--;
                num /= 10;
            }

            MultiPrecision<N> x = FromStringCore(sign, 0, num, exponent);

            return x;
        }

        public static implicit operator MultiPrecision<N>(BigInteger bigint) {
            return $"{bigint}";
        }

        public static implicit operator MultiPrecision<N>(Int64 v) {
            UInt64 v_abs = UIntUtil.Abs(v);

            return CreateInteger((v >= 0) ? Sign.Plus : Sign.Minus, v_abs);
        }

        public double ToDouble() {
            if (IsFinite(this)) {
                UInt64 n = mantissa.MostSignificantDigits;

                return (double)n * Math.Pow(2, (double)(Exponent - UIntUtil.UInt64Bits + 1)) * ((Sign == Sign.Plus) ? 1 : -1);
            }
            else if (mantissa.IsZero) {
                return (Sign == Sign.Plus) ? double.PositiveInfinity : double.NegativeInfinity;
            }
            else {
                return double.NaN;
            }
        }
    }
}
