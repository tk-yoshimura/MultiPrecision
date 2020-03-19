using System.Linq;

namespace MultiPrecision {
    internal sealed partial class BigUInt<N, K> {

        public static BigUInt<N, K> Zero { get; } = new BigUInt<N, K>();
        
        public static BigUInt<N, K> Full { get; } = new BigUInt<N, K>(Enumerable.Repeat(0xFFFFFFFFu, Length).ToArray());
    }
}
