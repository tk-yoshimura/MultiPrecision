using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> : ICloneable where N : struct, IConstant {

        private readonly UInt32[] value;
        public static int Length { get; } = checked(default(N).Value);
        public static int Bits { get; } = checked(Length * UIntUtil.UInt32Bits);
        public static int MaxDecimalDigits { get; } = checked((int)(Bits * 30103L / 100000L) - 4); //10^(4 - 1) = 1000 approx equals 1024
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

        public BigUInt(UInt32[] arr, bool enable_clone) {
            if (arr.Length != Length) {
                throw new ArgumentException(nameof(arr));
            }

            this.value = enable_clone ? (UInt32[])arr.Clone() : arr;
        }

        public BigUInt(IReadOnlyList<UInt32> arr) {
            if (arr.Count != Length) {
                throw new ArgumentException(nameof(arr));
            }

            this.value = arr.ToArray();
        }

        public BigUInt(UInt32[] arr, int offset, bool carry = false) : this() {
            if (offset + arr.Length > Length) {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (offset >= 0) {
                Array.Copy(arr, 0, this.value, offset, arr.Length);
            }
            else {
                Array.Copy(arr, -offset, this.value, 0, offset + arr.Length);
            }

            if (carry) {
                CarryAdd(0, 1u);
            }
        }

        public BigUInt(IReadOnlyList<UInt32> arr, int offset, bool carry = false)
            : this(arr.ToArray(), offset, carry) { }

        public bool IsZero => UIntUtil.IsZero(value);

        public bool IsFull => UIntUtil.IsFull(value);

        public int Digits => UIntUtil.Digits(value);

        public UInt64 MostSignificantDigits => UIntUtil.Pack(value[Length - 1], value[Length - 2]);

        public int LeadingZeroCount => UIntUtil.LeadingZeroCount(value);

        public UInt32 this[int index] => value[index];

        public object Clone() {
            return Copy();
        }

        public BigUInt<N> Copy() {
            return new BigUInt<N>(value, enable_clone: true);
        }

        public override bool Equals(object obj) {
            return (!(obj is null)) && (obj is BigUInt<N> n) && (n == this);
        }

        public override int GetHashCode() {
            return value[0].GetHashCode() ^ value.Last().GetHashCode();
        }
    }
}
