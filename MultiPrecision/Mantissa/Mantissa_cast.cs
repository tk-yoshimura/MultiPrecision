using System;
using System.Numerics;

namespace MultiPrecision {
    public sealed partial class Mantissa<N> where N : struct, IConstant {

        public static implicit operator BigInteger(Mantissa<N> n){
            byte[] b = new byte[Length * 4 + 1];

            for (int i = 0, j = 0; i < Length; i++, j += 4) {
                b[j + 0] = unchecked((byte)n.arr[i]);
                b[j + 1] = unchecked((byte)(n.arr[i] >> 8));
                b[j + 2] = unchecked((byte)(n.arr[i] >> 16));
                b[j + 3] = unchecked((byte)(n.arr[i] >> 24));
            }

            return new BigInteger(b);
        }

        public static implicit operator Mantissa<N>(UInt32 v){
            return new Mantissa<N>(v);
        }

        public static implicit operator Mantissa<N>(UInt64 v){
            return new Mantissa<N>(v);
        }
    }
}
