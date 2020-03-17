namespace MultiPrecision {    
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> e = null;
        }

        public static MultiPrecision<N> E {
            get {
                if (Consts.e is null) {
                    Consts.e = GenerateE();
                }

                return Consts.e;
            }
        }

        private static MultiPrecision<N> GenerateE() { 
            Accumulator<N> v = new Accumulator<N>(Mantissa<N>.One, UIntUtil.UInt32Bits - 1);
            Accumulator<N> m = v;
            Accumulator<N> i = 2;

            while (!v.IsZero) { 
                m += v;
                v /= i;
                i += 1;
            }

            (Mantissa<N> n, int _) = m.Mantissa;

            return new MultiPrecision<N>(Sign.Plus, exponent: 1, n, denormal_flush: false);
        }
    }
}
