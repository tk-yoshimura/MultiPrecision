using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest.Common {

    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ParseTest() {

            for (int i = -10; i <= 10; i++) {
                string num = $"0.314e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"3.14e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"31.4e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"314e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"-0.314e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"-3.14e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"-31.4e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"-314e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"+0.314e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"+3.14e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"+31.4e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = -10; i <= 10; i++) {
                string num = $"+314e{i}";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = 1; i <= Accumulator<Pow2.N8>.MaxDecimalDigits + 10; i++) {
                string num = "314" + new string('0', i);

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = 1; i <= Accumulator<Pow2.N8>.MaxDecimalDigits + 10; i++) {
                string num = "00314" + new string('0', i);

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = 1; i <= Accumulator<Pow2.N8>.MaxDecimalDigits + 10; i++) {
                string num = "314" + new string('0', i) + ".00";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }

            for (int i = 1; i <= Accumulator<Pow2.N8>.MaxDecimalDigits + 10; i++) {
                string num = "00314" + new string('0', i) + ".00";

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }
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
            };

            foreach (string v in vs) {
                Assert.ThrowsException<FormatException>(() => {
                    MultiPrecision<Pow2.N8> u = v;
                    Console.WriteLine(u);
                }, v);
            }
        }
    }
}
