using System;
using System.Diagnostics;
using System.Numerics;

namespace MultiPrecision {

    [DebuggerDisplay("{ToDouble()}")]
    public sealed partial class MultiPrecision<N> {

        public static explicit operator double(MultiPrecision<N> v) {
            return v.ToDouble();
        }

        public static explicit operator decimal(MultiPrecision<N> v) {
            return (decimal)v.ToDouble();
        }

        public static explicit operator Int64(MultiPrecision<N> v) {
            if (v.IsNaN) {
                throw new InvalidCastException("NaN");
            }

            if (v.IsZero || v.Exponent < 0) {
                return 0;
            }

            if (v.Exponent >= UIntUtil.UInt64Bits) {
                throw new OverflowException();
            }

            Accumulator<N> acc = new(v.mantissa, v.Exponent - Mantissa<N>.Bits + 1);

            if (acc.Digits > 2) {
                throw new OverflowException();
            }

            UInt64 u = UIntUtil.Pack(acc.Value[1], acc.Value[0]);

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
            if (v == 0) {
                return Zero;
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
            
            UInt32[] mantissa = new UInt32[Length * 2 + 2];
            
            mantissa[0] = (uint)arr[0];
            mantissa[1] = (uint)arr[1];
            mantissa[2] = (uint)arr[2];

            Accumulator<Plus1<N>> num = new(mantissa, enable_clone: false);

            while(exponent > 0 && num % 10 == 0) {
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
            if (IsFinite) {
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
