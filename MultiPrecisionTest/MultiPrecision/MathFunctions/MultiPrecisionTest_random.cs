using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void RandomTest() {
            Random random = new Random(1234);

            MultiPrecision<Pow2.N8>[] vs = (new MultiPrecision<Pow2.N8>[1000]).Select((v) => MultiPrecision<Pow2.N8>.Random(random)).ToArray();

            foreach (var v in vs) {
                Console.WriteLine(v);
            }

            MultiPrecision<Pow2.N8> vs_sum = vs.Sum();
            MultiPrecision<Pow2.N8> vs_average = vs.Average();

            Console.WriteLine($"sum : {vs_sum}");
            Console.WriteLine($"average : {vs_average}");

            MultiPrecision<Pow2.N8>[] vs_sorted = vs.OrderBy((v) => v).ToArray();

            Console.WriteLine($"median : {vs_sorted[vs.Length / 2]}");

            MultiPrecision<Pow2.N8> vs_min = vs.Min();

            Console.WriteLine($"min : {vs_min}");

            MultiPrecision<Pow2.N8> vs_max = vs.Max();

            Console.WriteLine($"min : {vs_max}");

            Assert.IsTrue(vs_min >= 0);
            Assert.IsTrue(vs_max < 1);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEquals(vs_average, 0.5, 1e-2));
        }
    }
}
