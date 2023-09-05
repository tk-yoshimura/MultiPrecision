using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Common {

    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ParseTest() {
            /* common */
            {
                for (decimal i = 0, v = 1; i >= -28; i--, v /= 2) {
                    MultiPrecision<Pow2.N4> m = v.ToString();
                    Assert.AreEqual(i, m.Exponent);
                    CollectionAssert.AreEqual(Mantissa<Pow2.N4>.One.Value, m.Mantissa);
                }

                for (decimal i = 0, v = 1; i >= -28; i--, v /= 2) {
                    MultiPrecision<Pow2.N8> m = v.ToString();
                    Assert.AreEqual(i, m.Exponent);
                    CollectionAssert.AreEqual(Mantissa<Pow2.N8>.One.Value, m.Mantissa);
                }

                for (decimal i = 0, v = 1; i >= -28; i--, v /= 2) {
                    MultiPrecision<Pow2.N16> m = v.ToString();
                    Assert.AreEqual(i, m.Exponent);
                    CollectionAssert.AreEqual(Mantissa<Pow2.N16>.One.Value, m.Mantissa);
                }

                for (decimal i = 0, v = 1; i <= 28; i++, v *= 2) {
                    MultiPrecision<Pow2.N4> m = v.ToString();
                    Assert.AreEqual(i, m.Exponent);
                    CollectionAssert.AreEqual(Mantissa<Pow2.N4>.One.Value, m.Mantissa);
                }

                for (decimal i = 0, v = 1; i <= 28; i++, v *= 2) {
                    MultiPrecision<Pow2.N8> m = v.ToString();
                    Assert.AreEqual(i, m.Exponent);
                    CollectionAssert.AreEqual(Mantissa<Pow2.N8>.One.Value, m.Mantissa);
                }

                for (decimal i = 0, v = 1; i <= 28; i++, v *= 2) {
                    MultiPrecision<Pow2.N16> m = v.ToString();
                    Assert.AreEqual(i, m.Exponent);
                    CollectionAssert.AreEqual(Mantissa<Pow2.N16>.One.Value, m.Mantissa);
                }
            }

            for (int i = 1; i <= 10000; i++) {
                MultiPrecision<Pow2.N4> v1 = $"2.56e{i}";
                Assert.AreEqual($"2.56e{i}", $"{v1:e2}");
            }

            for (int i = 1; i <= 10000; i++) {
                MultiPrecision<Pow2.N4> v1 = $"2.56e-{i}";
                Assert.AreEqual($"2.56e-{i}", $"{v1:e2}");
            }

            for (int i = 1; i <= 10000; i++) {
                MultiPrecision<Pow2.N8> v1 = $"2.56e{i}";
                Assert.AreEqual($"2.56e{i}", $"{v1:e2}");
            }

            for (int i = 1; i <= 10000; i++) {
                MultiPrecision<Pow2.N8> v1 = $"2.56e-{i}";
                Assert.AreEqual($"2.56e-{i}", $"{v1:e2}");
            }

            for (int i = 1; i <= 10000; i++) {
                MultiPrecision<Pow2.N16> v1 = $"2.56e{i}";
                Assert.AreEqual($"2.56e{i}", $"{v1:e2}");
            }

            for (int i = 1; i <= 10000; i++) {
                MultiPrecision<Pow2.N16> v1 = $"2.56e-{i}";
                Assert.AreEqual($"2.56e-{i}", $"{v1:e2}");
            }
        }

        [TestMethod]
        public void FormatParseTest() {
            /* without sign */
            {
                /* without exp */
                {
                    MultiPrecision<Pow2.N8> v1 = "2.56";
                    Assert.AreEqual(2.56, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "02.56";
                    Assert.AreEqual(2.56, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "2.560";
                    Assert.AreEqual(2.56, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "02.560";
                    Assert.AreEqual(2.56, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "25.6";
                    Assert.AreEqual(25.6, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "256";
                    Assert.AreEqual(256, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "2560";
                    Assert.AreEqual(2560, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "02560";
                    Assert.AreEqual(2560, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "256" + new string('0', 10);
                    Assert.AreEqual(2.56e12, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "256." + new string('0', 10);
                    Assert.AreEqual(256, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "256" + new string('0', 1000);
                    Assert.AreEqual("2.56e1002", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "256." + new string('0', 1000);
                    Assert.AreEqual(256, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = new string('0', 10) + "256";
                    Assert.AreEqual(256, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = new string('0', 1000) + "256";
                    Assert.AreEqual(256, (double)v14);
                }

                /* with e10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "2.56e10";
                    Assert.AreEqual(2.56e10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "02.56e10";
                    Assert.AreEqual(2.56e10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "2.560e10";
                    Assert.AreEqual(2.56e10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "02.560e10";
                    Assert.AreEqual(2.56e10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "25.6e10";
                    Assert.AreEqual(2.56e11, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "256e10";
                    Assert.AreEqual(2.56e12, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "2560e10";
                    Assert.AreEqual(2.56e13, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "02560e10";
                    Assert.AreEqual(2.56e13, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "256" + new string('0', 10) + "e10";
                    Assert.AreEqual(2.56e22, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "256." + new string('0', 10) + "e10";
                    Assert.AreEqual(2.56e12, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "256" + new string('0', 1000) + "e10";
                    Assert.AreEqual("2.56e1012", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "256." + new string('0', 1000) + "e10";
                    Assert.AreEqual(2.56e12, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = new string('0', 10) + "256e10";
                    Assert.AreEqual(2.56e12, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = new string('0', 1000) + "256e10";
                    Assert.AreEqual(2.56e12, (double)v14);
                }

                /* with e+10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "2.56e+10";
                    Assert.AreEqual(2.56e10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "02.56e+10";
                    Assert.AreEqual(2.56e10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "2.560e+10";
                    Assert.AreEqual(2.56e10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "02.560e+10";
                    Assert.AreEqual(2.56e10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "25.6e+10";
                    Assert.AreEqual(2.56e11, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "256e+10";
                    Assert.AreEqual(2.56e12, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "2560e+10";
                    Assert.AreEqual(2.56e13, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "02560e+10";
                    Assert.AreEqual(2.56e13, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "256" + new string('0', 10) + "e+10";
                    Assert.AreEqual(2.56e22, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "256." + new string('0', 10) + "e+10";
                    Assert.AreEqual(2.56e12, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "256" + new string('0', 1000) + "e+10";
                    Assert.AreEqual("2.56e1012", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "256." + new string('0', 1000) + "e+10";
                    Assert.AreEqual(2.56e12, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = new string('0', 10) + "256e+10";
                    Assert.AreEqual(2.56e12, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = new string('0', 1000) + "256e+10";
                    Assert.AreEqual(2.56e12, (double)v14);
                }

                /* with e-10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "2.56e-10";
                    Assert.AreEqual(2.56e-10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "02.56e-10";
                    Assert.AreEqual(2.56e-10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "2.560e-10";
                    Assert.AreEqual(2.56e-10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "02.560e-10";
                    Assert.AreEqual(2.56e-10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "25.6e-10";
                    Assert.AreEqual(2.56e-9, (double)v5, 2.56e-20);

                    MultiPrecision<Pow2.N8> v6 = "256e-10";
                    Assert.AreEqual(2.56e-8, (double)v6, 2.56e-20);

                    MultiPrecision<Pow2.N8> v7 = "2560e-10";
                    Assert.AreEqual(2.56e-7, (double)v7, 2.56e-20);

                    MultiPrecision<Pow2.N8> v8 = "02560e-10";
                    Assert.AreEqual(2.56e-7, (double)v8, 2.56e-20);

                    MultiPrecision<Pow2.N8> v9 = "256" + new string('0', 10) + "e-10";
                    Assert.AreEqual(2.56e2, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "256." + new string('0', 10) + "e-10";
                    Assert.AreEqual(2.56e-8, (double)v10, 2.56e-20);

                    MultiPrecision<Pow2.N8> v11 = "256" + new string('0', 1000) + "e-10";
                    Assert.AreEqual("2.56e992", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "256." + new string('0', 1000) + "e-10";
                    Assert.AreEqual(2.56e-8, (double)v12, 2.56e-20);

                    MultiPrecision<Pow2.N8> v13 = new string('0', 10) + "256e-10";
                    Assert.AreEqual(2.56e-8, (double)v13, 2.56e-20);

                    MultiPrecision<Pow2.N8> v14 = new string('0', 1000) + "256e-10";
                    Assert.AreEqual(2.56e-8, (double)v14, 2.56e-20);
                }
            }

            /* plus sign */
            {
                /* without exp */
                {
                    MultiPrecision<Pow2.N8> v1 = "+2.56";
                    Assert.AreEqual(2.56, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "+02.56";
                    Assert.AreEqual(2.56, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "+2.560";
                    Assert.AreEqual(2.56, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "+02.560";
                    Assert.AreEqual(2.56, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "+25.6";
                    Assert.AreEqual(25.6, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "+256";
                    Assert.AreEqual(256, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "+2560";
                    Assert.AreEqual(2560, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "+02560";
                    Assert.AreEqual(2560, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "+256" + new string('0', 10);
                    Assert.AreEqual(2.56e12, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "+256." + new string('0', 10);
                    Assert.AreEqual(256, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "+256" + new string('0', 1000);
                    Assert.AreEqual("2.56e1002", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "+256." + new string('0', 1000);
                    Assert.AreEqual(256, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = "+" + new string('0', 10) + "256";
                    Assert.AreEqual(256, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = "+" + new string('0', 1000) + "256";
                    Assert.AreEqual(256, (double)v14);
                }

                /* with e10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "+2.56e10";
                    Assert.AreEqual(2.56e10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "+02.56e10";
                    Assert.AreEqual(2.56e10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "+2.560e10";
                    Assert.AreEqual(2.56e10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "+02.560e10";
                    Assert.AreEqual(2.56e10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "+25.6e10";
                    Assert.AreEqual(2.56e11, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "+256e10";
                    Assert.AreEqual(2.56e12, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "+2560e10";
                    Assert.AreEqual(2.56e13, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "+02560e10";
                    Assert.AreEqual(2.56e13, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "+256" + new string('0', 10) + "e10";
                    Assert.AreEqual(2.56e22, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "+256." + new string('0', 10) + "e10";
                    Assert.AreEqual(2.56e12, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "+256" + new string('0', 1000) + "e10";
                    Assert.AreEqual("2.56e1012", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "+256." + new string('0', 1000) + "e10";
                    Assert.AreEqual(2.56e12, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = "+" + new string('0', 10) + "256e10";
                    Assert.AreEqual(2.56e12, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = "+" + new string('0', 1000) + "256e10";
                    Assert.AreEqual(2.56e12, (double)v14);
                }

                /* with e+10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "+2.56e+10";
                    Assert.AreEqual(2.56e10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "+02.56e+10";
                    Assert.AreEqual(2.56e10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "+2.560e+10";
                    Assert.AreEqual(2.56e10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "+02.560e+10";
                    Assert.AreEqual(2.56e10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "+25.6e+10";
                    Assert.AreEqual(2.56e11, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "+256e+10";
                    Assert.AreEqual(2.56e12, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "+2560e+10";
                    Assert.AreEqual(2.56e13, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "+02560e+10";
                    Assert.AreEqual(2.56e13, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "+256" + new string('0', 10) + "e+10";
                    Assert.AreEqual(2.56e22, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "+256." + new string('0', 10) + "e+10";
                    Assert.AreEqual(2.56e12, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "+256" + new string('0', 1000) + "e+10";
                    Assert.AreEqual("2.56e1012", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "+256." + new string('0', 1000) + "e+10";
                    Assert.AreEqual(2.56e12, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = "+" + new string('0', 10) + "256e+10";
                    Assert.AreEqual(2.56e12, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = "+" + new string('0', 1000) + "256e+10";
                    Assert.AreEqual(2.56e12, (double)v14);
                }

                /* with e-10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "+2.56e-10";
                    Assert.AreEqual(2.56e-10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "+02.56e-10";
                    Assert.AreEqual(2.56e-10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "+2.560e-10";
                    Assert.AreEqual(2.56e-10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "+02.560e-10";
                    Assert.AreEqual(2.56e-10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "+25.6e-10";
                    Assert.AreEqual(2.56e-9, (double)v5, 2.56e-20);

                    MultiPrecision<Pow2.N8> v6 = "+256e-10";
                    Assert.AreEqual(2.56e-8, (double)v6, 2.56e-20);

                    MultiPrecision<Pow2.N8> v7 = "+2560e-10";
                    Assert.AreEqual(2.56e-7, (double)v7, 2.56e-20);

                    MultiPrecision<Pow2.N8> v8 = "+02560e-10";
                    Assert.AreEqual(2.56e-7, (double)v8, 2.56e-20);

                    MultiPrecision<Pow2.N8> v9 = "+256" + new string('0', 10) + "e-10";
                    Assert.AreEqual(2.56e2, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "+256." + new string('0', 10) + "e-10";
                    Assert.AreEqual(2.56e-8, (double)v10, 2.56e-20);

                    MultiPrecision<Pow2.N8> v11 = "+256" + new string('0', 1000) + "e-10";
                    Assert.AreEqual("2.56e992", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "+256." + new string('0', 1000) + "e-10";
                    Assert.AreEqual(2.56e-8, (double)v12, 2.56e-20);

                    MultiPrecision<Pow2.N8> v13 = "+" + new string('0', 10) + "256e-10";
                    Assert.AreEqual(2.56e-8, (double)v13, 2.56e-20);

                    MultiPrecision<Pow2.N8> v14 = "+" + new string('0', 1000) + "256e-10";
                    Assert.AreEqual(2.56e-8, (double)v14, 2.56e-20);
                }
            }

            /* plus sign */
            {
                /* without exp */
                {
                    MultiPrecision<Pow2.N8> v1 = "-2.56";
                    Assert.AreEqual(-2.56, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "-02.56";
                    Assert.AreEqual(-2.56, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "-2.560";
                    Assert.AreEqual(-2.56, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "-02.560";
                    Assert.AreEqual(-2.56, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "-25.6";
                    Assert.AreEqual(-25.6, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "-256";
                    Assert.AreEqual(-256, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "-2560";
                    Assert.AreEqual(-2560, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "-02560";
                    Assert.AreEqual(-2560, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "-256" + new string('0', 10);
                    Assert.AreEqual(-2.56e12, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "-256." + new string('0', 10);
                    Assert.AreEqual(-256, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "-256" + new string('0', 1000);
                    Assert.AreEqual("-2.56e1002", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "-256." + new string('0', 1000);
                    Assert.AreEqual(-256, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = "-" + new string('0', 10) + "256";
                    Assert.AreEqual(-256, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = "-" + new string('0', 1000) + "256";
                    Assert.AreEqual(-256, (double)v14);
                }

                /* with e10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "-2.56e10";
                    Assert.AreEqual(-2.56e10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "-02.56e10";
                    Assert.AreEqual(-2.56e10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "-2.560e10";
                    Assert.AreEqual(-2.56e10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "-02.560e10";
                    Assert.AreEqual(-2.56e10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "-25.6e10";
                    Assert.AreEqual(-2.56e11, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "-256e10";
                    Assert.AreEqual(-2.56e12, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "-2560e10";
                    Assert.AreEqual(-2.56e13, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "-02560e10";
                    Assert.AreEqual(-2.56e13, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "-256" + new string('0', 10) + "e10";
                    Assert.AreEqual(-2.56e22, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "-256." + new string('0', 10) + "e10";
                    Assert.AreEqual(-2.56e12, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "-256" + new string('0', 1000) + "e10";
                    Assert.AreEqual("-2.56e1012", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "-256." + new string('0', 1000) + "e10";
                    Assert.AreEqual(-2.56e12, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = "-" + new string('0', 10) + "256e10";
                    Assert.AreEqual(-2.56e12, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = "-" + new string('0', 1000) + "256e10";
                    Assert.AreEqual(-2.56e12, (double)v14);
                }

                /* with e+10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "-2.56e+10";
                    Assert.AreEqual(-2.56e10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "-02.56e+10";
                    Assert.AreEqual(-2.56e10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "-2.560e+10";
                    Assert.AreEqual(-2.56e10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "-02.560e+10";
                    Assert.AreEqual(-2.56e10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "-25.6e+10";
                    Assert.AreEqual(-2.56e11, (double)v5);

                    MultiPrecision<Pow2.N8> v6 = "-256e+10";
                    Assert.AreEqual(-2.56e12, (double)v6);

                    MultiPrecision<Pow2.N8> v7 = "-2560e+10";
                    Assert.AreEqual(-2.56e13, (double)v7);

                    MultiPrecision<Pow2.N8> v8 = "-02560e+10";
                    Assert.AreEqual(-2.56e13, (double)v8);

                    MultiPrecision<Pow2.N8> v9 = "-256" + new string('0', 10) + "e+10";
                    Assert.AreEqual(-2.56e22, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "-256." + new string('0', 10) + "e+10";
                    Assert.AreEqual(-2.56e12, (double)v10);

                    MultiPrecision<Pow2.N8> v11 = "-256" + new string('0', 1000) + "e+10";
                    Assert.AreEqual("-2.56e1012", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "-256." + new string('0', 1000) + "e+10";
                    Assert.AreEqual(-2.56e12, (double)v12);

                    MultiPrecision<Pow2.N8> v13 = "-" + new string('0', 10) + "256e+10";
                    Assert.AreEqual(-2.56e12, (double)v13);

                    MultiPrecision<Pow2.N8> v14 = "-" + new string('0', 1000) + "256e+10";
                    Assert.AreEqual(-2.56e12, (double)v14);
                }

                /* with e-10 */
                {
                    MultiPrecision<Pow2.N8> v1 = "-2.56e-10";
                    Assert.AreEqual(-2.56e-10, (double)v1);

                    MultiPrecision<Pow2.N8> v2 = "-02.56e-10";
                    Assert.AreEqual(-2.56e-10, (double)v2);

                    MultiPrecision<Pow2.N8> v3 = "-2.560e-10";
                    Assert.AreEqual(-2.56e-10, (double)v3);

                    MultiPrecision<Pow2.N8> v4 = "-02.560e-10";
                    Assert.AreEqual(-2.56e-10, (double)v4);

                    MultiPrecision<Pow2.N8> v5 = "-25.6e-10";
                    Assert.AreEqual(-2.56e-9, (double)v5, 2.56e-20);

                    MultiPrecision<Pow2.N8> v6 = "-256e-10";
                    Assert.AreEqual(-2.56e-8, (double)v6, 2.56e-20);

                    MultiPrecision<Pow2.N8> v7 = "-2560e-10";
                    Assert.AreEqual(-2.56e-7, (double)v7, 2.56e-20);

                    MultiPrecision<Pow2.N8> v8 = "-02560e-10";
                    Assert.AreEqual(-2.56e-7, (double)v8, 2.56e-20);

                    MultiPrecision<Pow2.N8> v9 = "-256" + new string('0', 10) + "e-10";
                    Assert.AreEqual(-2.56e2, (double)v9);

                    MultiPrecision<Pow2.N8> v10 = "-256." + new string('0', 10) + "e-10";
                    Assert.AreEqual(-2.56e-8, (double)v10, 2.56e-20);

                    MultiPrecision<Pow2.N8> v11 = "-256" + new string('0', 1000) + "e-10";
                    Assert.AreEqual("-2.56e992", v11.ToString());

                    MultiPrecision<Pow2.N8> v12 = "-256." + new string('0', 1000) + "e-10";
                    Assert.AreEqual(-2.56e-8, (double)v12, 2.56e-20);

                    MultiPrecision<Pow2.N8> v13 = "-" + new string('0', 10) + "256e-10";
                    Assert.AreEqual(-2.56e-8, (double)v13, 2.56e-20);

                    MultiPrecision<Pow2.N8> v14 = "-" + new string('0', 1000) + "256e-10";
                    Assert.AreEqual(-2.56e-8, (double)v14, 2.56e-20);
                }
            }

            /* zero */
            {
                MultiPrecision<Pow2.N8> v1 = "0";
                Assert.AreEqual(0, (double)v1);

                MultiPrecision<Pow2.N8> v2 = "0.0";
                Assert.AreEqual(0, (double)v2);

                MultiPrecision<Pow2.N8> v3 = "00";
                Assert.AreEqual(0, (double)v3);

                MultiPrecision<Pow2.N8> v4 = "0e1";
                Assert.AreEqual(0, (double)v4);

                MultiPrecision<Pow2.N8> v5 = "0e-1";
                Assert.AreEqual(0, (double)v5);

                MultiPrecision<Pow2.N8> v6 = "0e0";
                Assert.AreEqual(0, (double)v6);

                MultiPrecision<Pow2.N8> v7 = "+0";
                Assert.AreEqual(0, (double)v7);

                MultiPrecision<Pow2.N8> v8 = "-0";
                Assert.AreEqual(-0, (double)v8);

                MultiPrecision<Pow2.N8> v9 = "+0.0";
                Assert.AreEqual(0, (double)v9);

                MultiPrecision<Pow2.N8> v10 = "-0.0";
                Assert.AreEqual(0, (double)v10);
            }
        }

        [TestMethod]
        public void ParseDigitsTest() {
            MultiPrecision<Pow2.N4> v2 = "1.23456789012345678901234567890123456789012345678901234567890123456789e-20";
            MultiPrecision<Pow2.N4> v3 = "0.0000000000000000000123456789012345678901234567890123456789012345678901234567890123456789";
            MultiPrecision<Pow2.N4> v4 = "0.000000000123456789012345678901234567890123456789012345678901234567890123456789e-10";
            MultiPrecision<Pow2.N4> v5 = "12345678901.2345678901234567890123456789012345678901234567890123456789e-30";
            MultiPrecision<Pow2.N4> v6 = "123456789012345678901.234567890123456789012345678901234567890123456789e-40";

            Assert.AreEqual(v2, v3);
            Assert.AreEqual(v2, v4);
            Assert.AreEqual(v2, v5);
            Assert.AreEqual(v2, v6);
        }

        [TestMethod]
        public void ParseZeroTest() {
            MultiPrecision<Pow2.N4> v2 = "00000";
            MultiPrecision<Pow2.N4> v3 = "0.0000";
            MultiPrecision<Pow2.N4> v4 = "0";
            MultiPrecision<Pow2.N4> v5 = "0.0";

            Assert.AreEqual(0, v2);
            Assert.AreEqual(0, v3);
            Assert.AreEqual(0, v4);
            Assert.AreEqual(0, v5);
        }

        [TestMethod]
        public void BadParseTest() {
            string[] vs = new string[] {
                string.Empty,
                "abcd",
                "e",
                ".",
                "+",
                "-",
                "+.",
                "-.",
                "+e",
                "-e",
                "+.e",
                "-.e",
                "e12",
                "1e",
                "1e99999999999999999999999999999999",
                ".e123",
                ".e12.3",
                "2.e12",
                "2.3.e12",
                "234.",
                "234.e",
                ".2",
                "*2.34"
            };

            foreach (string v in vs) {
                Assert.ThrowsException<FormatException>(() => {
                    MultiPrecision<Pow2.N8> u = v;
                    Console.WriteLine(u);
                }, v);
            }
        }

        [TestMethod]
        public void ParseIrregularTest() {
            MultiPrecision<Pow2.N8> nan1 = double.NaN.ToString();
            MultiPrecision<Pow2.N8> nan2 = "nan";
            MultiPrecision<Pow2.N8> nan3 = "NaN";

            MultiPrecision<Pow2.N8> pinf1 = double.PositiveInfinity.ToString();
            MultiPrecision<Pow2.N8> pinf2 = "inf";
            MultiPrecision<Pow2.N8> pinf3 = "+inf";
            MultiPrecision<Pow2.N8> pinf4 = "INF";
            MultiPrecision<Pow2.N8> pinf5 = "+INF";

            MultiPrecision<Pow2.N8> ninf1 = double.NegativeInfinity.ToString();
            MultiPrecision<Pow2.N8> ninf2 = "-inf";
            MultiPrecision<Pow2.N8> ninf3 = "-INF";

            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(nan1));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(nan2));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNaN(nan3));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(pinf1));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(pinf2));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(pinf3));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(pinf4));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsPositiveInfinity(pinf5));

            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegativeInfinity(ninf1));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegativeInfinity(ninf2));
            Assert.IsTrue(MultiPrecision<Pow2.N8>.IsNegativeInfinity(ninf3));
        }
    }
}
