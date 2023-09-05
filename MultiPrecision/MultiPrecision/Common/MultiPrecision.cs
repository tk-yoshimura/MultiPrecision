using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> : ICloneable where N : struct, IConstant {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public const UInt32 ExponentMin = UInt32.MinValue;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public const UInt32 ExponentMax = UInt32.MaxValue;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public const UInt32 ExponentZero = UInt32.MaxValue / 2 + 1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly UInt32 exponent;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Mantissa<N> mantissa;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static int Length { get; } = Mantissa<N>.Length;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static int Bits { get; } = Mantissa<N>.Bits;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static int DecimalDigits { get; } = BigUInt<N>.MaxDecimalDigits;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Sign Sign { get; }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Int64 Exponent => (Int64)exponent - (Int64)ExponentZero;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ReadOnlyCollection<UInt32> Mantissa => mantissa.Value;

        public MultiPrecision(Sign sign, Int64 exponent, UInt32[] mantissa)
            : this(sign, exponent, new Mantissa<N>(mantissa, enable_clone: true), round: false) { }

        internal MultiPrecision(Sign sign, UInt32 exponent, Mantissa<N> mantissa) {
            this.Sign = sign;
            this.exponent = exponent;
            this.mantissa = mantissa;
        }

        internal MultiPrecision(Sign sign, Int64 exponent, Mantissa<N> mantissa, bool round) {
            Int64 exponent_zerosft = checked(exponent + ExponentZero);

            this.Sign = sign;

            if (mantissa.IsZero || exponent_zerosft <= ExponentMin) {
                this.exponent = ExponentMin;
                this.mantissa = Mantissa<N>.Zero;
            }
            else {
                if (round) {
                    if (mantissa.IsFull) {
                        mantissa = Mantissa<N>.One;
                        exponent_zerosft = checked(exponent_zerosft + 1);
                    }
                    else {
                        mantissa += 1;
                    }
                }

                if (exponent_zerosft >= ExponentMax) {
                    this.exponent = ExponentMax;
                    this.mantissa = Mantissa<N>.Zero;
                }
                else {
                    if (mantissa.Value[Length - 1] <= UIntUtil.UInt32Round) {
                        throw new ArgumentException(null, nameof(mantissa));
                    }

                    this.exponent = unchecked((UInt32)exponent_zerosft);
                    this.mantissa = mantissa;
                }
            }
        }

        private static MultiPrecision<N> CreateInteger(Sign sign, BigUInt<N> n) {
            int lzc = (int)n.LeadingZeroCount;
            return new MultiPrecision<N>(sign, (Int64)BigUInt<N>.Bits - lzc - 1, new Mantissa<N>(n << lzc), round: false);
        }

        private static MultiPrecision<N> CreateInteger(Sign sign, UInt64 num) {
            if (num == 0u) {
                return sign == Sign.Plus ? Zero : MinusZero;
            }

            UInt32[] vs = new UInt32[2], us = new UInt32[Length];
            (vs[^1], vs[^2]) = UIntUtil.Unpack(num);

            int lzc = (int)UIntUtil.LeadingZeroCount(vs);

            UIntUtil.LeftShift(vs, lzc);

            us[^1] = vs[^1];
            us[^2] = vs[^2];

            return new MultiPrecision<N>(sign, (uint)(UIntUtil.UInt64Bits - lzc - 1) + ExponentZero, new Mantissa<N>(us, enable_clone: false));
        }

        public static bool IsZero(MultiPrecision<N> value) => value.exponent <= ExponentMin && value.mantissa.IsZero;

        public static bool IsNaN(MultiPrecision<N> value) => value.exponent >= ExponentMax && !value.mantissa.IsZero;

        public static bool IsFinite(MultiPrecision<N> value) => value.exponent < ExponentMax;

        public static bool IsInfinity(MultiPrecision<N> value) => value.exponent >= ExponentMax && value.mantissa.IsZero;

        public static bool IsPositiveInfinity(MultiPrecision<N> value) => IsInfinity(value) && value.Sign == Sign.Plus;

        public static bool IsNegativeInfinity(MultiPrecision<N> value) => IsInfinity(value) && value.Sign == Sign.Minus;

        public static bool IsNormal(MultiPrecision<N> value) => (value.exponent > ExponentMin && value.exponent < ExponentMax) || IsZero(value);
        public static bool IsSubnormal(MultiPrecision<N> value) => false;

        public object Clone() {
            return Copy();
        }

        public MultiPrecision<N> Copy() {
            return new MultiPrecision<N>(Sign, exponent, mantissa);
        }

        public MultiPrecision<Ndst> Convert<Ndst>() where Ndst : struct, IConstant {
            return MultiPrecisionUtil.Convert<Ndst, N>(this);
        }

        public override bool Equals(object obj) {
            return (obj is MultiPrecision<N> n) && (n == this || (IsNaN(n) && IsNaN(this)));
        }

        public override int GetHashCode() {
            return Sign.GetHashCode() ^ exponent.GetHashCode() ^ mantissa.GetHashCode();
        }

        public string ToHexcode() {
            return $"{Sign} {Exponent}, {UIntUtil.ToHexcode(Mantissa)}";
        }
    }
}