using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> : ICloneable where N : struct, IConstant {

        private Sign sign;
        private UInt32 exponent;
        private Mantissa<N> mantissa;

        public static int Length => Mantissa<N>.Length;
        public static int Bits => Length * sizeof(float) * 8;
        public static int Digits => Bits * 30103 / 100000;

        public MultiPrecision() {
            this.sign = Sign.Plus;
            this.exponent = 0;
            this.mantissa = Mantissa<N>.Zero;
        }

        internal MultiPrecision(Sign sign, UInt32 exponent, Mantissa<N> mantissa) {
            this.sign = sign;
            this.exponent = exponent;
            this.mantissa = mantissa.Copy();
        }

        public bool IsZero => exponent == 0 && mantissa.IsZero;

        public bool IsNaN => exponent == UInt32.MaxValue && !mantissa.IsZero;

        public bool IsFinite => exponent < UInt32.MaxValue || mantissa.IsZero;

        public bool IsNormal => (exponent > 0 && exponent < UInt32.MaxValue) || IsZero;
        
        public void Zeroset() {
            sign = Sign.Plus;
            exponent = 0;
            mantissa.Zeroset();
        }

        public object Clone() {
            return Copy();
        }

        public MultiPrecision<N> Copy() {
            return new MultiPrecision<N>(sign, exponent, mantissa);
        }

        public override bool Equals(object obj) {
            return (obj is MultiPrecision<N> n) && (n == this);
        }

        public override int GetHashCode() {
            return sign.GetHashCode() ^ exponent.GetHashCode() ^ mantissa.GetHashCode();
        }
    }
}