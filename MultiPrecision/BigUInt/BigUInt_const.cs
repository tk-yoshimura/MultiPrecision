using System.Diagnostics;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N> {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static BigUInt<N> top40000000u = null, top80000000u = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static BigUInt<N> Top40000000u {
            get {
                top40000000u ??= new(Enumerable.Repeat(0u, Length - 1).Concat(new UInt32[] { 0x40000000u }).ToArray(), enable_clone: false);

                return top40000000u.Copy();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static BigUInt<N> Top80000000u {
            get {
                top80000000u ??= new(Enumerable.Repeat(0u, Length - 1).Concat(new UInt32[] { 0x80000000u }).ToArray(), enable_clone: false);

                return top80000000u.Copy();
            }
        }
    }
}
