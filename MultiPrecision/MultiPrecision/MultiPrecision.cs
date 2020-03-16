using System;
using System.Diagnostics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> : ICloneable where N : struct, IConstant {

        public const UInt32 ExponentMin = UInt32.MinValue;
        public const UInt32 ExponentMax = UInt32.MaxValue;
        public const UInt32 ExponentZero = UInt32.MaxValue / 2 + 1;

        private Sign sign;
        private UInt32 exponent;
        private Mantissa<N> mantissa;

        public static int Length => Mantissa<N>.Length;
        public static int Bits => Length * sizeof(float) * 8;
        public static int Digits => Bits * 30103 / 100000;
        
        private MultiPrecision() {}

        internal MultiPrecision(Sign sign, Int64 exponent, Mantissa<N> mantissa) {
            Int64 exponent_zerosft = exponent + ExponentZero;

            this.sign = sign;

            if(exponent_zerosft >= ExponentMax) { 
                this.exponent = ExponentMax;
                this.mantissa = Mantissa<N>.Zero;
            }
            else if (mantissa.IsZero) { 
                this.exponent = ExponentMin;
                this.mantissa = Mantissa<N>.Zero;
            }
            else if(exponent_zerosft <= ExponentMin) { 
                this.exponent = ExponentMin;
                this.mantissa = mantissa >> (int)Math.Min(Mantissa<N>.Bits, -exponent_zerosft);
            }
            else{ 
                this.exponent = unchecked((UInt32)exponent_zerosft);
                this.mantissa = mantissa.Copy();
            }
        }

        internal static MultiPrecision<N> Create(Sign sign, UInt32 exponent, Mantissa<N> mantissa) { 
            MultiPrecision<N> ret = new MultiPrecision<N>();
            ret.sign = sign;
            ret.exponent = exponent;
            ret.mantissa = mantissa.Copy();

            return ret;
        }

        public bool IsZero => exponent <= ExponentMin && mantissa.IsZero;

        public bool IsNaN => exponent >= ExponentMax && !mantissa.IsZero;

        public bool IsFinite => exponent < ExponentMax;

        public bool IsNormal => (exponent > ExponentMin && exponent < ExponentMax) || IsZero;

        public Int64 Exponent => (Int64)exponent - (Int64)ExponentZero;

        public object Clone() {
            return Copy();
        }

        public MultiPrecision<N> Copy() {
            return Create(sign, exponent, mantissa);
        }

        public override bool Equals(object obj) {
            return (obj is MultiPrecision<N> n) && (n == this);
        }

        public override int GetHashCode() {
            return sign.GetHashCode() ^ exponent.GetHashCode() ^ mantissa.GetHashCode();
        }
    }
}