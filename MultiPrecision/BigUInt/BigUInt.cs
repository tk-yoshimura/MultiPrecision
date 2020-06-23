using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> : ICloneable where N : struct, IConstant where K : struct, IConstant {

        private readonly UInt32[] value;
        public static int Length { get; } = checked(default(N).Value * default(K).Value);
        public static int Bits { get; } = checked(Length * UIntUtil.UInt32Bits);
        public static int MaxDecimalDigits { get; } = checked(Bits * 30103 / 100000 - 4); //10^(4 - 1) = 1000 approx equals 1024
        public ReadOnlyCollection<UInt32> Value => Array.AsReadOnly(value);

        static BigUInt() {
            if (Length < 4) {
                throw new ArgumentOutOfRangeException(nameof(Length));
            }
        }

        public BigUInt() {
            this.value = new UInt32[Length];
        }

        public BigUInt(UInt32 v) : this() {
            this.value[0] = v;
        }

        public BigUInt(UInt64 v) : this() {
            (this.value[1], this.value[0]) = UIntUtil.Unpack(v);
        }

        public BigUInt([DisallowNull] UInt32[] arr, bool enable_clone = true) {
            if (arr == null || arr.Length != Length) {
                throw new ArgumentException();
            }

            this.value = enable_clone ? (UInt32[])arr.Clone() : arr;
        }

        public BigUInt([DisallowNull] ReadOnlyCollection<UInt32> arr) {
            if (arr == null || arr.Count != Length) {
                throw new ArgumentException();
            }

            this.value = arr.ToArray();
        }

        public BigUInt([DisallowNull] UInt32[] arr, int index) : this() {
            if (arr == null || arr.Length - index != Length || index < 0) {
                throw new ArgumentException();
            }
            Array.Copy(arr, index, this.value, 0, Length);
        }

        public BigUInt([DisallowNull] ReadOnlyCollection<UInt32> arr, int index, bool carry) : this() {
            if (arr == null || index < 0) {
                throw new ArgumentException();
            }

            for (int i = 0; i < Length && i + index < arr.Count; i++) {
                this.value[i] = arr[i + index];
            }

            if (carry) {
                CarryAdd(0, 1u);
            }
        }

        public bool IsZero => UIntUtil.IsZero(value);

        public bool IsFull => UIntUtil.IsFull(value);

        public int Digits => UIntUtil.Digits(value);

        public UInt64 MostSignificantDigits => UIntUtil.Pack(value[Length - 1], value[Length - 2]);

        public int LeadingZeroCount => UIntUtil.LeadingZeroCount(value);

        public UInt32 this[int index] {
            get {
                return value[index];
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
