using System;
using System.Numerics;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        public static implicit operator BigInteger(BigUInt<N, K> n){
            byte[] b = new byte[Length * 4 + 1];

            for (int i = 0, j = 0; i < Length; i++, j += 4) {
                b[j + 0] = unchecked((byte)n.value[i]);
                b[j + 1] = unchecked((byte)(n.value[i] >> 8));
                b[j + 2] = unchecked((byte)(n.value[i] >> 16));
                b[j + 3] = unchecked((byte)(n.value[i] >> 24));
            }

            return new BigInteger(b);
        }

        public static implicit operator BigUInt<N, K>(UInt32 v){
            return new BigUInt<N, K>(v);
        }

        public static implicit operator BigUInt<N, K>(UInt64 v){
            return new BigUInt<N, K>(v);
        }
    }
}
