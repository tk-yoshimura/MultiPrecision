using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void LogTest() {
            foreach(MultiPrecision<Pow2.N8> x in TestTool.PositiveRangeSet<Pow2.N8>()) { 

                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Log(x);

                Console.WriteLine(x);
                Console.WriteLine(y);
                
                TestTool.Tolerance(Math.Log((double)x), y);
            }
        }
    }
}
