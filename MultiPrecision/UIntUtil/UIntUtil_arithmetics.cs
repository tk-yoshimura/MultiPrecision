using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32[] Mul(UInt64 a, UInt64 b) {
            (UInt64 a_hi, UInt64 a_lo) = Unpack(a);
            (UInt64 b_hi, UInt64 b_lo) = Unpack(b);

            (UInt64 v10, UInt32 v0) = Unpack(a_lo * b_lo);
            (UInt64 v20, UInt64 v11) = Unpack(a_lo * b_hi);
            (UInt64 v21, UInt64 v12) = Unpack(a_hi * b_lo);
            (UInt32 v3, UInt64 v22) = Unpack(a_hi * b_hi);

            (UInt64 carry1, UInt32 v1) = Unpack(v10 + v11 + v12);
            (UInt32 carry2, UInt32 v2) = Unpack(v20 + v21 + v22 + carry1);

            v3 += carry2;

            return new UInt32[] { v0, v1, v2, v3 };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Abs(Int64 x) { 
            return (x >= 0) ? unchecked((UInt64)x) : ~unchecked((UInt64)x) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sign Sign(Int64 x) { 
            return (x >= 0) ? MultiPrecision.Sign.Plus : MultiPrecision.Sign.Minus;
        }
    }
}
