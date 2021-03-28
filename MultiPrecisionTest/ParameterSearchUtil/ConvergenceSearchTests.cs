using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecision.ParameterSearchUtil;
using System;

namespace MultiPrecisionTest.ParameterSearchUtil {
    [TestClass()]
    public class ConvergenceSearchTests {
        [TestMethod()]
        public void ConvergenceSearchTest1() {
            MultiPrecision<Pow2.N4> ramp(long x, long a) => Math.Min(0, x - a);

            for (long a = -50; a <= 450; a++) {
                Console.WriteLine($"a = {a}");

                ConvergenceSearch<Pow2.N4> search = new ConvergenceSearch<Pow2.N4>((100, 200), (0, 400));

                while (!search.IsSearched) {
                    foreach (long sample_point in search.SampleRequests) {
                        MultiPrecision<Pow2.N4> sample = ramp(sample_point, a);

                        search.PushSampleResult(sample_point, sample);

                        Console.WriteLine($"sample {sample_point}, {sample}");
                    }

                    Console.WriteLine($"step {search.Step}, convpoint {search.ConvergencePoint}\n");
                    search.ReflashSampleRequests();
                }

                Assert.AreEqual(1, search.Step);
                Assert.AreEqual(Math.Min(400, Math.Max(0, a)), search.ConvergencePoint);
            }
        }

        [TestMethod()]
        public void ConvergenceSearchTest2() {
            MultiPrecision<Pow2.N4> ramp(long x, long a) => Math.Max(0, x - a);

            for (long a = -50; a <= 450; a++) {
                Console.WriteLine($"a = {a}");

                ConvergenceSearch<Pow2.N4> search = new ConvergenceSearch<Pow2.N4>((100, 200), (0, 400), larger_convergence: false);

                while (!search.IsSearched) {
                    foreach (long sample_point in search.SampleRequests) {
                        MultiPrecision<Pow2.N4> sample = ramp(sample_point, a);

                        search.PushSampleResult(sample_point, sample);

                        Console.WriteLine($"sample {sample_point}, {sample}");
                    }

                    Console.WriteLine($"step {search.Step}, convpoint {search.ConvergencePoint}\n");
                    search.ReflashSampleRequests();
                }

                Assert.AreEqual(1, search.Step);
                Assert.AreEqual(Math.Min(400, Math.Max(0, a)), search.ConvergencePoint);
            }
        }
    }
}