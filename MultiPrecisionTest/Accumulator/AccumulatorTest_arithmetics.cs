using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Accumulator {
    public partial class AccumulatorTest {

        [TestMethod]
        public void MulShiftTest() {
            Accumulator<Pow2.N4> v1 = new Accumulator<Pow2.N4>(Mantissa<Pow2.N4>.Full);
            Accumulator<Pow2.N4> v2 = new Accumulator<Pow2.N4>(Mantissa<Pow2.N4>.One);
            Accumulator<Pow2.N4> v3 = v1 * 5 / 9;

            Accumulator<Pow2.N4> v11 = Accumulator<Pow2.N4>.MulShift(v1, v1);
            Console.WriteLine(v11.ToHexcode());
            Assert.AreEqual(5, v11.Digits);

            Accumulator<Pow2.N4> v12 = Accumulator<Pow2.N4>.MulShift(v1, v2);
            Console.WriteLine(v12.ToHexcode());
            Assert.AreEqual(4, v12.Digits);
            Assert.AreEqual(v1, v12);

            Accumulator<Pow2.N4> v13 = Accumulator<Pow2.N4>.MulShift(v1, v3);
            Console.WriteLine(v13.ToHexcode());
            Assert.AreEqual(5, v13.Digits);

            Accumulator<Pow2.N4> v21 = Accumulator<Pow2.N4>.MulShift(v2, v1);
            Console.WriteLine(v21.ToHexcode());
            Assert.AreEqual(4, v21.Digits);
            Assert.AreEqual(v1, v21);

            Accumulator<Pow2.N4> v22 = Accumulator<Pow2.N4>.MulShift(v2, v2);
            Console.WriteLine(v22.ToHexcode());
            Assert.AreEqual(4, v22.Digits);
            Assert.AreEqual(v2, v22);

            Accumulator<Pow2.N4> v23 = Accumulator<Pow2.N4>.MulShift(v2, v3);
            Console.WriteLine(v23.ToHexcode());
            Assert.AreEqual(4, v23.Digits);
            Assert.AreEqual(v3, v23);

            Accumulator<Pow2.N4> v31 = Accumulator<Pow2.N4>.MulShift(v3, v1);
            Console.WriteLine(v31.ToHexcode());
            Assert.AreEqual(5, v31.Digits);

            Accumulator<Pow2.N4> v32 = Accumulator<Pow2.N4>.MulShift(v3, v2);
            Console.WriteLine(v32.ToHexcode());
            Assert.AreEqual(4, v32.Digits);
            Assert.AreEqual(v3, v32);

            Accumulator<Pow2.N4> v33 = Accumulator<Pow2.N4>.MulShift(v3, v3);
            Console.WriteLine(v33.ToHexcode());
            Assert.AreEqual(4, v33.Digits);
        }
    }
}
