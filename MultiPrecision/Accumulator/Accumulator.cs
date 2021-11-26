using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MultiPrecision {

    [DebuggerDisplay("{ToHexcode()}")]
    internal sealed partial class Accumulator<N> : ICloneable where N : struct, IConstant {

        private readonly BigUInt<Double<N>> value;

        public static int Length { get; } = BigUInt<Double<N>>.Length;
        public static int Bits { get; } = BigUInt<Double<N>>.Bits;
        public static int MaxDecimalDigits { get; } = BigUInt<Double<N>>.MaxDecimalDigits;
        public IReadOnlyList<UInt32> Value => value.Value;

        public Accumulator() {
            this.value = new BigUInt<Double<N>>();
        }

        public Accumulator(UInt32 v) : this() {
            this.value = new BigUInt<Double<N>>(v);
        }

        public Accumulator(UInt64 v) : this() {
            this.value = new BigUInt<Double<N>>(v);
        }

        public Accumulator(UInt32[] arr, bool enable_clone) {
            this.value = new BigUInt<Double<N>>(arr, enable_clone);
        }

        public Accumulator(IReadOnlyList<UInt32> arr) {
            this.value = new BigUInt<Double<N>>(arr);
        }

        public Accumulator(BigUInt<Double<N>> value) {
            this.value = value;
        }

        public Accumulator(Mantissa<N> n, Int64 sft = 0) {
            if (sft == 0) {
                this.value = new BigUInt<Double<N>>(n.Value, 0);
            }
            else if (sft > 0) {
                this.value = new BigUInt<Double<N>>(n.Value, 0) << checked((int)sft);
            }
            else if (sft < 0 && sft > -Bits) {
                this.value = new BigUInt<Double<N>>(n.Value, 0) >> (int)-sft;
            }
            else {
                this.value = BigUInt<Double<N>>.Zero;
            }
        }

        public Accumulator(Mantissa<N> n, int sft) : this(n, (long)sft) { }

        public bool IsZero => value.IsZero;

        public bool IsFull => value.IsFull;

        public int Digits => value.Digits;

        public int LeadingZeroCount => value.LeadingZeroCount;

        public UInt64 MostSignificantDigits => value.MostSignificantDigits;

        public object Clone() {
            return Copy();
        }

        public Accumulator<N> Copy() {
            return new Accumulator<N>(value.Copy());
        }

        public override bool Equals(object obj) {
            return (obj is Accumulator<N> n) && (n == this);
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
