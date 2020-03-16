namespace MultiPrecision {
    internal sealed partial class Mantissa<N> {

        public int LeadingZeroCount => UIntUtil.LeadingZeroCount(arr);
    }
}
