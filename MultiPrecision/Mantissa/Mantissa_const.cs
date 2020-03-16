using System;
using System.Linq;

namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        private static Mantissa<N> zero = null, one = null, full = null;

        public static Mantissa<N> Zero{
            get {
                if(zero is null) { 
                    zero = new Mantissa<N>();
                }

                return zero.Copy();
            }
        }

        public static Mantissa<N> One{
            get {
                if(one is null) { 
                    one = new Mantissa<N>(Enumerable.Repeat(0x00000000u, Length - 1).Concat(new UInt32[] { 0x80000000u }).ToArray());
                }

                return one.Copy();
            }
        }

        public static Mantissa<N> Full{
            get {
                if(full is null) { 
                    full = new Mantissa<N>(Enumerable.Repeat(0xFFFFFFFFu, Length).ToArray());
                }

                return full.Copy();
            }
        }
    }
}
