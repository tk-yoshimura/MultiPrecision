using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest.Mantissa {
    [TestClass]
    public partial class MantissaTest {
        [TestMethod]
        public void CreateTest() {
            int length = Mantissa<Pow2.N4>.Length;

            UInt32[] vs = new UInt32[] { 
                0x11111111u, 0x22222222u, 0x33333333u, 0x44444444u, 0x55555555u, 0x66666666u, 0x77777777u, 0x88888888u,
                0x99999999u, 0xAAAAAAAAu, 0xBBBBBBBBu, 0xCCCCCCCCu, 0xDDDDDDDDu, 0xEEEEEEEEu, 0xFFFFFFFFu, 0u,
            };
            UInt32[] us = vs.Reverse().ToArray();

            Accumulator<Pow2.N4> acc1 = new Accumulator<Pow2.N4>(vs.Take(length * 2).ToArray());
            Accumulator<Pow2.N4> acc2 = new Accumulator<Pow2.N4>(vs.Skip(length).Take(length * 2).ToArray());
            Accumulator<Pow2.N4> acc3 = new Accumulator<Pow2.N4>(vs.Skip(length * 2).Take(length * 2).ToArray());
            Accumulator<Pow2.N4> acc4 = new Accumulator<Pow2.N4>(us.Take(length * 2).ToArray());
            Accumulator<Pow2.N4> acc5 = new Accumulator<Pow2.N4>(us.Skip(length).Take(length * 2).ToArray());
            Accumulator<Pow2.N4> acc6 = new Accumulator<Pow2.N4>(us.Skip(length * 2).Take(length * 2).ToArray());

            Mantissa<Pow2.N4> n1 = new Mantissa<Pow2.N4>(acc1);
            Mantissa<Pow2.N4> n2 = new Mantissa<Pow2.N4>(acc2);
            Mantissa<Pow2.N4> n3 = new Mantissa<Pow2.N4>(acc3);
            Mantissa<Pow2.N4> n4 = new Mantissa<Pow2.N4>(acc4);
            Mantissa<Pow2.N4> n5 = new Mantissa<Pow2.N4>(acc5);
            Mantissa<Pow2.N4> n6 = new Mantissa<Pow2.N4>(acc6);

            CollectionAssert.AreEqual(new UInt32[]{ 0x55555555u, 0x66666666u, 0x77777777u, 0x88888888u }, n1.Value);
            CollectionAssert.AreEqual(new UInt32[]{ 0x9999999Au, 0xAAAAAAAAu, 0xBBBBBBBBu, 0xCCCCCCCCu }, n2.Value);
            CollectionAssert.AreEqual(new UInt32[]{ 0xDDDDDDDEu, 0xEEEEEEEEu, 0xFFFFFFFFu, 0u }, n3.Value);
            CollectionAssert.AreEqual(new UInt32[]{ 0xCCCCCCCDu, 0xBBBBBBBBu, 0xAAAAAAAAu, 0x99999999u }, n4.Value);
            CollectionAssert.AreEqual(new UInt32[]{ 0x88888889u, 0x77777777u, 0x66666666u, 0x55555555u }, n5.Value);
            CollectionAssert.AreEqual(new UInt32[]{ 0x44444444u, 0x33333333u, 0x22222222u, 0x11111111u }, n6.Value);
        }
    }
}
