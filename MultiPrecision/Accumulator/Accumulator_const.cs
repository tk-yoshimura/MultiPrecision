using System.Linq;

namespace MultiPrecision {
    internal sealed partial class Accumulator<N> {

        private static Accumulator<N> zero = null, one = null, full = null;

        public static Accumulator<N> Zero{
            get {
                if(zero is null) { 
                    zero = new Accumulator<N>();
                }

                return zero;
            }
        }

        public static Accumulator<N> One{
            get {
                if(one is null) { 
                    one = new Accumulator<N>(Mantissa<N>.One) * new Accumulator<N>(Mantissa<N>.One);
                }

                return one;
            }
        }

        public static Accumulator<N> Full{
            get {
                if(full is null) { 
                    full = new Accumulator<N>(Enumerable.Repeat(0xFFFFFFFFu, Length).ToArray());
                }

                return full;
            }
        }
    }
}
