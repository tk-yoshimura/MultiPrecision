using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Numerics;

namespace MultiPrecisionTest.UIntUtils {

    [TestClass]
    public partial class UintUtilTest {
        [TestMethod]
        public void MulULongTest() {
            UInt64[] vs = { 0, 1, 12, 123, 4567, 0xFFFFu, 0xFFFFFFFFu, 0xFFFFFFFFFFFFFFFFul };

            foreach (UInt64 v in vs) {
                foreach (UInt64 u in vs) {
                    BigUInt<Pow2.N4> x = new BigUInt<Pow2.N4>(UIntUtil.Mul(v, u));

                    BigInteger y = (BigInteger)v * (BigInteger)u;

                    Console.WriteLine($"{u} * {v} = {x}");
                    Console.WriteLine($"{u} * {v} = {y}");
                    Console.WriteLine($"{x.ToHexcode()}");

                    Assert.AreEqual(y, x);
                }
            }
        }
    }
}
