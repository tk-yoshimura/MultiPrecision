namespace MultiPrecision {

    public static partial class MultiPrecisionUtil {

        public static MultiPrecision<Ndst> Convert<Ndst, Nsrc>(MultiPrecision<Nsrc> v) where Nsrc : struct, IConstant where Ndst : struct, IConstant {
            if (!MultiPrecision<Nsrc>.IsFinite(v)) {
                if (MultiPrecision<Nsrc>.IsNaN(v)) {
                    return MultiPrecision<Ndst>.NaN;
                }
                return v.Sign == Sign.Plus ? MultiPrecision<Ndst>.PositiveInfinity : MultiPrecision<Ndst>.NegativeInfinity;
            }

            int src_length = MultiPrecision<Nsrc>.Length, dst_length = MultiPrecision<Ndst>.Length;

            if (src_length == dst_length) {
                return new MultiPrecision<Ndst>(v.Sign, v.Exponent, [.. v.Mantissa]);
            }

            UInt32[] mantissa = new UInt32[MultiPrecision<Ndst>.Length];

            if (src_length <= dst_length) {
                Array.Copy(v.Mantissa.ToArray(), 0, mantissa, dst_length - src_length, src_length);

                return new MultiPrecision<Ndst>(
                    v.Sign, v.Exponent,
                    new Mantissa<Ndst>(mantissa, enable_clone: false), round: false);
            }
            else {
                Array.Copy(v.Mantissa.ToArray(), src_length - dst_length, mantissa, 0, dst_length);

                return new MultiPrecision<Ndst>(
                    v.Sign, v.Exponent,
                    new Mantissa<Ndst>(mantissa, enable_clone: false),
                    round: v.Mantissa[src_length - dst_length - 1] > UIntUtil.UInt32Round);
            }
        }
    }
}
