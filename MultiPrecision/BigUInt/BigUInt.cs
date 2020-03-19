using System;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> : ICloneable where N : struct, IConstant where K : struct, IConstant {
        public static int Length { get; } = default(N).Value * default(K).Value;
        public static int Bits { get; } = Length * UIntUtil.UInt32Bits;
        public static int MaxDecimalDigits { get; } = Bits * 30103 / 100000;
        public UInt32[] Value { get; }

        public BigUInt() {
            this.Value = new UInt32[Length];
        }

        public BigUInt(UInt32 v) : this() {
            this.Value[0] = v;
        }

        public BigUInt(UInt64 v) : this() {
            (this.Value[1], this.Value[0]) = Unpack(v);
        }

        public BigUInt(UInt32[] arr) {
            if (arr == null || arr.Length != Length) {
                throw new ArgumentException();
            }

            this.Value = (UInt32[])arr.Clone();
        }

        public BigUInt(UInt32[] arr, int index) : this(){
            if (arr == null || arr.Length - index != Length) {
                throw new ArgumentException();
            }
            Array.Copy(arr, index, this.Value, 0, Length);
        }

        public unsafe bool IsZero{
            get {
                fixed (UInt32* v = Value) {
                    for (int i = 0; i < Length; i++) {
                        if (v[i] != 0) {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public unsafe bool IsFull{
            get {
                fixed (UInt32* v = Value) {
                    for (int i = 0; i < Length; i++) {
                    if (v[i] != UInt32.MaxValue) {
                        return false;
                    }
                }
            }

            return true;
            }
        }
        
        public unsafe int Digits{
            get {
                fixed (UInt32* v = Value) {
                    for (int i = Length - 1; i >= 0; i--) {
                        if (v[i] != 0) {
                            return i + 1;
                        }
                    }
                }
        
                return 1;
            }
        }

        public UInt64 MostSignificantDigits => Pack(Value[Length - 1], Value[Length - 2]);

        public UInt32 this[int index] {
            get {
                return Value[index];
            }
            set {
                Value[index] = value;
            }
        }

        public unsafe void Zeroset() { 
            fixed (UInt32* v = Value) {
                for (int i = 0; i < Length; i++) {
                    v[i] = 0;
                }
            }
        }

        public object Clone() {
            return Copy();
        }

        public BigUInt<N, K> Copy() {
            return new BigUInt<N, K>(Value);
        }

        public override bool Equals(object obj) {
            return (obj is BigUInt<N, K> n) && (n == this);
        }

        public override int GetHashCode() {
            return Value[0].GetHashCode();
        }
    }
}
