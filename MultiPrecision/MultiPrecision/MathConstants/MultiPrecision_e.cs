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
            Accumulator<N> v = Accumulator<N>.One >> Accumulator<N>.TaylorTableShift;

            foreach (Accumulator<N> u in Accumulator<N>.TaylorTable) {
                v += u;
            }

            (Mantissa<N> n, int sft) = v.Mantissa;

            return new MultiPrecision<N>(Sign.Plus, Accumulator<N>.TaylorTableShift - sft + 1, n, denormal_flush: false);
        }
    }
}
