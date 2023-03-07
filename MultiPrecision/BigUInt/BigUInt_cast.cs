using System.Numerics;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {

        public static implicit operator BigInteger(BigUInt<N> n) {
            byte[] b = new byte[Length * 4 + 1];

            for (int i = 0, j = 0; i < Length; i++, j += 4) {
                b[j + 0] = unchecked((byte)n.value[i]);
                b[j + 1] = unchecked((byte)(n.value[i] >> UIntUtil.ShiftIDX1));
                b[j + 2] = unchecked((byte)(n.value[i] >> UIntUtil.ShiftIDX2));
                b[j + 3] = unchecked((byte)(n.value[i] >> UIntUtil.ShiftIDX3));
            }

            return new BigInteger(b);
        }

        public static implicit operator BigUInt<N>(UInt32 v) {
            return new BigUInt<N>(v);
        }

        public static implicit operator BigUInt<N>(UInt64 v) {
            return new BigUInt<N>(v);
        }
    }
}
