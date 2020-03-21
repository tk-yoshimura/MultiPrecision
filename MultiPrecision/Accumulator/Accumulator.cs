using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {

    [DebuggerDisplay("{ToHexcode()}")]
    internal sealed partial class Accumulator<N> : ICloneable where N : struct, IConstant {

        private readonly BigUInt<N, Pow2.N2> value;

        public static int Length { get; } = BigUInt<N, Pow2.N2>.Length;
        public static int Bits { get; } = BigUInt<N, Pow2.N2>.Bits;
        public static int MaxDecimalDigits { get; } = BigUInt<N, Pow2.N2>.MaxDecimalDigits;
        public ReadOnlyCollection<UInt32> Value => value.Value;

        public Accumulator() {
            this.value = new BigUInt<N, Pow2.N2>();
        }

        public Accumulator(UInt32 v) : this() {
            this.value = new BigUInt<N, Pow2.N2>(v);
        }

        public Accumulator(UInt64 v) : this() {
            this.value = new BigUInt<N, Pow2.N2>(v);
        }

        public Accumulator(UInt32[] arr) {
            this.value = new BigUInt<N, Pow2.N2>(arr);
        }

        public Accumulator(ReadOnlyCollection<UInt32> arr) {
            this.value = new BigUInt<N, Pow2.N2>(arr);
        }

        public Accumulator(BigUInt<N, Pow2.N2> value) {
            this.value = value;
        }

        public Accumulator(Mantissa<N> n, Int64 sft = 0) {
            if (sft == 0) {
                this.value = new BigUInt<N, Pow2.N2>(n.Value, 0, carry: false);
            }
            else if (sft > 0) {
                this.value = new BigUInt<N, Pow2.N2>(n.Value, 0, carry: false) << checked((int)sft);
            }
            else if (sft < 0 && sft > -Bits) {
                this.value = new BigUInt<N, Pow2.N2>(n.Value, 0, carry: false) >> (int)-sft;
            }
            else {
                this.value = BigUInt<N, Pow2.N2>.Zero;
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
