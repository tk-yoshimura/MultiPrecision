using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.UIntUtils {

    [TestClass]
    public partial class UIntUtilTest {
        [TestMethod]
        public void RandomTest() {
            const int length = 4;
            Random random = new();

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                UInt32[] vs = UIntUtil.Random(random, length, i);

                Console.WriteLine($"{i} : {UIntUtil.ToHexcode(vs)}");
            }
        }
    }
}
