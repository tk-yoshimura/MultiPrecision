using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecision {
    internal sealed partial class MantissaBuffer<N> {

        private static MantissaBuffer<N> zero = null, one = null, full = null;

        public static MantissaBuffer<N> Zero{
            get {
                if(zero is null) { 
                    zero = new MantissaBuffer<N>();
                }

                return zero.Copy();
            }
        }

        public static MantissaBuffer<N> One{
            get {
                if(one is null) { 
                    one = new MantissaBuffer<N>(Enumerable.Repeat(0x00000000u, Length - 1).Concat(new UInt32[] { 0x40000000u }).ToArray());
                }

                return one.Copy();
            }
        }

        public static MantissaBuffer<N> Full{
            get {
                if(full is null) { 
                    full = new MantissaBuffer<N>(Enumerable.Repeat(0xFFFFFFFFu, Length).ToArray());
                }

                return full.Copy();
            }
        }
    }
}
