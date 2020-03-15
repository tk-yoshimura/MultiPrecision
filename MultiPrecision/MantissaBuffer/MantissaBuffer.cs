using System;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class MantissaBuffer<N> : ICloneable where N : struct, IConstant {

        internal readonly UInt32[] arr;

        public static int Length => default(N).Value * 2;
        public static int Bits => Length * sizeof(float) * 8;
        public UInt32[] Value => (UInt32[])arr.Clone();

        public MantissaBuffer() {
            this.arr = new UInt32[Length];
        }

        public MantissaBuffer(UInt32 v) : this() {
            this.arr[0] = v;
        }

        public MantissaBuffer(UInt64 v) : this() {
            (this.arr[1], this.arr[0]) = UIntUtil.Unpack(v);
        }

        public MantissaBuffer(UInt32[] arr) {
            if (arr == null || arr.Length != Length) {
                throw new ArgumentException();
            }

            this.arr = (UInt32[])arr.Clone();
        }

        public MantissaBuffer(Mantissa<N> n) : this() {
            Array.Copy(n.arr, 0, arr, 0, Mantissa<N>.Length);
        }

        public MantissaBuffer(Mantissa<N> n, int sft) : this(n) {
            if (sft > 0) {
                LeftShift((uint)sft);
            }
            else if (sft < 0) {
                RightShift((uint)sft);
            }
        }

        public bool IsZero {
            get {
                for (int i = 0; i < Length; i++) {
                    if (arr[i] != 0) {
                        return false;
                    }
                }
                 
                return true;
            }
        }

        public void Zeroset() {
            for (int i = 0; i < Length; i++) {
                arr[i] = 0;
            }
        }

        public uint Digits {
            get {
                for (int i = Length - 1; i >= 0; i--) {
                    if (arr[i] != 0) {
                        return (uint)(i + 1);
                    }
                }
        
                return 1;
            }
        }

        public object Clone() {
            return Copy();
        }

        public MantissaBuffer<N> Copy() {
            return new MantissaBuffer<N>(arr);
        }

        public override bool Equals(object obj) {
            return (obj is MantissaBuffer<N> n) && (n == this);
        }

        public override int GetHashCode() {
            return arr[0].GetHashCode();
        }
    }
}
