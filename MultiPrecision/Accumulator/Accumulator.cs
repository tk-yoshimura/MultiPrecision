using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {

    [DebuggerDisplay("{ToHexcode()}")]
    internal sealed partial class Accumulator<N> : ICloneable where N : struct, IConstant {

        private readonly BigUInt<N, Pow2.N2> value;

        public static int Length { get; } = BigUInt<N, Pow2.N2>.Length;
        public static int Bits { get; } = BigUInt<N, Pow2.N2>.Bits;
        public ReadOnlyCollection<UInt32> Value => Array.AsReadOnly(value.Value);

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

        public Accumulator(BigUInt<N, Pow2.N2> value) {
            this.value = value;
        }

        public Accumulator(Mantissa<N> n) : this() {
            ReadOnlyCollection<UInt32> m = n.Value;

            UInt32[] vs = value.Value;

            for(int i = 0; i < Mantissa<N>.Length; i++) { 
                vs[i] = m[i];
            }
        }

        public Accumulator(Mantissa<N> n, int sft) : this(n) {
            if (sft > 0) {
                value.LeftShift(sft);
            }
            else if (sft < 0 && -sft >= 0) {
                value.RightShift(-sft);
            }
        }

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
