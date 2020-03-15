using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecision {
    internal sealed partial class MantissaBuffer<N> where N : struct, IConstant {

        private static readonly MantissaBuffer<N> zero, one, full;

        public static MantissaBuffer<N> Zero => zero.Copy();
        public static MantissaBuffer<N> One => one.Copy();
        public static MantissaBuffer<N> Full => full.Copy();

        static MantissaBuffer(){
            zero = new MantissaBuffer<N>();
            one = new MantissaBuffer<N>(Enumerable.Repeat(0x00000000u, Length - 1).Concat(new UInt32[] { 0x40000000u }).ToArray());
            full = new MantissaBuffer<N>(Enumerable.Repeat(0xFFFFFFFFu, Length).ToArray());
        }
    }
}
