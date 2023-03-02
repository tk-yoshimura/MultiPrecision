using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {

    [DebuggerDisplay("{ToHexcode()}")]
    internal sealed partial class Mantissa<N> : ICloneable where N : struct, IConstant {

        private readonly BigUInt<N> value;

        public static int Length { get; } = BigUInt<N>.Length;
        public static int Bits { get; } = BigUInt<N>.Bits;
        public static int MaxDecimalDigits { get; } = BigUInt<N>.MaxDecimalDigits;
        public ReadOnlyCollection<UInt32> Value => value.Value;

        public Mantissa() {
            this.value = new BigUInt<N>();
        }

        public Mantissa(UInt32 v) {
            this.value = new BigUInt<N>(v);
        }

        public Mantissa(UInt64 v) {
            this.value = new BigUInt<N>(v);
        }

        public Mantissa(UInt32[] arr, bool enable_clone) {
            this.value = new BigUInt<N>(arr, enable_clone);
        }

        public Mantissa(IReadOnlyList<UInt32> arr) {
            this.value = new BigUInt<N>(arr);
        }

        public Mantissa(BigUInt<N> value) {
            this.value = value;
        }

        public bool IsZero => value.IsZero;

        public bool IsFull => value.IsFull;

        public int Digits => (int)value.Digits;

        public int LeadingZeroCount => (int)value.LeadingZeroCount;

        public UInt64 MostSignificantDigits => value.MostSignificantDigits;

        public object Clone() {
            return Copy();
        }

        public Mantissa<N> Copy() {
            return new Mantissa<N>(value.Copy());
        }

        public override bool Equals([AllowNull] object obj) {
            return obj is not null && obj is Mantissa<N> n && n == this;
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

        public override string ToString() {
            return value.ToString();
        }

        public string ToHexcode() {
            return value.ToHexcode();
        }
    }
}
