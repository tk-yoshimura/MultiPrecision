using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {

    [DebuggerDisplay("{ToHexcode()}")]
    internal sealed partial class Mantissa<N> : ICloneable where N : struct, IConstant {

        private readonly BigUInt<N, Pow2.N1> value;

        public static int Length { get; } = BigUInt<N, Pow2.N1>.Length;
        public static int Bits { get; } = BigUInt<N, Pow2.N1>.Bits;
        public static int MaxDecimalDigits { get; } = BigUInt<N, Pow2.N1>.MaxDecimalDigits;
        public ReadOnlyCollection<UInt32> Value => Array.AsReadOnly(value.Value);

        public Mantissa() {
            this.value = new BigUInt<N, Pow2.N1>();
        }

        public Mantissa(UInt32 v) { 
            this.value = new BigUInt<N, Pow2.N1>(v);
        }

        public Mantissa(UInt64 v) {
            this.value = new BigUInt<N, Pow2.N1>(v);
        }

        public Mantissa(UInt32[] arr) {
            this.value = new BigUInt<N, Pow2.N1>(arr);
        }

        public Mantissa(BigUInt<N, Pow2.N1> value) {
            this.value = value;
        }

        public Mantissa(UInt32[] arr, int src_index) {
            this.value = new BigUInt<N, Pow2.N1>(arr, src_index);
        }

        public bool IsZero => value.IsZero;

        public bool IsFull => value.IsFull;
        
        public int Digits => value.Digits;

        public int LeadingZeroCount => value.LeadingZeroCount;

        public UInt64 MostSignificantDigits => value.MostSignificantDigits;

        public object Clone() {
            return Copy();
        }

        public Mantissa<N> Copy() {
            return new Mantissa<N>(value.Copy());
        }

        public override bool Equals(object obj) {
            return (obj is Mantissa<N> n) && (n == this);
        }

        public override int GetHashCode() {
            return HashCode.Combine(this.value);
        }

        public override string ToString() {
            return value.ToString();
        }

        public string ToHexcode() {
            return value.ToHexcode();
        }
    }
}
