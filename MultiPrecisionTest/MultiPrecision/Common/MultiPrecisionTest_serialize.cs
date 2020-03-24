using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void SerializeTest() {
            const string filename_bin = "mp_serialtest.bin";

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

            using (FileStream stream = new FileStream(filename_bin, FileMode.Create, FileAccess.Write)) {
                IFormatter formatter = new BinaryFormatter();

                foreach (MultiPrecision<Pow2.N8> v in vs) {
                    formatter.Serialize(stream, v);
                }
            }

            using (FileStream stream = new FileStream(filename_bin, FileMode.Open, FileAccess.Read)) {
                IFormatter formatter = new BinaryFormatter();

                for (int i = 0; i < vs.Length; i++) {
                    MultiPrecision<Pow2.N8> u = (MultiPrecision<Pow2.N8>)formatter.Deserialize(stream);

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
