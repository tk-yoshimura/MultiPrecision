using System;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> : ICloneable where N : struct, IConstant {

        internal readonly UInt32[] arr;

        public static int Length => default(N).Value * 2;
        public static int Bits => Length * sizeof(float) * 8;
        public UInt32[] Value => (UInt32[])arr.Clone();

        public Accumulator() {
            this.arr = new UInt32[Length];
        }

        public Accumulator(UInt32 v) : this() {
            this.arr[0] = v;
        }

        public Accumulator(UInt64 v) : this() {
            (this.arr[1], this.arr[0]) = UIntUtil.Unpack(v);
        }

        public Accumulator(UInt32[] arr) {
            if (arr == null || arr.Length != Length) {
                throw new ArgumentException();
            }

            this.arr = (UInt32[])arr.Clone();
        }

        public Accumulator(Mantissa<N> n) : this() {
            Array.Copy(n.arr, 0, arr, 0, Mantissa<N>.Length);
        }

        public Accumulator(Mantissa<N> n, int sft) : this(n) {
            if (sft > 0) {
                LeftShift(sft);
            }
            else if (sft < 0) {
                RightShift(-sft);
            }
        }

        public bool IsZero => UIntUtil.IsZero(arr);

        public void Zeroset() {
            UIntUtil.Zeroset(arr);
        }

        public int Digits => UIntUtil.Digits(arr);

        public object Clone() {
            return Copy();
        }

        public Accumulator<N> Copy() {
            return new Accumulator<N>(arr);
        }

        public override bool Equals(object obj) {
            return (obj is Accumulator<N> n) && (n == this);
        }

        public override int GetHashCode() {
            return arr[0].GetHashCode();
        }
    }
}
