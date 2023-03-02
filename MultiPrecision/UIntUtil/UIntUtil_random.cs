using System;
using System.Linq;

namespace MultiPrecision {
    internal static partial class UIntUtil {
        public static UInt32[] Random(Random random, int length, int bits) {
            int block = bits / UIntUtil.UInt32Bits, rem = bits % UIntUtil.UInt32Bits;

            if (block > length || (block == length && rem > 0)) {
                throw new ArgumentOutOfRangeException(nameof(bits));
            }

            UInt32[] value = (new UInt32[length]).Select((_, idx) => idx < block ? (UInt32)random.NextUInt32() : 0u).ToArray();

            if (rem > 0) {
                value[block] = (UInt32)random.NextUInt32() >> (UIntUtil.UInt32Bits - rem);
            }

            return value;
        }

        public static UInt32 NextUInt32(this Random random) {
            byte[] vs = new byte[sizeof(UInt32)];

            random.NextBytes(vs);

            return (UInt32)vs[0] | ((UInt32)vs[1] << ShiftIDX1) | ((UInt32)vs[2] << ShiftIDX2) | ((UInt32)vs[3] << ShiftIDX3);
        }
    }
}
