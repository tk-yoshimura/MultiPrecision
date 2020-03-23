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
            
            foreach(var v in vs) { 
                Console.WriteLine(v);
            }

            MultiPrecision<Pow2.N8> vs_sum = vs.Sum();
            MultiPrecision<Pow2.N8> vs_average = vs.Average();

            Console.WriteLine($"sum : {vs_sum}");
            Console.WriteLine($"average : {vs_average}");
        }
    }
}
