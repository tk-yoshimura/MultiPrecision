using System;
using System.Collections.ObjectModel;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> : ICloneable where N : struct, IConstant {

        private readonly UInt32[] arr;

        public static int Length => default(N).Value;
        public static int Bits => Length * UIntUtil.UInt32Bits;
        public ReadOnlyCollection<UInt32> Value => Array.AsReadOnly(arr);

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

        public Mantissa(UInt32[] arr, int src_index) {
            if (arr == null || arr.Length - src_index != Length) {
                throw new ArgumentException();
            }

            this.arr = new UInt32[Length];
            Array.Copy(arr, src_index, this.arr, 0, Length);
        }

        public bool IsZero => UIntUtil.IsZero(arr);

        public bool IsFull => UIntUtil.IsFull(arr);
        
        public int Digits => UIntUtil.Digits(arr);

        public UInt64 MostSignificantDigits => UIntUtil.Pack(arr[Length - 1], arr[Length - 2]);

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
