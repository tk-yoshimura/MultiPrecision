using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecision {
    public sealed partial class Mantissa<N> where N : struct, IConstant {

        private static readonly Mantissa<N> zero, one, full;

        public static Mantissa<N> Zero => zero.Copy();
        public static Mantissa<N> One => one.Copy();
        public static Mantissa<N> Full => full.Copy();

        static Mantissa(){
            zero = new Mantissa<N>();
            one = new Mantissa<N>(Enumerable.Repeat(0x00000000u, Length - 1).Concat(new UInt32[] { 0x80000000u }).ToArray());
            full = new Mantissa<N>(Enumerable.Repeat(0xFFFFFFFFu, Length).ToArray());
        }
    }
}
