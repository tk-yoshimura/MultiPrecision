using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> : ICloneable where N : struct, IConstant {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly UInt32[] value;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static int Length { get; } = checked(default(N).Value);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static int Bits { get; } = checked(Length * UIntUtil.UInt32Bits);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static int MaxDecimalDigits { get; } = checked((int)(Bits * 30103L / 100000L) - 4); //10^(4 - 1) = 1000 approx equals 1024
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ReadOnlyCollection<UInt32> Value => Array.AsReadOnly(value);

        static BigUInt() {
            if (Length < 4 || Length > 0x2000000) {
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
                throw new ArgumentException("invalid length.", nameof(arr));
            }

            this.value = enable_clone ? (UInt32[])arr.Clone() : arr;
        }

        public BigUInt(IReadOnlyList<UInt32> arr) {
            if (arr.Count != Length) {
                throw new ArgumentException("invalid length.", nameof(arr));
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

            if (!carry) {
                return;
            }

            for (int i = 0; i < Length; i++) {
                if ((~this.value[i]) != 0) {
                    this.value[i]++;
                    return;
                }
                else {
                    this.value[i] = 0u;
                }
            }

            throw new OverflowException();
        }

        public BigUInt(IReadOnlyList<UInt32> arr, int offset, bool carry = false)
            : this(arr.ToArray(), offset, carry) { }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static BigUInt<N> Zero => new();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsZero => UIntUtil.IsZero((uint)Length, value);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static BigUInt<N> Full =>
            new(Enumerable.Repeat(~0u, Length).ToArray(), enable_clone: false);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsFull => UIntUtil.IsFull((uint)Length, value);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public uint Digits => UIntUtil.Digits(value);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public UInt64 MostSignificantDigits => UIntUtil.Pack(value[Length - 1], value[Length - 2]);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public uint LeadingZeroCount => UIntUtil.LeadingZeroCount(value);

        public UInt32 this[int index] => value[index];

        public object Clone() {
            return Copy();
        }

        public BigUInt<N> Copy() {
            return new BigUInt<N>(value, enable_clone: true);
        }

        public BigUInt<M> Convert<M>(bool check_overflow = true) where M : struct, IConstant {
            int length_src = Length, length_dst = default(M).Value;

            if (!check_overflow && length_src > length_dst) {
                return new BigUInt<M>(value[..length_dst], enable_clone: false);
            }
            else if (length_src <= length_dst) {
                UInt32[] ret = new UInt32[length_dst];
                Array.Copy(value, ret, length_src);

                return new BigUInt<M>(ret, enable_clone: false);
            }
            else {
                uint digits = Digits;
                if (digits > length_dst) {
                    throw new OverflowException();
                }

                UInt32[] ret = new UInt32[length_dst];
                Array.Copy(value, ret, digits);

                return new BigUInt<M>(ret, enable_clone: false);
            }
        }

        public BigUInt<M> Convert<M>(int offset) where M : struct, IConstant {
            return new BigUInt<M>(value, offset, carry: false);
        }

        public override bool Equals([AllowNull] object obj) {
            return obj is not null && obj is BigUInt<N> n && n == this;
        }

        public override int GetHashCode() {
            return value[0].GetHashCode() ^ value[^1].GetHashCode();
        }
    }
}