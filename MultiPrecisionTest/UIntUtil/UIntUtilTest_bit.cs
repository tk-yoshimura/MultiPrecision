using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest.UIntUtils {

    [TestClass]
    public partial class UIntUtilTest {
        [TestMethod]
        public void BitTest() {
            const int length = 4;

            UInt32[] vs = new UInt32[length];

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                Assert.AreEqual(0u, UIntUtil.GetMSB(vs, i));

                UIntUtil.SetMSB(vs, i);

                Assert.AreEqual(1u, UIntUtil.GetMSB(vs, i));

                Console.WriteLine(string.Join(' ', vs.Reverse().Select((v) => $"{v:X8}")));
            }

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                Assert.AreEqual(1u, UIntUtil.GetMSB(vs, i));

                UIntUtil.ResetMSB(vs, i);

                Assert.AreEqual(0u, UIntUtil.GetMSB(vs, i));

                Console.WriteLine(string.Join(' ', vs.Reverse().Select((v) => $"{v:X8}")));
            }

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                UInt32[] us = Enumerable.Repeat(0xFFFFFFFFu, length).ToArray();

                UIntUtil.FlushMSB(us, i);

                Console.WriteLine(string.Join(' ', us.Reverse().Select((u) => $"{u:X8}")));
            }

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                Assert.AreEqual(0u, UIntUtil.GetLSB(vs, i));

                UIntUtil.SetLSB(vs, i);

                Assert.AreEqual(1u, UIntUtil.GetLSB(vs, i));

                Console.WriteLine(string.Join(' ', vs.Reverse().Select((v) => $"{v:X8}")));
            }

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                Assert.AreEqual(1u, UIntUtil.GetLSB(vs, i));

                UIntUtil.ResetLSB(vs, i);

                Assert.AreEqual(0u, UIntUtil.GetLSB(vs, i));

                Console.WriteLine(string.Join(' ', vs.Reverse().Select((v) => $"{v:X8}")));
            }

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                UInt32[] us = Enumerable.Repeat(0xFFFFFFFFu, length).ToArray();

                UIntUtil.FlushLSB(us, i);

                Console.WriteLine(string.Join(' ', us.Reverse().Select((u) => $"{u:X8}")));
            }
        }

        [TestMethod]
        public void RandomTest() {
            const int length = 4;
            Random random = new Random();

            for (int i = 0; i < length * UIntUtil.UInt32Bits; i++) {
                UInt32[] vs = UIntUtil.Random(random, length, i);

                Console.WriteLine($"{i} : {string.Join(' ', vs.Reverse().Select((v) => $"{v:X8}"))}");
            }
        }
    }
}
