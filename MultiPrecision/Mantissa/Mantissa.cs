using System;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> : ICloneable where N : struct, IConstant {

        internal readonly UInt32[] arr;

        public static int Length => default(N).Value;
        public static int Bits => Length * sizeof(float) * 8;
        public UInt32[] Value => (UInt32[])arr.Clone();

        public Mantissa() {
            this.arr = new UInt32[Length];
        }

        public Mantissa(UInt32 v) : this() {
            this.arr[0] = v;
        }

        public Mantissa(UInt64 v) : this() {
            (this.arr[1], this.arr[0]) = UIntUtil.Unpack(v);
        }

        public Mantissa(UInt32[] arr) {
            if (arr == null || arr.Length != Length) {
                throw new ArgumentException();
            }

            this.arr = (UInt32[])arr.Clone();
        }

        public bool IsZero => UIntUtil.IsZero(arr);

        public void Zeroset() {
            UIntUtil.Zeroset(arr);
        }

        public int Digits => UIntUtil.Digits(arr);

        public object Clone() {
            return Copy();
        }

        public Mantissa<N> Copy() {
            return new Mantissa<N>(arr);
        }

        public override bool Equals(object obj) {
            return (obj is Mantissa<N> n) && (n == this);
        }

        public override int GetHashCode() {
            return arr[0].GetHashCode();
        }
    }
}
