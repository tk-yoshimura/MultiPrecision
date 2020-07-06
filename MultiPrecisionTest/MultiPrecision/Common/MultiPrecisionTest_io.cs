using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System.Collections.Generic;
using System.IO;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void IOTest() {
            const string filename_bin = "mp_iotest.bin";

            MultiPrecision<Pow2.N8>[] vs = {
                MultiPrecision<Pow2.N8>.Zero,
                MultiPrecision<Pow2.N8>.MinusZero,
                1, 2, 3, 4, 5, 7, 10, 11, 13, 100, 1000,
                -1, -2, -3, -4, -5, -7, -10, -11, -13, -100, -1000,
                MultiPrecision<Pow2.N8>.One / 2,
                MultiPrecision<Pow2.N8>.One / 3,
                MultiPrecision<Pow2.N8>.One / 4,
                MultiPrecision<Pow2.N8>.One / 5,
                MultiPrecision<Pow2.N8>.One / 7,
                MultiPrecision<Pow2.N8>.One / 10,
                MultiPrecision<Pow2.N8>.One / 11,
                MultiPrecision<Pow2.N8>.One / 13,
                MultiPrecision<Pow2.N8>.One / 100,
                MultiPrecision<Pow2.N8>.One / 1000,
                MultiPrecision<Pow2.N8>.MinusOne / 2,
                MultiPrecision<Pow2.N8>.MinusOne / 3,
                MultiPrecision<Pow2.N8>.MinusOne / 4,
                MultiPrecision<Pow2.N8>.MinusOne / 5,
                MultiPrecision<Pow2.N8>.MinusOne / 7,
                MultiPrecision<Pow2.N8>.MinusOne / 10,
                MultiPrecision<Pow2.N8>.MinusOne / 11,
                MultiPrecision<Pow2.N8>.MinusOne / 13,
                MultiPrecision<Pow2.N8>.MinusOne / 100,
                MultiPrecision<Pow2.N8>.MinusOne / 1000,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
                MultiPrecision<Pow2.N8>.NaN,
            };

            List<MultiPrecision<Pow2.N8>> us = new List<MultiPrecision<Pow2.N8>>();

            using (BinaryWriter stream = new BinaryWriter(File.OpenWrite(filename_bin))) {
                foreach (MultiPrecision<Pow2.N8> v in vs) {
                    stream.Write(v);
                }
            }

            using (BinaryReader stream = new BinaryReader(File.OpenRead(filename_bin))) {
                for (int i = 0; i < vs.Length; i++) {
                    MultiPrecision<Pow2.N8> u = stream.ReadMultiPrecision<Pow2.N8>();

                    us.Add(u);
                }
            }

            for (int i = 0; i < vs.Length; i++) {
                Assert.AreEqual(vs[i], us[i]);
            }

            File.Delete(filename_bin);
        }
    }
}
