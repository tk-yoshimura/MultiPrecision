//namespace MultiPrecision {    
//    public sealed partial class MultiPrecision<N> {

//        private static partial class Consts {
//            public static MultiPrecision<N> pi = null;
//        }

//        public static MultiPrecision<N> PI {
//            get {
//                if (Consts.sqrt2 is null) {
//                    Consts.sqrt2 = GeneratePI();
//                }

//                return Consts.sqrt2;
//            }
//        }

//        private static MultiPrecision<N> GeneratePI() { 
//            MultiPrecision<N> a = One, b = Sqrt2 / 2, t = Ldexp(Mantissa<N>.One, -2), p = One;

//            while(x.LeadingZeroCount >= 2) { 
//                Accumulator<N> x_next = x + (y << 1);
//                Accumulator<N> y_next = x + y;

//                x = x_next;
//                y = y_next;
//            }

//            y >>= Mantissa<N>.Bits;

//            Accumulator<N> acc = x / y;
//            (Mantissa<N> n, int _) = acc.Mantissa;

//            return new MultiPrecision<N>(Sign.Plus, exponent: 0, n, denormal_flush: false);
//        }
//    }
//}
