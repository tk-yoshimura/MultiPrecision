using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecision.ParameterSearchUtil;
using System;

namespace MultiPrecisionTest.ParameterSearchUtil {
    [TestClass()]
    public class MinSearchTests {
        [TestMethod()]
        public void MinSearchTest() {
            MultiPrecision<Pow2.N4> curve(long x, long a) => (x - a) * (x - a);

            for (long a = -50; a <= 450; a++) {
                Console.WriteLine($"a = {a}");

                MinSearch<Pow2.N4> search = new((100, 200), (0, 400));

                while (!search.IsSearched) {
                    foreach (long sample_point in search.SampleRequests) {
                        MultiPrecision<Pow2.N4> sample = curve(sample_point, a);

                        search.PushSampleResult(sample_point, sample);

                        Console.WriteLine($"sample {sample_point}, {sample}");
                    }

                    Console.WriteLine($"step {search.Step}, minpoint {search.MinPoint}\n");
                    search.ReflashSampleRequests();
                }

                Assert.AreEqual(1, search.Step);
                Assert.AreEqual(Math.Min(400, Math.Max(0, a)), search.MinPoint);
                Assert.AreEqual(a > 0 && a < 400, search.IsConvergenced);
            }
        }
    }
}