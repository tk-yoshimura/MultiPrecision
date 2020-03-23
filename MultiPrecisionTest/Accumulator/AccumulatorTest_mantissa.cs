using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest.Accumulator {
    public partial class AccumulatorTest {

        [TestMethod]
        public void MantissaTest() {
            UInt32[] v0 = new UInt32[] { 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u };
            UInt32[] v1 = new UInt32[] { 0x3FFFFFFFu, ~0u, ~0u, ~0u, 0u, 0u, 0u, 0u };
            UInt32[] v2 = new UInt32[] { 0x7FFFFFFFu, ~0u, ~0u, ~0u, 0u, 0u, 0u, 0u };
            UInt32[] v3 = new UInt32[] { 0x7FFFFFFFu, ~0u, ~0u, ~0u, 0x7FFFFFFFu, 0u, 0u, 0u };
            UInt32[] v4 = new UInt32[] { 0x7FFFFFFFu, ~0u, ~0u, ~0u, 0x80000000u, 0u, 0u, 0u };
            UInt32[] v5 = new UInt32[] { ~0u, ~0u, ~0u, ~0u, 0u, 0u, 0u, 0u };
            UInt32[] v6 = new UInt32[] { ~0u, ~0u, ~0u, ~0u, 0x7FFFFFFFu, 0u, 0u, 0u };
            UInt32[] v7 = new UInt32[] { ~0u, ~0u, ~0u, ~0u, 0x80000000u, 0u, 0u, 0u };
            UInt32[] v8 = new UInt32[] { ~0u, ~0u, ~0u, ~0u, ~0u, 0u, 0u, 0u };
            UInt32[] v9 = new UInt32[]  { 0u, 0x3FFFFFFFu, ~0u, ~0u, ~0u, 0u, 0u, 0u };
            UInt32[] v10 = new UInt32[] { 0u, 0x7FFFFFFFu, ~0u, ~0u, ~0u, 0u, 0u, 0u };
            UInt32[] v11 = new UInt32[] { 0u, 0x7FFFFFFFu, ~0u, ~0u, ~0u, 0x7FFFFFFFu, 0u, 0u };
            UInt32[] v12 = new UInt32[] { 0u, 0x7FFFFFFFu, ~0u, ~0u, ~0u, 0x80000000u, 0u, 0u };
            UInt32[] v13 = new UInt32[] { 0u, ~0u, ~0u, ~0u, ~0u, 0u, 0u, 0u };
            UInt32[] v14 = new UInt32[] { 0u, ~0u, ~0u, ~0u, ~0u, 0x7FFFFFFFu, 0u, 0u };
            UInt32[] v15 = new UInt32[] { 0u, ~0u, ~0u, ~0u, ~0u, 0x80000000u, 0u, 0u };
            UInt32[] v16 = new UInt32[] { 0u, ~0u, ~0u, ~0u, ~0u, ~0u, 0u, 0u };

            Accumulator<Pow2.N4> acc0 = new Accumulator<Pow2.N4>(v0.Reverse().ToArray());
            Accumulator<Pow2.N4> acc1 = new Accumulator<Pow2.N4>(v1.Reverse().ToArray());
            Accumulator<Pow2.N4> acc2 = new Accumulator<Pow2.N4>(v2.Reverse().ToArray());
            Accumulator<Pow2.N4> acc3 = new Accumulator<Pow2.N4>(v3.Reverse().ToArray());
            Accumulator<Pow2.N4> acc4 = new Accumulator<Pow2.N4>(v4.Reverse().ToArray());
            Accumulator<Pow2.N4> acc5 = new Accumulator<Pow2.N4>(v5.Reverse().ToArray());
            Accumulator<Pow2.N4> acc6 = new Accumulator<Pow2.N4>(v6.Reverse().ToArray());
            Accumulator<Pow2.N4> acc7 = new Accumulator<Pow2.N4>(v7.Reverse().ToArray());
            Accumulator<Pow2.N4> acc8 = new Accumulator<Pow2.N4>(v8.Reverse().ToArray());
            Accumulator<Pow2.N4> acc9 = new Accumulator<Pow2.N4>(v9.Reverse().ToArray());
            Accumulator<Pow2.N4> acc10 = new Accumulator<Pow2.N4>(v10.Reverse().ToArray());
            Accumulator<Pow2.N4> acc11 = new Accumulator<Pow2.N4>(v11.Reverse().ToArray());
            Accumulator<Pow2.N4> acc12 = new Accumulator<Pow2.N4>(v12.Reverse().ToArray());
            Accumulator<Pow2.N4> acc13 = new Accumulator<Pow2.N4>(v13.Reverse().ToArray());
            Accumulator<Pow2.N4> acc14 = new Accumulator<Pow2.N4>(v14.Reverse().ToArray());
            Accumulator<Pow2.N4> acc15 = new Accumulator<Pow2.N4>(v15.Reverse().ToArray());
            Accumulator<Pow2.N4> acc16 = new Accumulator<Pow2.N4>(v16.Reverse().ToArray());

            (Mantissa<Pow2.N4> n0, int sft0) = acc0.Mantissa;
            (Mantissa<Pow2.N4> n1, int sft1) = acc1.Mantissa;
            (Mantissa<Pow2.N4> n2, int sft2) = acc2.Mantissa;
            (Mantissa<Pow2.N4> n3, int sft3) = acc3.Mantissa;
            (Mantissa<Pow2.N4> n4, int sft4) = acc4.Mantissa;
            (Mantissa<Pow2.N4> n5, int sft5) = acc5.Mantissa;
            (Mantissa<Pow2.N4> n6, int sft6) = acc6.Mantissa;
            (Mantissa<Pow2.N4> n7, int sft7) = acc7.Mantissa;
            (Mantissa<Pow2.N4> n8, int sft8) = acc8.Mantissa;
            (Mantissa<Pow2.N4> n9, int sft9) = acc9.Mantissa;
            (Mantissa<Pow2.N4> n10, int sft10) = acc10.Mantissa;
            (Mantissa<Pow2.N4> n11, int sft11) = acc11.Mantissa;
            (Mantissa<Pow2.N4> n12, int sft12) = acc12.Mantissa;
            (Mantissa<Pow2.N4> n13, int sft13) = acc13.Mantissa;
            (Mantissa<Pow2.N4> n14, int sft14) = acc14.Mantissa;
            (Mantissa<Pow2.N4> n15, int sft15) = acc15.Mantissa;
            (Mantissa<Pow2.N4> n16, int sft16) = acc16.Mantissa;

            CollectionAssert.AreEqual(new UInt32[]{ 0u, 0u, 0u, 0u }.Reverse().ToArray(), n0.Value);
            Assert.AreEqual(0, sft0);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u << 2 }.Reverse().ToArray(), n1.Value);
            Assert.AreEqual(2, sft1);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u << 1 }.Reverse().ToArray(), n2.Value);
            Assert.AreEqual(1, sft2);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, (~0u << 1) + 1 }.Reverse().ToArray(), n3.Value);
            Assert.AreEqual(1, sft3);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u }.Reverse().ToArray(), n4.Value);
            Assert.AreEqual(1, sft4);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u }.Reverse().ToArray(), n5.Value);
            Assert.AreEqual(0, sft5);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u }.Reverse().ToArray(), n6.Value);
            Assert.AreEqual(0, sft6);

            CollectionAssert.AreEqual(new UInt32[]{ 0x80000000u, 0u, 0u, 0u }.Reverse().ToArray(), n7.Value);
            Assert.AreEqual(-1, sft7);

            CollectionAssert.AreEqual(new UInt32[]{ 0x80000000u, 0u, 0u, 0u }.Reverse().ToArray(), n8.Value);
            Assert.AreEqual(-1, sft8);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u << 2 }.Reverse().ToArray(), n9.Value);
            Assert.AreEqual(34, sft9);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u << 1 }.Reverse().ToArray(), n10.Value);
            Assert.AreEqual(33, sft10);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, (~0u << 1) + 1 }.Reverse().ToArray(), n11.Value);
            Assert.AreEqual(33, sft11);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u }.Reverse().ToArray(), n12.Value);
            Assert.AreEqual(33, sft12);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u }.Reverse().ToArray(), n13.Value);
            Assert.AreEqual(32, sft13);

            CollectionAssert.AreEqual(new UInt32[]{ ~0u, ~0u, ~0u, ~0u }.Reverse().ToArray(), n14.Value);
            Assert.AreEqual(32, sft14);

            CollectionAssert.AreEqual(new UInt32[]{ 0x80000000u, 0u, 0u, 0u }.Reverse().ToArray(), n15.Value);
            Assert.AreEqual(31, sft15);

            CollectionAssert.AreEqual(new UInt32[]{ 0x80000000u, 0u, 0u, 0u }.Reverse().ToArray(), n16.Value);
            Assert.AreEqual(31, sft16);
        }
    }
}
