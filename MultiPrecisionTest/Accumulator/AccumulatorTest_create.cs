using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;

namespace MultiPrecisionTest.Accumulator {
    [TestClass]
    public partial class AccumulatorTest {
        [TestMethod]
        public void CreateTest() {
            Mantissa<Pow2.N4> n = new Mantissa<Pow2.N4>(new UInt32[] { 0x12345678u, 0xABCDEF01u, 0x97531ABCu, 0xAE012458u }, enable_clone: false);

            Accumulator<Pow2.N4> acc0 = new Accumulator<Pow2.N4>(n);
            Accumulator<Pow2.N4> acc1 = new Accumulator<Pow2.N4>(n, 4);
            Accumulator<Pow2.N4> acc2 = new Accumulator<Pow2.N4>(n, 8);
            Accumulator<Pow2.N4> acc3 = new Accumulator<Pow2.N4>(n, 32);
            Accumulator<Pow2.N4> acc4 = new Accumulator<Pow2.N4>(n, -4);
            Accumulator<Pow2.N4> acc5 = new Accumulator<Pow2.N4>(n, -8);
            Accumulator<Pow2.N4> acc6 = new Accumulator<Pow2.N4>(n, -32);

            CollectionAssert.AreEqual(new UInt32[] { 0x12345678u, 0xABCDEF01u, 0x97531ABCu, 0xAE012458u, 0u, 0u, 0u, 0u }, acc0.Value.ToList());
            CollectionAssert.AreEqual(new UInt32[] { 0x23456780u, 0xBCDEF011u, 0x7531ABCAu, 0xE0124589u, 0xAu, 0u, 0u, 0u }, acc1.Value.ToList());
            CollectionAssert.AreEqual(new UInt32[] { 0x34567800u, 0xCDEF0112u, 0x531ABCABu, 0x01245897u, 0xAEu, 0u, 0u, 0u }, acc2.Value.ToList());
            CollectionAssert.AreEqual(new UInt32[] { 0u, 0x12345678u, 0xABCDEF01u, 0x97531ABCu, 0xAE012458u, 0u, 0u, 0u }, acc3.Value.ToList());
            CollectionAssert.AreEqual(new UInt32[] { 0x11234567u, 0xCABCDEF0u, 0x897531ABu, 0x0AE01245u, 0u, 0u, 0u, 0u }, acc4.Value.ToList());
            CollectionAssert.AreEqual(new UInt32[] { 0x01123456u, 0xBCABCDEFu, 0x5897531Au, 0x00AE0124u, 0u, 0u, 0u, 0u }, acc5.Value.ToList());
            CollectionAssert.AreEqual(new UInt32[] { 0xABCDEF01u, 0x97531ABCu, 0xAE012458u, 0u, 0u, 0u, 0u, 0u }, acc6.Value.ToList());
        }
    }
}
