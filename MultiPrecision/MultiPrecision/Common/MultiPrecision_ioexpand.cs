using System;
using System.IO;

namespace MultiPrecision {

    public static class MultiPrecisionIOExpand {

        public static void Write<N>(this BinaryWriter writer, MultiPrecision<N> n) where N : struct, IConstant {
            writer.Write((byte)n.Sign);
            writer.Write(checked((UInt32)(n.Exponent + MultiPrecision<N>.ExponentZero)));
            for (int i = 0; i < MultiPrecision<N>.Length; i++) {
                writer.Write((UInt32)n.Mantissa[i]);
            }
        }

        public static MultiPrecision<N> ReadMultiPrecision<N>(this BinaryReader reader) where N : struct, IConstant {
            Sign sign = (Sign)reader.ReadByte();
            if (!Enum.IsDefined(typeof(Sign), sign)) {
                throw new FormatException(nameof(sign));
            }

            UInt32 exponent = (UInt32)reader.ReadUInt32();
            UInt32[] mantissa = new UInt32[MultiPrecision<N>.Length];
            for (int i = 0; i < MultiPrecision<N>.Length; i++) {
                mantissa[i] = reader.ReadUInt32();
            }

            if (UIntUtil.IsZero((uint)MultiPrecision<N>.Length, mantissa)) {
                if (exponent != MultiPrecision<N>.ExponentMin && exponent != MultiPrecision<N>.ExponentMax) {
                    throw new FormatException(nameof(exponent));
                }
            }
            else {
                if (UIntUtil.LeadingZeroCount(mantissa) != 0u) {
                    throw new FormatException(nameof(mantissa));
                }
            }

            return new MultiPrecision<N>(sign, exponent, new Mantissa<N>(mantissa, enable_clone: false));
        }
    }
}
