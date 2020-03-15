using System;
using System.Numerics;

namespace MultiPrecision {
    internal sealed partial class MantissaBuffer<N> where N : struct, IConstant {

        public static implicit operator BigInteger(MantissaBuffer<N> n){
            byte[] b = new byte[Length * 4 + 1];

            for (int i = 0, j = 0; i < Length; i++, j += 4) {
                b[j + 0] = unchecked((byte)n.arr[i]);
                b[j + 1] = unchecked((byte)(n.arr[i] >> 8));
                b[j + 2] = unchecked((byte)(n.arr[i] >> 16));
                b[j + 3] = unchecked((byte)(n.arr[i] >> 24));
            }

            return new BigInteger(b);
        }

        public static implicit operator MantissaBuffer<N>(UInt32 v){
            return new MantissaBuffer<N>(v);
        }

        public static implicit operator MantissaBuffer<N>(UInt64 v){
            return new MantissaBuffer<N>(v);
        }
    }
}
