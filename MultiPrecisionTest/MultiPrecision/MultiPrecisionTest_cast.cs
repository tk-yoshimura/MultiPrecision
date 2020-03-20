using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void Int64Test() {
            MultiPrecision<Pow2.N8> zero = 0;
            Console.WriteLine((double)zero);
            Console.WriteLine((Int64)zero);

            MultiPrecision<Pow2.N8> one = 1;
            Console.WriteLine((double)one);
            Console.WriteLine((Int64)one);

            MultiPrecision<Pow2.N8> minus_one = -1;
            Console.WriteLine((double)minus_one);
            Console.WriteLine((Int64)minus_one);

            MultiPrecision<Pow2.N8> maxhalf_value = Int64.MaxValue / 2;
            Console.WriteLine((double)maxhalf_value);
            Console.WriteLine((Int64)maxhalf_value);

            MultiPrecision<Pow2.N8> minhalf_value = Int64.MinValue / 2;
            Console.WriteLine((double)minhalf_value);
            Console.WriteLine((Int64)minhalf_value);

            MultiPrecision<Pow2.N8> max_value = Int64.MaxValue;
            Console.WriteLine((double)max_value);
            Console.WriteLine((Int64)max_value);

            MultiPrecision<Pow2.N8> min_value = Int64.MinValue;
            Console.WriteLine((double)min_value);
            Console.WriteLine((Int64)min_value);

            for (Int64 i = 10; i <= 100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Console.WriteLine((double)m);
                Console.WriteLine((Int64)m);
            }

            for (Int64 i = -10; i >= -100000000000; i *= 10) {
                MultiPrecision<Pow2.N8> m = i;
                Console.WriteLine((double)m);
                Console.WriteLine((Int64)m);
            }

            Assert.ThrowsException<OverflowException>(() => {
                Int64 v = (Int64)(min_value - 1);
            });

            Assert.ThrowsException<OverflowException>(() => {
                Int64 v = (Int64)(max_value + 1);
            });
        }
    }
}
