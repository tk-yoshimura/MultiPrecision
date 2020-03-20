using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> : ICloneable where N : struct, IConstant where K : struct, IConstant {
        private readonly UInt32[] value;
        public static int Length { get; } = checked(default(N).Value * default(K).Value);
        public static int Bits { get; } = checked(Length * UIntUtil.UInt32Bits);
        public static int MaxDecimalDigits { get; } = checked(Bits * 30103 / 100000 - 3);
        public ReadOnlyCollection<UInt32> Value => Array.AsReadOnly(value);

        public BigUInt() {
            this.value = new UInt32[Length];
        }

        public BigUInt(UInt32 v) : this() {
            this.value[0] = v;
        }

        public BigUInt(UInt64 v) : this() {
            (this.value[1], this.value[0]) = UIntUtil.Unpack(v);
        }

        public BigUInt(UInt32[] arr) {
            if (arr == null || arr.Length != Length) {
                throw new ArgumentException();
            }

            this.value = (UInt32[])arr.Clone();
        }

        public BigUInt(ReadOnlyCollection<UInt32> arr) {
            if (arr == null || arr.Count != Length) {
                throw new ArgumentException();
            }

            this.value = arr.ToArray();
        }

        public BigUInt(UInt32[] arr, int index) : this() {
            if (arr == null || arr.Length - index != Length) {
                throw new ArgumentException();
            }
            Array.Copy(arr, index, this.value, 0, Length);
        }

        public BigUInt(ReadOnlyCollection<UInt32> arr, int index, bool carry) : this() {
            if (arr == null) {
                throw new ArgumentException();
            }

            for (int i = 0; i < arr.Count && i < Length; i++) {
                this.value[i] = arr[i + index];
            }

            if (carry) {
                CarryAdd(0, 1u);
            }
        }

        public unsafe bool IsZero {
            get {
                fixed (UInt32* v = value) {
                    for (int i = 0; i < Length; i++) {
                        if (v[i] != 0) {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public unsafe bool IsFull {
            get {
                fixed (UInt32* v = value) {
                    for (int i = 0; i < Length; i++) {
                        if (v[i] != UInt32.MaxValue) {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public unsafe int Digits {
            get {
                fixed (UInt32* v = value) {
                    for (int i = Length - 1; i >= 0; i--) {
                        if (v[i] != 0) {
                            return i + 1;
                        }
                    }
                }

                return 1;
            }
        }

        public UInt64 MostSignificantDigits => UIntUtil.Pack(value[Length - 1], value[Length - 2]);

        public UInt32 this[int index] {
            get {
                return value[index];
            }
        }

        public unsafe void Zeroset() {
            fixed (UInt32* v = value) {
                for (int i = 0; i < Length; i++) {
                    v[i] = 0;
                }
            }
        }

        public object Clone() {
            return Copy();
        }

        public BigUInt<N, K> Copy() {
            return new BigUInt<N, K>(value);
        }

        public override bool Equals(object obj) {
            return (obj is BigUInt<N, K> n) && (n == this);
        }

        public override int GetHashCode() {
            return value[0].GetHashCode();
        }
    }
}
