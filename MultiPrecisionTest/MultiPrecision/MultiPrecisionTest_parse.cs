using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest {

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
                string num = "314" + new string(Enumerable.Repeat('0', i).ToArray());

                MultiPrecision<Pow2.N8> v = num;

                Console.WriteLine($"{v} {num}");
            }
        }
    }
}
