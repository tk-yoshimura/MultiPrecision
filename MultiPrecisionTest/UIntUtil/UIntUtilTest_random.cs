using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;

namespace MultiPrecisionTest.UIntUtils {

    [TestClass]
    public partial class UIntUtilTest {
        [TestMethod]
        public void RandomTest() {
            const int length = 4;
            Random random = new Random();

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                UInt32[] vs = UIntUtil.Random(random, length, i);

                Console.WriteLine($"{i} : {UIntUtil.ToHexcode(vs)}");
            }
        }
    }
}
